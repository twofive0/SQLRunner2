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
			currentDataset.Clear();
			currentConnection.getDBAdapter(getCurrentQueryText()).Fill(currentDataset);
			gridResults.DataSource = bindingSource1;
			gridResults.AutoGenerateColumns = true;
			bindingSource1.DataSource = currentDataset;
			bindingSource1.DataMember = currentDataset.Tables[0].TableName;
			currentTable = currentDataset.Tables[0];
			gridResults.Refresh();
			Application.DoEvents();
		}
		
		public int runCurrentQueryTextAction()
		{
			currentConnection = dbConnMan.getCurrentConnection();
			int recordsModified = 0;

			try
			{
				currentConnection = dbConnMan.getCurrentConnection();
				IDbConnection dbcon = currentConnection.getDBConnection();
				dbcon.Open();
				IDbCommand cmd = dbcon.CreateCommand();
				cmd.CommandText = getCurrentQueryText();
				recordsModified = cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			return recordsModified;
		}

		public string fileExport(string fileType, string fileName, char delimiter = ',')
		{
			DataSetFileExport exp = new DataSetFileExport();
			bool expSuccess = false;
			string exportText = "";
			//Excel(.xlsx)
			//CSV
			//Tab Delimited
			//Choose Delimiter
			//XML
			//Multiset

			switch (fileType)
			{
				case "Excel (.xlsx)":
					exportText = DataSetFileExport.SendDataTableToExcel(currentTable, fileName);
					break;
				case "CSV":
					expSuccess = exp.DataTable2CSV(currentTable, fileName);
					break;
				case "Tab Delimited":
					expSuccess = exp.DataTable2txt(currentTable, fileName, '\t');
					break;
				case "Choose Delimiter":
					expSuccess = exp.DataTable2txt(currentTable, fileName, delimiter);
					break;
				case "XML":
					try
					{
						currentTable.WriteXml(fileName, XmlWriteMode.WriteSchema);
					}
					catch (Exception ex)
					{
						exportText = ex.Message.ToString();
					}
					break;
				case "Multiset":
					exp.dataTable2Multiset(ref currentTable, fileName);
					SevenZip.SevenZipCompressor szip = new SevenZip.SevenZipCompressor();
					szip.ScanOnlyWritable = true;
					szip.CompressFiles(fileName + ".7z", fileName);
					System.IO.File.Delete(fileName);
					expSuccess = true;
					break;
				default:
					return "No file type selected!";
			}

			if (expSuccess)
				return "File export success";
			else
				return "File export failed";

		}
	}
}
