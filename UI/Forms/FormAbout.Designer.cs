namespace PaulasCadenza.UI.Forms
{
	partial class FormAbout
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
			this.PicSonicMouse = new System.Windows.Forms.PictureBox();
			this.LblTitle = new System.Windows.Forms.Label();
			this.LblVersion = new System.Windows.Forms.Label();
			this.LnkGitHub = new System.Windows.Forms.LinkLabel();
			this.LblAbout = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.LblBuildDate = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.PicSonicMouse)).BeginInit();
			this.SuspendLayout();
			// 
			// PicSonicMouse
			// 
			this.PicSonicMouse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.PicSonicMouse.Image = global::PaulasCadenza.Properties.Resources.SonicMouse;
			this.PicSonicMouse.InitialImage = null;
			this.PicSonicMouse.Location = new System.Drawing.Point(175, 12);
			this.PicSonicMouse.Name = "PicSonicMouse";
			this.PicSonicMouse.Size = new System.Drawing.Size(110, 263);
			this.PicSonicMouse.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.PicSonicMouse.TabIndex = 0;
			this.PicSonicMouse.TabStop = false;
			// 
			// LblTitle
			// 
			this.LblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LblTitle.Location = new System.Drawing.Point(12, 12);
			this.LblTitle.Name = "LblTitle";
			this.LblTitle.Size = new System.Drawing.Size(157, 23);
			this.LblTitle.TabIndex = 0;
			this.LblTitle.Text = "Paula\'s Cadenza";
			this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LblVersion
			// 
			this.LblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LblVersion.Location = new System.Drawing.Point(12, 35);
			this.LblVersion.Name = "LblVersion";
			this.LblVersion.Size = new System.Drawing.Size(157, 15);
			this.LblVersion.TabIndex = 1;
			this.LblVersion.Text = "{version}";
			this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LnkGitHub
			// 
			this.LnkGitHub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LnkGitHub.Location = new System.Drawing.Point(9, 65);
			this.LnkGitHub.Name = "LnkGitHub";
			this.LnkGitHub.Size = new System.Drawing.Size(157, 13);
			this.LnkGitHub.TabIndex = 3;
			this.LnkGitHub.TabStop = true;
			this.LnkGitHub.Text = "Visit us on GitHub";
			this.LnkGitHub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.LnkGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkGitHub_LinkClicked);
			// 
			// LblAbout
			// 
			this.LblAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LblAbout.Location = new System.Drawing.Point(12, 93);
			this.LblAbout.Name = "LblAbout";
			this.LblAbout.Size = new System.Drawing.Size(157, 182);
			this.LblAbout.TabIndex = 5;
			this.LblAbout.Text = resources.GetString("LblAbout.Text");
			this.LblAbout.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Location = new System.Drawing.Point(12, 86);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(157, 2);
			this.label1.TabIndex = 4;
			// 
			// LblBuildDate
			// 
			this.LblBuildDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LblBuildDate.Location = new System.Drawing.Point(12, 50);
			this.LblBuildDate.Name = "LblBuildDate";
			this.LblBuildDate.Size = new System.Drawing.Size(157, 15);
			this.LblBuildDate.TabIndex = 2;
			this.LblBuildDate.Text = "{build_date}";
			this.LblBuildDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FormAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(297, 285);
			this.Controls.Add(this.LblBuildDate);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.LblAbout);
			this.Controls.Add(this.LnkGitHub);
			this.Controls.Add(this.LblVersion);
			this.Controls.Add(this.LblTitle);
			this.Controls.Add(this.PicSonicMouse);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormAbout";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About Paula\'s Cadenza";
			this.Load += new System.EventHandler(this.FormAbout_Load);
			((System.ComponentModel.ISupportInitialize)(this.PicSonicMouse)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox PicSonicMouse;
		private System.Windows.Forms.Label LblTitle;
		private System.Windows.Forms.Label LblVersion;
		private System.Windows.Forms.LinkLabel LnkGitHub;
		private System.Windows.Forms.Label LblAbout;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label LblBuildDate;
	}
}