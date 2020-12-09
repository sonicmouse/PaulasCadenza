namespace PaulasCadenza.UI.Pages
{
	partial class PageCtrlRoomActions
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.ChkAvatarMale = new System.Windows.Forms.CheckBox();
			this.BtnRndHLAvatar = new System.Windows.Forms.Button();
			this.BtnRndAvatar = new System.Windows.Forms.Button();
			this.TxtAvatar = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ChkShout = new System.Windows.Forms.CheckBox();
			this.TxtTalk = new System.Windows.Forms.TextBox();
			this.BtnActions = new System.Windows.Forms.Button();
			this.CtlFloor = new PaulasCadenza.UI.Controls.CtrlFloor();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.CtlFloor);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.BtnActions);
			this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
			this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
			this.splitContainer1.Size = new System.Drawing.Size(527, 253);
			this.splitContainer1.SplitterDistance = 162;
			this.splitContainer1.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.ChkAvatarMale);
			this.groupBox2.Controls.Add(this.BtnRndHLAvatar);
			this.groupBox2.Controls.Add(this.BtnRndAvatar);
			this.groupBox2.Controls.Add(this.TxtAvatar);
			this.groupBox2.Location = new System.Drawing.Point(3, 62);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(355, 52);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Avatar";
			// 
			// ChkAvatarMale
			// 
			this.ChkAvatarMale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ChkAvatarMale.AutoSize = true;
			this.ChkAvatarMale.Location = new System.Drawing.Point(252, 24);
			this.ChkAvatarMale.Name = "ChkAvatarMale";
			this.ChkAvatarMale.Size = new System.Drawing.Size(37, 19);
			this.ChkAvatarMale.TabIndex = 5;
			this.ChkAvatarMale.Text = "&M";
			this.ChkAvatarMale.UseVisualStyleBackColor = true;
			// 
			// BtnRndHLAvatar
			// 
			this.BtnRndHLAvatar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnRndHLAvatar.Location = new System.Drawing.Point(295, 21);
			this.BtnRndHLAvatar.Name = "BtnRndHLAvatar";
			this.BtnRndHLAvatar.Size = new System.Drawing.Size(24, 23);
			this.BtnRndHLAvatar.TabIndex = 4;
			this.BtnRndHLAvatar.Text = "H";
			this.BtnRndHLAvatar.UseVisualStyleBackColor = true;
			this.BtnRndHLAvatar.Click += new System.EventHandler(this.BtnRndHLAvatar_Click);
			// 
			// BtnRndAvatar
			// 
			this.BtnRndAvatar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnRndAvatar.Location = new System.Drawing.Point(325, 21);
			this.BtnRndAvatar.Name = "BtnRndAvatar";
			this.BtnRndAvatar.Size = new System.Drawing.Size(24, 23);
			this.BtnRndAvatar.TabIndex = 3;
			this.BtnRndAvatar.Text = "R";
			this.BtnRndAvatar.UseVisualStyleBackColor = true;
			this.BtnRndAvatar.Click += new System.EventHandler(this.BtnRndAvatar_Click);
			// 
			// TxtAvatar
			// 
			this.TxtAvatar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TxtAvatar.Location = new System.Drawing.Point(6, 20);
			this.TxtAvatar.Name = "TxtAvatar";
			this.TxtAvatar.Size = new System.Drawing.Size(240, 23);
			this.TxtAvatar.TabIndex = 0;
			this.TxtAvatar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtAvatar_KeyDown);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.ChkShout);
			this.groupBox1.Controls.Add(this.TxtTalk);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(355, 53);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Talk";
			// 
			// ChkShout
			// 
			this.ChkShout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ChkShout.AutoSize = true;
			this.ChkShout.Checked = true;
			this.ChkShout.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ChkShout.Location = new System.Drawing.Point(292, 26);
			this.ChkShout.Name = "ChkShout";
			this.ChkShout.Size = new System.Drawing.Size(57, 19);
			this.ChkShout.TabIndex = 1;
			this.ChkShout.Text = "&Shout";
			this.ChkShout.UseVisualStyleBackColor = true;
			// 
			// TxtTalk
			// 
			this.TxtTalk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TxtTalk.Location = new System.Drawing.Point(6, 22);
			this.TxtTalk.Name = "TxtTalk";
			this.TxtTalk.Size = new System.Drawing.Size(280, 23);
			this.TxtTalk.TabIndex = 0;
			this.TxtTalk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtTalk_KeyDown);
			// 
			// BtnActions
			// 
			this.BtnActions.Location = new System.Drawing.Point(3, 120);
			this.BtnActions.Name = "BtnActions";
			this.BtnActions.Size = new System.Drawing.Size(75, 23);
			this.BtnActions.TabIndex = 9;
			this.BtnActions.Text = "&Actions";
			this.BtnActions.UseVisualStyleBackColor = true;
			this.BtnActions.Click += new System.EventHandler(this.BtnActions_Click);
			// 
			// CtlFloor
			// 
			this.CtlFloor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CtlFloor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CtlFloor.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.CtlFloor.HeightMap = null;
			this.CtlFloor.Location = new System.Drawing.Point(3, 3);
			this.CtlFloor.Name = "CtlFloor";
			this.CtlFloor.Size = new System.Drawing.Size(156, 247);
			this.CtlFloor.TabIndex = 6;
			// 
			// PageCtrlRoomActions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "PageCtrlRoomActions";
			this.Size = new System.Drawing.Size(527, 253);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private Controls.CtrlFloor CtlFloor;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox ChkAvatarMale;
		private System.Windows.Forms.Button BtnRndHLAvatar;
		private System.Windows.Forms.Button BtnRndAvatar;
		private System.Windows.Forms.TextBox TxtAvatar;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox ChkShout;
		private System.Windows.Forms.TextBox TxtTalk;
		private System.Windows.Forms.Button BtnActions;
	}
}
