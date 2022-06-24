namespace Buzagmod
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelVersion = new System.Windows.Forms.Label();
            this.modsListContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_fileDialog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(12, 513);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(41, 13);
            this.labelVersion.TabIndex = 1;
            this.labelVersion.Text = "version";
            // 
            // modsListContainer
            // 
            this.modsListContainer.AllowDrop = true;
            this.modsListContainer.AutoScroll = true;
            this.modsListContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.modsListContainer.Location = new System.Drawing.Point(0, 0);
            this.modsListContainer.Name = "modsListContainer";
            this.modsListContainer.Size = new System.Drawing.Size(700, 500);
            this.modsListContainer.TabIndex = 0;
            this.modsListContainer.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel1_DragDrop);
            this.modsListContainer.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel1_DragEnter);
            // 
            // btn_fileDialog
            // 
            this.btn_fileDialog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_fileDialog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_fileDialog.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_fileDialog.Location = new System.Drawing.Point(593, 506);
            this.btn_fileDialog.Name = "btn_fileDialog";
            this.btn_fileDialog.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btn_fileDialog.Size = new System.Drawing.Size(107, 25);
            this.btn_fileDialog.TabIndex = 0;
            this.btn_fileDialog.Text = "טען קובץ מוד...";
            this.btn_fileDialog.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btn_fileDialog.UseVisualStyleBackColor = true;
            this.btn_fileDialog.Click += new System.EventHandler(this.fileDialog_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 535);
            this.Controls.Add(this.btn_fileDialog);
            this.Controls.Add(this.modsListContainer);
            this.Controls.Add(this.labelVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buzagmod";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel modsListContainer;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Button btn_fileDialog;
    }
}

