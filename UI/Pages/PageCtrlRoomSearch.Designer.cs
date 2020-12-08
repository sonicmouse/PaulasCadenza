namespace PaulasCadenza.UI.Pages
{
	partial class PageCtrlRoomSearch
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
			this.LstRooms = new System.Windows.Forms.ListBox();
			this.TxtSearch = new System.Windows.Forms.TextBox();
			this.CmbSearchFilter = new System.Windows.Forms.ComboBox();
			this.CmbSearchType = new System.Windows.Forms.ComboBox();
			this.BtnLeaveRoom = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// LstRooms
			// 
			this.LstRooms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LstRooms.FormattingEnabled = true;
			this.LstRooms.IntegralHeight = false;
			this.LstRooms.ItemHeight = 15;
			this.LstRooms.Location = new System.Drawing.Point(3, 32);
			this.LstRooms.Name = "LstRooms";
			this.LstRooms.Size = new System.Drawing.Size(490, 190);
			this.LstRooms.TabIndex = 3;
			this.LstRooms.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstRooms_MouseDoubleClick);
			// 
			// TxtSearch
			// 
			this.TxtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TxtSearch.Location = new System.Drawing.Point(213, 3);
			this.TxtSearch.Name = "TxtSearch";
			this.TxtSearch.Size = new System.Drawing.Size(280, 23);
			this.TxtSearch.TabIndex = 2;
			this.TxtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
			// 
			// CmbSearchFilter
			// 
			this.CmbSearchFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CmbSearchFilter.FormattingEnabled = true;
			this.CmbSearchFilter.Location = new System.Drawing.Point(108, 3);
			this.CmbSearchFilter.Name = "CmbSearchFilter";
			this.CmbSearchFilter.Size = new System.Drawing.Size(99, 23);
			this.CmbSearchFilter.TabIndex = 1;
			// 
			// CmbSearchType
			// 
			this.CmbSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CmbSearchType.FormattingEnabled = true;
			this.CmbSearchType.Location = new System.Drawing.Point(3, 3);
			this.CmbSearchType.Name = "CmbSearchType";
			this.CmbSearchType.Size = new System.Drawing.Size(99, 23);
			this.CmbSearchType.TabIndex = 0;
			// 
			// BtnLeaveRoom
			// 
			this.BtnLeaveRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BtnLeaveRoom.Location = new System.Drawing.Point(3, 228);
			this.BtnLeaveRoom.Name = "BtnLeaveRoom";
			this.BtnLeaveRoom.Size = new System.Drawing.Size(75, 23);
			this.BtnLeaveRoom.TabIndex = 4;
			this.BtnLeaveRoom.Text = "&Leave";
			this.BtnLeaveRoom.UseVisualStyleBackColor = true;
			this.BtnLeaveRoom.Click += new System.EventHandler(this.BtnLeaveRoom_Click);
			// 
			// PageCtrlRoom
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.BtnLeaveRoom);
			this.Controls.Add(this.CmbSearchType);
			this.Controls.Add(this.CmbSearchFilter);
			this.Controls.Add(this.TxtSearch);
			this.Controls.Add(this.LstRooms);
			this.Name = "PageCtrlRoom";
			this.Size = new System.Drawing.Size(496, 254);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox LstRooms;
		private System.Windows.Forms.TextBox TxtSearch;
		private System.Windows.Forms.ComboBox CmbSearchFilter;
		private System.Windows.Forms.ComboBox CmbSearchType;
		private System.Windows.Forms.Button BtnLeaveRoom;
	}
}
