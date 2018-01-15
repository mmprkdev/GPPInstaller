namespace GPPInstaller
{
    partial class Form1
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
            this.core_checkBox = new System.Windows.Forms.CheckBox();
            this.visuals_checkBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressLabel = new System.Windows.Forms.Label();
            this.lowResClouds_checkBox = new System.Windows.Forms.CheckBox();
            this.highResClouds_checkBox = new System.Windows.Forms.CheckBox();
            this.cloudTextureLabel = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.restartButton = new System.Windows.Forms.Button();
            this.pictureBoxLeftPic = new System.Windows.Forms.PictureBox();
            this.pictureBoxRightPic = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.utility_checkBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeftPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRightPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // core_checkBox
            // 
            this.core_checkBox.AutoSize = true;
            this.core_checkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.core_checkBox.Location = new System.Drawing.Point(175, 29);
            this.core_checkBox.Name = "core_checkBox";
            this.core_checkBox.Size = new System.Drawing.Size(80, 19);
            this.core_checkBox.TabIndex = 7;
            this.core_checkBox.Text = "GPP Core";
            this.core_checkBox.UseVisualStyleBackColor = true;
            this.core_checkBox.CheckedChanged += new System.EventHandler(this.core_checkBox_CheckedChanged);
            // 
            // visuals_checkBox
            // 
            this.visuals_checkBox.AutoSize = true;
            this.visuals_checkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.visuals_checkBox.Location = new System.Drawing.Point(175, 148);
            this.visuals_checkBox.Name = "visuals_checkBox";
            this.visuals_checkBox.Size = new System.Drawing.Size(145, 19);
            this.visuals_checkBox.TabIndex = 8;
            this.visuals_checkBox.Text = "Visual Enhancements";
            this.visuals_checkBox.UseVisualStyleBackColor = true;
            this.visuals_checkBox.CheckedChanged += new System.EventHandler(this.visuals_checkBox_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(326, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(336, 30);
            this.label3.TabIndex = 9;
            this.label3.Text = "All required mods for the most basic GPP install: Kopernicus,\r\n GPP, and GPP_Text" +
    "ures.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(326, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(279, 30);
            this.label4.TabIndex = 10;
            this.label4.Text = "A collection of visual encancment mods: Scatterer,\r\nEVE, and Distant Object Enhan" +
    "cement.";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(209, 345);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(322, 23);
            this.progressBar1.TabIndex = 11;
            this.progressBar1.Visible = false;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressLabel.Location = new System.Drawing.Point(183, 375);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(0, 15);
            this.progressLabel.TabIndex = 12;
            this.progressLabel.Visible = false;
            // 
            // lowResClouds_checkBox
            // 
            this.lowResClouds_checkBox.AutoSize = true;
            this.lowResClouds_checkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lowResClouds_checkBox.Location = new System.Drawing.Point(424, 193);
            this.lowResClouds_checkBox.Name = "lowResClouds_checkBox";
            this.lowResClouds_checkBox.Size = new System.Drawing.Size(74, 19);
            this.lowResClouds_checkBox.TabIndex = 13;
            this.lowResClouds_checkBox.Text = "Low Res";
            this.lowResClouds_checkBox.UseVisualStyleBackColor = true;
            this.lowResClouds_checkBox.Visible = false;
            this.lowResClouds_checkBox.CheckedChanged += new System.EventHandler(this.lowResClouds_checkBox_CheckedChanged);
            // 
            // highResClouds_checkBox
            // 
            this.highResClouds_checkBox.AutoSize = true;
            this.highResClouds_checkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.highResClouds_checkBox.Location = new System.Drawing.Point(424, 218);
            this.highResClouds_checkBox.Name = "highResClouds_checkBox";
            this.highResClouds_checkBox.Size = new System.Drawing.Size(77, 19);
            this.highResClouds_checkBox.TabIndex = 14;
            this.highResClouds_checkBox.Text = "High Res";
            this.highResClouds_checkBox.UseVisualStyleBackColor = true;
            this.highResClouds_checkBox.Visible = false;
            this.highResClouds_checkBox.CheckedChanged += new System.EventHandler(this.highResClouds_checkBox_CheckedChanged);
            // 
            // cloudTextureLabel
            // 
            this.cloudTextureLabel.AutoSize = true;
            this.cloudTextureLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cloudTextureLabel.Location = new System.Drawing.Point(201, 194);
            this.cloudTextureLabel.Name = "cloudTextureLabel";
            this.cloudTextureLabel.Size = new System.Drawing.Size(202, 15);
            this.cloudTextureLabel.TabIndex = 15;
            this.cloudTextureLabel.Text = "Choose the cloud texture resolution:";
            this.cloudTextureLabel.Visible = false;
            // 
            // applyButton
            // 
            this.applyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applyButton.Location = new System.Drawing.Point(204, 466);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 16;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(347, 466);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 17;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Visible = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(479, 466);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(122, 23);
            this.exitButton.TabIndex = 19;
            this.exitButton.Text = "Exit and Run KSP";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // restartButton
            // 
            this.restartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restartButton.Location = new System.Drawing.Point(275, 466);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(75, 23);
            this.restartButton.TabIndex = 22;
            this.restartButton.Text = "Restart";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Visible = false;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // pictureBoxLeftPic
            // 
            this.pictureBoxLeftPic.Image = global::GPPInstaller.Properties.Resources.screenshot2_left;
            this.pictureBoxLeftPic.Location = new System.Drawing.Point(1, 1);
            this.pictureBoxLeftPic.Name = "pictureBoxLeftPic";
            this.pictureBoxLeftPic.Size = new System.Drawing.Size(145, 563);
            this.pictureBoxLeftPic.TabIndex = 21;
            this.pictureBoxLeftPic.TabStop = false;
            // 
            // pictureBoxRightPic
            // 
            this.pictureBoxRightPic.Image = global::GPPInstaller.Properties.Resources.screenshot2_right;
            this.pictureBoxRightPic.Location = new System.Drawing.Point(686, 1);
            this.pictureBoxRightPic.Name = "pictureBoxRightPic";
            this.pictureBoxRightPic.Size = new System.Drawing.Size(145, 563);
            this.pictureBoxRightPic.TabIndex = 20;
            this.pictureBoxRightPic.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GPPInstaller.Properties.Resources.checkmark_green;
            this.pictureBox1.Location = new System.Drawing.Point(377, 280);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 45);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // utility_checkBox
            // 
            this.utility_checkBox.AutoSize = true;
            this.utility_checkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.utility_checkBox.Location = new System.Drawing.Point(175, 87);
            this.utility_checkBox.Name = "utility_checkBox";
            this.utility_checkBox.Size = new System.Drawing.Size(86, 19);
            this.utility_checkBox.TabIndex = 23;
            this.utility_checkBox.Text = "Extra Utility";
            this.utility_checkBox.UseVisualStyleBackColor = true;
            this.utility_checkBox.CheckedChanged += new System.EventHandler(this.utility_checkBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(326, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 15);
            this.label1.TabIndex = 24;
            this.label1.Text = "Kerbal Engineer, Kerbal Alarm Clock";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 565);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.utility_checkBox);
            this.Controls.Add(this.restartButton);
            this.Controls.Add(this.pictureBoxLeftPic);
            this.Controls.Add(this.pictureBoxRightPic);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.cloudTextureLabel);
            this.Controls.Add(this.highResClouds_checkBox);
            this.Controls.Add(this.lowResClouds_checkBox);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.visuals_checkBox);
            this.Controls.Add(this.core_checkBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeftPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRightPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox core_checkBox;
        private System.Windows.Forms.CheckBox visuals_checkBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.CheckBox lowResClouds_checkBox;
        private System.Windows.Forms.CheckBox highResClouds_checkBox;
        private System.Windows.Forms.Label cloudTextureLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.PictureBox pictureBoxRightPic;
        private System.Windows.Forms.PictureBox pictureBoxLeftPic;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.CheckBox utility_checkBox;
        private System.Windows.Forms.Label label1;
    }
}

