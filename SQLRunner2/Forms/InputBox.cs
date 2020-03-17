/*
 * Created by SharpDevelop.
 * User: devin
 * Date: 6/5/2019
 * Time: 2:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SQLRunner2.Forms
{
	/// <summary>
	/// Description of InputBox.
	/// </summary>
	public partial class InputBox : Form
	{
		public string InputText {
			get
			{
				return txtText.Text;
			}
			set
			{
				txtText.Text = value;
			}
		}
		
		public InputBox(string inputText)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			lblCaption.Text = inputText;
			txtText.Select();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void BtnOKClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
		void BtnCancelClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
		void TxtTextKeyDown(object sender, KeyEventArgs e)
		{
		    if(e.KeyData == Keys.Enter)   
		    {  
		    	if (string.IsNullOrEmpty(txtText.Text))
		    	    DialogResult = DialogResult.Cancel;
		    	else
		    		DialogResult = DialogResult.OK;
		    }
		}

	}
}
