/*
 * Created by SharpDevelop.
 * User: devin
 * Date: 12/9/2015
 * Time: 9:43 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Management;
using System.Net;
using System.Net.Cache;
using System.Text.RegularExpressions;
using System.Net.Security;

namespace Utils
{
	/// <summary>
	/// Globals for all apps.
	/// </summary>
	public static class Globals
	{
		
		/// <summary>
		/// Current Application as string.  Used to route error logging to proper folder.
		/// </summary>
		public static string CurrentApplication = string.Empty;
		
		/// <summary>
		/// Get user's appdata folder
		/// </summary>
		/// <returns>Appdata folder as string</returns>
		/// 		
		public static string getWorkingFolder()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		}
		
		/// <summary>
		/// Get user's temp folder.
		/// </summary>
		/// <returns></returns>
		public static string getTempFolder()
		{
			string tmpDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

			tmpDir += Path.DirectorySeparatorChar + "Temp";
				if (!Directory.Exists(tmpDir))
				{
					try
					{
						Directory.CreateDirectory(tmpDir);
					}
					catch
					{}
				}
			
			return tmpDir;
		}
		
		/// <summary>
		/// Gets unique hardware ID based on logical disk manager for drive C.  Returns 000000 if unsuccessful.
		/// </summary>
		/// <returns>Unique hardware ID</returns>
		public static string getUniqueHardwareID()
        {
			
			string platform = System.Environment.OSVersion.Platform.ToString();
			string volumeSerial = "000000";
			
			if (platform.Contains("Win"))
			{
				try
				{
		            ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + "C" + @":""");
		            dsk.Get();
		            volumeSerial = dsk["VolumeSerialNumber"].ToString();
				}
				catch{}
			}
			
			return volumeSerial;
        }
		
		/// <summary>
		/// Helper function to return stored SQL text in /SQL folder.
		/// </summary>
		/// <param name="scriptName">File name of script without path or extension</param>
		/// <returns>File text</returns>
		public static string getStoredSQLText(string scriptName)
		{
			try
			{
				string sqlText = File.ReadAllText(Globals.getExeDirectory + Path.DirectorySeparatorChar  + "SQL" + Path.DirectorySeparatorChar + scriptName + ".sql");
				return sqlText;
			}
			catch
			{
				Logging.LogError("Could not read SQL file: " + scriptName, "", "", "SQLRunner2");
				return string.Empty;
			}
		}
	
		/// <summary>
		/// Get appdata directory for user for specific application.  Creates the folder if it doesn't exist.  
		/// </summary>
		/// <param name="applicationName">Application name</param>
		/// <returns>Appdata folder plus application name</returns>
		public static string getWorkingFolder(string applicationName)
		{			
			string appDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			
			if (string.IsNullOrEmpty(applicationName))
			{
				return appDir;
			}
			else
			{
				appDir += Path.DirectorySeparatorChar + applicationName;
				if (!Directory.Exists(appDir))
				{
					try
					{
						Directory.CreateDirectory(appDir);
					}
					catch
					{}
				}
			}
			
			return appDir;	
		}
		
		
		/// <summary>
		/// Gets folder for stored projects for control panel.  All inventory base data files are stored here.  Folder is created if it doesn't exist.
		/// </summary>
		/// <returns>Projects folder path</returns>
		public static string getProjectsFolder()
		{
			string appDir = getWorkingFolder("SQLRunner2");
			
			appDir += Path.DirectorySeparatorChar + "Projects";
			if (!Directory.Exists(appDir))
			{
				try
				{
					Directory.CreateDirectory(appDir);
				}
				catch
				{}
			}
			
			return appDir;			
		}
		
	
        /// <summary>
        /// Get directory of current executable.
        /// </summary>
		public static string getExeDirectory
        {
            get
            {
                return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        /// <summary>
        /// Deletes then re-creates the working folder.
        /// </summary>
        /// <param name="applicationName">Current application name as string.</param>
		public static void cleanWorkingDirectory(string applicationName)
        {
            //delete it to make sure we are clean...
            try
            {
            	if (System.IO.Directory.Exists(getWorkingFolder(applicationName)))
                {
                    System.IO.Directory.Delete(getWorkingFolder(applicationName), true);

                    //Move to recycle bin
                    //FileSystem.DeleteDirectory(getWorkingFolder(applicationName), UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                }

                System.IO.Directory.CreateDirectory(getWorkingFolder(applicationName));
            }
            catch { }

        }
        
		/// <summary>
		/// Gets current OS version.
		/// </summary>
		/// <returns>String of OS version</returns>
        private static string GetWindowsVersion()
        {
            return Environment.OSVersion.Version.ToString();
        }
        
        public static bool IsOSLinux()
        {
        	return Environment.OSVersion.Platform == PlatformID.Unix;
        }

        /// <summary>
        /// Get current executable assembly version.
        /// </summary>
        /// <returns>Assembly version</returns>
        public static string GetCurrentAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
   
        /// <summary>
        /// Removes characters that can foul up CSV files
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Cleaned string</returns>
        public static string textCleaner(string str)
        {
            //this function makes sure certain characters aren't fouling up our csv
            string tmpString = string.Empty;

            if (string.IsNullOrEmpty(str)) return "";

            foreach (char c in str)
            {
                if (c != '"' && c != '\\')
                {
                    tmpString += c;
                }
            }

            return tmpString;
        }
        
        /// <summary>
        /// Generic JSON object serializer.
        /// </summary>
        /// <param name="t">Object Type</param>
        /// <returns>JSON txt serialization of object</returns>
        public static string SerializeObject<T>(T t)
        {
            string text = "";
            using (MemoryStream stream1 = new MemoryStream())
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                ser.WriteObject(stream1, t);
                stream1.Position = 0;
                StreamReader sr = new StreamReader(stream1);
                text = sr.ReadToEnd();
            }
            return text;
        }

        /// <summary>
        /// Generic JSON object deserializer
        /// </summary>
        /// <param name="json">JSON txt</param>
        /// <returns>Object of T</returns>
        public static T DeserializeObject<T>(string json)
        {
            if (String.IsNullOrEmpty(json)) return default(T);

            Encoding enc = new UnicodeEncoding(false, true, false);
            
            using (MemoryStream stream1 = new MemoryStream(enc.GetBytes(json)))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                stream1.Position = 0;
                return (T)ser.ReadObject(stream1);
            }
        }
        
        public static DateTime GetNistTime()
		{
		    DateTime dateTime = DateTime.MinValue;
		    
		    if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()) return dateTime;

		    try
		    {
		    	string html = downloadWebPage("https://nist.time.gov/actualtime.cgi?lzbc=siqm9b");
			        string time = Regex.Match(html, @"(?<=\btime="")[^""]*").Value;
			        double milliseconds = Convert.ToInt64(time) / 1000.0;
			        dateTime = new DateTime(1970, 1, 1).AddMilliseconds(milliseconds).ToLocalTime();
		    }
		    catch (Exception ex)
		    {
		    	System.Diagnostics.Debug.Print("No Go: " + ex.Message);
		    }
		
		    return dateTime;
		}
        
	    public static string downloadWebPage(string theURL)
	    {
	    	if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()) return string.Empty;
	    	
	        //### download a web page to a string
	        WebClient client = new WebClient();
	
	        try
	        {
		        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
		        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
		        client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
		        
		        return client.DownloadString(theURL);
	        }
	        catch 
	        {
	        	return string.Empty;
	        }
	    }
		
	}
	
	
	public class StatusEventArgs : EventArgs
    {
        private string message;
        private int percent;

        public StatusEventArgs(string message, int percent = 0)
        {
            this.message = message;
            this.percent = percent;
        }

        public string Message
        {
            get
            {
                return message;
            }
        }
        
        public int Percent
        {
            get
            {
                return Percent;
            }
        }
    }
}