namespace PaulasCadenza.UI.Controls
{
	partial class CtrlRoomUsers
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
			this.LstRoomUsers = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// LstRoomUsers
			// 
			this.LstRoomUsers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LstRoomUsers.FormattingEnabled = true;
			this.LstRoomUsers.IntegralHeight = false;
			this.LstRoomUsers.ItemHeight = 15;
			this.LstRoomUsers.Location = new System.Drawing.Point(0, 0);
			this.LstRoomUsers.Name = "LstRoomUsers";
			this.LstRoomUsers.Size = new System.Drawing.Size(101, 94);
			this.LstRoomUsers.TabIndex = 0;
			this.LstRoomUsers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstRoomUsers_MouseDoubleClick);
			// 
			// CtrlRoomUsers
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.LstRoomUsers);
			this.Name = "CtrlRoomUsers";
			this.Size = new System.Drawing.Size(101, 94);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox LstRoomUsers;
	}
}
