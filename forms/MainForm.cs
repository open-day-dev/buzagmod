﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using System.IO;

namespace Buzagmod
{
    public partial class MainForm : Form
    {
        ModManager modManager = new ModManager();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowVersion();
            this.modManager.ModListModified += RefreshModContent;
            RefreshModContent(null, null);
        }

        private void ShowVersion()
        {
            labelVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public void RefreshModContent(object sender, EventArgs e)
        {
            this.modManager.RefreshMods();
            DrawModsList();
        }

        private void DrawModsList()
        {
            //Clear the mods display list.
            modsListContainer.Controls.Clear();

            if (this.modManager.GetMods().Count == 0)
            {
                modsListContainer.BackgroundImage = Properties.Resources.drag;
            }
            else
            {
                modsListContainer.BackgroundImage = null;
            }

            //Draw the current mods.
            foreach (KeyValuePair<Guid, Mod> modEntry in this.modManager.GetMods())
            {
                bool filesPresent = this.modManager.IsModFilesPresent(modEntry.Key);
                ModItemDisplay modItemDisplay = new ModItemDisplay();
                modItemDisplay.FilesPresent = filesPresent;
                modItemDisplay.ModManager = this.modManager;
                modItemDisplay.Id = modEntry.Key;
                modItemDisplay.Mod = modEntry.Value;
                modsListContainer.Controls.Add(modItemDisplay);
            }
            return;
        }

        private void flowLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {
            foreach (string archivePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                this.modManager.AddMod(archivePath);
            }
        }

        private void flowLayoutPanel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void fileDialog_Click(object sender, EventArgs e)
        {
            string path = "";

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "ZIP Files (*.zip)|*.zip";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    path = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    modManager.AddMod(path);
                    btn_fileDialog.Visible = false; //Hides the button after the mod has been loaded
                }
                else
                {
                    MessageBox.Show("אופס, נראה שהייתה בעייה בקליטת הקובץ. נסה שוב!", "שגיאה");
                }
            }
        }
    }
}
