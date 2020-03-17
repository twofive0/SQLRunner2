/*
 * Created by SharpDevelop.
 * User: devin
 * Date: 5/10/2016
 * Time: 12:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SQLRunner2.Subforms
{
	partial class frmConnectionManager
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox grpFile;
		private System.Windows.Forms.Button btnFileSelect;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtFilePath;
		private System.Windows.Forms.TextBox txtFilePassword;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.GroupBox grpSQL;
		private System.Windows.Forms.CheckBox chkIntegratedSecurity;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtDatabase;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtServer;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboDatabaseType;
		private System.Windows.Forms.TextBox txtConnectionName;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.ListView lvConnections;
		private System.Windows.Forms.ColumnHeader columnConnName;
		private System.Windows.Forms.ColumnHeader columnID;
		private System.Windows.Forms.ImageList imageList1;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConnectionManager));
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.grpFile = new System.Windows.Forms.GroupBox();
			this.btnFileSelect = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.txtFilePath = new System.Windows.Forms.TextBox();
			this.txtFilePassword = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.grpSQL = new System.Windows.Forms.GroupBox();
			this.chkIntegratedSecurity = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.txtDatabase = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtServer = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cboDatabaseType = new System.Windows.Forms.ComboBox();
			this.txtConnectionName = new System.Windows.Forms.TextBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnNew = new System.Windows.Forms.Button();
			this.lvConnections = new System.Windows.Forms.ListView();
			this.columnConnName = new System.Windows.Forms.ColumnHeader();
			this.columnID = new System.Windows.Forms.ColumnHeader();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.groupBox1.SuspendLayout();
			this.grpFile.SuspendLayout();
			this.grpSQL.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left)));
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 14);
			this.label1.TabIndex = 0;
			this.label1.Text = "Stored Connections";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.grpFile);
			this.groupBox1.Controls.Add(this.grpSQL);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.cboDatabaseType);
			this.groupBox1.Controls.Add(this.txtConnectionName);
			this.groupBox1.Location = new System.Drawing.Point(226, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(401, 420);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Details";
			// 
			// grpFile
			// 
			this.grpFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.grpFile.Controls.Add(this.btnFileSelect);
			this.grpFile.Controls.Add(this.label9);
			this.grpFile.Controls.Add(this.txtFilePath);
			this.grpFile.Controls.Add(this.txtFilePassword);
			this.grpFile.Controls.Add(this.label10);
			this.grpFile.Location = new System.Drawing.Point(7, 308);
			this.grpFile.Name = "grpFile";
			this.grpFile.Size = new System.Drawing.Size(388, 105);
			this.grpFile.TabIndex = 7;
			this.grpFile.TabStop = false;
			this.grpFile.Text = "File-based DB Parameters";
			// 
			// btnFileSelect
			// 
			this.btnFileSelect.Location = new System.Drawing.Point(345, 25);
			this.btnFileSelect.Name = "btnFileSelect";
			this.btnFileSelect.Size = new System.Drawing.Size(27, 27);
			this.btnFileSelect.TabIndex = 1;
			this.btnFileSelect.Text = "...";
			this.btnFileSelect.UseVisualStyleBackColor = true;
			this.btnFileSelect.Click += new System.EventHandler(this.BtnFileSelectClick);
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(11, 68);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 23);
			this.label9.TabIndex = 12;
			this.label9.Text = "Password";
			// 
			// txtFilePath
			// 
			this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtFilePath.Location = new System.Drawing.Point(134, 30);
			this.txtFilePath.Name = "txtFilePath";
			this.txtFilePath.Size = new System.Drawing.Size(202, 20);
			this.txtFilePath.TabIndex = 0;
			// 
			// txtFilePassword
			// 
			this.txtFilePassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtFilePassword.Location = new System.Drawing.Point(134, 65);
			this.txtFilePassword.Name = "txtFilePassword";
			this.txtFilePassword.PasswordChar = '*';
			this.txtFilePassword.Size = new System.Drawing.Size(238, 20);
			this.txtFilePassword.TabIndex = 2;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(11, 33);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100, 23);
			this.label10.TabIndex = 10;
			this.label10.Text = "File Location";
			// 
			// grpSQL
			// 
			this.grpSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.grpSQL.Controls.Add(this.chkIntegratedSecurity);
			this.grpSQL.Controls.Add(this.label8);
			this.grpSQL.Controls.Add(this.txtDatabase);
			this.grpSQL.Controls.Add(this.label7);
			this.grpSQL.Controls.Add(this.txtPassword);
			this.grpSQL.Controls.Add(this.label6);
			this.grpSQL.Controls.Add(this.txtUserName);
			this.grpSQL.Controls.Add(this.label5);
			this.grpSQL.Controls.Add(this.txtServer);
			this.grpSQL.Location = new System.Drawing.Point(7, 105);
			this.grpSQL.Name = "grpSQL";
			this.grpSQL.Size = new System.Drawing.Size(388, 196);
			this.grpSQL.TabIndex = 6;
			this.grpSQL.TabStop = false;
			this.grpSQL.Text = "SQL Parameters";
			// 
			// chkIntegratedSecurity
			// 
			this.chkIntegratedSecurity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkIntegratedSecurity.Location = new System.Drawing.Point(257, 160);
			this.chkIntegratedSecurity.Name = "chkIntegratedSecurity";
			this.chkIntegratedSecurity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.chkIntegratedSecurity.Size = new System.Drawing.Size(125, 25);
			this.chkIntegratedSecurity.TabIndex = 4;
			this.chkIntegratedSecurity.Text = "Integrated Security";
			this.chkIntegratedSecurity.UseVisualStyleBackColor = true;
			this.chkIntegratedSecurity.CheckedChanged += new System.EventHandler(this.ChkIntegratedSecurityCheckedChanged);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(21, 60);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 23);
			this.label8.TabIndex = 7;
			this.label8.Text = "Database";
			// 
			// txtDatabase
			// 
			this.txtDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDatabase.Location = new System.Drawing.Point(144, 57);
			this.txtDatabase.Name = "txtDatabase";
			this.txtDatabase.Size = new System.Drawing.Size(238, 20);
			this.txtDatabase.TabIndex = 1;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(21, 130);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 23);
			this.label7.TabIndex = 5;
			this.label7.Text = "Password";
			// 
			// txtPassword
			// 
			this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtPassword.Location = new System.Drawing.Point(144, 127);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(238, 20);
			this.txtPassword.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(21, 95);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 23);
			this.label6.TabIndex = 3;
			this.label6.Text = "User Name";
			// 
			// txtUserName
			// 
			this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtUserName.Location = new System.Drawing.Point(144, 92);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(238, 20);
			this.txtUserName.TabIndex = 2;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(21, 27);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 23);
			this.label5.TabIndex = 1;
			this.label5.Text = "Server";
			// 
			// txtServer
			// 
			this.txtServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtServer.Location = new System.Drawing.Point(144, 23);
			this.txtServer.Name = "txtServer";
			this.txtServer.Size = new System.Drawing.Size(238, 20);
			this.txtServer.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "Database Type";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(16, 27);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "Connection Name";
			// 
			// cboDatabaseType
			// 
			this.cboDatabaseType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.cboDatabaseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDatabaseType.FormattingEnabled = true;
			this.cboDatabaseType.Location = new System.Drawing.Point(151, 59);
			this.cboDatabaseType.Name = "cboDatabaseType";
			this.cboDatabaseType.Size = new System.Drawing.Size(244, 21);
			this.cboDatabaseType.TabIndex = 1;
			this.cboDatabaseType.TextChanged += new System.EventHandler(this.CboDatabaseTypeTextChanged);
			// 
			// txtConnectionName
			// 
			this.txtConnectionName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtConnectionName.Location = new System.Drawing.Point(151, 25);
			this.txtConnectionName.Name = "txtConnectionName";
			this.txtConnectionName.Size = new System.Drawing.Size(244, 20);
			this.txtConnectionName.TabIndex = 0;
			this.txtConnectionName.TextChanged += new System.EventHandler(this.TxtConnectionNameTextChanged);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnSave.Location = new System.Drawing.Point(187, 460);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "&Save/Close";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnDelete.Location = new System.Drawing.Point(279, 460);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 2;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.BtnDeleteClick);
			// 
			// btnNew
			// 
			this.btnNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnNew.Location = new System.Drawing.Point(370, 460);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(75, 23);
			this.btnNew.TabIndex = 3;
			this.btnNew.Text = "&New";
			this.btnNew.UseVisualStyleBackColor = true;
			this.btnNew.Click += new System.EventHandler(this.BtnNewClick);
			// 
			// lvConnections
			// 
			this.lvConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.lvConnections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnConnName,
			this.columnID});
			this.lvConnections.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvConnections.LargeImageList = this.imageList1;
			this.lvConnections.Location = new System.Drawing.Point(13, 30);
			this.lvConnections.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
			this.lvConnections.MultiSelect = false;
			this.lvConnections.Name = "lvConnections";
			this.lvConnections.Size = new System.Drawing.Size(202, 404);
			this.lvConnections.SmallImageList = this.imageList1;
			this.lvConnections.StateImageList = this.imageList1;
			this.lvConnections.TabIndex = 4;
			this.lvConnections.UseCompatibleStateImageBehavior = false;
			this.lvConnections.View = System.Windows.Forms.View.List;
			this.lvConnections.SelectedIndexChanged += new System.EventHandler(this.LvConnectionsSelectedIndexChanged);
			// 
			// columnConnName
			// 
			this.columnConnName.Text = "Connection";
			this.columnConnName.Width = 200;
			// 
			// columnID
			// 
			this.columnID.Text = "ID";
			this.columnID.Width = 0;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "win.png");
			this.imageList1.Images.SetKeyName(1, "sqlite.png");
			this.imageList1.Images.SetKeyName(2, "pda.png");
			this.imageList1.Images.SetKeyName(3, "mariadb-226022.png");
			this.imageList1.Images.SetKeyName(4, "psql.png");
			this.imageList1.Images.SetKeyName(5, "access.png");
			this.imageList1.Images.SetKeyName(6, "db.png");
			this.imageList1.Images.SetKeyName(7, "srn2_32.ico");
			// 
			// ConnectionManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(639, 493);
			this.Controls.Add(this.lvConnections);
			this.Controls.Add(this.btnNew);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConnectionManager";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = " Connection Manager";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.grpFile.ResumeLayout(false);
			this.grpFile.PerformLayout();
			this.grpSQL.ResumeLayout(false);
			this.grpSQL.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}
