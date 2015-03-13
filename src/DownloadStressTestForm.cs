using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

// Much of this example drawn from: http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.begingetresponse.aspx


namespace DownloadStressTest
{
    // Callbacks to allow updating GUI during transfer or upon completion
    public delegate void ResponseInfoDelegate(string statusDescr, string contentLength);
    public delegate void ProgressDelegate(int totalBytes, double pctComplete, double transferRate);
    public delegate void DoneDelegate();

    /// <summary>
    /// Main form to allow downloading files via HTTP or FTP
    /// </summary>
    public partial class DownloadStressTestForm : Form
    {
        const int BUFFER_SIZE = 1448;	// TCP packet size if 1448 bytes (seen thru inspection)

        public DownloadStressTestForm()
        {
            InitializeComponent();

            lblDownloadComplete.Visible = false;
        }

        /// <summary>
        /// "Get File" button, which starts the transfer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetFile_Click(object sender, EventArgs e)
        {
            try
            {
                lblDownloadComplete.Visible = false;

                WebRequest req = null;
                WebRequestState reqState = null;
                Uri fileURI = new Uri(txtURI.Text);

                if (fileURI.Scheme == Uri.UriSchemeHttp)
                {
                    req = (HttpWebRequest)HttpWebRequest.Create(fileURI);
                    reqState = new HttpWebRequestState(BUFFER_SIZE);
                    reqState.request = req;
                }
                else if (fileURI.Scheme == Uri.UriSchemeFtp)
                {
                    // Get credentials
                    GetCredentialsForm frmCreds = new GetCredentialsForm();
                    DialogResult result = frmCreds.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        req = (FtpWebRequest)FtpWebRequest.Create(fileURI);
                        req.Credentials = new NetworkCredential(frmCreds.Username, frmCreds.Password);
                        reqState = new FtpWebRequestState(BUFFER_SIZE);

                        // Set FTP-specific stuff
                        ((FtpWebRequest)req).KeepAlive = false;

                        // First thing we do is get file size.  2nd step, done later, 
                        // will be to download actual file.
                        ((FtpWebRequest)req).Method = WebRequestMethods.Ftp.GetFileSize;
                        reqState.FTPMethod = WebRequestMethods.Ftp.GetFileSize;

                        reqState.request = req;
                    }
                    else
                        req = null;	// abort

                }
                else
                    MessageBox.Show("URL must be either http://xxx or ftp://xxxx");

                if (req != null)
                {
                    reqState.fileURI = fileURI;
                    reqState.respInfoCB = new ResponseInfoDelegate(SetResponseInfo);
                    reqState.progCB = new ProgressDelegate(Progress);
                    reqState.doneCB = new DoneDelegate(Done);
                    reqState.transferStart = DateTime.Now;

                    // Start the asynchronous request.
                    IAsyncResult result =
                      (IAsyncResult)req.BeginGetResponse(new AsyncCallback(RespCallback), reqState);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("EXC in button1_Click(): {0}", ex.Message));
            }
        }

        /// <summary>
        /// Response info callback, called after we get HTTP response header.
        /// Used to set info in GUI about max download size.
        /// </summary>
        private void SetResponseInfo(string statusDescr, string contentLength)
        {
            if (this.InvokeRequired)
            {
                ResponseInfoDelegate del = new ResponseInfoDelegate(SetResponseInfo);
                this.Invoke(del, new object[] { statusDescr, contentLength });
            }
            else
            {
                lblStatusDescr.Text = statusDescr;
                lblContentLength.Text = contentLength;
            }
        }

        /// <summary>
        /// Progress callback, called when we've read another packet of data.
        /// Used to set info in GUI on % complete & transfer rate.
        /// </summary>
        private void Progress(int totalBytes, double pctComplete, double transferRate)
        {
            if (this.InvokeRequired)
            {
                ProgressDelegate del = new ProgressDelegate(Progress);
                this.Invoke(del, new object[] { totalBytes, pctComplete, transferRate });
            }
            else
            {
                lblBytesRead.Text = totalBytes.ToString();
                progressBar1.Value = (int)pctComplete;
                lblRate.Text = transferRate.ToString("f0");
            }
        }

        /// <summary>
        /// GUI-updating callback called when download has completed.
        /// </summary>
        private void Done()
        {
            if (this.InvokeRequired)
            {
                DoneDelegate del = new DoneDelegate(Done);
                this.Invoke(del, new object[] { });
            }
            else
            {
                progressBar1.Value = 0;
                lblDownloadComplete.Visible = true;
            }
        }

        /// <summary>
        /// Main response callback, invoked once we have first Response packet from
        /// server.  This is where we initiate the actual file transfer, reading from
        /// a stream.
        /// </summary>
        private static void RespCallback(IAsyncResult asyncResult)
        {
            try
            {
                // Will be either HttpWebRequestState or FtpWebRequestState
                WebRequestState reqState = ((WebRequestState)(asyncResult.AsyncState));
                WebRequest req = reqState.request;
                string statusDescr = "";
                string contentLength = "";

                // HTTP 
                if (reqState.fileURI.Scheme == Uri.UriSchemeHttp)
                {
                    HttpWebResponse resp = ((HttpWebResponse)(req.EndGetResponse(asyncResult)));
                    reqState.response = resp;
                    statusDescr = resp.StatusDescription;
                    reqState.totalBytes = reqState.response.ContentLength;
                    contentLength = reqState.response.ContentLength.ToString();   // # bytes
                }

                // FTP part 1 - response to GetFileSize command
                else if ((reqState.fileURI.Scheme == Uri.UriSchemeFtp) &&
                         (reqState.FTPMethod == WebRequestMethods.Ftp.GetFileSize))
                {
                    // First FTP command was GetFileSize, so this 1st response is the size of 
                    // the file.
                    FtpWebResponse resp = ((FtpWebResponse)(req.EndGetResponse(asyncResult)));
                    statusDescr = resp.StatusDescription;
                    reqState.totalBytes = resp.ContentLength;
                    contentLength = resp.ContentLength.ToString();   // # bytes
                }

                // FTP part 2 - response to DownloadFile command
                else if ((reqState.fileURI.Scheme == Uri.UriSchemeFtp) &&
                         (reqState.FTPMethod == WebRequestMethods.Ftp.DownloadFile))
                {
                    FtpWebResponse resp = ((FtpWebResponse)(req.EndGetResponse(asyncResult)));
                    reqState.response = resp;
                }

                else
                    throw new ApplicationException("Unexpected URI");


                // Get this info back to the GUI -- max # bytes, so we can do progress bar
                if (statusDescr != "")
                    reqState.respInfoCB(statusDescr, contentLength);

                // FTP part 1 done, need to kick off 2nd FTP request to get the actual file
                if ((reqState.fileURI.Scheme == Uri.UriSchemeFtp) && (reqState.FTPMethod == WebRequestMethods.Ftp.GetFileSize))
                {
                    // Note: Need to create a new FtpWebRequest, because we're not allowed to change .Method after
                    // we've already submitted the earlier request.  I.e. FtpWebRequest not recyclable.
                    // So create a new request, moving everything we need over to it.
                    FtpWebRequest req2 = (FtpWebRequest)FtpWebRequest.Create(reqState.fileURI);
                    req2.Credentials = req.Credentials;
                    req2.UseBinary = true;
                    req2.KeepAlive = true;
                    req2.Method = WebRequestMethods.Ftp.DownloadFile;

                    reqState.request = req2;
                    reqState.FTPMethod = WebRequestMethods.Ftp.DownloadFile;

                    // Start the asynchronous request, which will call back into this same method
                    IAsyncResult result =
                      (IAsyncResult)req2.BeginGetResponse(new AsyncCallback(RespCallback), reqState);
                }
                else	// HTTP or FTP part 2 -- we're ready for the actual file download
                {
                    // Set up a stream, for reading response data into it
                    Stream responseStream = reqState.response.GetResponseStream();
                    reqState.streamResponse = responseStream;

                    // Begin reading contents of the response data
                    IAsyncResult ar = responseStream.BeginRead(reqState.bufferRead, 0, BUFFER_SIZE, new AsyncCallback(ReadCallback), reqState);
                }

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("EXC in RespCallback(): {0}", ex.Message));
            }
        }

        /// <summary>
        /// Main callback invoked in response to the Stream.BeginRead method, when we have some data.
        /// </summary>
        private static void ReadCallback(IAsyncResult asyncResult)
        {
            try
            {
                // Will be either HttpWebRequestState or FtpWebRequestState
                WebRequestState reqState = ((WebRequestState)(asyncResult.AsyncState));

                Stream responseStream = reqState.streamResponse;

                // Get results of read operation
                int bytesRead = responseStream.EndRead(asyncResult);

                // Got some data, need to read more
                if (bytesRead > 0)
                {
                    // Report some progress, including total # bytes read, % complete, and transfer rate
                    reqState.bytesRead += bytesRead;
                    double pctComplete = ((double)reqState.bytesRead / (double)reqState.totalBytes) * 100.0f;

                    // Note: bytesRead/totalMS is in bytes/ms.  Convert to kb/sec.
                    TimeSpan totalTime = DateTime.Now - reqState.transferStart;
                    double kbPerSec = (reqState.bytesRead * 1000.0f) / (totalTime.TotalMilliseconds * 1024.0f);

                    reqState.progCB(reqState.bytesRead, pctComplete, kbPerSec);

                    // Kick off another read
                    IAsyncResult ar = responseStream.BeginRead(reqState.bufferRead, 0, BUFFER_SIZE, new AsyncCallback(ReadCallback), reqState);
                    return;
                }

                // EndRead returned 0, so no more data to be read
                else
                {
                    responseStream.Close();
                    reqState.response.Close();
                    reqState.doneCB();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("EXC in ReadCallback(): {0}", ex.Message));
            }
        }

        /// <summary>
        /// When we load form, clear out design-time labels.
        /// </summary>
        private void DownloadStressTestForm_Load(object sender, EventArgs e)
        {
            // Clean out design-time labels
            lblStatusDescr.Text = "";
            lblContentLength.Text = "";
            lblBytesRead.Text = "";
            lblRate.Text = "";
        }
    }
}