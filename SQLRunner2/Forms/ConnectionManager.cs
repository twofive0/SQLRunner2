/*
 * Created by SharpDevelop.
 * User: devin
 * Date: 5/10/2016
 * Time: 12:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using DataMovement;
using System.Data;

namespace SQLRunner2.Subforms
{
	/// <summary>
	/// Description of ConnectionManager.
	/// </summary>
	public partial class frmConnectionManager : Form
	{
		private DBConnectionManager connMan;
		private DBConnection currentConnection;
		private int currentLVIndex = -1;
		
		public frmConnectionManager(DBConnectionManager connManager)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			connMan = connManager;
			
			connMan.loadConnections();
			
			cboDatabaseType.Items.AddRange(connMan.DBTypes.ToArray());
			
			if (connMan.ConnectionList != null)
			{
				if (connMan.ConnectionList.Count > 0)
				{
					updateConnectionList();
					getFirstListViewItem();
					setDBControls();
				    CboDatabaseTypeTextChanged(this, null);
					ChkIntegratedSecurityCheckedChanged(this, null);
				}
				else
				{
					BtnNewClick(this, null);
				}
			}
			
		}
		
		ListViewItem getListViewItemByID(string ID)
		{
			foreach (ListViewItem lvi in lvConnections.Items)
			{
				if (lvi.SubItems[1].Text == ID)
				{
					return lvi;
				}
			}
			
			return null;
		}
		
		void getFirstListViewItem()
		{
			if (lvConnections.Items.Count > 0)
			{
				lvConnections.Items[0].Selected = true;
				currentLVIndex = 0;
				currentConnection = connMan.getConnectionByID(lvConnections.Items[0].SubItems[1].Text);
			}
		}
		
		void setListViewItemByID(string ID)
		{
			foreach (ListViewItem lvi in lvConnections.Items)
			{
				if (lvi.SubItems[1].Text == ID)
				{
					lvi.Selected = true;
					currentLVIndex = lvi.Index;
				}
			}
		}
		
		void setDBControls()
		{
			if (currentConnection != null)
			{
				txtConnectionName.Text = currentConnection.ConnectionName;
				txtDatabase.Text = currentConnection.Database;
				txtFilePassword.Text = currentConnection.FilePassword;
				txtFilePath.Text = currentConnection.FileLocation;
				txtPassword.Text = currentConnection.UserPassword;
				txtServer.Text = currentConnection.Server;
				txtUserName.Text = currentConnection.UserName;
				cboDatabaseType.SelectedItem = currentConnection.ConnectionType;
				chkIntegratedSecurity.Checked = currentConnection.UseTrustedConnection;
			}
		}
		void CboDatabaseTypeTextChanged(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(cboDatabaseType.Text)) return;
			
			if (cboDatabaseType.Text == "SQL Server")
			{
				grpSQL.Enabled = true;
				grpFile.Enabled = false;
			}
			else
			{
			    grpSQL.Enabled = false;
				grpFile.Enabled = true;
			}
			
			//update listbox icon
			try{
			switch (cboDatabaseType.Text)
			{
				case "SQL Server":
					getListViewItemByID(currentConnection.ConnectionID).ImageIndex = 0;
					break;
				case "SQLite":
					getListViewItemByID(currentConnection.ConnectionID).ImageIndex = 1;
					break;
				case "SQL Compact":
					getListViewItemByID(currentConnection.ConnectionID).ImageIndex = 2;
					break;
				case "PostgreSQL":
					getListViewItemByID(currentConnection.ConnectionID).ImageIndex = 4;
					break; 
				case "MySQL":
					getListViewItemByID(currentConnection.ConnectionID).ImageIndex = 3;
					break;
				case "Access MDB":
					getListViewItemByID(currentConnection.ConnectionID).ImageIndex = 5;
					break;
				case "Access ACCDB":
					getListViewItemByID(currentConnection.ConnectionID).ImageIndex = 5;
					break;
				default:
					getListViewItemByID(currentConnection.ConnectionID).ImageIndex = 6;
					break;
			}
			} catch(Exception ex) {}
		}
		
		void CboDatabaseTargetTextChanged(object sender, EventArgs e)
		{
	
		}
		
		void BtnFileSelectClick(object sender, EventArgs e)
		{
			OpenFileDialog fd = new OpenFileDialog();
			
			DialogResult dr = fd.ShowDialog();
			
			if (dr == DialogResult.OK)
			{
				this.txtFilePath.Text = fd.FileName;
			}
		}
		void BtnSaveClick(object sender, EventArgs e)
		{
			//bindingSourceConnections.EndEdit();
			commitDBControls();
			updateConnectionList();
			connMan.saveConnections();
			this.Close();
		}
		void commitDBControls()
		{
			currentConnection.ConnectionName = txtConnectionName.Text;
			currentConnection.ConnectionType = cboDatabaseType.Text;
			currentConnection.Database = txtDatabase.Text;
			currentConnection.FileLocation = txtFilePath.Text;
			currentConnection.FilePassword = txtFilePassword.Text;
			currentConnection.Server = txtServer.Text;
			currentConnection.UserName = txtUserName.Text;
			currentConnection.UserPassword = txtPassword.Text;
			currentConnection.UseTrustedConnection = chkIntegratedSecurity.Checked;
		}
		void BtnDeleteClick(object sender, EventArgs e)
		{
			connMan.ConnectionList.Remove(currentConnection);
			
			if (connMan.ConnectionList.Count == 0)
			{
				BtnNewClick(this, null);
			}
			
			updateConnectionList();
			
			getFirstListViewItem();
		}
		void BtnNewClick(object sender, EventArgs e)
		{
			
			DBConnection newConnection = new DBConnection();
			newConnection.ConnectionName = "New Connection";
			connMan.ConnectionList.Add(newConnection);
			
			updateConnectionList();
			setListViewItemByID(newConnection.ConnectionID);
			currentConnection = connMan.getConnectionByID(newConnection.ConnectionID);
			setDBControls();
	
		}
		void ChkIntegratedSecurityCheckedChanged(object sender, EventArgs e)
		{
			if (chkIntegratedSecurity.Checked == false)
			{
				txtUserName.Enabled = true;
				txtPassword.Enabled = true;
			}
			else
			{
				txtUserName.Enabled = false;
				txtPassword.Enabled = false;
			}
		}
		void updateConnectionList()
		{
			//fill the list
			lvConnections.Items.Clear();
			foreach(DBConnection dbc in connMan.ConnectionList)
			{
				ListViewItem lvi = new ListViewItem(new String[] { dbc.ConnectionName, dbc.ConnectionID });
				
				//update listbox icon
				try{
					switch (dbc.ConnectionType)
					{
						case "SQL Server":
							lvi.ImageIndex = 0;
							break;
						case "SQLite":
							lvi.ImageIndex = 1;
							break;
						case "SQL Compact":
							lvi.ImageIndex = 2;
							break;
						case "PostgreSQL":
							lvi.ImageIndex = 4;
							break; 
						case "MySQL":
							lvi.ImageIndex = 3;
							break;
						case "Access MDB":
							lvi.ImageIndex = 5;
							break;
						case "Access ACCDB":
							lvi.ImageIndex = 5;
							break;
						default:
							lvi.ImageIndex = 6;
							break;
					}
				} catch(Exception ex) {}
				
				lvConnections.Items.Add(lvi);
			}
		}

		void TxtConnectionNameTextChanged(object sender, EventArgs e)
		{
			lvConnections.Items[currentLVIndex].SubItems[0].Text = txtConnectionName.Text;
		}
		
		void LvConnectionsSelectedIndexChanged(object sender, EventArgs e)
		{
			commitDBControls();
			connMan.saveConnections();
			
			if (lvConnections.SelectedItems.Count > 0)
			{
				string currentID = lvConnections.SelectedItems[0].SubItems[1].Text;
				currentConnection = connMan.getConnectionByID(currentID);
				currentLVIndex = lvConnections.SelectedItems[0].Index;
				setDBControls();
			}
		}

        private void btnUpgradeSDF_Click(object sender, EventArgs e)
        {
			DBConnectionManager.UpgradeDatabasewithCaseSensitive(txtFilePath.Text);
        }
    }
}
