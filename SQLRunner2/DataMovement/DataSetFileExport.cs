using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using ClosedXML.Excel;

using Utils;

namespace DataMovement
{

	/// <summary>
	/// /This class reads from a datatable object and exports to various formats
	/// </summary>
    public class DataSetFileExport
    {

        /// <summary>
        /// Sends ADO.Net DataTable to Excel .csv format.
        /// </summary>
        /// <param name="dt">ADO.Net DataTable to convert</param>
        /// <param name="strExcelFile">Destination File path</param>
        /// <returns>True if success</returns>
    	public bool DataTable2Excel(DataTable dt, string strExcelFile)
        {
            bool functionReturnValue = false;

            StringBuilder myString = new StringBuilder();
            bool bFirstRecord = true;
            int rowIndex = 0;

            functionReturnValue = true;


            try
            {
                //write the header row
                bFirstRecord = true;
                foreach (DataColumn column in dt.Columns)
                {
                    if (!bFirstRecord)
                    {
                        myString.Append('\t');
                    }
                    myString.Append(column.ColumnName);
                    bFirstRecord = false;
                }
                myString.Append(Environment.NewLine);


                foreach (DataRow dr in dt.Rows)
                {
                    rowIndex += 1;
                    bFirstRecord = true;
                    foreach (object field in dr.ItemArray)
                    {
                        if (!bFirstRecord)
                        {
                            myString.Append('\t');
                        }
                        myString.Append(field.ToString());
                        bFirstRecord = false;
                    }
                    //New Line to differentiate next row
                    myString.Append(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
            	Logging.LogError(ex.Message, ex.Source, ex.StackTrace, Globals.CurrentApplication);
            	functionReturnValue = false;
                return functionReturnValue;
            }

            try
            {
                System.IO.File.WriteAllText(strExcelFile, myString.ToString());
            }
            catch (Exception ex)
            {
                Logging.LogError(ex.Message, ex.Source, ex.StackTrace, Globals.CurrentApplication);
            	functionReturnValue = false;
            }
            return functionReturnValue;


        }

    	/// <summary>
    	/// Sends ADO.Net DataTable to CSV file
    	/// </summary>
    	/// <param name="dt">ADO.Net DataTable to convert</param>
    	/// <param name="fileName">Destination File Name</param>
    	/// <returns></returns>
        public bool DataTable2CSV(DataTable dt, string fileName)
        {
            bool functionReturnValue = false;

            StringBuilder myString = new StringBuilder();
            bool bFirstRecord = true;
            int rowIndex = 0;

            functionReturnValue = true;


            try
            {
                //write the header row
                bFirstRecord = true;
                foreach (DataColumn column in dt.Columns)
                {
                    if (!bFirstRecord)
                    {
                        myString.Append(",");
                    }
                    myString.Append(column.ColumnName);
                    bFirstRecord = false;
                }
                myString.Append(Environment.NewLine);


                foreach (DataRow dr in dt.Rows)
                {
                    rowIndex += 1;
                    bFirstRecord = true;
                    foreach (object field in dr.ItemArray)
                    {
                        if (!bFirstRecord)
                        {
                            myString.Append(",");
                        }
                        myString.Append(field.ToString());
                        bFirstRecord = false;
                    }
                    //New Line to differentiate next row
                    myString.Append(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex.Message, ex.Source, ex.StackTrace, Globals.CurrentApplication);
            	functionReturnValue = false;
                return functionReturnValue;
            }

            try
            {
                System.IO.File.WriteAllText(fileName, myString.ToString());
            }
            catch (Exception ex)
            {
                Logging.LogError(ex.Message, ex.Source, ex.StackTrace, Globals.CurrentApplication);
            	functionReturnValue = false;
            }
            return functionReturnValue;


        }

        /// <summary>
        /// Sends ADO.Net table to delimited file, specify the delimiter
        /// </summary>
        /// <param name="dt">ADO.Net DataTable</param>
        /// <param name="fileName">Destination File Name</param>
        /// <param name="delimiter">Delimiter</param>
        /// <returns></returns>
        public bool DataTable2txt(DataTable dt, string fileName, char delimiter)
        {
            bool functionReturnValue = false;

            StringBuilder myString = new StringBuilder();
            bool bFirstRecord = true;
            int rowIndex = 0;

            functionReturnValue = true;


            try
            {
                //write the header row
                bFirstRecord = true;
                foreach (DataColumn column in dt.Columns)
                {
                    if (!bFirstRecord)
                    {
                        myString.Append(delimiter);
                    }
                    myString.Append(column.ColumnName);
                    bFirstRecord = false;
                }
                myString.Append(Environment.NewLine);


                foreach (DataRow dr in dt.Rows)
                {
                    rowIndex += 1;
                    bFirstRecord = true;
                    foreach (object field in dr.ItemArray)
                    {
                        if (!bFirstRecord)
                        {
                            myString.Append(delimiter);
                        }
                        myString.Append(field.ToString());
                        bFirstRecord = false;
                    }
                    //New Line to differentiate next row
                    myString.Append(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
            	Logging.LogError(ex.Message, ex.Source, ex.StackTrace, Globals.CurrentApplication);
                functionReturnValue = false;
                return functionReturnValue;
            }

            try
            {
                System.IO.File.WriteAllText(fileName, myString.ToString());
            }
            catch (Exception ex)
            {
            	Logging.LogError(ex.Message, ex.Source, ex.StackTrace, Globals.CurrentApplication);
                functionReturnValue = false;
            }
            return functionReturnValue;


        }

        /// <summary>
        /// Convert an ADO.Net DataTable to an HTML report using an HTML/CSS template.  The declared report title will replace a div tag with id "Title" and Text "ReportTitle"  The declared ADO.Net DataTable will replace a table tag with id "MainTable" and no content.
        /// </summary>
        /// <param name="targetTable">ADO.Net table to be used in the report</param>
        /// <param name="ReportTemplateName">File name to the HTML Template to use</param>
        /// <param name="ReportTemplatePath">File path to the HTML Template to use</param>
        /// <param name="ReportTitle">Report title to be displayed</param>
        /// <returns></returns>
        public string DataTable2HTMLReport(DataTable targetTable, string ReportTemplateName, string ReportTemplatePath, string ReportTitle)
        {

            string[] sourceHTML = File.ReadAllLines(ReportTemplatePath + "\\" + ReportTemplateName + ".htm");
            StringBuilder destinationHTML = new StringBuilder();
            StringBuilder tableHTML = new StringBuilder();

            if (targetTable == null) return string.Empty;

            tableHTML.Append("<table id=\"MainTable\">");
            tableHTML.Append("<thead>");

            //append header values
            foreach (DataColumn dc in targetTable.Columns)
            {
                tableHTML.Append("<th>" + dc.ColumnName + "</th>");
            }

            tableHTML.Append("</thead>");
            tableHTML.Append("</tbody>");

            //Add the data rows.
            foreach (DataRow myRow in targetTable.Rows)
            {
                tableHTML.Append("<tr>");

                foreach (DataColumn myColumn in targetTable.Columns)
                {
                    tableHTML.Append("<td>");
                    if (myRow[myColumn.ColumnName].ToString() == null)
                    {
                        tableHTML.Append("&nbsp;");
                    }
                    else
                    {
                        tableHTML.Append(myRow[myColumn.ColumnName].ToString());
                    }
                    tableHTML.Append("</td>");
                }


                tableHTML.Append("</tr>");
            }

            tableHTML.Append("</tbody>");
            tableHTML.Append("</table>");

            //finish building the report based on the template
            foreach (string htmlRow in sourceHTML)
            {
                bool skipLine = false;
                if (htmlRow.Contains("<div id=\"Title\">ReportTitle</div>"))
                {
                    destinationHTML.Append("<div id=\"Title\">" + ReportTitle + "</div>");
                    skipLine = true;
                }
                if (htmlRow.Contains("<table id=\"MainTable\"></table>"))
                {
                    destinationHTML.Append(tableHTML.ToString());
                    skipLine = true;
                }

                if (!skipLine)
                {
                    destinationHTML.Append(htmlRow);
                }
            }

            return destinationHTML.ToString(); 
        }

		/// <summary>
		/// Convert ADO.Net DataTable to a simple HTML report.
		/// </summary>
		/// <param name="targetTable">ADO.Net table to use</param>
		/// <param name="fileName">Destination file path</param>
		/// <returns></returns>
        public string ConvertToHtmlFile(DataTable targetTable, string fileName)
        {
            string myHtmlFile = "";

            if ((targetTable == null))
            {
                throw new System.ArgumentNullException("targetTable");
            }
            else
            {
                //Continue.
            }

            //Get a worker object.
            System.Text.StringBuilder myBuilder = new System.Text.StringBuilder();

            //Open tags and write the top portion.
            myBuilder.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            myBuilder.Append("<head>");
            myBuilder.Append("<title>");
            myBuilder.Append("Page-");
            myBuilder.Append(Guid.NewGuid().ToString());
            myBuilder.Append("</title>");
            myBuilder.Append("</head>");
            myBuilder.Append("<body>");
            myBuilder.Append("<table border='1px' cellpadding='5' cellspacing='0' ");
            myBuilder.Append("style='border: solid 1px Silver; font-size: x-small;'>");

            //Add the headings row.

            myBuilder.Append("<tr align='left' valign='top'>");

            foreach (DataColumn myColumn in targetTable.Columns)
            {
                myBuilder.Append("<td align='left' valign='top'>");
                myBuilder.Append(myColumn.ColumnName);
                myBuilder.Append("</td>");
            }

            myBuilder.Append("</tr>");


            //Add the data rows.
            foreach (DataRow myRow in targetTable.Rows)
            {
                myBuilder.Append("<tr align='left' valign='top'>");

                foreach (DataColumn myColumn in targetTable.Columns)
                {
                    myBuilder.Append("<td align='left' valign='top'>");
                    if (myRow[myColumn.ColumnName].ToString() == null)
                    {
                        myBuilder.Append("&nbsp;");
                    }
                    else
                    {
                        myBuilder.Append(myRow[myColumn.ColumnName].ToString());
                    }
                    //myBuilder.Append(myRow(myColumn.ColumnName).ToString())
                    myBuilder.Append("</td>");
                }


                myBuilder.Append("</tr>");
            }

            //Close tags.
            myBuilder.Append("</table>");
            myBuilder.Append("</body>");
            myBuilder.Append("</html>");

            //Get the string for return.
            myHtmlFile = myBuilder.ToString();

            try
            {
                System.IO.File.WriteAllText(fileName, myHtmlFile.ToString());
            }
            catch (Exception ex)
            {
            	Logging.LogError(ex.Message, ex.Source, ex.StackTrace, Globals.CurrentApplication);
            }

            return myHtmlFile;
        }

        /// <summary>
        /// Sends ADO.Net DataTable to Excel .xlsx format using ClosedXML library.
        /// </summary>
        /// <param name="dt">ADO.Net DataTable to convert</param>
        /// <param name="fileName">Destination File path</param>
        /// <returns>Error message is failure occurs</returns>
        public static string SendDataTableToExcel(DataTable dt, string fileName)
        {
            string errorMessage = string.Empty;
            
            try
            {
                XLWorkbook wb = new XLWorkbook();
                string parentId = string.Empty;

                wb.Worksheets.Add(dt, Path.GetFileNameWithoutExtension(fileName));
                wb.SaveAs(fileName);
            }
            catch (Exception ex)
            {
                errorMessage = "An error occurred while writing to Excel: " + ex.Message;
            }

            return errorMessage;
        }

        /// <summary>
        /// Get an ADO.Net DataTable object from an Excel .xlsx file
        /// </summary>
        /// <param name="fileName">File to use</param>
        /// <returns>ADO.Net DataTable</returns>
        public static DataTable GetDataTableFromExcel(string fileName)
        {
            DataTable newDt = new DataTable();

            XLWorkbook wb = new XLWorkbook(fileName);
            IXLWorksheet ws = wb.Worksheet(1);

            var xlRange = ws.RangeUsed();

            IXLTable compTable = xlRange.AsTable();

            foreach (IXLRangeColumn hello in compTable.Columns())
            {
                newDt.Columns.Add(hello.FirstCell().Value.ToString(), typeof(string)); 
            }

            bool headerFound = false;
            foreach (IXLRangeRow row in compTable.Rows())
            {
            	if (headerFound)
            	{
	            	DataRow dr = newDt.NewRow();
	                int itemCt = 0;
	                foreach (IXLCell cell in row.Cells())
	                {
	                    dr[itemCt] = row.Cell(itemCt+1).Value.ToString();
	                    itemCt++;
	                }
	                newDt.Rows.Add(dr);
            	}
            	else
            	{
            		headerFound = true;
            	}
            }

            return newDt;
        }
        
        /// <summary>
        /// Get a list of header values from an Excel file
        /// </summary>
        /// <param name="fileName">File to use</param>
        /// <returns>list of header values</returns>
        public static List<string> GetHeaderListFromExcel(string fileName)
        {
        	
        	List<string> headerList = new List<string>();

            XLWorkbook wb = new XLWorkbook(fileName);
            IXLWorksheet ws = wb.Worksheet(1);

            var xlRange = ws.RangeUsed();

            IXLTable compTable = xlRange.AsTable();

            foreach (IXLRangeColumn hello in compTable.Columns())
            {
                headerList.Add(hello.FirstCell().Value.ToString()); 
            }

            return headerList;
        }
        
    }
}