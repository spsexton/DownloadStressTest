using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DownloadStressTest
{
    public partial class GetCredentialsForm : Form
    {
        public string Username; 
        public string Password;

	public GetCredentialsForm()
	{
	    InitializeComponent();
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
	    Username = txtUsername.Text;
	    Password = txtPassword.Text;
	    this.Close();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
	    this.Close();
	}
    }
}