using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.SqlServerCe;


namespace DataMovement
{
    /// <summary>
    /// Stores/retrieves database connections.  The stored connections are saved to (UserDataFolder)\SQLRunner2\savedconnections.xml
    /// </summary>
    public class DBConnectionManager
    {
        public List<DBConnection> ConnectionList { get; set; }
        public string SavedConnectionsFileLocation { get; set; }
        public List<string> DBTypes;
        public List<string> TargetTypes;
        public string ApplicationName { get; set; }

        /// <summary>
        /// Default constructor creates storage folder and .xml saved connections file.
        /// </summary>
        public DBConnectionManager(string applicationName)
        {

            ApplicationName = applicationName;

            string AppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + ApplicationName;

            if (!Directory.Exists(AppDataDir))
            {
                try
                {
                    Directory.CreateDirectory(AppDataDir);
                }
                catch (Exception ex)
                {
                    Utils.Logging.LogError(ex.Message, ex.Source, ex.StackTrace, ApplicationName);
                }
            }

            SavedConnectionsFileLocation = AppDataDir + "\\savedconnections.xml";

            ConnectionList = new List<DBConnection>();

            DBTypes = new List<string>();
            DBTypes.Add("SQL Server");
            DBTypes.Add("SQL Compact");
            DBTypes.Add("SQLite");
            DBTypes.Add("Access MDB");
            DBTypes.Add("Access ACCDB");
            DBTypes.Add("PostgreSQL");
            DBTypes.Add("MySQL");

        }

        /// <summary>
        /// get a DBConnection object by saved connection name.
        /// </summary>
        /// <param name="connectionName">Saved connection name</param>
        /// <returns>DBConnection object</returns>
        public DBConnection getConnectionByName(string connectionName)
        {
            return ConnectionList.Where(c => c.ConnectionName == connectionName).FirstOrDefault();
        }

        /// <summary>
        /// get a DBConnection object by connection ID (Guid).
        /// </summary>
        /// <param name="connectionID">Connection ID (Guid)</param>
        /// <returns>DBConnection object</returns>
        public DBConnection getConnectionByID(string connectionID)
        {
            return ConnectionList.Where(c => c.ConnectionID == connectionID).FirstOrDefault();
        }

        /// <summary>
        /// Returns the currently active DB connection.
        /// </summary>
        /// <returns></returns>
        public DBConnection getCurrentConnection()
        {
            return ConnectionList.Where(c => c.InUse == true).FirstOrDefault();
        }

        /// <summary>
        /// Get connection string by stored connection name.
        /// </summary>
        /// <param name="connectionName">Stored connection name</param>
        /// <returns>Database connection string</returns>
        public string getConnectionStringByName(string connectionName)
        {
            DBConnection currentConnection = ConnectionList.Where(c => c.ConnectionName == connectionName).FirstOrDefault();

            if (currentConnection == null) return string.Empty;

            return currentConnection.getConnectionString();
        }


        /// <summary>
        /// Get connection string by connectionID.
        /// </summary>
        /// <param name="connectionID">ConnectionID (Guid)</param>
        /// <returns>Database connection string</returns>
        public string getConnectionStringByID(string connectionID)
        {
            DBConnection currentConnection = ConnectionList.Where(c => c.ConnectionID == connectionID).FirstOrDefault();

            if (currentConnection == null) return string.Empty;

            return currentConnection.getConnectionString();
        }

        /// <summary>
        /// Sets the currently active connection by stored connection name
        /// </summary>
        /// <param name="connectionName">Stored connection name</param>
        public void setCurrentConnectionByName(string connectionName)
        {
            foreach (DBConnection dbc in ConnectionList)
            {
                if (dbc.ConnectionName == connectionName)
                {
                    dbc.InUse = true;
                }
                else
                {
                    dbc.InUse = false;
                }
            }
        }

        /// <summary>
        /// Sets the currently active connection by connection ID
        /// </summary>
        /// <param name="connectionID">ConnectionID (Guid)</param>
        public void setCurrentConnectionByID(string connectionID)
        {
            foreach (DBConnection dbc in ConnectionList)
            {
                if (dbc.ConnectionID == connectionID)
                {
                    dbc.InUse = true;
                }
                else
                {
                    dbc.InUse = false;
                }
            }
        }

        /// <summary>
        /// Saves stored connections to users appdata\roaming\SQLRunner2\storedconnections.xml
        /// </summary>
        /// <returns></returns>
        public string saveConnections()
        {
            return DataSetSQLExport.SaveObjectAsXMLFile<List<DBConnection>>(ConnectionList, SavedConnectionsFileLocation);
        }

        /// <summary>
        /// Loads stored connections from users appdata\roaming\SQLRunner2\storedconnections.xml
        /// </summary>
        /// <returns></returns>
        public bool loadConnections()
        {

            if (!File.Exists(SavedConnectionsFileLocation)) return false;

            try
            {
                object import = DataSetSQLExport.LoadObjectFromXMLFile<List<DBConnection>>(SavedConnectionsFileLocation);
                if (import != null)
                {
                    ConnectionList = (List<DBConnection>)import;
                }
            }
            catch (Exception ex)
            {
                Utils.Logging.LogError(ex.Message, ex.Source, ex.StackTrace, ApplicationName);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Demonstrates how to upgrade a database with case sensitivity.
        /// </summary>
        public static void UpgradeDatabasewithCaseSensitive(string fileName)
        {
            // <Snippet2>
            // Default case-insentive connection string.
            // Note that Northwind.sdf is an old 3.1 version database.

            string connStringCI = "Data Source= " + fileName + "; LCID= 1033";

            // Set "Case Sensitive" to true to change the collation from CI to CS.
            string connStringCS = "Data Source= " + fileName + "; LCID= 1033; Case Sensitive=true";

            SqlCeEngine engine = new SqlCeEngine(connStringCI);

            // The collation of the database will be case sensitive because of 
            // the new connection string used by the Upgrade method.                
            engine.Upgrade(connStringCS);

            SqlCeConnection conn = null;
            conn = new SqlCeConnection(connStringCI);
            conn.Open();

            //Retrieve the connection string information - notice the 'Case Sensitive' value.
            List<KeyValuePair<string, string>> dbinfo = conn.GetDatabaseInfo();

            Console.WriteLine("\nGetDatabaseInfo() results:");

            foreach (KeyValuePair<string, string> kvp in dbinfo)
            {
                Console.WriteLine(kvp);
            }
            // </Snippet2>
        }

    }

    /// <summary>
    /// Storage class for database connection parameters
    /// </summary>
    public class DBConnection
    {
    	public string ConnectionID { get; set; }
    	public string ConnectionName { get; set; }
        public string ConnectionType { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string FileLocation { get; set; }
        public bool UseTrustedConnection { get; set; }
        public bool InUse { get; set; }
        public DateTime LastConnection { get; set; }
        public string LastUser { get; set; }
        public string FilePassword {get; set;}
        
        public DBConnection()
        {
        	ConnectionID = Guid.NewGuid().ToString();
        	ConnectionName = "New Connection";
        	ConnectionType = "SQL Server";
        	Server = "";
        	Database = "";
        	Port = "";
        	UserName = "";
        	UserPassword = "";
        	FileLocation = "";
        	UseTrustedConnection = false;
        	InUse = false;
        	LastConnection = DateTime.MinValue;
        	LastUser = "";
        	FilePassword = "";
        }

        /// <summary>
        /// Get ADO.Net connection for current DBConnection
        /// </summary>
        /// <returns>ADO.Net database connection</returns>
        public IDbConnection getDBConnection()
        {
            IDbConnection dbConn;
            
            switch (ConnectionType)
            {
               
                case "SQL Server":
                    dbConn = new SqlConnection(getConnectionString());
                    break;
                case "SQLite":
                    dbConn = new SQLiteConnection(getConnectionString());
                    break;
//                case "Firebird":
//                    dbConn = new FbConnection(getConnectionString());
//                    dbConn = null;
//                    break;
                case "Access MDB":
                    dbConn = new OleDbConnection(getConnectionString());
                    break;
                case "SQL Compact":
                    dbConn = new SqlCeConnection(getConnectionString());
                    break;
                default:
                    dbConn = new SqlConnection(getConnectionString());
                    break;
            }

            return dbConn;
        }

        /// <summary>
        /// Gets a database adapter for the current connection
        /// </summary>
        /// <param name="SQL">The SQL select command to retrieve</param>
        /// <returns>An ADO.Net DataAdapter</returns>
        public IDataAdapter getDBAdapter(string SQL)
        {
            IDataAdapter dbAdapt;
            
            switch (ConnectionType)
            {
                case "SQL Server":
                    dbAdapt = new SqlDataAdapter(SQL, getConnectionString());
                    break;
                case "SQLite":
                    dbAdapt = new SQLiteDataAdapter(SQL, getConnectionString());
                    break;
//                case "Firebird":
//                    dbAdapt = new FbDataAdapter(SQL, getConnectionString());
//                    break;
                case "Access MDB":
                    dbAdapt = new OleDbDataAdapter(SQL, getConnectionString());
                    break;
                case "SQL Compact":
                    dbAdapt = new SqlCeDataAdapter(SQL, getConnectionString());
                    break ;
                default:
                    dbAdapt = new SqlDataAdapter(SQL, getConnectionString());
                    break;
            }

            return dbAdapt;
        }

        /// <summary>
        /// Get a connection string for the current database connection
        /// </summary>
        /// <returns>Connection String</returns>
        public string getConnectionString()
        {
            string ConnectionString = string.Empty;

            switch (ConnectionType)
            {
                case "SQL Server":
                    if (UseTrustedConnection)
                    {
                        ConnectionString = "Data Source=" + Server + ";Initial Catalog=" + Database + ";Trusted_Connection=true";
                    }
                    else
                    {
                        ConnectionString = "Data Source=" + Server + "; Initial Catalog=" + Database + "; User Id=" + UserName + "; Password=" + UserPassword + ";";
                    }
                    break;
                case "SQLite":
                    ConnectionString = "Data Source=" + FileLocation + ";Version=3;DateTimeFormat=CurrentCulture;";
                    break;
                case "Firebird":
                    ConnectionString = "User=SYSDBA;Password=masterkey;Database=" + FileLocation + ";DataSource=localhost;" +
                                                    "Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;" +
                    								"MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=1;";
                    break;
                case "Access MDB":
                    ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                    	"Data Source=" + FileLocation + ";";// +
                    	//"Persist Security Info=False;Jet OLEDB:System Database=" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Access\\system.mdw;";
                    break;
                case "SQL Compact":
                    if (string.IsNullOrEmpty(FilePassword))
                    {
                    	ConnectionString = "Data Source =" + this.FileLocation + ";Persist Security Info=False;";
                    }
                    else
                    {
                    	ConnectionString = "Data Source =" + this.FileLocation + "; Password =" + FilePassword + ";";
                    }
                    break;
                default:
                    break;
            }

            return ConnectionString;
        }
        
        /// <summary>
        /// Gets a string list of tables from the current connection
        /// </summary>
        /// <returns>List of strings.</returns>
        public List<string> getTablesList()
        {
        	List<string> tList = new List<string>();
        	string tListSQL = string.Empty;
        	DataSet dt = new DataSet();
        	
        	try
        	{
	        	
	        	switch (ConnectionType)
	            {
	                case "SQL Server":
	        			tListSQL = "SELECT TABLE_NAME AS name FROM information_schema.TABLES ORDER BY name";
	        		    SqlConnection conn = new SqlConnection(getConnectionString());
	                    SqlDataAdapter da = new SqlDataAdapter(tListSQL, conn);
	                    da.Fill(dt);
	                    conn.Close();
	                    conn.Dispose();
	        	        break;
	                case "SQLite":
	                    tListSQL = "Select CAST(name as TEXT) as Name from sqlite_master WHERE type='table' or type='view' order by Name;";
	                    SQLiteConnection sqliConn = new SQLiteConnection(getConnectionString());
	                    SQLiteDataAdapter sqliDa = new SQLiteDataAdapter(tListSQL, sqliConn);
	                    sqliDa.Fill(dt);
	                    sqliConn.Close();
	                    sqliConn.Dispose();
	                    break;
	                case "Firebird":
	                    tListSQL = "select rdb$relation_name from rdb$relations where rdb$view_blr is null and (rdb$system_flag is null or rdb$system_flag = 0);";
	                    break;
	                case "Access MDB":
	                    tListSQL = "SELECT name FROM MSysObjects WHERE Type=1 AND Flags=0";
	                    OleDbConnection tmpConnect = new OleDbConnection(getConnectionString() + "Jet OLEDB:System Database=" + Utils.Globals.getWorkingFolder() + "\\Microsoft\\Access\\system.mdw;" );
	                    tmpConnect.Open();
	                    OleDbCommand tempCmd = new OleDbCommand("GRANT SELECT ON MSysObjects TO Admin;", tmpConnect);
	                    tempCmd.ExecuteNonQuery();
			                    
		        		IDataAdapter daMDB = getDBAdapter(tListSQL);
		        		daMDB.Fill(dt);
	                    
	                    tmpConnect.Close();
	                    break;
	                 case "SQL Compact":
	                   	tListSQL = "SELECT TABLE_NAME AS name FROM information_schema.TABLES ORDER BY name";
	        		    SqlCeConnection SQLCEconn = new SqlCeConnection(getConnectionString());
	                    SqlCeDataAdapter SQLCEda = new SqlCeDataAdapter(tListSQL, SQLCEconn);
	                    SQLCEda.Fill(dt);
	                    SQLCEconn.Close();
	                    SQLCEconn.Dispose();
	        	        break;
	                default:
	                    break;
	            }
	       		        	
	        	if (dt.Tables != null)
	        	{
	        		foreach( DataRow dr in dt.Tables[0].Rows)
	        		{
	        			tList.Add(dr[0].ToString());
	        		}
	        	}
	        	
        	}
        	catch (Exception ex)
        	{
        		Utils.Logging.LogError(ex.Message, ex.Source, ex.StackTrace, "SQLRunner2");
        		throw;
        	}
        	
        	return tList;
        }

        
            /// <summary>
            /// Return db command object for given sql
            /// </summary>
            /// <param name="SQL">SQL to use for command</param>
            /// <returns>IDbCommand</returns>
            public IDbCommand getDbCommand(string SQL)
        {
            switch (ConnectionType)
            {
                case "SQLite":
                    SQLiteConnection sqliConn = new SQLiteConnection(getConnectionString());
                    SQLiteCommand sqlCmd = new SQLiteCommand(SQL, sqliConn);
                    return sqlCmd;
                    break;
                default:
                    return new SqlCommand();
                    break;
            }
        }
    
    }
    
}
