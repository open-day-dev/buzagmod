using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Buzagmod
{
    internal class ModManager
    {
        public event EventHandler ModListModified;
        private readonly string dataPath = "data/data.mods";
        private readonly string modArchiveMetadataPath = "mod.txt";
        private readonly string modArchiveIconPath = "icon.jpg";
        private readonly string modArchiveContentPath = "content/";
        private string modManagerPath = Directory.GetCurrentDirectory();
        private Dictionary<Guid, Mod> mods;
        private Dictionary<string, Guid> occupiedFiles;

        public ModManager()
        {
            RefreshMods();
        }

        public void RefreshMods()
        {
            PopulateModsFromDisk();
            PopulateOccupiedFiles();
        }

        public bool IsModFilesPresent(Guid modId)
        {
            foreach (string file in this.mods[modId].files)
            {
                string path = GetFullFilePath("content", file);
                if (!File.Exists(path)) {
                    return false;
                }
            }
            return true;
        }

        public Dictionary<Guid, Mod> GetMods()
        {
            return this.mods;
        }

        public void SaveMods()
        {
            Dictionary<Guid, string> modsDictionary = new Dictionary<Guid, string>();
            foreach (KeyValuePair<Guid, Mod> modItem in this.mods)
            {
                modsDictionary.Add(modItem.Key, modItem.Value.ToJson());
            }
            string json = JsonConvert.SerializeObject(modsDictionary);
            File.WriteAllText(this.dataPath, json);
        }

        public void AddMod(string archivePath)
        {
            ZipArchive archive = null;
            try
            {
                archive = OpenModArchive(archivePath);

                AssertModInstallable(archive);

                //Instantiate new mod.
                Guid modId = Guid.NewGuid();
                Mod mod = new Mod().ParseFromJsonString(GetModMetadata(archive));
                mod.files = BuildArchiveModFileList(archive);
                mod.md5 = CalculateMD5(archivePath);

                //Extract contents.
                ExtractModIcon(archive, modId);
                ExtractModContent(archive);
                archive?.Dispose();

                //Add the mod to the dictionary.
                this.mods[modId] = mod;
                SaveMods();

                //Notify the user.
                MessageBox.Show("המוד \"" + mod.name + "\" הותקן בהצלחה.", "הידד!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

                //Reload Form
                this.ModListModified?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה בהוספת המוד", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
            finally
            {
                archive?.Dispose();
            }
        }

        public void RemoveMod(Guid id)
        {
            //Remove mod files.
            RemoveModFiles(this.mods[id].files);

            //Remove mod icon.
            RemoveModIcon(id);

            //Remove mod from list.
            this.mods.Remove(id);

            //Save upldated mod list.
            SaveMods();

            //Notify the user.
            MessageBox.Show("המוד הוסר.", "הידד!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);

            //Reload Form
            this.ModListModified?.Invoke(this, EventArgs.Empty);
        }

        private void ExtractModContent(ZipArchive archive)
        {
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (IsSupportedContentPath(entry.FullName))
                {
                    string relativePath = entry.FullName.Substring(entry.FullName.IndexOf('/') + 1);
                    string filePath = GetFullFilePath("content", relativePath);
                    if (filePath.StartsWith(this.modManagerPath))
                    {
                        string folderPath = filePath.Substring(0, filePath.LastIndexOf('\\'));
                        Directory.CreateDirectory(folderPath);
                        entry.ExtractToFile(filePath, true);
                    }
                    else
                    {
                        string errorString = "זוהה נתיב שחורג ממגבלות מנהל המודים.";
                        MessageBox.Show(errorString, "שגיאה בפענוח נתיבים", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                    }
                }
            }
        }

        private void ExtractModIcon(ZipArchive archive, Guid modId)
        {
            ZipArchiveEntry iconEntry = archive.GetEntry(this.modArchiveIconPath);
            if (iconEntry != null)
            {
                string destination = GetFullFilePath("icons", modId.ToString() + ".jpg");
                try
                {
                    iconEntry.ExtractToFile(destination);
                }
                catch (Exception ex)
                {
                    //Let it install without an icon.
                }
            }
        }

        private ZipArchive OpenModArchive(string archivePath)
        {
            ZipArchive archive = null;
            try
            {
                archive = ZipFile.OpenRead(archivePath);
            }
            catch (Exception ex)
            {
                throw new CannotOpenModArchiveException("לא ניתן לפתוח את המוד לקריאה.");
            }
            return archive;
        }

        /// <summary>
        /// Asserts the conditions for installing a mod.
        /// In case of failure, an exception will be thrown.
        /// - Archive is open
        /// - The mod metadata is valid
        /// - The mod has content
        /// - The mod does not conflict with installed mods
        /// </summary>
        /// <param name="archive"></param>
        /// <exception cref="Exception"></exception>
        private void AssertModInstallable(ZipArchive archive)
        {
            //Assert archive is not null.
            if (archive == null)
            {
                throw new CannotOpenModArchiveException("התרחשה שגיאה בפתיחת קובץ המוד.");
            }

            //Assert metadata is valid.
            if (!HasMetadata(archive))
            {
                throw new InvalidMetadataException("ישנם חוסרים בפרטי המוד (שם, יוצר, תיאור).");
            }

            //Assert content path exists.
            ZipArchiveEntry contentEntry = archive.GetEntry(this.modArchiveContentPath);
            if (contentEntry == null)
            {
                throw new ModArchiveContentException("לא ניתן לאתר את תיקיית התכנים של המוד.");
            }

            //Assert content exists.
            List<string> filenames = BuildArchiveModFileList(archive);
            if (filenames.Count == 0)
            {
                throw new ModArchiveContentException("לא נמצאו תכנים בתיקיית התוכן של המוד.");
            }

            //Assert mod content does not conflict with existing mods.
            HashSet<string> collisions = FindArchiveCollisionsWithInstalledMods(archive);
            if (collisions.Count > 0)
            {
                StringBuilder sb = new StringBuilder("ישנה התנגשות עם המודים הבאים:\n");
                foreach (string collision in collisions)
                {
                    sb.Append(collision + "\n");
                }
                throw new ModCollisionException(sb.ToString());
            }
        }

        private HashSet<string> FindArchiveCollisionsWithInstalledMods(ZipArchive archive)
        {
            HashSet<string> collisions = new HashSet<string>();
            List<string> filenames = BuildArchiveModFileList(archive);
            foreach (string filename in filenames)
            {
                if (this.occupiedFiles.ContainsKey(filename))
                {
                    Guid collisionModId = this.occupiedFiles[filename];
                    collisions.Add(this.mods[collisionModId].name);
                }
            }
            return collisions;
        }

        private bool IsSupportedContentPath(string filename)
        {
            return 
                filename.StartsWith(this.modArchiveContentPath + "audio") && filename.EndsWith(".ogg") ||
                filename.StartsWith(this.modArchiveContentPath + "img") && filename.EndsWith(".png");
        }

        private List<string> BuildArchiveModFileList(ZipArchive archive)
        {
            List<string> filenames = new List<string>();
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (IsSupportedContentPath(entry.FullName))
                {
                    string relativePath = entry.FullName.Substring(entry.FullName.IndexOf('/') + 1);
                    filenames.Add(relativePath);
                }
            }
            return filenames;
        }

        private string GetModMetadata(ZipArchive archive)
        {
            string jsonString = "";
            ZipArchiveEntry metadataEntry = archive.GetEntry(this.modArchiveMetadataPath);
            if (metadataEntry != null)
            {
                using (StreamReader reader = new StreamReader(metadataEntry.Open()))
                {
                    jsonString = reader.ReadToEnd();
                }
            }
            return jsonString;
        }

        /// <summary>
        /// This method checks that the mod contained in the given Zip archive has valid metadata.
        /// The metadata is used for the mod's name, description and author.
        /// </summary>
        /// <param name="archive"></param>
        /// <returns>True if the archive has valid Mod metadata. False otherwise.</returns>
        private bool HasMetadata(ZipArchive archive)
        {
            //Metadata file exists.
            ZipArchiveEntry metadataEntry = archive.GetEntry(this.modArchiveMetadataPath);
            if (metadataEntry == null) return false;
            
            //Check metadata in file.
            bool validMetadata = true;
            Mod mod = new Mod().ParseFromJsonString(GetModMetadata(archive));
            validMetadata &= mod.name != null && mod.name.Length > 0;
            validMetadata &= mod.description != null && mod.description.Length > 0;
            validMetadata &= mod.author != null && mod.author.Length > 0;
            return validMetadata;
        }

        private string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        private void RemoveModIcon(Guid id)
        {
            string path = GetFullFilePath("icons", id.ToString() + ".jpg");
            RemoveNestedFile(path);
        }

        private void RemoveNestedFile(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    string errorString = "לא ניתן היה להסיר את הקובץ:\n" + path;
                    MessageBox.Show(errorString, "שגיאה בהסרת המוד", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                }
            }
        }

        private string GetFullFilePath(string category, string relativePath)
        {
            return Path.GetFullPath(Path.Combine(this.modManagerPath, category, relativePath));
        }

        private void RemoveModFiles(List<string> fileList)
        {
            foreach (string file in fileList)
            {
                string path = GetFullFilePath("content", file);
                RemoveNestedFile(path);
            }
        }
       
        private void PopulateModsFromDisk()
        {
            JObject data = null;

            //Reset the mods dictionary.
            this.mods = new Dictionary<Guid, Mod>();

            //If the data file doesn't exists, create an empty file
            if(!File.Exists(this.dataPath))
            {
                CreateDataFile();
            }

            //Read the mods json.
            try
            {
                data = JObject.Parse(File.ReadAllText(this.dataPath));
            }
            catch (Exception ex)
            {
                QuitWithErrorMessage("שגיאה: לא ניתן לטעון את מילון המודים.");
            }

            //Populate the dictionary using the IDs in the data file
            foreach (string modId in data.Properties().Select(p => p.Name).ToList())
            {
                Guid id = new Guid(modId);
                Mod mod = new Mod().ParseFromJsonString(data.Property(modId).Value.ToString());
                this.mods.Add(id, mod);
            }
        }

        private void CreateDataFile()
        {
            string fileName = Path.GetFileName(this.dataPath);
            string path = Path.GetDirectoryName(this.dataPath);

            Directory.CreateDirectory(path);

            //Create the data file with an empty JSON
            File.WriteAllText(path + "/" + fileName, "{}");
        }

        private void PopulateOccupiedFiles()
        {
            this.occupiedFiles = new Dictionary<string, Guid>();
            foreach(KeyValuePair<Guid, Mod> modItem in this.mods)
            {
                foreach(string filePath in modItem.Value.files)
                {
                    try
                    {
                        this.occupiedFiles.Add(filePath, modItem.Key);
                    }
                    catch (ArgumentException)
                    {
                        QuitWithErrorMessage("יש התנגשות בין המודים שלך! אי אפשר לטעון אותם.\nניתן לפנות לדיסקורד לעזרה.");
                    }
                }
            }
        }

        private static void QuitWithErrorMessage(string errorString)
        {
            MessageBox.Show(errorString, "אופסי וופסי", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            Environment.Exit(1);
        }
    }

}
