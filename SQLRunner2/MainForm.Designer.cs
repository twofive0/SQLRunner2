/*
 * Created by SharpDevelop.
 * User: devin
 * Date: 2/4/2019
 * Time: 4:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SQLRunner2
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem connectionManagerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tabsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusMain;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripMenuItem openScriptToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveScriptCurrentTabToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem printHTMLReportToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem convertMultisetToAccessToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem convertMultisetToSQLiteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sendResultsToStoredConnectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem xMLDataEditorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadFromXMLToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem openScriptLogToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newTabToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeTabToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameTabToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton btnRunQuery;
		private System.Windows.Forms.ToolStripButton btnRunAction;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton btnOpenScript;
		private System.Windows.Forms.ToolStripButton btnSaveResultsAs;
		private System.Windows.Forms.ToolStripComboBox cboExportFormat;
		private System.Windows.Forms.ToolStripButton btnAboutHelp;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabControl tabDatabaseObjects;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TreeView tvTablesList;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabControl tabQueries;
		private System.Windows.Forms.ToolStripStatusLabel txtCurrentDatabase;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel txtStatus;
		private System.Windows.Forms.TreeView tvViewsList;
		private System.Windows.Forms.TreeView tvProceduresList;
		private System.Windows.Forms.ToolStripButton btnRefreshDatabase;
		private System.Windows.Forms.ToolStripComboBox cboDatabaseConnection;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ContextMenuStrip mnuQueryTabs;
		private System.Windows.Forms.ToolStripMenuItem mnuQTabsNew;
		private System.Windows.Forms.ToolStripMenuItem mnuQTabsClose;
		private System.Windows.Forms.ToolStripMenuItem mnuQTabsRename;
		private System.Windows.Forms.ContextMenuStrip mnuTableOptions;
		private System.Windows.Forms.ToolStripMenuItem mnuTableDrop;
		private System.Windows.Forms.ToolStripMenuItem mnuTableViewCreate;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Views");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Procedures");
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveScriptCurrentTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.printHTMLReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.connectionManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.convertMultisetToAccessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.convertMultisetToSQLiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sendResultsToStoredConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.xMLDataEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadFromXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.openScriptLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusMain = new System.Windows.Forms.StatusStrip();
			this.txtCurrentDatabase = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.txtStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnRefreshDatabase = new System.Windows.Forms.ToolStripButton();
			this.cboDatabaseConnection = new System.Windows.Forms.ToolStripComboBox();
			this.btnRunQuery = new System.Windows.Forms.ToolStripButton();
			this.btnRunAction = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.btnOpenScript = new System.Windows.Forms.ToolStripButton();
			this.btnSaveResultsAs = new System.Windows.Forms.ToolStripButton();
			this.cboExportFormat = new System.Windows.Forms.ToolStripComboBox();
			this.btnAboutHelp = new System.Windows.Forms.ToolStripButton();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tabDatabaseObjects = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tvTablesList = new System.Windows.Forms.TreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tvViewsList = new System.Windows.Forms.TreeView();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tvProceduresList = new System.Windows.Forms.TreeView();
			this.tabQueries = new System.Windows.Forms.TabControl();
			this.mnuQueryTabs = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuQTabsNew = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuQTabsClose = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuQTabsRename = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTableOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuTableDrop = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTableViewCreate = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.statusMain.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabDatabaseObjects.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.mnuQueryTabs.SuspendLayout();
			this.mnuTableOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.fileToolStripMenuItem,
			this.editToolStripMenuItem,
			this.toolsToolStripMenuItem,
			this.tabsToolStripMenuItem,
			this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(900, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.openScriptToolStripMenuItem,
			this.saveScriptCurrentTabToolStripMenuItem,
			this.printHTMLReportToolStripMenuItem,
			this.toolStripSeparator1,
			this.connectionManagerToolStripMenuItem,
			this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// openScriptToolStripMenuItem
			// 
			this.openScriptToolStripMenuItem.Name = "openScriptToolStripMenuItem";
			this.openScriptToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl-O";
			this.openScriptToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.openScriptToolStripMenuItem.Text = "&Open Script";
			// 
			// saveScriptCurrentTabToolStripMenuItem
			// 
			this.saveScriptCurrentTabToolStripMenuItem.Name = "saveScriptCurrentTabToolStripMenuItem";
			this.saveScriptCurrentTabToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl-S";
			this.saveScriptCurrentTabToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.saveScriptCurrentTabToolStripMenuItem.Text = "Save Script";
			// 
			// printHTMLReportToolStripMenuItem
			// 
			this.printHTMLReportToolStripMenuItem.Name = "printHTMLReportToolStripMenuItem";
			this.printHTMLReportToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.printHTMLReportToolStripMenuItem.Text = "Print HTML Report";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
			// 
			// connectionManagerToolStripMenuItem
			// 
			this.connectionManagerToolStripMenuItem.Name = "connectionManagerToolStripMenuItem";
			this.connectionManagerToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.connectionManagerToolStripMenuItem.Text = "&Connection Manager";
			this.connectionManagerToolStripMenuItem.Click += new System.EventHandler(this.ConnectionManagerToolStripMenuItemClick);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
			this.exitToolStripMenuItem.Text = "&Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.undoToolStripMenuItem,
			this.cutToolStripMenuItem,
			this.copyToolStripMenuItem,
			this.pasteToolStripMenuItem,
			this.selectAllToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.undoToolStripMenuItem.Text = "Undo";
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.cutToolStripMenuItem.Text = "Cut";
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.selectAllToolStripMenuItem.Text = "Select All";
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.convertMultisetToAccessToolStripMenuItem,
			this.convertMultisetToSQLiteToolStripMenuItem,
			this.sendResultsToStoredConnectionToolStripMenuItem,
			this.toolStripSeparator2,
			this.xMLDataEditorToolStripMenuItem,
			this.loadFromXMLToolStripMenuItem,
			this.toolStripSeparator3,
			this.openScriptLogToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// convertMultisetToAccessToolStripMenuItem
			// 
			this.convertMultisetToAccessToolStripMenuItem.Name = "convertMultisetToAccessToolStripMenuItem";
			this.convertMultisetToAccessToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
			this.convertMultisetToAccessToolStripMenuItem.Text = "Convert Multiset to Access";
			// 
			// convertMultisetToSQLiteToolStripMenuItem
			// 
			this.convertMultisetToSQLiteToolStripMenuItem.Name = "convertMultisetToSQLiteToolStripMenuItem";
			this.convertMultisetToSQLiteToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
			this.convertMultisetToSQLiteToolStripMenuItem.Text = "Convert Multiset to SQLite";
			// 
			// sendResultsToStoredConnectionToolStripMenuItem
			// 
			this.sendResultsToStoredConnectionToolStripMenuItem.Name = "sendResultsToStoredConnectionToolStripMenuItem";
			this.sendResultsToStoredConnectionToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
			this.sendResultsToStoredConnectionToolStripMenuItem.Text = "Send Results to Stored Connection";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(253, 6);
			// 
			// xMLDataEditorToolStripMenuItem
			// 
			this.xMLDataEditorToolStripMenuItem.Name = "xMLDataEditorToolStripMenuItem";
			this.xMLDataEditorToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
			this.xMLDataEditorToolStripMenuItem.Text = "XML Data Editor";
			// 
			// loadFromXMLToolStripMenuItem
			// 
			this.loadFromXMLToolStripMenuItem.Name = "loadFromXMLToolStripMenuItem";
			this.loadFromXMLToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
			this.loadFromXMLToolStripMenuItem.Text = "Load From XML";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(253, 6);
			// 
			// openScriptLogToolStripMenuItem
			// 
			this.openScriptLogToolStripMenuItem.Name = "openScriptLogToolStripMenuItem";
			this.openScriptLogToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
			this.openScriptLogToolStripMenuItem.Text = "Open Script Log";
			// 
			// tabsToolStripMenuItem
			// 
			this.tabsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.newTabToolStripMenuItem,
			this.closeTabToolStripMenuItem,
			this.renameTabToolStripMenuItem});
			this.tabsToolStripMenuItem.Name = "tabsToolStripMenuItem";
			this.tabsToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
			this.tabsToolStripMenuItem.Text = "&Tabs";
			// 
			// newTabToolStripMenuItem
			// 
			this.newTabToolStripMenuItem.Name = "newTabToolStripMenuItem";
			this.newTabToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.newTabToolStripMenuItem.Text = "New Tab";
			this.newTabToolStripMenuItem.Click += new System.EventHandler(this.NewTabToolStripMenuItemClick);
			// 
			// closeTabToolStripMenuItem
			// 
			this.closeTabToolStripMenuItem.Name = "closeTabToolStripMenuItem";
			this.closeTabToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.closeTabToolStripMenuItem.Text = "Close Tab";
			this.closeTabToolStripMenuItem.Click += new System.EventHandler(this.CloseTabToolStripMenuItemClick);
			// 
			// renameTabToolStripMenuItem
			// 
			this.renameTabToolStripMenuItem.Name = "renameTabToolStripMenuItem";
			this.renameTabToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
			this.renameTabToolStripMenuItem.Text = "Rename Tab";
			this.renameTabToolStripMenuItem.Click += new System.EventHandler(this.RenameTabToolStripMenuItemClick);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
			// 
			// statusMain
			// 
			this.statusMain.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.txtCurrentDatabase,
			this.toolStripStatusLabel2,
			this.txtStatus});
			this.statusMain.Location = new System.Drawing.Point(0, 464);
			this.statusMain.Name = "statusMain";
			this.statusMain.Size = new System.Drawing.Size(900, 22);
			this.statusMain.TabIndex = 1;
			this.statusMain.Text = "statusStrip1";
			// 
			// txtCurrentDatabase
			// 
			this.txtCurrentDatabase.Name = "txtCurrentDatabase";
			this.txtCurrentDatabase.Size = new System.Drawing.Size(133, 17);
			this.txtCurrentDatabase.Text = "Current Database: None";
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 17);
			this.toolStripStatusLabel2.Text = "|";
			// 
			// txtStatus
			// 
			this.txtStatus.Name = "txtStatus";
			this.txtStatus.Size = new System.Drawing.Size(74, 17);
			this.txtStatus.Text = "Status: None";
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.btnRefreshDatabase,
			this.cboDatabaseConnection,
			this.btnRunQuery,
			this.btnRunAction,
			this.toolStripSeparator4,
			this.btnOpenScript,
			this.btnSaveResultsAs,
			this.cboExportFormat,
			this.btnAboutHelp});
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(900, 27);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnRefreshDatabase
			// 
			this.btnRefreshDatabase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnRefreshDatabase.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshDatabase.Image")));
			this.btnRefreshDatabase.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnRefreshDatabase.Name = "btnRefreshDatabase";
			this.btnRefreshDatabase.Size = new System.Drawing.Size(24, 24);
			this.btnRefreshDatabase.Text = "Refresh";
			this.btnRefreshDatabase.Click += new System.EventHandler(this.BtnRefreshDatabaseClick);
			// 
			// cboDatabaseConnection
			// 
			this.cboDatabaseConnection.DropDownWidth = 200;
			this.cboDatabaseConnection.Name = "cboDatabaseConnection";
			this.cboDatabaseConnection.Size = new System.Drawing.Size(150, 27);
			this.cboDatabaseConnection.Text = "(No DB Selected)";
			// 
			// btnRunQuery
			// 
			this.btnRunQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnRunQuery.Image")));
			this.btnRunQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnRunQuery.Name = "btnRunQuery";
			this.btnRunQuery.Size = new System.Drawing.Size(87, 24);
			this.btnRunQuery.Text = "Run Query";
			this.btnRunQuery.Click += new System.EventHandler(this.BtnRunQueryClick);
			// 
			// btnRunAction
			// 
			this.btnRunAction.Image = ((System.Drawing.Image)(resources.GetObject("btnRunAction.Image")));
			this.btnRunAction.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnRunAction.Name = "btnRunAction";
			this.btnRunAction.Size = new System.Drawing.Size(125, 24);
			this.btnRunAction.Text = "Run Action Query";
			this.btnRunAction.Click += new System.EventHandler(this.BtnRunActionClick);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
			// 
			// btnOpenScript
			// 
			this.btnOpenScript.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenScript.Image")));
			this.btnOpenScript.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnOpenScript.Name = "btnOpenScript";
			this.btnOpenScript.Size = new System.Drawing.Size(93, 24);
			this.btnOpenScript.Text = "Open Script";
			this.btnOpenScript.Click += new System.EventHandler(this.BtnOpenScriptClick);
			// 
			// btnSaveResultsAs
			// 
			this.btnSaveResultsAs.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveResultsAs.Image")));
			this.btnSaveResultsAs.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnSaveResultsAs.Name = "btnSaveResultsAs";
			this.btnSaveResultsAs.Size = new System.Drawing.Size(114, 24);
			this.btnSaveResultsAs.Text = "Save Results As:";
			// 
			// cboExportFormat
			// 
			this.cboExportFormat.Items.AddRange(new object[] {
			"Excel (.xlsx)",
			"CSV",
			"Tab Delimited",
			"Choose Delimiter",
			"XML",
			"Multiset"});
			this.cboExportFormat.Name = "cboExportFormat";
			this.cboExportFormat.Size = new System.Drawing.Size(114, 27);
			this.cboExportFormat.Text = "Excel (.xlsx)";
			// 
			// btnAboutHelp
			// 
			this.btnAboutHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnAboutHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnAboutHelp.Image")));
			this.btnAboutHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnAboutHelp.Name = "btnAboutHelp";
			this.btnAboutHelp.Size = new System.Drawing.Size(24, 24);
			this.btnAboutHelp.Text = "Help";
			this.btnAboutHelp.Click += new System.EventHandler(this.BtnAboutHelpClick);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 51);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tabDatabaseObjects);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabQueries);
			this.splitContainer1.Size = new System.Drawing.Size(900, 413);
			this.splitContainer1.SplitterDistance = 201;
			this.splitContainer1.SplitterWidth = 3;
			this.splitContainer1.TabIndex = 3;
			// 
			// tabDatabaseObjects
			// 
			this.tabDatabaseObjects.Controls.Add(this.tabPage1);
			this.tabDatabaseObjects.Controls.Add(this.tabPage2);
			this.tabDatabaseObjects.Controls.Add(this.tabPage3);
			this.tabDatabaseObjects.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabDatabaseObjects.Location = new System.Drawing.Point(0, 0);
			this.tabDatabaseObjects.Margin = new System.Windows.Forms.Padding(2);
			this.tabDatabaseObjects.Name = "tabDatabaseObjects";
			this.tabDatabaseObjects.SelectedIndex = 0;
			this.tabDatabaseObjects.Size = new System.Drawing.Size(201, 413);
			this.tabDatabaseObjects.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.tvTablesList);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
			this.tabPage1.Size = new System.Drawing.Size(193, 387);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Tables";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tvTablesList
			// 
			this.tvTablesList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvTablesList.ImageIndex = 0;
			this.tvTablesList.ImageList = this.imageList1;
			this.tvTablesList.Location = new System.Drawing.Point(2, 2);
			this.tvTablesList.Margin = new System.Windows.Forms.Padding(2);
			this.tvTablesList.Name = "tvTablesList";
			this.tvTablesList.SelectedImageIndex = 0;
			this.tvTablesList.ShowLines = false;
			this.tvTablesList.ShowRootLines = false;
			this.tvTablesList.Size = new System.Drawing.Size(189, 383);
			this.tvTablesList.TabIndex = 0;
			this.tvTablesList.DoubleClick += new System.EventHandler(this.TvTablesListDoubleClick);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "table.png");
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.tvViewsList);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
			this.tabPage2.Size = new System.Drawing.Size(193, 387);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Views";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// tvViewsList
			// 
			this.tvViewsList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvViewsList.Location = new System.Drawing.Point(2, 2);
			this.tvViewsList.Name = "tvViewsList";
			treeNode1.Name = "Views";
			treeNode1.Text = "Views";
			this.tvViewsList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
			treeNode1});
			this.tvViewsList.Size = new System.Drawing.Size(189, 383);
			this.tvViewsList.TabIndex = 0;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.tvProceduresList);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
			this.tabPage3.Size = new System.Drawing.Size(193, 387);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Procedures";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// tvProceduresList
			// 
			this.tvProceduresList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvProceduresList.Location = new System.Drawing.Point(2, 2);
			this.tvProceduresList.Name = "tvProceduresList";
			treeNode2.Name = "Procedures";
			treeNode2.Text = "Procedures";
			this.tvProceduresList.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
			treeNode2});
			this.tvProceduresList.Size = new System.Drawing.Size(189, 383);
			this.tvProceduresList.TabIndex = 0;
			// 
			// tabQueries
			// 
			this.tabQueries.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabQueries.Location = new System.Drawing.Point(0, 0);
			this.tabQueries.Margin = new System.Windows.Forms.Padding(2);
			this.tabQueries.Name = "tabQueries";
			this.tabQueries.SelectedIndex = 0;
			this.tabQueries.Size = new System.Drawing.Size(696, 413);
			this.tabQueries.TabIndex = 0;
			this.tabQueries.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TabQueriesMouseClick);
			// 
			// mnuQueryTabs
			// 
			this.mnuQueryTabs.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.mnuQueryTabs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuQTabsNew,
			this.mnuQTabsClose,
			this.mnuQTabsRename});
			this.mnuQueryTabs.Name = "mnuQueryTabs";
			this.mnuQueryTabs.Size = new System.Drawing.Size(139, 70);
			// 
			// mnuQTabsNew
			// 
			this.mnuQTabsNew.Name = "mnuQTabsNew";
			this.mnuQTabsNew.Size = new System.Drawing.Size(138, 22);
			this.mnuQTabsNew.Text = "New Tab";
			this.mnuQTabsNew.Click += new System.EventHandler(this.MnuQTabsNewClick);
			// 
			// mnuQTabsClose
			// 
			this.mnuQTabsClose.Name = "mnuQTabsClose";
			this.mnuQTabsClose.Size = new System.Drawing.Size(138, 22);
			this.mnuQTabsClose.Text = "Close Tab";
			this.mnuQTabsClose.Click += new System.EventHandler(this.MnuQTabsCloseClick);
			// 
			// mnuQTabsRename
			// 
			this.mnuQTabsRename.Name = "mnuQTabsRename";
			this.mnuQTabsRename.Size = new System.Drawing.Size(138, 22);
			this.mnuQTabsRename.Text = "Rename Tab";
			this.mnuQTabsRename.Click += new System.EventHandler(this.MnuQTabsRenameClick);
			// 
			// mnuTableOptions
			// 
			this.mnuTableOptions.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.mnuTableOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuTableDrop,
			this.mnuTableViewCreate});
			this.mnuTableOptions.Name = "mnuTableOptions";
			this.mnuTableOptions.Size = new System.Drawing.Size(172, 48);
			// 
			// mnuTableDrop
			// 
			this.mnuTableDrop.Name = "mnuTableDrop";
			this.mnuTableDrop.Size = new System.Drawing.Size(171, 22);
			this.mnuTableDrop.Text = "Drop";
			// 
			// mnuTableViewCreate
			// 
			this.mnuTableViewCreate.Name = "mnuTableViewCreate";
			this.mnuTableViewCreate.Size = new System.Drawing.Size(171, 22);
			this.mnuTableViewCreate.Text = "View Create Query";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(900, 486);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusMain);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "SQLRunner2";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusMain.ResumeLayout(false);
			this.statusMain.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tabDatabaseObjects.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.mnuQueryTabs.ResumeLayout(false);
			this.mnuTableOptions.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
