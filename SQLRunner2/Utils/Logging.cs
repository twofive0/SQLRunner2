/*
 * Created by SharpDevelop.
 * User: devin
 * Date: 12/10/2015
 * Time: 09:44
 */
using System;
using System.IO;

namespace Utils
{
	/// <summary>
	/// Error logging utilities.  Writes error information to appdata\roaming\appName as txt file.
	/// </summary>
	public static class Logging
	{
	
		/// <summary>
		/// Get error log path plus filename for application.
		/// </summary>
		/// <param name="applicationName">Application Name</param>
		/// <returns>Error Log Path as String</returns>
		public static string getErrorLogPath(string applicationName)
        {
        	string bFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        	
        	if (!System.IO.Directory.Exists(bFolder + Path.DirectorySeparatorChar + applicationName))
    	    {
        		System.IO.Directory.CreateDirectory(bFolder + Path.DirectorySeparatorChar + applicationName);
    	    }
        	
            return bFolder + Path.DirectorySeparatorChar + applicationName + Path.DirectorySeparatorChar + "errorlog.txt";
        }
		
        /// <summary>
        /// Write an error to the error log file with specified debug info.
        /// </summary>
        /// <param name="msg">Error message as string</param>
        /// <param name="source">Code source as string</param>
        /// <param name="stackTrace">Stack trace as string</param>
        /// <param name="applicationName">Current application name as string</param>
		public static void LogError(string msg, string source, string stackTrace, string applicationName)
        {

        	truncateErrorLog(applicationName);
            
            try
            {
            	System.IO.File.AppendAllText(getErrorLogPath(applicationName), DateTime.Now.ToString() + " - " + Environment.OSVersion + " - " + Environment.MachineName + " - " + source + Environment.NewLine);
                System.IO.File.AppendAllText(getErrorLogPath(applicationName), msg + Environment.NewLine + Environment.NewLine);
            	System.IO.File.AppendAllText(getErrorLogPath(applicationName), source + Environment.NewLine + Environment.NewLine);
            	System.IO.File.AppendAllText(getErrorLogPath(applicationName), stackTrace + Environment.NewLine + Environment.NewLine);
            }
            catch
            { }
        }
		
		/// <summary>
        /// Override: Write an error to the error log file with specified debug info from specified plugin.
        /// </summary>
        /// <param name="msg">Error message as string</param>
        /// <param name="source">Code source as string</param>
        /// <param name="stackTrace">Stack trace as string</param>
        /// <param name="applicationName">Current application name as string</param>
        /// <param name="pluginName">Plugin name as string</param>
		public static void LogError(string msg, string source, string stackTrace, string applicationName, string pluginName)
        {

        	truncateErrorLog(applicationName);
            
            try
            {
            	System.IO.File.AppendAllText(getErrorLogPath(applicationName), DateTime.Now.ToString() + " - " + Environment.OSVersion + " - " + Environment.MachineName + " - " + source + Environment.NewLine + " Plugin:" + pluginName);
                System.IO.File.AppendAllText(getErrorLogPath(applicationName), msg + Environment.NewLine + Environment.NewLine);
            	System.IO.File.AppendAllText(getErrorLogPath(applicationName), source + Environment.NewLine + Environment.NewLine);
            	System.IO.File.AppendAllText(getErrorLogPath(applicationName), stackTrace + Environment.NewLine + Environment.NewLine);
            }
            catch
            { }
        }

        /// <summary>
        /// Truncates the error log and creates a new one if file size is big
        /// </summary>
		private static void truncateErrorLog(string applicationName)
        {
						
			string ErrorLog = getErrorLogPath(applicationName);
			
            if (System.IO.File.Exists(ErrorLog))
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(ErrorLog);
                if (fi.Length > 1000000)
                {
                    //if the log file gets too big for it's britches, make a copy and truncate
                    try
                    {
                        string oldErrorLogPath = ErrorLog + DateTime.Now.ToString("ddMMyyy_Hmmss");
                        System.IO.File.Copy(ErrorLog, oldErrorLogPath);
                        string oldLog = System.IO.File.ReadAllText(ErrorLog);
                        if (oldLog.Length > 10000)
                        {
                            //save last bit of stuff
                            string newLog = "<Log truncated from " + oldErrorLogPath + ">" + oldLog.Substring(oldLog.Length - 10000, 10000);
                            System.IO.File.WriteAllText(ErrorLog, newLog);
                        }
                    }
                    catch { }
                }
            }
        }
	}
}
