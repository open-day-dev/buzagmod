namespace Buzagmod
{
    partial class ModItemDisplay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imageModIcon = new System.Windows.Forms.PictureBox();
            this.labelModName = new System.Windows.Forms.Label();
            this.labelModDescription = new System.Windows.Forms.Label();
            this.labelModAuthor = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.labelHash = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageModIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // imageModIcon
            // 
            this.imageModIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageModIcon.Location = new System.Drawing.Point(559, 24);
            this.imageModIcon.Name = "imageModIcon";
            this.imageModIcon.Size = new System.Drawing.Size(80, 80);
            this.imageModIcon.TabIndex = 0;
            this.imageModIcon.TabStop = false;
            // 
            // labelModName
            // 
            this.labelModName.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelModName.Location = new System.Drawing.Point(123, 14);
            this.labelModName.Name = "labelModName";
            this.labelModName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelModName.Size = new System.Drawing.Size(427, 33);
            this.labelModName.TabIndex = 1;
            this.labelModName.Text = "שם המוד";
            this.labelModName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelModDescription
            // 
            this.labelModDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelModDescription.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelModDescription.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.labelModDescription.Location = new System.Drawing.Point(123, 48);
            this.labelModDescription.Name = "labelModDescription";
            this.labelModDescription.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelModDescription.Size = new System.Drawing.Size(424, 23);
            this.labelModDescription.TabIndex = 2;
            this.labelModDescription.Text = "תיאור המוד";
            this.labelModDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelModAuthor
            // 
            this.labelModAuthor.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelModAuthor.Location = new System.Drawing.Point(123, 69);
            this.labelModAuthor.Name = "labelModAuthor";
            this.labelModAuthor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelModAuthor.Size = new System.Drawing.Size(383, 18);
            this.labelModAuthor.TabIndex = 3;
            this.labelModAuthor.Text = "יוצר המוד";
            this.labelModAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(509, 69);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(38, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "מאת:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonRemove
            // 
            this.buttonRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonRemove.Location = new System.Drawing.Point(47, 33);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(70, 57);
            this.buttonRemove.TabIndex = 5;
            this.buttonRemove.Text = "הסר";
            this.buttonRemove.UseVisualStyleBackColor = false;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // labelHash
            // 
            this.labelHash.AutoSize = true;
            this.labelHash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelHash.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHash.ForeColor = System.Drawing.Color.Gray;
            this.labelHash.Location = new System.Drawing.Point(355, 92);
            this.labelHash.Name = "labelHash";
            this.labelHash.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelHash.Size = new System.Drawing.Size(192, 13);
            this.labelHash.TabIndex = 6;
            this.labelHash.Text = "dbbea9a843e504f85403fc69205be738";
            this.labelHash.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelHash.Click += new System.EventHandler(this.copyHashToClipboard);
            // 
            // ModItemDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelHash);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelModAuthor);
            this.Controls.Add(this.labelModDescription);
            this.Controls.Add(this.labelModName);
            this.Controls.Add(this.imageModIcon);
            this.Name = "ModItemDisplay";
            this.Size = new System.Drawing.Size(661, 125);
            ((System.ComponentModel.ISupportInitialize)(this.imageModIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imageModIcon;
        private System.Windows.Forms.Label labelModName;
        private System.Windows.Forms.Label labelModDescription;
        private System.Windows.Forms.Label labelModAuthor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Label labelHash;
    }
}
