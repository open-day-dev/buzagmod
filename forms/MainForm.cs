using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

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
    }
}
