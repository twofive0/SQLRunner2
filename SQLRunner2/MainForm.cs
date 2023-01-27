/*
 * Created by SharpDevelop.
 * User: devin
 * Date: 2/4/2019
 * Time: 4:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using SQLRunner2.Forms;
using SQLRunner2.Subforms;
using SQLRunner2.Controls;
using DataMovement;
using Utils;

namespace SQLRunner2
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		public DBConnectionManager dbConnMan = new DBConnectionManager("SQLRunner2");
		public frmConnectionManager frmConnMan;
		
		//local storage objects
		public DataTable dt;
		public DataTable tblListTable;
		public DataTable tblListViews;
		public DataTable tblListProcs;
		private bool firstOpen = true;
			
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			frmConnMan = new frmConnectionManager(dbConnMan);
			Globals.CurrentApplication = "SQLRunner2";
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			newTab("New Query");
			
			dbConnMan.loadConnections();
			
		}
		void ConnectionManagerToolStripMenuItemClick(object sender, EventArgs e)
		{
			frmConnMan.ShowDialog();
			RefreshCurrentDataSource();
		}
		void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			ShowAboutHelp();
		}
		void BtnAboutHelpClick(object sender, EventArgs e)
		{
			ShowAboutHelp();
		}
		
		void ShowAboutHelp()
		{
			MessageBox.Show("SQLRunner2 by Devin Williams - Under Construction");
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			
			Text = "SQLRunner2 - 2023.01.27.0";
			
			txtStatus.Text = "Not Connected (Press Refresh)";
			Application.DoEvents();
			
			tblListProcs = new DataTable("Procs");
			tblListTable = new DataTable("Tables");
			tblListViews = new DataTable("Views");
			
			//do first open stuff...
			
			RefreshCurrentDataSource();
			
		}
		
		void clearTreeViews()
		{
			
			
			if (tvTablesList.Nodes.Count > 0)
			{
				tvTablesList.Nodes.Clear();
			}
			
			if (tvViewsList.Nodes.Count > 0)
			{
				tvViewsList.Nodes.Clear();
			}
			
			if (tvProceduresList.Nodes.Count > 0)
			{
				TreeNode[] procRoot = tvProceduresList.Nodes.Find("Procedures",false);
				procRoot[0].Nodes.Clear();
			}
						
			tvTablesList.Refresh();
			tvViewsList.Refresh();
			tvProceduresList.Refresh();
			
		}
		
		void fillDatabaseList()
		{
			cboDatabaseConnection.Items.Clear();
			cboDatabaseConnection.Items.Add("(No DB Selected)");
			
			foreach(DBConnection dbc in dbConnMan.ConnectionList)
			{
				cboDatabaseConnection.Items.Add(dbc.ConnectionName);
			}
		}
		
		void RefreshCurrentDataSource()
		{
			clearTreeViews();
			fillDatabaseList();
		}
		
		void BtnRefreshDatabaseClick(object sender, EventArgs e)
		{
			if (cboDatabaseConnection.Text == "(No DB Selected)")
			{
				MessageBox.Show("Please select a database from the database list");
				return;
			}
			
			dbConnMan.setCurrentConnectionByName(cboDatabaseConnection.Text);
			
			clearTreeViews();
			
			try
			{
				List<string> tblList = new List<string>();
				tblList = dbConnMan.getCurrentConnection().getTablesList();
				
				foreach(string s in tblList)
				{
					TreeNode tvNode = new TreeNode(s, 0, 0);
					tvNode.ContextMenuStrip = mnuTableOptions;
					tvTablesList.Nodes.Add(tvNode);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Couldn't connect to the database: " + ex.Message);
			}
			
			txtStatus.Text = "Status: Connected";
			txtStatus.ForeColor = Color.ForestGreen;
			txtCurrentDatabase.Text = "Current Database: " + dbConnMan.getCurrentConnection().ConnectionName;
		}
		
		void newTab(string tabName)
		{			
			int nameCount = 0;
			foreach (TabPage tp in tabQueries.TabPages)
			{
				if (tp.Text == tabName) nameCount++;
				foreach (TabPage tp1 in tabQueries.TabPages)
				{
					if (tp.Text == tabName + nameCount.ToString()) nameCount++;
				}
			}
			
			if (nameCount > 0)
			{
				tabName = tabName + nameCount.ToString();
			}
			
			TabPage newTab = new TabPage(tabName);
			ucQueryWindow newQW = new ucQueryWindow(dbConnMan);
			newQW.Dock = DockStyle.Fill;
			newTab.Controls.Add(newQW);
			//tabQueries.ContextMenuStrip = mnuQueryTabs;
			tabQueries.TabPages.Add(newTab);
			tabQueries.SelectedTab = newTab;
			newQW.Focus();
		}
		
		void NewTabToolStripMenuItemClick(object sender, EventArgs e)
		{
			newTab("New Query");
		}
		void RenameTabToolStripMenuItemClick(object sender, EventArgs e)
		{
			InputBox ib = new InputBox("Rename Tab");
			
			ib.ShowDialog();
			
			if (ib.DialogResult == DialogResult.OK) tabQueries.SelectedTab.Text = ib.InputText;
		}
		void CloseTabToolStripMenuItemClick(object sender, EventArgs e)
		{
			tabQueries.TabPages.Remove(tabQueries.SelectedTab);
		}
		void BtnRunQueryClick(object sender, EventArgs e)
		{
			ucQueryWindow currentQuery = ((ucQueryWindow)tabQueries.SelectedTab.Controls[0]);
			currentQuery.runCurrentQueryText();
			Logging.LogScript(currentQuery.getCurrentQueryText(), "SQLRunner2");
		}
		void MnuQTabsNewClick(object sender, EventArgs e)
		{
			NewTabToolStripMenuItemClick(this, null);
		}
		void MnuQTabsCloseClick(object sender, EventArgs e)
		{
			CloseTabToolStripMenuItemClick(this, null);
		}
		void MnuQTabsRenameClick(object sender, EventArgs e)
		{
			RenameTabToolStripMenuItemClick(this, null);
		}
		void TvTablesListDoubleClick(object sender, EventArgs e)
		{
			if (tvTablesList.SelectedNode != null)
			{
				string tableName = tvTablesList.SelectedNode.Text;

				newTab(tableName);
				ucQueryWindow currentQuery = ((ucQueryWindow)tabQueries.SelectedTab.Controls[0]);
				currentQuery.setCurrentQuery(getSelectTopSQL(tableName));
				currentQuery.runCurrentQueryText();
				Logging.LogScript(currentQuery.getCurrentQueryText(), "SQLRunner2");
			}
		}
		void TabQueriesMouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
	        {
	            for (int i = 0; i < tabQueries.TabCount; ++i)
	            {
	                Rectangle r = tabQueries.GetTabRect(i);
	                if (r.Contains(e.Location) /* && it is the header that was clicked*/)
	                {
	                    mnuQueryTabs.Show(tabQueries, e.Location);
	                    break;
	                }
	                else
	                {
	                	ucQueryWindow currentQuery = ((ucQueryWindow)tabQueries.SelectedTab.Controls[0]);
	                }
	            }
	        }
		}
		
		string getSelectTopSQL(string tblName)
		{
			switch (dbConnMan.getCurrentConnection().ConnectionType)
			{
			case "SQL Server":
					return "SELECT TOP 1000 * FROM " + tblName;
				break;
			case "SQLite":
					return "SELECT * FROM " + tblName + " LIMIT 1000";
				break;
			case "SQL Compact":
				return "SELECT TOP 1000 * FROM " + tblName;
				break;
			case "PostgreSQL":
				return "SELECT * FROM " + tblName + " LIMIT 1000";
				break; 
			case "MySQL":
				return "SELECT * FROM " + tblName + " LIMIT 1000";
				break;
			case "Access MDB":
				return "SELECT TOP 1000 * FROM " + tblName;
				break;
			case "Access ACCDB":
				return "SELECT TOP 1000 * FROM " + tblName;
				break;
			default:
					return "SELECT * FROM " + tblName + " LIMIT 1000";
				break;
			}
		}
		void BtnRunActionClick(object sender, EventArgs e)
		{
			txtStatus.Text = "Running update...";
			ucQueryWindow currentQuery = ((ucQueryWindow)tabQueries.SelectedTab.Controls[0]);
			int recordsModified = currentQuery.runCurrentQueryTextAction();
			txtStatus.Text = recordsModified.ToString() + " records modified @ " + DateTime.Now.ToString();
			Logging.LogScript(currentQuery.getCurrentQueryText(), "SQLRunner2");
		}
		void BtnOpenScriptClick(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "SQL Files (*.SQL)|*.sql|All Files (*.*)|*.*";
			
			DialogResult dr = ofd.ShowDialog();
			
			//TODO: Needs save file funcitonality, cleanup
			
			if (dr == DialogResult.OK)
			{
				newTab(ofd.FileName.ToString());
				ucQueryWindow currentQuery = ((ucQueryWindow)tabQueries.SelectedTab.Controls[0]);
				currentQuery.setCurrentQuery(File.ReadAllText(ofd.FileName.ToString()));
			}
		}

        private void btnSaveResultsAs_Click(object sender, EventArgs e)
        {
			SaveFileDialog sfd = new SaveFileDialog();

			DialogResult dr = sfd.ShowDialog();
			string expSuccess = "Nothing happened";

			if (dr == DialogResult.OK)
			{
				ucQueryWindow currentQuery = ((ucQueryWindow)tabQueries.SelectedTab.Controls[0]);
				expSuccess = currentQuery.fileExport(this.cboExportFormat.Text, sfd.FileName);
			}

			txtStatus.Text = expSuccess + " @ " + DateTime.Now.ToString();

		}

        private void openScriptLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
			newTab("Query Log");
			ucQueryWindow currentQuery = ((ucQueryWindow)tabQueries.SelectedTab.Controls[0]);
			currentQuery.setCurrentQuery(File.ReadAllText(Logging.getScriptLogPath("SQLRunner2")));
		}
    }
}
