using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SQLite;
using System.IO;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;
using System.Data.OleDb;
using System.Data.SqlClient;
using Utils;

namespace DataMovement
{
	/// <summary>
	/// This class is responsible for moving data from DataSet/DataTable ADO.Net objects to a stored data source.
	/// </summary>   
    public class DataSetSQLExport
    {

        /// <summary>
        /// Background worker responsible for executing transactions.
        /// </summary>
    	public BackgroundWorker TransferDataWorker = new BackgroundWorker();

        //Local DB Objects
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();

        //Selected DB Props
        public string ConnType { get; set;}
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string PW { get; set; }
        public string FileName { get; set; }
        public string FilePW { get; set; }
        public string xferTableName { get; set; }
        
        private bool CreateIDField = true;

        /// <summary>
        /// Default constructor, simply sets up BackgroundWorker for transfer duty.
        /// </summary>
        public DataSetSQLExport()
        {
            TransferDataWorker.WorkerReportsProgress = true;
            TransferDataWorker.WorkerSupportsCancellation = true;
            TransferDataWorker.DoWork += new DoWorkEventHandler(TransferDataWorkerDoWork);
            TransferDataWorker.ProgressChanged += TransferDataWorker_ProgressChanged;
        }

        
        /// <summary>
        /// Sets the current connection from a DBConnection object.
        /// </summary>
        /// <param name="dbConnect">DBConnection object</param>
        public void SetConnection(DBConnection dbConnect)
        {
            ConnType = dbConnect.ConnectionType;
            ServerName = dbConnect.Server;
            DatabaseName = dbConnect.Database;
            UserName = dbConnect.UserName;
            PW = dbConnect.UserPassword;
            FileName = dbConnect.FileLocation;
            FilePW = dbConnect.UserPassword;
        }

        /// <summary>
        /// Starts data transfer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TransferDataWorkerDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            SendData();
        }

		private void TransferDataWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			//write error messages to log
			if (Globals.CurrentApplication == string.Empty)
			{
				//Logging.LogError(e.UserState.ToString(), "DataSetSQLExport", "TransferDataWorker.DoWork",Globals.CurrentApplication);
			}
			else
			{
				//Logging.LogError(e.UserState.ToString(), "DataSetSQLExport", "TransferDataWorker.DoWork", Globals.CurrentApplication);
			}
		}
		
		
        /// <summary>
        /// Send data to the declared stored connection.
        /// </summary>
        /// <param name="tblData">The ADO.Net data table to be transferred</param>
        /// <returns></returns>
        public string SendToConnection(DataTable tblData, bool createIDField = true)
        {
            string errorMessage = string.Empty;
            
            CreateIDField = createIDField;

            dt = tblData;

            //don't start an operation until the previous is finished
            while (TransferDataWorker.IsBusy)
            {
                System.Threading.Thread.Sleep(10);
            }
            //SendData
            //TransferDataWorker.RunWorkerAsync();
            SendData();

            return errorMessage;

        }

        private void SendData()
        {

            switch (ConnType)
            {
                case "SQL Server":
                    CopyToSQL(DoesTableExistSQL());
                    break;
                case "SQLite":
                    if (!File.Exists(FileName))
                    {
                        try
                        {
                            SQLiteConnection.CreateFile(FileName);
                        }
                        catch (Exception ex)
                        {
                        	TransferDataWorker.ReportProgress(0, "Couldn't create SQLite 3 database" + ex.Message);
                        }
                    }
                    CopyToSQLite(DoesTableExistSQLite(), FileName);
                    break;
                case "Firebird":
                    if (!File.Exists(FileName))
                    {
                        try
                        {
                            //FbConnection fbConnect = new FbConnection("User=SYSDBA;Password=masterkey;Database=" + FileName + ";DataSource=localhost;" +
                            //                                            "Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;" +
                            //                                            "MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=1;");
                        }
                        catch (Exception ex)
                        {
                            TransferDataWorker.ReportProgress(0, "Couldn't create Firebird database" + ex.Message);
                        }
                    }
                    CopyToFirebird(DoesTableExistFirebird(), FileName);
                    break;
            }

        }


    #region "SQLite"

        private bool DoesTableExistSQLite()
        {
            bool functionReturnValue = false;

            functionReturnValue = false;

            try
            {
                string connString = null;
                if (FilePW == null)
                {
                    connString = "Data Source =" + FileName + ";Version=3;DateTimeFormat=CurrentCulture;";
                }
                else
                {
                    connString = "Data Source =" + FileName + ";Version=3;Password =" + FilePW + ";;DateTimeFormat=CurrentCulture";
                }
                SQLiteConnection sqliteConnect = new SQLiteConnection(connString);
                SQLiteDataAdapter sqliteDA = new SQLiteDataAdapter("SELECT name FROM sqlite_master WHERE type='table' and name = '" + xferTableName + "'", sqliteConnect);
                DataTable tblListTable = new DataTable();
                tblListTable.TableName = "TableList";
                tblListTable.Clear();
                sqliteDA.Fill(tblListTable);

                if (tblListTable.Rows.Count > 0)
                    functionReturnValue = true;

            }
            catch (Exception ex)
            {
            	TransferDataWorker.ReportProgress(0, ex.Message);
            }
            return functionReturnValue;

        }


        private void CopyToSQLite(bool tableExists, string target)
        {

            this.TransferDataWorker.ReportProgress(0, "Saving to SQLite");

            int rowCtr = 0;

            try
            {
                string connString = "Data Source =" + FileName + ";Version=3;DateTimeFormat=CurrentCulture";
                SQLiteConnection sqlConnection = new SQLiteConnection(connString);
                string addTblCommand = createMakeSQLiteTableCmd(dt);
                SQLiteCommand sqliteCommand = new SQLiteCommand(addTblCommand, sqlConnection);
                SQLiteCommand sqliteCommand2 = new SQLiteCommand("", sqlConnection);
                sqlConnection.Open();
                if (!DoesTableExistSQLite())
                    sqliteCommand.ExecuteNonQuery();
                SQLiteTransaction bulkInsert = sqlConnection.BeginTransaction();
                try
                {
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        sqliteCommand2.CommandText = createInsertSQLiteDataCmd(xferTableName, dtRow);
                        sqliteCommand2.ExecuteNonQuery();
                        double rc = rowCtr;
                        double rt = dt.Rows.Count;
                        double pDone = (rc / rt) * 100;
                        int percentDone = Convert.ToInt32(pDone);
                        this.TransferDataWorker.ReportProgress(percentDone, "Saving to SQLite: " + xferTableName);
                        rowCtr += 1;
                        //handle transactions 9999 records at a time
                        if (rowCtr % 9999 == 0.0)
                        {
                            bulkInsert.Commit();
                            bulkInsert = sqlConnection.BeginTransaction();
                        }
                    }
                    bulkInsert.Commit();
                }
                catch (Exception ex)
                {
                	TransferDataWorker.ReportProgress(0, ex.Message);
                    bulkInsert.Rollback();
                    throw;
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                this.TransferDataWorker.ReportProgress(0, "Error Saving to SQLite: " + ex.Message);
                Logging.LogError(ex.Message, ex.Source, ex.StackTrace, Globals.CurrentApplication);
            }

        }

        /// <summary>
        /// Create a view in SQLite for the current connection.
        /// </summary>
        /// <param name="SQL">SQL to execute</param>
        public void createSQLiteView(string SQL)
        {
            try
            {
                string connString = "Data Source =" + FileName + ";Version=3;DateTimeFormat=CurrentCulture";
                SQLiteConnection sqlConnection = new SQLiteConnection(connString);
                SQLiteCommand sqliteCommand = new SQLiteCommand(SQL, sqlConnection);
                sqlConnection.Open();
                //if (!DoesTableExistSQLite())
                    sqliteCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
            	Logging.LogError(ex.Message, ex.Source, ex.StackTrace, Globals.CurrentApplication);
            }
        }

        /// <summary>
        /// Get SQLite command to create a new table based off of an ADO.Net DataTable object
        /// </summary>
        /// <param name="dt">ADO.Net DataTable</param>
        /// <returns></returns>
        public string createMakeSQLiteTableCmd(DataTable dt)
        {

            string tmpCmdString = null;

            if (CreateIDField)
            	tmpCmdString = "CREATE TABLE [" + xferTableName + "] (UNIQUE_ID INTEGER PRIMARY KEY AUTOINCREMENT,";
            else
            	tmpCmdString = "CREATE TABLE [" + xferTableName + "] (";

            foreach (DataColumn clm in dt.Columns)
            {
                tmpCmdString = tmpCmdString + "[" + clm.ColumnName.ToString() + "] ";
                switch (clm.DataType.ToString())
                {
                    case "System.Integer":
                        tmpCmdString = tmpCmdString + "INTEGER, ";
                        break;
                    case "System.Int32":
                        tmpCmdString = tmpCmdString + "INTEGER, ";
                        break;
                    case "System.Double":
                        tmpCmdString = tmpCmdString + "DOUBLE, ";
                        break;
                    case "System.Int16":
                        tmpCmdString = tmpCmdString + "INTEGER, ";
                        break;
                    case "System.Int64":
                        tmpCmdString = tmpCmdString + "INTEGER, ";
                        break;
                    case "System.String":
                        tmpCmdString = tmpCmdString + "TEXT, ";
                        break;
                    case "System.DateTime":
                        tmpCmdString = tmpCmdString + "DATETIME, ";
                        break;
                    case "System.Boolean":
                        tmpCmdString = tmpCmdString + "BOOLEAN, ";
                        break;
                    default:
                        tmpCmdString = tmpCmdString + "TEXT, ";
                        break;
                }
            }

            //trim extra comma
            tmpCmdString = tmpCmdString.TrimEnd(' ').TrimEnd(',');

            tmpCmdString = tmpCmdString + ")";

            return tmpCmdString;

        }

        /// <summary>
        /// Get SQLite insert command to add a row of data from ADO.Net DataRow into a specific table.
        /// </summary>
        /// <param name="tableName">Target table name</param>
        /// <param name="dr">ADO.Net DataRow</param>
        /// <returns></returns>
        public string createInsertSQLiteDataCmd(string tableName, DataRow dr)
        {

            string tmpCmdString = null;

            tmpCmdString = "INSERT INTO [" + tableName + "](";

            foreach (DataColumn col in dr.Table.Columns)
            {
                tmpCmdString = tmpCmdString + "[" + col.ColumnName + "], ";
            }

            //trim extra comma
            tmpCmdString = tmpCmdString.TrimEnd(' ').TrimEnd(',');
            tmpCmdString = tmpCmdString + ") VALUES(";

            foreach (object itm in dr.ItemArray)
            {
                switch (itm.GetType().ToString())
                {
                    case "System.Integer":
                        tmpCmdString = tmpCmdString + itm.ToString() + ", ";
                        break;
                    case "System.Double":
                        tmpCmdString = tmpCmdString + itm.ToString() + ", ";
                        break;
                    case "System.Int32":
                        tmpCmdString = tmpCmdString + itm.ToString() + ", ";
                        break;
                    case "System.Int16":
                        tmpCmdString = tmpCmdString + itm.ToString() + ", ";
                        break;
                    case "System.Int64":
                        tmpCmdString = tmpCmdString + itm.ToString() + ", ";
                        break;
                    case "System.String":
                        tmpCmdString = tmpCmdString + "'" + itm.ToString().Replace("'", "''").Replace("\"", "\"\"") + "', ";
                        break;
                    case "System.DateTime":
                        tmpCmdString = tmpCmdString + "'" + string.Format("{0:u}", itm.ToString().Replace("'", "''").Replace("\"", "\"\"")) + "', ";
                        break;
                    case "System.Boolean":
                        if (itm.ToString() == "True")
                        {
                            tmpCmdString = tmpCmdString + "1, ";
                        }
                        else
                        {
                            tmpCmdString = tmpCmdString + "0, ";
                        }
                        break;
                    default:
                        tmpCmdString = tmpCmdString + "NULL, ";
                        break;
                }
            }

            //trim extra comma
            tmpCmdString = tmpCmdString.TrimEnd(' ').TrimEnd(',');

            tmpCmdString = tmpCmdString + ")";

            return tmpCmdString;

        }

        /// <summary>
        /// Get parameterized SQLite ADO.Net Insert Command object for an ADO.Net DataRow object.
        /// </summary>
        /// <param name="tableName">Target table name</param>
        /// <param name="dr">ADO.Net DataRow</param>
        /// <returns></returns>
        public static SQLiteCommand getSQLiteDataAdapterInsertCmd(string tableName, DataRow dr)
        {

            string tmpCmdString = string.Empty;
            
            SQLiteCommand cmd = new SQLiteCommand();

            List<SQLiteParameter> queryParams = new List<SQLiteParameter>();

            tmpCmdString = "INSERT INTO " + tableName + "(";

            foreach (DataColumn col in dr.Table.Columns)
            {
                if (col.ColumnName != "UNIQUE_ID") tmpCmdString = tmpCmdString + "[" + col.ColumnName + "], ";
            }

            //trim extra comma
            tmpCmdString = tmpCmdString.TrimEnd(' ').TrimEnd(',');
            tmpCmdString = tmpCmdString + ") VALUES(";

            foreach (DataColumn col in dr.Table.Columns)
            {
                if (col.ColumnName != "UNIQUE_ID")
                {
                    tmpCmdString = tmpCmdString + "@" + col.ColumnName + ",";
                    switch (col.DataType.ToString())
                    {
                        case "System.Integer":
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.Int16, col.ColumnName));
                            break;
                        case "System.Double":
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.Double, col.ColumnName));
                            break;
                        case "System.Int32":
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.Int32, col.ColumnName));
                            break;
                        case "System.Int16":
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.Int16, col.ColumnName));
                            break;
                        case "System.Int64":
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.Int64, col.ColumnName));
                            break;
                        case "System.String":
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.String, col.ColumnName));
                            break;
                        case "System.DateTime":
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.DateTime, col.ColumnName));
                            break;
                        case "System.Boolean":
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.Boolean, col.ColumnName));
                            break;
                        default:
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.String, col.ColumnName));
                            break;
                    }
                }
            }

            //trim extra comma
            tmpCmdString = tmpCmdString.TrimEnd(' ').TrimEnd(',');

            tmpCmdString = tmpCmdString + ")";

            cmd.Parameters.AddRange(queryParams.ToArray());
            cmd.CommandText = tmpCmdString;

            return cmd;
        }
        
        /// <summary>
        /// Get parameterized SQLite ADO.Net Update Command object for an ADO.Net DataRow object.
        /// </summary>
        /// <param name="tableName">Target table name</param>
        /// <param name="dr">ADO.Net DataRow</param>
        /// <returns></returns>
        public static SQLiteCommand getSQLiteDataAdapterUpdateCmd(string tableName, DataRow dr)
        {

            string tmpCmdString = string.Empty;

            SQLiteCommand cmd = new SQLiteCommand();

            List<SQLiteParameter> queryParams = new List<SQLiteParameter>();

            tmpCmdString = "UPDATE " + tableName + " SET ";

            foreach (DataColumn col in dr.Table.Columns)
            {
                if (col.ColumnName != "UNIQUE_ID") tmpCmdString = tmpCmdString + "[" + col.ColumnName + "] = @" + col.ColumnName + ", ";
            }

            //trim extra comma
            tmpCmdString = tmpCmdString.TrimEnd(' ').TrimEnd(',');
            tmpCmdString = tmpCmdString + " WHERE UNIQUE_ID = @UNIQUE_ID";
            queryParams.Add(new SQLiteParameter("@UNIQUE_ID", DbType.Int64, "UNIQUE_ID"));

            foreach (DataColumn col in dr.Table.Columns)
            {
                if (col.ColumnName != "UNIQUE_ID")
                {
                    switch (col.DataType.ToString())
                    {
                        case "System.Integer":
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.Int16, col.ColumnName));
                            break;
                        case "System.Double":
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.Double, col.ColumnName));
                            break;
                        case "System.Int32":
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.Int32, col.ColumnName));
                            break;
                        case "System.Int16":
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.Int16, col.ColumnName));
                            break;
                        case "System.Int64":
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.Int64, col.ColumnName));
                            break;
                        case "System.String":
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.String, col.ColumnName));
                            break;
                        case "System.DateTime":
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.DateTime, col.ColumnName));
                            break;
                        case "System.Boolean":
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.Boolean, col.ColumnName));
                            break;
                        default:
                            queryParams.Add(new SQLiteParameter("@" + col.ColumnName, DbType.String, col.ColumnName));
                            break;
                    }
                }
            }

            cmd.Parameters.AddRange(queryParams.ToArray());
            cmd.CommandText = tmpCmdString;

            return cmd;
        }
        
        /// <summary>
        /// Get parameterized SQLite ADO.Net Delete Command object for an ADO.Net DataRow object.
        /// </summary>
        /// <param name="tableName">Target table name</param>
        /// <param name="dr">ADO.Net DataRow</param>
        /// <returns></returns>
        public static SQLiteCommand getSQLiteDataAdapterDeleteCmd(string tableName, DataRow dr)
        {

            string tmpCmdString = string.Empty;

            SQLiteCommand cmd = new SQLiteCommand();

            List<SQLiteParameter> queryParams = new List<SQLiteParameter>();

            tmpCmdString = "DELETE FROM [" + tableName + "] WHERE UNIQUE_ID = @UNIQUE_ID";

            queryParams.Add(new SQLiteParameter("@UNIQUE_ID", DbType.Int64, "UNIQUE_ID"));

            cmd.Parameters.AddRange(queryParams.ToArray());
            cmd.CommandText = tmpCmdString;

            return cmd;
        }

        /// <summary>
        /// Deletes all data from target SQLite table.
        /// </summary>
        /// <param name="tableName">Target table name</param>
        /// <param name="sqlConnection">SQLiteConnection object to use</param>
        public static void clearSQLiteTable(string tableName, SQLiteConnection sqlConnection)
        {
            try
            {
                SQLiteCommand sqliteCommand = new SQLiteCommand("delete from " + tableName, sqlConnection);
            }
            catch { }
        }

        #endregion

    #region "MSSQL"

        private bool DoesTableExistSQL()
        {
            bool functionReturnValue = false;

            functionReturnValue = false;

            try
            {
                string connString = null;
                connString = "Data Source =" + ServerName + "; Initial Catalog =" + DatabaseName + "; User Id =" + UserName + "; Password =" + PW + "; ";
                SqlConnection sqlConnect = new SqlConnection(connString);
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT TABLE_NAME AS name FROM information_schema.TABLES where TABLE_NAME = '" + xferTableName + "'", sqlConnect);
                DataTable tblListTable = new DataTable();
                tblListTable.TableName = "TableList";
                tblListTable.Clear();
                sqlDA.Fill(tblListTable);

                if (tblListTable.Rows.Count > 0)
                    functionReturnValue = true;

            }
            catch (Exception ex)
            {
            	TransferDataWorker.ReportProgress(0, ex.Message);
            }
            return functionReturnValue;

        }


        private void CopyToSQL(bool tableExists)
        {

            this.TransferDataWorker.ReportProgress(0, "Saving to SQL");

            int rowCtr = 0;

            try
            {
                string connString = "Data Source =" + ServerName + "; Initial Catalog =" + DatabaseName + "; User Id =" + UserName + "; Password =" + PW + "; ";
                SqlConnection sqlConnection = new SqlConnection(connString);
                string addTblCommand = createMakeSQLTableCmd(dt);
                SqlCommand sqlCommand = new SqlCommand(addTblCommand, sqlConnection);
                SqlCommand sqlCommand2 = new SqlCommand("", sqlConnection);
                sqlConnection.Open();
                if (!DoesTableExistSQL())
                    sqlCommand.ExecuteNonQuery();
                foreach (DataRow dtRow in dt.Rows)
                {
                    sqlCommand2.CommandText = createInsertSQLDataCmd(xferTableName, dtRow);
                    sqlCommand2.ExecuteNonQuery();
                    this.TransferDataWorker.ReportProgress((rowCtr / dt.Rows.Count) * 100, "Saving to SQL: " + xferTableName);
                    rowCtr += 1;
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                this.TransferDataWorker.ReportProgress(0, "Error Saving to SQL: " + ex.Message);
                Logging.LogError(ex.Message, ex.Source, ex.StackTrace, Globals.CurrentApplication);
            }

        }

        /// <summary>
        /// Get SQL statement to create a table based off of an ADO.Net DataTable object.
        /// </summary>
        /// <param name="dt">ADO.Net DataTable</param>
        /// <returns>SQL statement as string</returns>
        public string createMakeSQLTableCmd(DataTable dt)
        {

            string tmpCmdString = null;

            tmpCmdString = "CREATE TABLE [" + xferTableName + "] (";

            foreach (DataColumn clm in dt.Columns)
            {
                tmpCmdString = tmpCmdString + "[" + clm.ColumnName.ToString() + "] ";
                switch (clm.DataType.ToString())
                {
                    case "System.Integer":
                        tmpCmdString = tmpCmdString + "DECIMAL, ";
                        break;
                    case "System.Int32":
                        tmpCmdString = tmpCmdString + "DECIMAL, ";
                        break;
                    case "System.Double":
                        tmpCmdString = tmpCmdString + "DECIMAL, ";
                        break;
                    case "System.Int16":
                        tmpCmdString = tmpCmdString + "INT, ";
                        break;
                    case "System.Int64":
                        tmpCmdString = tmpCmdString + "DECIMAL, ";
                        break;
                    case "System.String":
                        tmpCmdString = tmpCmdString + "NVARCHAR(MAX), ";
                        break;
                    case "System.DateTime":
                        tmpCmdString = tmpCmdString + "DATETIME, ";
                        break;
                    case "System.Boolean":
                        tmpCmdString = tmpCmdString + "BIT, ";
                        break;
                    default:
                        tmpCmdString = tmpCmdString + "NVARCHAR(MAX), ";
                        break;
                }
            }

            //trim extra comma
            tmpCmdString = tmpCmdString.TrimEnd(' ').TrimEnd(',');

            tmpCmdString = tmpCmdString + ")";

            return tmpCmdString;

        }

        /// <summary>
        /// Get SQL insert statement from ADO.Net DataRow object.
        /// </summary>
        /// <param name="tableName">Target table name</param>
        /// <param name="dr">ADO.Net DataRow object</param>
        /// <returns>SQL statement as string</returns>
        public string createInsertSQLDataCmd(string tableName,DataRow dr)
        {

            string tmpCmdString = null;

            tmpCmdString = "INSERT INTO [" + tableName + "](";

            foreach (DataColumn col in dr.Table.Columns)
            {
                tmpCmdString = tmpCmdString + col.ColumnName + ", ";
            }

            //trim extra comma
            tmpCmdString = tmpCmdString.TrimEnd(' ').TrimEnd(',');
            tmpCmdString = tmpCmdString + ") VALUES(";


            foreach (object itm in dr.ItemArray)
            {
                switch (itm.GetType().ToString())
                {
                    case "System.Integer":
                        tmpCmdString = tmpCmdString + itm.ToString() + ", ";
                        break;
                    case "System.Double":
                        tmpCmdString = tmpCmdString + itm.ToString() + ", ";
                        break;
                    case "System.Int32":
                        tmpCmdString = tmpCmdString + itm.ToString() + ", ";
                        break;
                    case "System.Int16":
                        tmpCmdString = tmpCmdString + itm.ToString() + ", ";
                        break;
                    case "System.Int64":
                        tmpCmdString = tmpCmdString + itm.ToString() + ", ";
                        break;
                    case "System.String":
                        tmpCmdString = tmpCmdString + "'" + itm.ToString().Replace("'", "''") + "', ";
                        break;
                    case "System.DateTime":
                        tmpCmdString = tmpCmdString + "'" + itm.ToString().Replace("'", "''") + "', ";
                        break;
                    case "System.Boolean":
                        if (itm.ToString() == "True")
                        {
                            tmpCmdString = tmpCmdString + "1, ";
                        }
                        else
                        {
                            tmpCmdString = tmpCmdString + "0, ";
                        }
                        break;
                    default:
                        tmpCmdString = tmpCmdString + "NULL, ";
                        break;
                }
            }

            //trim extra comma
            tmpCmdString = tmpCmdString.TrimEnd(' ').TrimEnd(',');

            tmpCmdString = tmpCmdString + ")";

            return tmpCmdString;

        }
        
                /// <summary>
        /// Get parameterized SQL Server ADO.Net Insert Command object for an ADO.Net DataRow object.
        /// </summary>
        /// <param name="tableName">Target table name</param>
        /// <param name="dr">ADO.Net DataRow</param>
        /// <returns></returns>
        public static SqlCommand getSQLDataAdapterInsertCmd(string tableName, DataRow dr)
        {

            string tmpCmdString = string.Empty;
            
            SqlCommand cmd = new SqlCommand();

            List<SqlParameter> queryParams = new List<SqlParameter>();

            tmpCmdString = "INSERT INTO " + tableName + "(";

            foreach (DataColumn col in dr.Table.Columns)
            {
                if (col.ColumnName != "UNIQUE_ID") tmpCmdString = tmpCmdString + "[" + col.ColumnName + "], ";
            }

            //trim extra comma
            tmpCmdString = tmpCmdString.TrimEnd(' ').TrimEnd(',');
            tmpCmdString = tmpCmdString + ") VALUES(";

            foreach (DataColumn col in dr.Table.Columns)
            {
                if (col.ColumnName != "UNIQUE_ID")
                {
                    tmpCmdString = tmpCmdString + "@" + col.ColumnName + ",";
                    switch (col.DataType.ToString())
                    {
                        case "System.Integer":
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.Int));
                            break;
                        case "System.Double":
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.Float));
                            break;
                        case "System.Int32":
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.Int));
                            break;
                        case "System.Int16":
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.SmallInt));
                            break;
                        case "System.Int64":
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.BigInt));
                            break;
                        case "System.String":
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.NVarChar));
                            break;
                        case "System.DateTime":
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.DateTime));
                            break;
                        case "System.Boolean":
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.Bit));
                            break;
                        default:
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.NVarChar));
                            break;
                    }
                }
            }

            //trim extra comma
            tmpCmdString = tmpCmdString.TrimEnd(' ').TrimEnd(',');

            tmpCmdString = tmpCmdString + ")";

            cmd.Parameters.AddRange(queryParams.ToArray());
            cmd.CommandText = tmpCmdString;

            return cmd;
        }
        
        /// <summary>
        /// Get parameterized SQL Server ADO.Net Update Command object for an ADO.Net DataRow object.
        /// </summary>
        /// <param name="tableName">Target table name</param>
        /// <param name="dr">ADO.Net DataRow</param>
        /// <returns></returns>
        public static SqlCommand getSQLDataAdapterUpdateCmd(string tableName, DataRow dr)
        {

            string tmpCmdString = string.Empty;

            SqlCommand cmd = new SqlCommand();

            List<SqlParameter> queryParams = new List<SqlParameter>();

            tmpCmdString = "UPDATE " + tableName + " SET ";

            foreach (DataColumn col in dr.Table.Columns)
            {
                if (col.ColumnName != "UNIQUE_ID") tmpCmdString = tmpCmdString + "[" + col.ColumnName + "] = @" + col.ColumnName + ", ";
            }

            //trim extra comma
            tmpCmdString = tmpCmdString.TrimEnd(' ').TrimEnd(',');
            tmpCmdString = tmpCmdString + " WHERE UNIQUE_ID = @UNIQUE_ID";
            queryParams.Add(new SqlParameter("@UNIQUE_ID", SqlDbType.Int));

            foreach (DataColumn col in dr.Table.Columns)
            {
                if (col.ColumnName != "UNIQUE_ID")
                {
                    switch (col.DataType.ToString())
                    {
                        case "System.Integer":
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.SmallInt));
                            break;
                        case "System.Double":
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.Float));
                            break;
                        case "System.Int32":
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.Int));
                            break;
                        case "System.Int16":
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.SmallInt));
                            break;
                        case "System.Int64":
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.BigInt));
                            break;
                        case "System.String":
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.NVarChar));
                            break;
                        case "System.DateTime":
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.DateTime));
                            break;
                        case "System.Boolean":
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.NVarChar));
                            break;
                        default:
                            queryParams.Add(new SqlParameter("@" + col.ColumnName, SqlDbType.NVarChar));
                            break;
                    }
                }
            }

            cmd.Parameters.AddRange(queryParams.ToArray());
            cmd.CommandText = tmpCmdString;

            return cmd;
        }

        #endregion

    #region Firebird

        private bool DoesTableExistFirebird()
        {
//            bool functionReturnValue = false;
//
//            functionReturnValue = false;
//
//            WriteFirebirdConfig(Path.GetDirectoryName(FileName));
//
//            try
//            {
//                string connString = null;
//                if (FilePW == null)
//                {
//                    connString = "User=SYSDBA;Password=masterkey;Database=" + FileName + ";DataSource=localhost;" +
//                                                                        "Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;" +
//                                                                        "MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=1;";
//                }
//                else
//                {
//                    connString = "User=SYSDBA;Password=masterkey;Database=" + FileName + ";DataSource=localhost;" +
//                                                                        "Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;" +
//                                                                        "MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=1;";
//                }
//                FbConnection sqliteConnect = new FbConnection(connString);
//                FbDataAdapter sqliteDA = new FbDataAdapter("SELECT RDB$RELATION_NAME from RDB$RELATIONS where RDB$FLAGS = 1 and RDB$RELATION_NAME = '" + xferTableName + "'", sqliteConnect);
//                DataTable tblListTable = new DataTable();
//                tblListTable.TableName = "TableList";
//                tblListTable.Clear();
//                sqliteDA.Fill(tblListTable);
//
//                if (tblListTable.Rows.Count > 0)
//                    functionReturnValue = true;

//            }
//            catch (Exception ex)
//            {
//            	TransferDataWorker.ReportProgress(0, ex.Message);
//            }
//            return functionReturnValue;
return false;

        }

        private void WriteFirebirdConfig(string BaseFilePath)
        {
            //File.WriteAllText(Path.GetDirectoryName(AppContext.BaseDirectory) + "\\firebird.conf", "Root Directory = " + BaseFilePath);       
        }

        private void CreateFirebirdDataFile(string FileName)
        {
//            FbConnectionStringBuilder builder = new FbConnectionStringBuilder();
//            builder.DataSource = "localhost";
//            builder.UserID = "SYSDBA";
//            builder.Password = "masterkey";
//            builder.Database = @FileName;
//            builder.ServerType = FbServerType.Default;
//
//            FbConnection.CreateDatabase(builder.ConnectionString);
        }

        private void CopyToFirebird(bool tableExists, string target)
        {
//
//            this.TransferDataWorker.ReportProgress(0, "Saving to Firebird");
//
//            int rowCtr = 0;
//
//            try
//            {
//                string connString = "User=SYSDBA;Password=masterkey;Database=" + FileName + ";DataSource=localhost;" +
//                                                                        "Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;" +
//                                                                        "MinPoolSize=0;MaxPoolSize=100;Packet Size=8192;ServerType=1;";
//                FbConnection sqlConnection = new FbConnection(connString);
//                string addTblCommand = createMakeFirebirdTableCmd(dt);
//                FbCommand firebirdCommand = new FbCommand(addTblCommand, sqlConnection);
//                FbCommand firebirdCommand2 = new FbCommand("", sqlConnection);
//                sqlConnection.Open();
//                if (!DoesTableExistFirebird())
//                    firebirdCommand.ExecuteNonQuery();
//                FbTransaction bulkInsert = sqlConnection.BeginTransaction();
//                firebirdCommand2.Transaction = bulkInsert;
//                try
//                {
//                    foreach (DataRow dtRow in dt.Rows)
//                    {
//                        firebirdCommand2.CommandText = createInsertFirebirdDataCmd(xferTableName, dtRow);
//                        firebirdCommand2.ExecuteNonQuery();
//                        double rc = rowCtr;
//                        double rt = dt.Rows.Count;
//                        double pDone = (rc / rt) * 100;
//                        int percentDone = Convert.ToInt32(pDone);
//                        this.TransferDataWorker.ReportProgress(percentDone, "Saving to Firebird: " + xferTableName);
//                        rowCtr += 1;
//                        //handle transactions 9999 records at a time
//                        if (rowCtr % 999 == 0.0)
//                        {
//                            bulkInsert.Commit();
//                            bulkInsert = sqlConnection.BeginTransaction();
//                            firebirdCommand2.Transaction = bulkInsert;
//                        }
//                    }
//                    bulkInsert.Commit();
//                }
//                catch (Exception ex)
//                {
//                	TransferDataWorker.ReportProgress(0, ex.Message);
//                    bulkInsert.Rollback();
//                    throw;
//                }
//                sqlConnection.Close();
//            }
//            catch (Exception ex)
//            {
//                this.TransferDataWorker.ReportProgress(0, "Error Saving to Firebird: " + ex.Message);
//            }

        }

        /// <summary>
        /// Get FBSql statement to create a table based off of an ADO.Net DataTable object.
        /// </summary>
        /// <param name="dt">ADO.Net DataTable object</param>
        /// <returns>FBSql statement as string</returns>
        public string createMakeFirebirdTableCmd(DataTable dt)
        {

            string tmpCmdString = null;

            tmpCmdString = "CREATE TABLE \"" + xferTableName + "\" (";

            foreach (DataColumn clm in dt.Columns)
            {
                tmpCmdString = tmpCmdString + "\"" + clm.ColumnName.ToString() + "\" ";
                switch (clm.DataType.ToString())
                {
                    case "System.Integer":
                        tmpCmdString = tmpCmdString + "decimal, ";
                        break;
                    case "System.Int32":
                        tmpCmdString = tmpCmdString + "decimal, ";
                        break;
                    case "System.Double":
                        tmpCmdString = tmpCmdString + "decimal, ";
                        break;
                    case "System.Int16":
                        tmpCmdString = tmpCmdString + "integer, ";
                        break;
                    case "System.Int64":
                        tmpCmdString = tmpCmdString + "decimal, ";
                        break;
                    case "System.String":
                        tmpCmdString = tmpCmdString + "varchar(255), ";
                        break;
                    case "System.DateTime":
                        tmpCmdString = tmpCmdString + "timestamp, ";
                        break;
                    case "System.Boolean":
                        tmpCmdString = tmpCmdString + "char, ";
                        break;
                    default:
                        tmpCmdString = tmpCmdString + "varchar(255), ";
                        break;
                }
            }

            //trim extra comma
            tmpCmdString = tmpCmdString.TrimEnd(' ').TrimEnd(',');

            tmpCmdString = tmpCmdString + ")";

            return tmpCmdString;

        }

        /// <summary>
        /// Get FBSql statement to insert a record.
        /// </summary>
        /// <param name="tableName">Target table name</param>
        /// <param name="dr">ADO.Net DataRow object</param>
        /// <returns>FBSql statement as string</returns>
        public string createInsertFirebirdDataCmd(string tableName, DataRow dr)
        {

            string tmpCmdString = null;

            tmpCmdString = "INSERT INTO \"" + tableName + "\"(";

            foreach (DataColumn col in dr.Table.Columns)
            {
                tmpCmdString = tmpCmdString + "\"" + col.ColumnName + "\", ";
            }

            //trim extra comma
            tmpCmdString = tmpCmdString.TrimEnd(' ').TrimEnd(',');
            tmpCmdString = tmpCmdString + ") VALUES(";

            foreach (object itm in dr.ItemArray)
            {
                switch (itm.GetType().ToString())
                {
                    case "System.Integer":
                        tmpCmdString = tmpCmdString + itm.ToString() + ", ";
                        break;
                    case "System.Double":
                        tmpCmdString = tmpCmdString + itm.ToString() + ", ";
                        break;
                    case "System.Int32":
                        tmpCmdString = tmpCmdString + itm.ToString() + ", ";
                        break;
                    case "System.Int16":
                        tmpCmdString = tmpCmdString + itm.ToString() + ", ";
                        break;
                    case "System.Int64":
                        tmpCmdString = tmpCmdString + itm.ToString() + ", ";
                        break;
                    case "System.String":
                        tmpCmdString = tmpCmdString + "'" + itm.ToString().Replace("'", "''").Replace("\"", "\"\"") + "', ";
                        break;
                    case "System.DateTime":
                        tmpCmdString = tmpCmdString + "'" + String.Format("{0:M/d/yyyy HH:mm:ss}", itm) + "', ";
                        break;
                    case "System.Boolean":
                        if (itm.ToString() == "True")
                        {
                            tmpCmdString = tmpCmdString + "1, ";
                        }
                        else
                        {
                            tmpCmdString = tmpCmdString + "0, ";
                        }
                        break;
                    default:
                        tmpCmdString = tmpCmdString + "NULL, ";
                        break;
                }
            }

            //trim extra comma
            tmpCmdString = tmpCmdString.TrimEnd(' ').TrimEnd(',');

            tmpCmdString = tmpCmdString + ")";

            return tmpCmdString;

        }



#endregion

    #region Excel

        /// <summary>
        /// Gets an ADO.Net DataTable object from an Excel file
        /// </summary>
        /// <param name="fileName">File path</param>
        /// <returns>ADO.Net DataTable object</returns>
    	public DataTable getDataSetFromExcelXML(string fileName)
        {
            DataTable dt = new DataTable();
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(fileName);
            }
            catch
            {
                return dt;
            }

            dt.TableName = "Sheet1";

            XmlNodeList workbook = xmlDoc.GetElementsByTagName("ss:Row");

            bool headerRow = true;

            try
            {
                foreach (XmlNode cell in workbook)
                {
                    if (headerRow)
                    {
                        foreach (XmlNode data in cell.ChildNodes)
                        {
                            dt.Columns.Add(data.FirstChild.InnerText, typeof(string));
                        }
                        headerRow = false;
                    }
                    else
                    {
                        DataRow newRow = dt.NewRow();
                        int rowIndex = 0;
                        foreach (XmlNode data in cell.ChildNodes)
                        {
                            newRow[rowIndex] = data.FirstChild.InnerText;
                            rowIndex++;
                        }
                        dt.Rows.Add(newRow);
                    }
                }
            }
            catch (Exception ex)
            {
                dt.TableName = "Error";
                dt.Columns.Add("ConversionError");
                DataRow errRow = dt.NewRow();
                errRow["ConversionError"] = ex.Message;
                dt.Rows.Add(errRow);
            }
            
            return dt;
        }

    #endregion

    #region DataTable2DataTable

        
    /// <summary>
    /// Copies data from one ADO.Net DataTable to another.
    /// </summary>
    /// <param name="tableSource">Source DataTable</param>
    /// <param name="tableTarget">Target DataTable</param>
    /// <param name="CopyStructure">Attempt to copy table structure</param>
    /// <returns></returns>
    public static string moveDataTableToDataTable(ref DataTable tableSource, ref DataTable tableTarget, bool CopyStructure = false)
        {
            string errMessage = string.Empty;

            if (CopyStructure)
            {
                tableTarget.Rows.Clear();
                tableTarget.Columns.Clear();
                foreach (DataColumn sourceColumn in tableSource.Columns)
                {
                    tableTarget.Columns.Add(new DataColumn(sourceColumn.ColumnName, sourceColumn.DataType));
                }
            }
            
            foreach (DataRow sourceRow in tableSource.Rows)
            {
                DataRow newDataRow = tableTarget.NewRow();

                for (int i = 0; i < sourceRow.ItemArray.Count(); i++)
                {
                    if (tableTarget.Columns.Contains(tableSource.Columns[i].ColumnName))
                    {
                        string targetColumnType = tableTarget.Columns[tableSource.Columns[i].ColumnName].DataType.ToString();
                        string changeValue = sourceRow.ItemArray[i].ToString();
                        switch (targetColumnType)
                        {
                            case "System.Double":
                                if (!String.IsNullOrEmpty(changeValue))
                                {
                                    newDataRow[tableSource.Columns[i].ColumnName] = Double.Parse(changeValue);
                                }
                                else
                                {
                                    newDataRow[tableSource.Columns[i].ColumnName] = 0;
                                }
                                break;
                            case "System.Int32":
                                if (!String.IsNullOrEmpty(changeValue))
                                {
                                    newDataRow[tableSource.Columns[i].ColumnName] = Int32.Parse(changeValue);
                                }
                                break;
                            case "System.Int64":
                                if (!String.IsNullOrEmpty(changeValue))
                                {
                                    newDataRow[tableSource.Columns[i].ColumnName] = Int64.Parse(changeValue);
                                }
                                break;
                            case "System.DateTime":
                                if (!String.IsNullOrEmpty(changeValue))
                                {
                                    newDataRow[tableSource.Columns[i].ColumnName] = DateTime.Parse(changeValue);
                                }
                                break;
                            case "Nullable.DateTime":
                                if (!String.IsNullOrEmpty(changeValue))
                                {
                                    newDataRow[tableSource.Columns[i].ColumnName] = DateTime.Parse(changeValue);
                                }
                                else
                                {
                                    newDataRow[tableSource.Columns[i].ColumnName] = DBNull.Value;
                                }
                                break;
                            case "System.Boolean":
                                if (!String.IsNullOrEmpty(changeValue))
                                {
                                    if (changeValue == "0" || changeValue == "1")
                                    {
                                        if (changeValue == "0")
                                            newDataRow[tableSource.Columns[i].ColumnName] = false;
                                        else
                                            newDataRow[tableSource.Columns[i].ColumnName] = true;
                                    }
                                    else
                                    {
                                        newDataRow[tableSource.Columns[i].ColumnName] = bool.Parse(changeValue);
                                    }
                                }
                                break;
                            case "System.String":
                                    newDataRow[tableSource.Columns[i].ColumnName] = changeValue;
                                break;
                            default:
                                errMessage = "Cannot load value of '" + changeValue + "' into Property for '" + tableSource.Columns[i].ColumnName + "'. The Type of '" + targetColumnType + "' is not supported.";
                                break;
                        }
                    }
                }
                tableTarget.Rows.Add(newDataRow);
            }

            return errMessage;
        }

    #endregion

    #region XML
    
    /// <summary>
    /// Save Object to XML File
    /// </summary>
    /// <param name="value">Object to serialize</param>
    /// <param name="FileName">Destination file path</param>
    /// <returns></returns>
    public static string SaveObjectAsXMLFile<T>(T value, string FileName)
        {
            try
            {
                var ser = new XmlSerializer(typeof(T));
                using (var stream = new FileStream(FileName, FileMode.Create))
                    ser.Serialize(stream, value);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return string.Empty;
        }

    	/// <summary>
    	/// Load object from XML file.
    	/// </summary>
    	/// <param name="FileName">Source file path</param>
    	/// <returns>Deserialized Object</returns>
        public static object LoadObjectFromXMLFile<T>(string FileName)
        {
            try
            {
                var ser = new XmlSerializer(typeof(T));
                using (var stream = new FileStream(FileName, FileMode.Open))
                    return ser.Deserialize(stream);
            }
            catch
            {
                return null;
            }
        }

    #endregion

    #region Object2DataTable
    
    	/// <summary>
    	/// Gets an ADO.Net DataTable based off of a generic List of Object.
    	/// </summary>
    	/// <param name="objectList">List of objects</param>
    	/// <param name="tableName">DataTable name</param>
    	/// <returns>ADO.Net DataTable</returns>
        public static DataTable GetObjectListAsTable<T>(List<T> objectList, string tableName)
        {
            DataTable dt = new DataTable();
            dt.TableName = tableName;

            if (objectList == null) return dt;

            List<PropertyInfo> propList;

            if (objectList.Count > 0)
            {
                propList = objectList[0].GetType().GetProperties().ToList();
            }
            else
            {
                return dt;
            }

            foreach (PropertyInfo propinfo in propList)
            {
                if (propinfo.PropertyType.ToString().Contains("Nullable"))
                {
                    if (propinfo.PropertyType.ToString().Contains("DateTime")) dt.Columns.Add(propinfo.Name, typeof(System.Data.SqlTypes.SqlDateTime));
                    if (propinfo.PropertyType.ToString().Contains("String")) dt.Columns.Add(propinfo.Name, typeof(System.Data.SqlTypes.SqlString));
                    if (propinfo.PropertyType.ToString().Contains("Int")) dt.Columns.Add(propinfo.Name, typeof(System.Data.SqlTypes.SqlInt32));
                    if (propinfo.PropertyType.ToString().Contains("Double")) dt.Columns.Add(propinfo.Name, typeof(System.Data.SqlTypes.SqlDouble));
                    if (propinfo.PropertyType.ToString().Contains("Boolean")) dt.Columns.Add(propinfo.Name, typeof(System.Data.SqlTypes.SqlBoolean));
                }
                else
                {
                    if (propinfo.Name != "Item") dt.Columns.Add(propinfo.Name, propinfo.PropertyType);
                }
            }


            foreach (T o in objectList)
            {
                DataRow dr = dt.NewRow(); 
                List<PropertyInfo> prList = o.GetType().GetProperties().ToList();
                foreach (PropertyInfo pi in prList.Where(c => c.GetType().Name.ToString() != "Item"))
                {
	                if (o.GetType().GetProperty(pi.Name).GetValue(o, null) == null)
                    {
                        dr[pi.Name] = DBNull.Value;
                    }
                    else
                    {
                        dr[pi.Name] = o.GetType().GetProperty(pi.Name).GetValue(o, null);
                    }
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static DataTable GetObjectAsPropertyTable<T>(T convertObject, string objectName)
        {
            DataTable dt = new DataTable();
            dt.TableName = objectName;

            if (convertObject == null) return dt;

            List<PropertyInfo> propList;

            propList = convertObject.GetType().GetProperties().ToList();

            dt.Columns.Add("Property", typeof(string));
            dt.Columns.Add("Value", typeof(string));

            foreach (PropertyInfo pi in propList)
            {
                if (!pi.PropertyType.ToString().Contains("List"))
                {
                    DataRow dr = dt.NewRow();
                    dr["Property"] = pi.Name;
                    if (convertObject.GetType().GetProperty(pi.Name).GetValue(convertObject, null) == null)
                    {
                        dr["Value"] = DBNull.Value;
                    }
                    else
                    {
                        dr["Value"] = convertObject.GetType().GetProperty(pi.Name).GetValue(convertObject, null);
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }
        
    }
    #endregion
}