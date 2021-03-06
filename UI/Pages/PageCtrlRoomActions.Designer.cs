﻿namespace PaulasCadenza.UI.Pages
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
			this.components = new System.ComponentModel.Container();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.CmbWalkType = new System.Windows.Forms.ComboBox();
			this.BtnActions = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.ChkAvatarMale = new System.Windows.Forms.CheckBox();
			this.BtnRndHLAvatar = new System.Windows.Forms.Button();
			this.BtnRndAvatar = new System.Windows.Forms.Button();
			this.TxtAvatar = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ChkShout = new System.Windows.Forms.CheckBox();
			this.TxtTalk = new System.Windows.Forms.TextBox();
			this.ChkJoke = new System.Windows.Forms.CheckBox();
			this.CtlFloor = new PaulasCadenza.UI.Controls.CtrlFloor();
			this.LstUsers = new PaulasCadenza.UI.Controls.CtrlRoomUsers();
			this.ToolTipMain = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
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
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.ChkJoke);
			this.splitContainer1.Panel2.Controls.Add(this.CmbWalkType);
			this.splitContainer1.Panel2.Controls.Add(this.BtnActions);
			this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
			this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
			this.splitContainer1.Size = new System.Drawing.Size(527, 182);
			this.splitContainer1.SplitterDistance = 293;
			this.splitContainer1.TabIndex = 0;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.CtlFloor);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.LstUsers);
			this.splitContainer2.Size = new System.Drawing.Size(293, 182);
			this.splitContainer2.SplitterDistance = 145;
			this.splitContainer2.TabIndex = 0;
			// 
			// CmbWalkType
			// 
			this.CmbWalkType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CmbWalkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CmbWalkType.FormattingEnabled = true;
			this.CmbWalkType.Location = new System.Drawing.Point(135, 120);
			this.CmbWalkType.Name = "CmbWalkType";
			this.CmbWalkType.Size = new System.Drawing.Size(92, 23);
			this.CmbWalkType.TabIndex = 3;
			// 
			// BtnActions
			// 
			this.BtnActions.Location = new System.Drawing.Point(2, 120);
			this.BtnActions.Name = "BtnActions";
			this.BtnActions.Size = new System.Drawing.Size(75, 23);
			this.BtnActions.TabIndex = 2;
			this.BtnActions.Text = "&Actions";
			this.BtnActions.UseVisualStyleBackColor = true;
			this.BtnActions.Click += new System.EventHandler(this.BtnActions_Click);
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
			this.groupBox2.Size = new System.Drawing.Size(224, 52);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Avatar";
			// 
			// ChkAvatarMale
			// 
			this.ChkAvatarMale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ChkAvatarMale.AutoSize = true;
			this.ChkAvatarMale.Location = new System.Drawing.Point(121, 24);
			this.ChkAvatarMale.Name = "ChkAvatarMale";
			this.ChkAvatarMale.Size = new System.Drawing.Size(37, 19);
			this.ChkAvatarMale.TabIndex = 1;
			this.ChkAvatarMale.Text = "&M";
			this.ChkAvatarMale.UseVisualStyleBackColor = true;
			// 
			// BtnRndHLAvatar
			// 
			this.BtnRndHLAvatar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnRndHLAvatar.Location = new System.Drawing.Point(164, 21);
			this.BtnRndHLAvatar.Name = "BtnRndHLAvatar";
			this.BtnRndHLAvatar.Size = new System.Drawing.Size(24, 23);
			this.BtnRndHLAvatar.TabIndex = 2;
			this.BtnRndHLAvatar.Text = "H";
			this.BtnRndHLAvatar.UseVisualStyleBackColor = true;
			this.BtnRndHLAvatar.Click += new System.EventHandler(this.BtnRndHLAvatar_Click);
			// 
			// BtnRndAvatar
			// 
			this.BtnRndAvatar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnRndAvatar.Location = new System.Drawing.Point(194, 21);
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
			this.TxtAvatar.Size = new System.Drawing.Size(109, 23);
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
			this.groupBox1.Size = new System.Drawing.Size(224, 53);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Talk";
			// 
			// ChkShout
			// 
			this.ChkShout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ChkShout.AutoSize = true;
			this.ChkShout.Checked = true;
			this.ChkShout.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ChkShout.Location = new System.Drawing.Point(161, 26);
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
			this.TxtTalk.Size = new System.Drawing.Size(149, 23);
			this.TxtTalk.TabIndex = 0;
			this.TxtTalk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtTalk_KeyDown);
			// 
			// ChkJoke
			// 
			this.ChkJoke.AutoSize = true;
			this.ChkJoke.Location = new System.Drawing.Point(3, 149);
			this.ChkJoke.Name = "ChkJoke";
			this.ChkJoke.Size = new System.Drawing.Size(53, 19);
			this.ChkJoke.TabIndex = 4;
			this.ChkJoke.Text = "/joke";
			this.ChkJoke.UseVisualStyleBackColor = true;
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
			this.CtlFloor.Size = new System.Drawing.Size(139, 176);
			this.CtlFloor.TabIndex = 0;
			// 
			// LstUsers
			// 
			this.LstUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LstUsers.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.LstUsers.Location = new System.Drawing.Point(3, 3);
			this.LstUsers.Name = "LstUsers";
			this.LstUsers.Size = new System.Drawing.Size(138, 176);
			this.LstUsers.TabIndex = 0;
			// 
			// PageCtrlRoomActions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "PageCtrlRoomActions";
			this.Size = new System.Drawing.Size(527, 182);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox ChkAvatarMale;
		private System.Windows.Forms.Button BtnRndHLAvatar;
		private System.Windows.Forms.Button BtnRndAvatar;
		private System.Windows.Forms.TextBox TxtAvatar;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox ChkShout;
		private System.Windows.Forms.TextBox TxtTalk;
		private System.Windows.Forms.Button BtnActions;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private Controls.CtrlFloor CtlFloor;
		private Controls.CtrlRoomUsers LstUsers;
		private System.Windows.Forms.ComboBox CmbWalkType;
		private System.Windows.Forms.CheckBox ChkJoke;
		private System.Windows.Forms.ToolTip ToolTipMain;
	}
}
