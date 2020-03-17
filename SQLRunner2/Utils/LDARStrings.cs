/*
 * Created by SharpDevelop.
 * User: devin
 * Date: 12/10/2015
 * Time: 09:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
	/// <summary>
	/// Helper functions for dealing with LDAR Specific strings such as Tags, Points, Root Tags, etc.
	/// </summary>
	public class LDARStrings
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public LDARStrings()
		{
			
		}
		
        /// <summary>
        /// Convert size string to double.
        /// </summary>
        /// <param name="str">Size as string</param>
        /// <returns>Size as double</returns>
		public static double getSizeFromString(string str)
        {
            //sizes have to be positive, so -1 indicates error
            double size = 0;
            double wholeNo = 0;

            if (str != null)
            {

                string rmdc = string.Empty;
                foreach (char c in str)
                {
                    if (c == '/') rmdc += c;
                    if (c == '.') rmdc += c;
                    if (c == ' ') rmdc += c;
                    if (char.IsNumber(c)) rmdc += c;
                }
                str = rmdc;

                if (str.Contains("/"))
                {
                    try
                    {
                        if (str.Contains(" "))
                        {
                            string[] wn = str.Split(' ');
                            wholeNo = double.Parse(wn[0]);
                            str = wn[1];
                        }
                        string[] frac = str.Split('/');
                        size = double.Parse(frac[0]) / double.Parse(frac[1]);
                    }
                    catch
                    {
                        size = 0;
                    }
                }
                else
                {
                    if (!double.TryParse(str, out size)) size = 0;
                }
            }

            return size + wholeNo;
        }

        /// <summary>
        /// Gets a tag point with specified number of padded zeros.
        /// </summary>
        /// <param name="pointNumber">Tag point number</param>
        /// <param name="decimalPoints">Number of places after the decimal</param>
        /// <returns></returns>
		public static string getTagPoint(int pointNumber, int decimalPoints = 1)
        {
            string pt = string.Empty;

            pt = pointNumber.ToString();

            while (pt.Length < decimalPoints)
            {
                pt = "0" + pt;
            }

            return pt;
        }

	
        /// <summary>
        /// Gets a tag's current point without the decimal as string
        /// </summary>
        /// <param name="TagID">LDAR Tag as string</param>
        /// <param name="separator">Optional: Tag-point separator</param>
        /// <returns>Tag Point as string</returns>
        public static string getPointFromExistingTag(string TagID, string separator = ".")
        {
            string pt = string.Empty;

            if (!string.IsNullOrEmpty(TagID))
            {
                if (TagID.Contains(separator))
                {
                	List<string> splitTag = TagID.Split(separator.ToCharArray()).ToList<string>();
                    pt = splitTag.LastOrDefault();
                }
            }

            return pt;
        }

        /// <summary>
        /// Get a Tag's root as string
        /// </summary>
        /// <param name="TagID">LDAR Tag as string</param>
        /// <param name="separator">Optional: Tag-point separator</param>
        /// <returns>Root tag as string</returns>
        public static string getBaseTag(string TagID, string separator = ".")
        {
            if (TagID.Contains(separator))
            {
            	return TagID.Split(separator.ToCharArray())[0];
            }
            else
            {
                return TagID;
            }
        }
        
        /// <summary>
        /// Format a tag with optional padded zeros.  Separate tag/point combinations should be concatenated with a separator and then parsed back out.
        /// </summary>
        /// <param name="TagID">LDAR Tag as string</param>
        /// <param name="tagLength">Number of characters tag rooty should be - 0 indicates no change</param>
        /// <param name="pointLength">Number of characters point should be - 0 indicates no change</param>
        /// <param name="separator">Optional - separator character, default is '.'</param>
        /// <param name="AllowZeroPoints">Optional True = allow points with numeric value zero, False (default) = prevent zero points from being added to tag</param>
        /// <param name="padCharacter">Optional - Defaults to 0 unless otherwise specified</param>
        /// <returns>formatted tag</returns>
        public static string formatTag(string TagID, int tagLength, int pointLength, string separator = ".", bool AllowZeroPoints = false, string padCharacter = "0")
        {
        	string NewTagRoot = string.Empty;
        	string NewTagPoint = string.Empty;
        	
        	if (string.IsNullOrEmpty(TagID)) return string.Empty;
        	
        	if (TagID.Contains(separator))
        	{
        		NewTagRoot = TagID.Split(separator.ToCharArray())[0];
        		NewTagPoint = TagID.Split(separator.ToCharArray())[1];
        	}
        	else
        	{
        		NewTagRoot = TagID;
        	}
        	
        	NewTagRoot = NewTagRoot.TrimStart('0');
        	NewTagPoint = NewTagPoint.TrimStart('0');
        	NewTagRoot = NewTagRoot.Trim(' ');
        	NewTagPoint = NewTagPoint.Trim(' ');
        	
        	if (tagLength > 0)
        	{
        		if (NewTagRoot.Length < tagLength)
        		{
        			for (int i = NewTagRoot.Length; i < tagLength; i++)
        			{
        				NewTagRoot = padCharacter + NewTagRoot;
        			}
        		}
        	}
        	
        	if (pointLength > 0)
        	{
        		if (NewTagPoint.Length < pointLength)
        		{
        			for (int i = NewTagPoint.Length; i < pointLength; i++)
        			{
        				NewTagPoint = padCharacter + NewTagPoint;
        			}
        		}
        	}
        	
        	if (!AllowZeroPoints)
        	{
        		if (string.IsNullOrEmpty(NewTagPoint.Trim('0'))) NewTagPoint = string.Empty;
        	}
        	
        	if (string.IsNullOrEmpty(NewTagPoint))
        		return NewTagRoot;
        	else
        		return NewTagRoot + separator + NewTagPoint;
        	
        }

        /// <summary>
        /// Get next tag in sequence with point.
        /// </summary>
        /// <param name="TagID">LDAR Tag</param>
        /// <param name="startAt">Number to start points at</param>
        /// <param name="paddedZeros">Minimum number of decimal places after point</param>
        /// <returns></returns>
        public static string getNextTagWithPoint(string TagID, int startAt, int paddedZeros)
        {

            string tmpPoint = getPointFromExistingTag(TagID);

            if (!string.IsNullOrEmpty(tmpPoint))
            {
                int tmpPointNo = 0;
                if (int.TryParse(tmpPoint, out tmpPointNo))
                {
                    tmpPoint = (tmpPointNo + 1).ToString();
                }
                else
                {
                    tmpPoint = startAt.ToString();
                }
            }
            else
            {
                tmpPoint = startAt.ToString();
            }

            while (tmpPoint.Length < paddedZeros)
            {
                tmpPoint = "0" + tmpPoint;
            }

            return getBaseTag(TagID) + "." + tmpPoint;
        }

        /// <summary>
        /// Get next extension number as string
        /// </summary>
        /// <param name="Extension">Current tag extension</param>
        /// <param name="startAt">Start extension numbering at</param>
        /// <param name="paddedZeros">Min number of decimal places</param>
        /// <returns></returns>
        public static string getNextExtension(string Extension, int startAt, int paddedZeros)
        {

            int tmpPointNo = startAt;
            string tmpPoint = string.Empty;

            if (int.TryParse(Extension, out tmpPointNo))
            {
                tmpPoint = (tmpPointNo + 1).ToString();
            }
            else
            {
                tmpPoint = startAt.ToString();
            }

            while (tmpPoint.Length < paddedZeros)
            {
                tmpPoint = "0" + tmpPoint;
            }

            return tmpPoint;
        }
	}
}
