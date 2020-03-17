/*
 * Created by SharpDevelop.
 * User: devin
 * Date: 4/10/2019
 * Time: 5:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using DataMovement;
using Utils;
using FastColoredTextBoxNS;

namespace SQLRunner2.Controls
{
	/// <summary>
	/// Description of ucQueryWindow.
	/// </summary>
	public partial class ucQueryWindow : UserControl
	{
		
		DataTable currentTable;
		DataSet currentDataset = new DataSet();
		DBConnectionManager dbConnMan;
		DBConnection currentConnection;
		FastColoredTextBox currentText = new FastColoredTextBox();
		
		public ucQueryWindow(DBConnectionManager dbc)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			dbConnMan = dbc;
			
			if (!Globals.IsOSLinux())
			{
				currentText.Language = Language.SQL;
				splitContainer1.Panel1.Controls.Remove(splitContainer1.Panel1.Controls[0]);
				currentText.Dock = DockStyle.Fill;
				splitContainer1.Panel1.Controls.Add(currentText);
			}
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public void setCurrentQuery(string queryText)
		{
			if (!Globals.IsOSLinux())
			{
				currentText.Text = queryText;
			}
			else
			{
				txtQuery.Text = queryText;
			}
		}
		
		public string getCurrentQueryText()
		{
			if (!Globals.IsOSLinux())
			{
				return currentText.Text;
			}
			else
			{
				return txtQuery.Text;
			}			
		}
		
		public void runCurrentQueryText()
		{
			currentConnection = dbConnMan.getCurrentConnection();
			
			currentConnection.getDBAdapter(getCurrentQueryText()).Fill(currentDataset);
			gridResults.DataSource = bindingSource1;
			gridResults.AutoGenerateColumns = true;
			bindingSource1.DataSource = currentDataset;
			bindingSource1.DataMember = currentDataset.Tables[0].TableName;
			
			gridResults.Refresh();
			Application.DoEvents();
		}
		
		public void runCurrentQueryTextAction()
		{
			currentConnection = dbConnMan.getCurrentConnection();
			int recordsModified = 0;
			
			switch (currentConnection.ConnectionType)
			{
				case "SQL Server":
					//use DataSetSQLExport?
					//get command, then executenonquery
				case "SQLite":
					break;
						
					
			}
			
		}

	}
}
