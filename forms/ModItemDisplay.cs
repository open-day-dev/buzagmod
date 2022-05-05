using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Buzagmod
{
    internal partial class ModItemDisplay : UserControl
    {
        public ModItemDisplay()
        {
            InitializeComponent();
        }

        public ModManager ModManager { get; set; }
        public Guid Id { get; set; }
        public bool FilesPresent { get; set; }

        private Mod mod;

        public Mod Mod
        {
            get { return mod; }
            set
            { 
                mod = value;
                labelModName.Text = mod.name;
                labelModAuthor.Text = mod.author;
                labelModDescription.Text = mod.description;
                labelHash.Text = mod.md5;
                try
                {
                    imageModIcon.Image = LoadIcon();
                }
                catch (Exception ex)
                {
                    //Let it load without an icon.
                }
            }
        }


        private Image LoadIcon()
        {
            Image img = Properties.Resources.modIconDefault;
            if (this.FilesPresent == true)
            {
                string path = "icons/" + this.Id.ToString().ToLower() + ".jpg";
                if (File.Exists(path))
                {
                    img = Image.FromFile(path);
                }
            }
            else
            {
                img = Properties.Resources.modIconError;
            }
            
            return img;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            string questionString = "למחוק את המוד?";
            DialogResult dialogResult = MessageBox.Show(questionString, "הסרת מוד", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            if (dialogResult == DialogResult.Yes)
            {
                //Removing the icon so the icon file will not be in use.
                this.imageModIcon.Image.Dispose();

                //Freeing the icon file.
                GC.Collect();

                //Remove the mod.
                ModManager.RemoveMod(this.Id);
            }
        }

        private void copyHashToClipboard(object sender, EventArgs e)
        {
            Clipboard.SetText(labelHash.Text);
        }

    }
}
