namespace PaulasCadenza.UI.Forms
{
	partial class FormMain
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.MenuStripMain = new System.Windows.Forms.MenuStrip();
			this.TSMIFile = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMIExit = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMIAccounts = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMIAddAccount = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMIRemoveAccount = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMIHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.TSMIAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.StatusStripMain = new System.Windows.Forms.StatusStrip();
			this.TSStatusMain = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.TabPageAccounts = new System.Windows.Forms.TabPage();
			this.BtnDisconnect = new System.Windows.Forms.Button();
			this.BtnConnect = new System.Windows.Forms.Button();
			this.LstViewAccounts = new PaulasCadenza.BaseUI.Controls.ListViewDoubleBuffered();
			this.CHEmail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.CHHotel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.CHName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ImgListMain = new System.Windows.Forms.ImageList(this.components);
			this.TabCtrlMain = new System.Windows.Forms.TabControl();
			this.TabPageRoomSearch = new System.Windows.Forms.TabPage();
			this.PageCtrlRoomSearch = new PaulasCadenza.UI.Pages.PageCtrlRoomSearch();
			this.TabPageRoomActions = new System.Windows.Forms.TabPage();
			this.PageCtrlRoomActions = new PaulasCadenza.UI.Pages.PageCtrlRoomActions();
			this.ToolTipMain = new System.Windows.Forms.ToolTip(this.components);
			this.MenuStripMain.SuspendLayout();
			this.StatusStripMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabControl2.SuspendLayout();
			this.TabPageAccounts.SuspendLayout();
			this.TabCtrlMain.SuspendLayout();
			this.TabPageRoomSearch.SuspendLayout();
			this.TabPageRoomActions.SuspendLayout();
			this.SuspendLayout();
			// 
			// MenuStripMain
			// 
			this.MenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIFile,
            this.TSMIAccounts,
            this.TSMIHelp});
			this.MenuStripMain.Location = new System.Drawing.Point(0, 0);
			this.MenuStripMain.Name = "MenuStripMain";
			this.MenuStripMain.Size = new System.Drawing.Size(524, 24);
			this.MenuStripMain.TabIndex = 0;
			this.MenuStripMain.Text = "menuStrip1";
			// 
			// TSMIFile
			// 
			this.TSMIFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIExit});
			this.TSMIFile.Name = "TSMIFile";
			this.TSMIFile.Size = new System.Drawing.Size(37, 20);
			this.TSMIFile.Text = "&File";
			// 
			// TSMIExit
			// 
			this.TSMIExit.Name = "TSMIExit";
			this.TSMIExit.Size = new System.Drawing.Size(93, 22);
			this.TSMIExit.Text = "E&xit";
			this.TSMIExit.Click += new System.EventHandler(this.TSMIExit_Click);
			// 
			// TSMIAccounts
			// 
			this.TSMIAccounts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIAddAccount,
            this.TSMIRemoveAccount});
			this.TSMIAccounts.Name = "TSMIAccounts";
			this.TSMIAccounts.Size = new System.Drawing.Size(69, 20);
			this.TSMIAccounts.Text = "&Accounts";
			// 
			// TSMIAddAccount
			// 
			this.TSMIAddAccount.Name = "TSMIAddAccount";
			this.TSMIAddAccount.Size = new System.Drawing.Size(165, 22);
			this.TSMIAddAccount.Text = "A&dd Account";
			this.TSMIAddAccount.Click += new System.EventHandler(this.TSMIAddAccount_Click);
			// 
			// TSMIRemoveAccount
			// 
			this.TSMIRemoveAccount.Name = "TSMIRemoveAccount";
			this.TSMIRemoveAccount.Size = new System.Drawing.Size(165, 22);
			this.TSMIRemoveAccount.Text = "&Remove Account";
			this.TSMIRemoveAccount.Click += new System.EventHandler(this.TSMIRemoveAccount_Click);
			// 
			// TSMIHelp
			// 
			this.TSMIHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIAbout});
			this.TSMIHelp.Name = "TSMIHelp";
			this.TSMIHelp.Size = new System.Drawing.Size(44, 20);
			this.TSMIHelp.Text = "&Help";
			// 
			// TSMIAbout
			// 
			this.TSMIAbout.Name = "TSMIAbout";
			this.TSMIAbout.Size = new System.Drawing.Size(107, 22);
			this.TSMIAbout.Text = "A&bout";
			this.TSMIAbout.Click += new System.EventHandler(this.TSMIAbout_Click);
			// 
			// StatusStripMain
			// 
			this.StatusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSStatusMain});
			this.StatusStripMain.Location = new System.Drawing.Point(0, 432);
			this.StatusStripMain.Name = "StatusStripMain";
			this.StatusStripMain.Size = new System.Drawing.Size(524, 22);
			this.StatusStripMain.TabIndex = 1;
			this.StatusStripMain.Text = "statusStrip1";
			// 
			// TSStatusMain
			// 
			this.TSStatusMain.Name = "TSStatusMain";
			this.TSStatusMain.Size = new System.Drawing.Size(57, 17);
			this.TSStatusMain.Text = "Welcome";
			// 
			// splitContainer1
			// 
			this.splitContainer1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
			this.splitContainer1.Panel1.Controls.Add(this.tabControl2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
			this.splitContainer1.Panel2.Controls.Add(this.TabCtrlMain);
			this.splitContainer1.Size = new System.Drawing.Size(524, 408);
			this.splitContainer1.SplitterDistance = 181;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 2;
			// 
			// tabControl2
			// 
			this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl2.Controls.Add(this.TabPageAccounts);
			this.tabControl2.Location = new System.Drawing.Point(3, 3);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(517, 175);
			this.tabControl2.TabIndex = 0;
			// 
			// TabPageAccounts
			// 
			this.TabPageAccounts.Controls.Add(this.BtnDisconnect);
			this.TabPageAccounts.Controls.Add(this.BtnConnect);
			this.TabPageAccounts.Controls.Add(this.LstViewAccounts);
			this.TabPageAccounts.Location = new System.Drawing.Point(4, 24);
			this.TabPageAccounts.Name = "TabPageAccounts";
			this.TabPageAccounts.Padding = new System.Windows.Forms.Padding(3);
			this.TabPageAccounts.Size = new System.Drawing.Size(509, 147);
			this.TabPageAccounts.TabIndex = 0;
			this.TabPageAccounts.Text = "Accounts";
			this.TabPageAccounts.UseVisualStyleBackColor = true;
			// 
			// BtnDisconnect
			// 
			this.BtnDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnDisconnect.Location = new System.Drawing.Point(470, 41);
			this.BtnDisconnect.Name = "BtnDisconnect";
			this.BtnDisconnect.Size = new System.Drawing.Size(33, 29);
			this.BtnDisconnect.TabIndex = 2;
			this.BtnDisconnect.Text = "D";
			this.BtnDisconnect.UseVisualStyleBackColor = true;
			this.BtnDisconnect.Click += new System.EventHandler(this.BtnDisconnect_Click);
			// 
			// BtnConnect
			// 
			this.BtnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnConnect.Location = new System.Drawing.Point(470, 6);
			this.BtnConnect.Name = "BtnConnect";
			this.BtnConnect.Size = new System.Drawing.Size(33, 29);
			this.BtnConnect.TabIndex = 1;
			this.BtnConnect.Text = "C";
			this.BtnConnect.UseVisualStyleBackColor = true;
			this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
			// 
			// LstViewAccounts
			// 
			this.LstViewAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LstViewAccounts.CheckBoxes = true;
			this.LstViewAccounts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CHEmail,
            this.CHHotel,
            this.CHName});
			this.LstViewAccounts.FullRowSelect = true;
			this.LstViewAccounts.HideSelection = false;
			this.LstViewAccounts.LabelWrap = false;
			this.LstViewAccounts.Location = new System.Drawing.Point(3, 3);
			this.LstViewAccounts.MultiSelect = false;
			this.LstViewAccounts.Name = "LstViewAccounts";
			this.LstViewAccounts.ShowItemToolTips = true;
			this.LstViewAccounts.Size = new System.Drawing.Size(461, 145);
			this.LstViewAccounts.SmallImageList = this.ImgListMain;
			this.LstViewAccounts.TabIndex = 0;
			this.LstViewAccounts.UseCompatibleStateImageBehavior = false;
			this.LstViewAccounts.View = System.Windows.Forms.View.Details;
			this.LstViewAccounts.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.LstViewAccounts_ItemChecked);
			this.LstViewAccounts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstViewAccounts_MouseDoubleClick);
			// 
			// CHEmail
			// 
			this.CHEmail.Text = "E-Mail";
			// 
			// CHHotel
			// 
			this.CHHotel.Text = "Hotel";
			// 
			// CHName
			// 
			this.CHName.Text = "Name";
			// 
			// ImgListMain
			// 
			this.ImgListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImgListMain.ImageStream")));
			this.ImgListMain.TransparentColor = System.Drawing.Color.Transparent;
			this.ImgListMain.Images.SetKeyName(0, "ImgConnected");
			this.ImgListMain.Images.SetKeyName(1, "ImgDisconnected");
			// 
			// TabCtrlMain
			// 
			this.TabCtrlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TabCtrlMain.Controls.Add(this.TabPageRoomSearch);
			this.TabCtrlMain.Controls.Add(this.TabPageRoomActions);
			this.TabCtrlMain.Location = new System.Drawing.Point(3, 3);
			this.TabCtrlMain.Name = "TabCtrlMain";
			this.TabCtrlMain.SelectedIndex = 0;
			this.TabCtrlMain.Size = new System.Drawing.Size(517, 199);
			this.TabCtrlMain.TabIndex = 0;
			// 
			// TabPageRoomSearch
			// 
			this.TabPageRoomSearch.Controls.Add(this.PageCtrlRoomSearch);
			this.TabPageRoomSearch.Location = new System.Drawing.Point(4, 24);
			this.TabPageRoomSearch.Name = "TabPageRoomSearch";
			this.TabPageRoomSearch.Padding = new System.Windows.Forms.Padding(3);
			this.TabPageRoomSearch.Size = new System.Drawing.Size(509, 171);
			this.TabPageRoomSearch.TabIndex = 0;
			this.TabPageRoomSearch.Text = "Room Search";
			this.TabPageRoomSearch.UseVisualStyleBackColor = true;
			// 
			// PageCtrlRoomSearch
			// 
			this.PageCtrlRoomSearch.AutoSize = true;
			this.PageCtrlRoomSearch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PageCtrlRoomSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.PageCtrlRoomSearch.Location = new System.Drawing.Point(3, 3);
			this.PageCtrlRoomSearch.Name = "PageCtrlRoomSearch";
			this.PageCtrlRoomSearch.Size = new System.Drawing.Size(503, 165);
			this.PageCtrlRoomSearch.TabIndex = 0;
			// 
			// TabPageRoomActions
			// 
			this.TabPageRoomActions.Controls.Add(this.PageCtrlRoomActions);
			this.TabPageRoomActions.Location = new System.Drawing.Point(4, 24);
			this.TabPageRoomActions.Name = "TabPageRoomActions";
			this.TabPageRoomActions.Size = new System.Drawing.Size(509, 171);
			this.TabPageRoomActions.TabIndex = 1;
			this.TabPageRoomActions.Text = "Room Actions";
			this.TabPageRoomActions.UseVisualStyleBackColor = true;
			// 
			// PageCtrlRoomActions
			// 
			this.PageCtrlRoomActions.AutoSize = true;
			this.PageCtrlRoomActions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PageCtrlRoomActions.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.PageCtrlRoomActions.Location = new System.Drawing.Point(0, 0);
			this.PageCtrlRoomActions.Name = "PageCtrlRoomActions";
			this.PageCtrlRoomActions.Size = new System.Drawing.Size(509, 171);
			this.PageCtrlRoomActions.TabIndex = 0;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(524, 454);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.StatusStripMain);
			this.Controls.Add(this.MenuStripMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.MenuStripMain;
			this.MaximizeBox = false;
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Paula\'s Cadenza";
			this.MenuStripMain.ResumeLayout(false);
			this.MenuStripMain.PerformLayout();
			this.StatusStripMain.ResumeLayout(false);
			this.StatusStripMain.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tabControl2.ResumeLayout(false);
			this.TabPageAccounts.ResumeLayout(false);
			this.TabCtrlMain.ResumeLayout(false);
			this.TabPageRoomSearch.ResumeLayout(false);
			this.TabPageRoomSearch.PerformLayout();
			this.TabPageRoomActions.ResumeLayout(false);
			this.TabPageRoomActions.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip MenuStripMain;
		private System.Windows.Forms.ToolStripMenuItem TSMIAccounts;
		private System.Windows.Forms.StatusStrip StatusStripMain;
		private System.Windows.Forms.ToolStripStatusLabel TSStatusMain;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.TabPage TabPageAccounts;
		private BaseUI.Controls.ListViewDoubleBuffered LstViewAccounts;
		private System.Windows.Forms.ColumnHeader CHEmail;
		private System.Windows.Forms.ColumnHeader CHHotel;
		private System.Windows.Forms.ColumnHeader CHName;
		private System.Windows.Forms.TabControl TabCtrlMain;
		private System.Windows.Forms.TabPage TabPageRoomSearch;
		private System.Windows.Forms.ToolStripMenuItem TSMIFile;
		private System.Windows.Forms.ToolStripMenuItem TSMIExit;
		private System.Windows.Forms.ToolStripMenuItem TSMIAddAccount;
		private System.Windows.Forms.ToolStripMenuItem TSMIRemoveAccount;
		private System.Windows.Forms.Button BtnDisconnect;
		private System.Windows.Forms.Button BtnConnect;
		private System.Windows.Forms.ImageList ImgListMain;
		private PaulasCadenza.UI.Pages.PageCtrlRoomSearch PageCtrlRoomSearch;
		private System.Windows.Forms.TabPage TabPageRoomActions;
		private PaulasCadenza.UI.Pages.PageCtrlRoomActions PageCtrlRoomActions;
		private System.Windows.Forms.ToolStripMenuItem TSMIHelp;
		private System.Windows.Forms.ToolStripMenuItem TSMIAbout;
		private System.Windows.Forms.ToolTip ToolTipMain;
	}
}

