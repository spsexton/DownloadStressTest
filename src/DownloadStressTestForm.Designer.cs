namespace DownloadStressTest
{
    partial class DownloadStressTestForm
    {
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
	    if (disposing && (components != null))
	    {
		components.Dispose();
	    }
	    base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
        this.btnGetFile = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.lblStatusDescr = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.lblContentLength = new System.Windows.Forms.Label();
        this.txtURI = new System.Windows.Forms.TextBox();
        this.progressBar1 = new System.Windows.Forms.ProgressBar();
        this.label4 = new System.Windows.Forms.Label();
        this.lblBytesRead = new System.Windows.Forms.Label();
        this.lblDownloadComplete = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.lblRate = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // btnGetFile
        // 
        this.btnGetFile.Location = new System.Drawing.Point(28, 27);
        this.btnGetFile.Name = "btnGetFile";
        this.btnGetFile.Size = new System.Drawing.Size(75, 23);
        this.btnGetFile.TabIndex = 0;
        this.btnGetFile.Text = "Get File";
        this.btnGetFile.UseVisualStyleBackColor = true;
        this.btnGetFile.Click += new System.EventHandler(this.btnGetFile_Click);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(76, 110);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(47, 13);
        this.label1.TabIndex = 1;
        this.label1.Text = "Status:";
        // 
        // lblStatusDescr
        // 
        this.lblStatusDescr.AutoSize = true;
        this.lblStatusDescr.Location = new System.Drawing.Point(135, 110);
        this.lblStatusDescr.Name = "lblStatusDescr";
        this.lblStatusDescr.Size = new System.Drawing.Size(81, 13);
        this.lblStatusDescr.TabIndex = 2;
        this.lblStatusDescr.Text = "[lblStatusDescr]";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(25, 141);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(98, 13);
        this.label2.TabIndex = 3;
        this.label2.Text = "Content Length:";
        // 
        // lblContentLength
        // 
        this.lblContentLength.AutoSize = true;
        this.lblContentLength.Location = new System.Drawing.Point(135, 141);
        this.lblContentLength.Name = "lblContentLength";
        this.lblContentLength.Size = new System.Drawing.Size(93, 13);
        this.lblContentLength.TabIndex = 4;
        this.lblContentLength.Text = "[lblContentLength]";
        // 
        // txtURI
        // 
        this.txtURI.Location = new System.Drawing.Point(120, 27);
        this.txtURI.Name = "txtURI";
        this.txtURI.Size = new System.Drawing.Size(335, 20);
        this.txtURI.TabIndex = 5;
        this.txtURI.Text = "http://mschnlnine.vo.llnwd.net/d1/pdc08/PPTX/BB01.pptx";
        // 
        // progressBar1
        // 
        this.progressBar1.Location = new System.Drawing.Point(28, 67);
        this.progressBar1.Name = "progressBar1";
        this.progressBar1.Size = new System.Drawing.Size(427, 23);
        this.progressBar1.TabIndex = 8;
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label4.Location = new System.Drawing.Point(268, 110);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(76, 13);
        this.label4.TabIndex = 9;
        this.label4.Text = "Bytes Read:";
        // 
        // lblBytesRead
        // 
        this.lblBytesRead.AutoSize = true;
        this.lblBytesRead.Location = new System.Drawing.Point(350, 110);
        this.lblBytesRead.Name = "lblBytesRead";
        this.lblBytesRead.Size = new System.Drawing.Size(75, 13);
        this.lblBytesRead.TabIndex = 10;
        this.lblBytesRead.Text = "[lblBytesRead]";
        // 
        // lblDownloadComplete
        // 
        this.lblDownloadComplete.AutoSize = true;
        this.lblDownloadComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblDownloadComplete.ForeColor = System.Drawing.Color.Navy;
        this.lblDownloadComplete.Location = new System.Drawing.Point(174, 188);
        this.lblDownloadComplete.Name = "lblDownloadComplete";
        this.lblDownloadComplete.Size = new System.Drawing.Size(102, 13);
        this.lblDownloadComplete.TabIndex = 11;
        this.lblDownloadComplete.Text = "Download Complete";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label3.Location = new System.Drawing.Point(241, 141);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(103, 13);
        this.label3.TabIndex = 12;
        this.label3.Text = "Avg Rate (kbps):";
        // 
        // lblRate
        // 
        this.lblRate.AutoSize = true;
        this.lblRate.Location = new System.Drawing.Point(350, 141);
        this.lblRate.Name = "lblRate";
        this.lblRate.Size = new System.Drawing.Size(46, 13);
        this.lblRate.TabIndex = 13;
        this.lblRate.Text = "[lblRate]";
        // 
        // DownloadStressTestForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(485, 220);
        this.Controls.Add(this.lblRate);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.lblDownloadComplete);
        this.Controls.Add(this.lblBytesRead);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.progressBar1);
        this.Controls.Add(this.txtURI);
        this.Controls.Add(this.lblContentLength);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.lblStatusDescr);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.btnGetFile);
        this.Name = "DownloadStressTestForm";
        this.Text = "Download Stress Test";
        this.Load += new System.EventHandler(this.DownloadStressTestForm_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button btnGetFile;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label lblStatusDescr;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.Label lblContentLength;
	private System.Windows.Forms.TextBox txtURI;
	private System.Windows.Forms.ProgressBar progressBar1;
	private System.Windows.Forms.Label label4;
	private System.Windows.Forms.Label lblBytesRead;
	private System.Windows.Forms.Label lblDownloadComplete;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label lblRate;
    }
}

