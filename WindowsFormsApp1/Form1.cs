using Aspose.Cells;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Lodop;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Web;
using System.Xml;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class Form1 : Form
    {
        public bool isactive = false;

        private static int count = 1;

        private static int count2 = 1;

        private static int count3 = 1;

        string ftpPassword = "Admin@123";

        string ftpServerIP = "47.97.154.144:6668";

        string ftpUserID = "connor";

        int key = 0;

        //LodopX lodop = new LodopX();
        private List<string> listCombobox;

        //private Uri LodopURI = new Uri("http://192.168.1.149/");
        private Uri PRINTURI = new Uri(Application.StartupPath + "\\PrintDesign.html");
        FtpWebRequest reqFTP;

        public Form1()
        {
            InitializeComponent();
            #region 双
            IsBegin = true;
            this.btnEnd.Enabled = false;
            #endregion

            #region combox
            List<string> list = new List<string>() {
            "1","2","222"
            };
            foreach (string i in list)
            {
                comboBox1.Items.Add(i);
            }
            listCombobox = getComboboxItems(this.comboBox1);
            //clss2 clss = new clss2();
            //clss.a();
            #endregion

            //webBrowser1 = new WebBrowser();
            //webBrowser1.Document.InvokeScript("LodopFuncs");
            #region listview导入
            ColumnHeader c1 = new ColumnHeader();
            c1.Width = 100;
            c1.Text = "姓名";
            ColumnHeader c2 = new ColumnHeader();
            c2.Width = 100;
            c2.Text = "性别";
            ColumnHeader c3 = new ColumnHeader();
            c3.Width = 100;
            c3.Text = "电话";
            //设置属性
            listView1.GridLines = true;  //显示网格线
            listView1.FullRowSelect = true;  //显示全行
            listView1.MultiSelect = false;  //设置只能单选
            listView1.View = View.Details;  //设置显示模式为详细
            //listView1.HoverSelection = true;  //当鼠标停留数秒后自动选择
            //把列名添加到listview中
            listView1.Columns.Add(c1);
            listView1.Columns.Add(c2);
            listView1.Columns.Add(c3);
            listView1.Columns.Add("籍贯", 100);

            ListViewItem li = new ListViewItem("1");
            //添加同一行的数据
            li.SubItems.Add("nan");
            li.SubItems.Add("1234");
            li.SubItems.Add("China");
            ListViewItem li2 = new ListViewItem("2");
            //添加同一行的数据
            li2.SubItems.Add("nan");
            li2.SubItems.Add("1234");
            li2.SubItems.Add("China");
            //将行对象绑定在listview对象中
            listView1.Items.Add(li);
            listView1.Items.Add(li2);
            //listView1.Items.Add()
            #endregion 

            this.webBrowser1.ObjectForScripting = this;
            this.webBrowser1.Navigate(PRINTURI);
            this.richTextBox1.Text = "LODOP.PRINT_INIT(\"\");";

            //this.webBrowser1.Navigate(new Uri(LodopURI));
            //隐藏WebBrowser控件
            //this.webBrowser1.Hide();
            //忽略脚本错误
            //this.webBrowser1.ScriptErrorsSuppressed = false;

            #region comboxlistview

            this.comboBox2.Items.Add("NC");
            this.comboBox2.Items.Add("WA");

            this.comboListView1.View = View.Details;
            this.comboListView1.GridLines = true;
            this.comboListView1.FullRowSelect = true;

            ColumnHeader columnheader;
            ListViewItem listviewitem;

            // Create sample ListView data.
            listviewitem = new ListViewItem("NC");
            listviewitem.SubItems.Add("North Carolina");
            this.comboListView1.Items.Add(listviewitem);

            listviewitem = new ListViewItem("WA");
            listviewitem.SubItems.Add("Washington");
            this.comboListView1.Items.Add(listviewitem);

            // Create column headers for the data.
            columnheader = new ColumnHeader();
            columnheader.Text = "State Abbr.";
            this.comboListView1.Columns.Add(columnheader);

            columnheader = new ColumnHeader();
            columnheader.Text = "State";
            this.comboListView1.Columns.Add(columnheader);

            foreach (ColumnHeader ch in this.comboListView1.Columns)
            {
                ch.Width = -2;
            }
            #endregion

            #region Datagridview-combobox
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = "ID";
            column.DataPropertyName = "id";//对应数据源的字段
            column.HeaderText = "ID";
            column.Width = 80;
            this.dataGridView1.Columns.Add(column);

            DataGridViewComboBoxColumn column1 = new DataGridViewComboBoxColumn();
            column1.Name = "Name";
            column1.DataPropertyName = "Name";//对应数据源的字段
            column1.HeaderText = "姓名";
            column1.Width = 80;
            this.dataGridView1.Columns.Add(column1);
            List<string> ListData = new List<string> { "张三", "里斯", "王六" };
            column1.DataSource = ListData; //这里需要设置一下combox的itemsource,以便combox根据数据库中对应的值自动显示信息

            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.Name = "Sex";
            column2.DataPropertyName = "Sex";//对应数据源的字段
            column2.HeaderText = "性别";
            column2.Width = 80;
            this.dataGridView1.Columns.Add(column2);

            //绑定数据源
            dataGridView1.DataSource = CreateTable();
            #endregion

        }

        //判断文件的目录是否存,不存则创建
        public static void FtpCheckDirectoryExist(string destFilePath, string ftpServerIP, string userName, string password)
        {
            string fullDir = FtpParseDirectory(destFilePath);
            string[] dirs = fullDir.Split('/');
            string curDir = "/";
            for (int i = 0; i < dirs.Length; i++)
            {
                string dir = dirs[i];
                //如果是以/开始的路径,第一个为空  
                if (dir != null && dir.Length > 0)
                {
                    try
                    {
                        curDir += dir + "/";
                        FtpMakeDir(curDir, ftpServerIP, userName, password);
                    }
                    catch (Exception)
                    { }
                }
            }
        }

        //创建目录
        public static Boolean FtpMakeDir(string localFile, string ftpServerIP, string userName, string password)
        {
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(ftpServerIP + localFile);
            req.Credentials = new NetworkCredential(userName, password);
            req.Method = WebRequestMethods.Ftp.MakeDirectory;
            try
            {
                FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                response.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                req.Abort();
                return false;
            }
            req.Abort();
            return true;
        }

        public static string FtpParseDirectory(string destFilePath)
        {
            return destFilePath.Substring(0, destFilePath.LastIndexOf("/"));
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public void Download(string filePath, string fileName)
        {
            /**/////上面的代码实现了从ftp服务器下载文件的功能
            try
            {
                //long allbye = (long)GetFileSize(fileName);
                string url = "ftp://" + ftpServerIP + "/" + fileName;
                Connect(url);//连接  
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount = 0;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                int startbye = 0;
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);

                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                    startbye += readCount;
                }

                ftpStream.Close();
                response.Close();
                outputStream.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("下载失败，原因: " + ex.Message);
            }
        }

        public string GetDirectoryListingRegexForUrl(string url)
        {
            if (url.Equals("http://192.168.137.1:81/test/test/"))
            //if (url.Equals("http://www.ibiblio.org/pub/"))
            {
                //return "<a href=\".*\">(?<name>.*)</a>";
                return " <A HREF=\".*\">(?<name>.*)</A>";
            }
            throw new NotSupportedException();
        }

        public void Upload(string filename) //上面的代码实现了从ftp服务器上载文件的功能
        {
            FileInfo fileInf = new FileInfo(filename);
            string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
            Connect(uri);//连接          
            // 默认为true，连接不会被关闭
            // 在一个命令之后被执行
            reqFTP.KeepAlive = false;
            // 指定执行什么命令
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            // 上传文件时通知服务器文件的大小
            reqFTP.ContentLength = fileInf.Length;
            // 缓冲大小设置为kb 
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            // 打开一个文件流(System.IO.FileStream) 去读上传的文件
            FileStream fs = fileInf.OpenRead();
            try
            {
                int allbye = (int)fileInf.Length;
                int startbye = 0;// 把上传的文件写入流
                Stream strm = reqFTP.GetRequestStream();//根据服务器的FTP配置不同，要使用不同的模式，否则会报错
                // 每次读文件流的kb 
                contentLen = fs.Read(buff, 0, buffLength);// 流内容没有结束
                while (contentLen != 0)
                {
                    // 把内容从file stream 写入upload stream 
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                    startbye += buffLength;
                }// 关闭两个流
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("上传失败,原因: " + ex.Message);

                fs.Close();
            }

        }

        /// <summary>
        /// ftp文件上传
        /// </summary>
        /// <param name="fileName">文件全路径</param>
        /// <param name="ftpServerIP">ftp地址</param>
        /// <param name="path">ftp文件夹路径</param>
        /// <param name="userName">ftp用户名</param>
        /// <param name="password">ftp密码</param>
        /// <returns></returns>
        public bool UploadFileByFtpWebRequest(string fileName, string ftpServerIP, string path, string userName, string password)
        {
            bool result = true;
            string etst = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
            string url = string.Format("ftp://{0}/{1}/{2}", ftpServerIP, path, etst);
            //FtpCheckDirectoryExist(path, "ftp://" + ftpServerIP, userName, password);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            request.KeepAlive = false;
            request.Credentials = new NetworkCredential(userName, password);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            Stream stream = null;
            FileStream fileStream = null;
            try
            {
                stream = request.GetRequestStream();
                fileStream = new FileStream(fileName, FileMode.Open);

                int packageSize = 1024 * 1024;
                int packageCount = (int)(fileStream.Length / packageSize);
                int rest = (int)(fileStream.Length % packageSize);

                for (int index = 0; index < packageCount; index++)
                {
                    byte[] buffer = new byte[packageSize];
                    fileStream.Read(buffer, 0, buffer.Length);
                    stream.Write(buffer, 0, buffer.Length);
                }

                if (rest != 0)
                {
                    byte[] buffer = new byte[rest];
                    fileStream.Read(buffer, 0, buffer.Length);
                    stream.Write(buffer, 0, buffer.Length);
                }
                stream.Close();
                fileStream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                result = false;
            }

            return result;
        }

        private static void _webClient_UploadDataCompleted(object sender, UploadDataCompletedEventArgs e)
        {
            MessageBox.Show("deleted!");
        }

        private void btnAddControl_Click(object sender, EventArgs e)
        {
            Button button = new Button();
            button.Text = "Text";
            //button.Height = 10;
            button.Location = new Point(10, count * 50);
            count++;
            this.pnlTestAddControl.Controls.Add(button);
        }

        private void btnFtpDown_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择下载路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string Dir = dialog.SelectedPath;

                //string fileName = "validationtypelist.xls";// URL.Substring(URL.LastIndexOf("\\") + 1); //被下载的文件名
                string fileName = "JS调用打印.png";// URL.Substring(URL.LastIndexOf("\\") + 1); //被下载的文件名

                try
                {
                    DownLoad(Dir, fileName, "test");
                    MessageBox.Show("下载成功，文件[ " + fileName + " ]也保存到[ " + Dir + " ]了，请查阅。");

                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, "Error");
                }
            }
            //Download("DLL打印.png");
        }

        private void btnFtpUp_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                UploadFileByFtpWebRequest(openFileDialog1.FileName.ToString(), ftpServerIP, "test/", ftpUserID, ftpPassword);
            }
        }

        private void btnHttpDelete_Click(object sender, EventArgs e)
        {
            //string result = CommonHttpRequest("http://192.168.137.1:81/test/test/js.png", "DELETE", "");
            //this.lblSize.Text = result;
            //deleteHttp();
            Dictionary<string, string> parameters = new Dictionary<string, string> {
                { "DocSetPath", "tets/test" }
            };    //参数列表
            string url = "http://localhost:20734/HttpDocDoUp.aspx";

            DeleteRequest(parameters, url);
        }

        private void btnHttpDeleteDoc_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string> {
                { "DocPath", "test/test/js.png" }
            };    //参数列表
            string url = "http://192.168.137.1:81/";
            MessageBox.Show(DeleteRequest(parameters, url));
        }
        private void btnHttpDown_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件下载路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string localFoldPath = dialog.SelectedPath + "\\";
                DownloadByHttp("http://192.168.137.1:81/" + "test/test/js.png", localFoldPath + "download.png");
                //string url =
            }
        }

        private void btnHttpGetName_Click(object sender, EventArgs e)
        {
            //string url = "http://www.ibiblio.org/pub/";
            string url = "http://192.168.137.1:81/test/test/";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string html = reader.ReadToEnd();
                    html = html.Replace("</A>", "</A>\n");
                    Regex regex = new Regex(GetDirectoryListingRegexForUrl(url));
                    MatchCollection matches = regex.Matches(html);
                    if (matches.Count > 0)
                    {
                        foreach (Match match in matches)
                        {
                            if (match.Success)
                            {
                                lblSize.Text = lblSize.Text + match.Groups["name"];
                            }
                        }
                    }
                }
            }

            //SortedList list = GetDirectoryContents(@"http://192.168.137.1:81/test/test/", true);
            //foreach (var item in list)
            //{
            //    lblSize.Text += item.ToString();
            //}


        }

        private void btnHttpUp_Click(object sender, EventArgs e)
        {
            string filePath = MyOpenFileDialog();
            string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
            //UpLoadFileToServer(filePath, "http://192.168.137.1:81/Http/", txtHttpPath.Text.Trim());
            //return;
            //UpLoadFileToServer(filePath, "//192.168.1.126:81/", "test");
            //if (!(Upload_Request2("http://192.168.137.1:81", filePath, txtHttpPath.Text.Trim()) == 1))
            if (!(Upload_Request2("http://localhost:20734/HttpDocDoUp.aspx", filePath, txtHttpPath.Text.Trim()) == 1))
            //if (!(Upload_Request2("http://192.168.1.126:81/HttpDocDoUp.aspx", filePath, "test2/上传测试2.png") == 1))
            {
                MessageBox.Show("错误");
            }
            //txtHttpPath.Text = fileName;
            //txtFileName.Text = fileName.Substring(fileName.LastIndexOf("\\") + 1);
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcesses();
            foreach (var item in processes)
            {
                rtbProcesses.Text = rtbProcesses.Text + item.ProcessName + "\r\n";
                listBoxProcess.Items.Add(item.ProcessName);
            }
        }

        private void btnSplitBy_Click(object sender, EventArgs e)
        {
            //string str = System.Text.RegularExpressions.Regex.Replace(@"带括号的(ddd)字符串(213)", @"(.*\()(.*)(\).*)", "$2");//
            //foreach (var item in getCodeToParam(rtbSourceName.Text.Trim()))
            //{
            //    rtbTargetName.Text += item;
            //}

            rtbTargetName.Text = method(rtbSourceName.Text);
        }

        private string method(string d)
        {
            return d.Replace("\t", ",");
        }

        private void btnStartProcess_Click(object sender, EventArgs e)
        {
            string ProcessName = tbprocess.Text;

            Process p = new Process();
            p.StartInfo.FileName = ProcessName;
            p.Start();
        }

        private void btnThread_Click(object sender, EventArgs e)
        {
            ThreadStart ts = new ThreadStart(PrintEven);
            Thread t = new Thread(ts);
            t.Start();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Button button = new Button();
            button.Text = "Text";
            //button.Height = 10;
            button.Location = new Point(10, count3 * 50);
            count3++;
            this.panel8.Controls.Add(button);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var fileNames = new List<string>();

            var reqFtp = (FtpWebRequest)WebRequest.Create(new Uri(this.txtftpserver.Text));
            //var reqFtp = (FtpWebRequest)WebRequest.Create(new Uri("ftp://47.97.154.144:6668/BSCCONFIGSET/Test/"));
            //var reqFtp = (FtpWebRequest)WebRequest.Create(new Uri("ftp://192.168.1.126:6667/BSCCONFIGSET/EmptyFolder/"));
            reqFtp.UsePassive = false;
            reqFtp.UseBinary = true;
            //reqFTP.EnableSsl = true;//加密方式传送数据 FTP 服务器要支持
            //reqFtp.ContentType
            //reqFtp.Credentials = new NetworkCredential("Administrator", "Admin123");
            reqFtp.Credentials = new NetworkCredential(txtftpusername.Text, txtftppassword.Text);
            reqFtp.Method = WebRequestMethods.Ftp.ListDirectory;
            reqFtp.Timeout = 2000;
            var response = (FtpWebResponse)reqFtp.GetResponse();
            var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string fileName = reader.ReadLine();
            while (fileName != null)
            {
                fileNames.Add(fileName);
                this.rtbftpfilename.Text += fileName;
                fileName = reader.ReadLine();
            }
            reader.Close();
            response.Close();
            //var fileNames = reader.ReadToEnd();
            reader.Close();
            response.Close();

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Form2 form = new Form2
            {
                TopMost = true
            };
            form.MinimizeBox = form.MaximizeBox = false;
            if (form.ShowDialog() == DialogResult.OK)
            {
                //
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择下载路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                DirectoryInfo theFolder = new DirectoryInfo(foldPath);

                //theFolder 包含文件路径

                FileInfo[] dirInfo = theFolder.GetFiles();
                //遍历文件夹                
                foreach (FileInfo file in dirInfo)
                {
                    MessageBox.Show(file.ToString());
                }
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Create a workbook object.
            Workbook workbook = new Workbook();

            // Get the first worksheet.
            Worksheet worksheet1 = workbook.Worksheets[0];

            // Add a new worksheet and access it.
            int i = workbook.Worksheets.Add();
            Worksheet worksheet2 = workbook.Worksheets[i];

            // Create a range in the second worksheet.
            Range range = worksheet2.Cells.CreateRange("E1", "E4");

            // Name the range.
            range.Name = "MyRange";

            // Fill different cells with data in the range.
            range[0, 0].PutValue("Blue");
            range[1, 0].PutValue("Red");
            range[2, 0].PutValue("Green");
            range[3, 0].PutValue("Yellow");

            // Get the validations collection.
            ValidationCollection validations = worksheet1.Validations;

            // Create a new validation to the validations list.
            Validation validation = validations[validations.Add()];

            // Set the validation type.
            validation.Type = Aspose.Cells.ValidationType.List;

            // Set the operator.
            validation.Operator = OperatorType.None;

            // Set the in cell drop down.
            validation.InCellDropDown = true;

            // Set the formula1.
            validation.Formula1 = "blue,red,green";

            // Enable it to show error.
            validation.ShowError = true;

            // Set the alert type severity level.
            validation.AlertStyle = ValidationAlertType.Stop;

            // Set the error title.
            validation.ErrorTitle = "Error";

            // Set the error message.
            validation.ErrorMessage = "Please select a color from the list";

            // Specify the validation area.
            CellArea area;
            area.StartRow = 0;
            area.EndRow = 4;
            area.StartColumn = 0;
            area.EndColumn = 0;

            // Add the validation area.
            validation.AreaList.Add(area);

            // Save the Excel file.
            workbook.Save("C:\\Users\\Administrator\\Desktop\\工具\\validationtypelist.xls");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Button button = new Button();
            button.Text = "Text";
            //button.Height = 10;
            button.Location = new Point(10, count2 * 50);
            count2++;
            this.panel7.Controls.Add(button);
        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupBox gpb = GetGroupBox();

            GroupBox gpb1 = GetGroupBox();
            GroupBox gpb2 = GetGroupBox();

            this.panel4.Controls.Add(gpb);
            this.panel4.Controls.Add(gpb1);
            this.panel4.Controls.Add(gpb2);
            //newpanel.Controls.Add(new Label() { Text = "123",Name = "newlabel"});
            //this.tableLayoutPanel1.ColumnStyles[0].Width = 50;
            //this.tableLayoutPanel1.ColumnStyles[1].Width = 0;
            //this.tableLayoutPanel1.ColumnStyles[2].Width = 50;
            //this.tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Percent;
            //this.tableLayoutPanel1.RowStyles[0]. = new Padding(10);
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            selectCombobox(comboBox1, listCombobox);
        }

        private void Connect(string path)
        {
            // 根据uri创建FtpWebRequest对象
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            // 指定数据传输类型
            reqFTP.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.UsePassive = false;
            // ftp用户名和密码
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void deleteHttp()
        {
            //定义_webClient对象
            WebClient _webClient = new WebClient();
            //使用Windows登录方式
            _webClient.Credentials = new NetworkCredential("iuser_test", "123");
            //待删除的文件链接地址（文件服务器）
            Uri _uri = new Uri(@"http://192.168.137.1:81/test/test/js.png");
            //注册删除完成时的事件（模拟删除）
            _webClient.UploadDataCompleted += _webClient_UploadDataCompleted;
            //异步从文件（模拟）删除文件
            _webClient.UploadDataAsync(_uri, "DELETE", new byte[0]);
        }

        private void DownLoad(string filePath, string fileName, string docpath)
        {
            FtpWebRequest reqFTP;
            try
            {
                //设置文件下载后的保存路径（文件夹）：filePath
                //命名下载后的文件名（可与原文件名不同）：fileName
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                string url = "ftp://" + ftpServerIP + "/" + docpath + "/" + fileName;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));

                //指定执行下载命令
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse(); //返回FTP服务器响应         
                Stream ftpStream = response.GetResponseStream();//检索从FTP服务器上发送的响应数据的流               
                long cl = response.ContentLength;//获取从FTP服务器上接收的数据的长度
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);//从当前流读取字节序列，并将此流中的位置提升读取的字节数
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);//使用从缓冲区中读取的数据，将字节块写入该流
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                //关闭两个流
                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void gBox_Enter(object sender, EventArgs e)
        {

        }

        private string[] getCodeToParam(string Code)
        {
            return Code.Split(new char[] { ',', ' ' });
            //string list = string.Empty;
            //string param_1 = Code;
            //while (param_1.IndexOf('{') >= 0)
            //{
            //    int first_code = param_1.IndexOf('{');
            //    param_1 = param_1.Substring(first_code + 1);
            //    int end_code = param_1.IndexOf('}');
            //    string param_2 = param_1.Substring(0, end_code);
            //    list += param_2;
            //}
            //return list;
        }

        private GroupBox GetGroupBox()
        {
            GroupBox gpb = new GroupBox
            {
                Dock = DockStyle.Top,
                Text = "123",
                BackColor = Color.White,
                Height = 50
            };
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripItem[] toolStripItems = new ToolStripItem[] {
                new ToolStripMenuItem{ Text = "test"},
            };
            contextMenuStrip.Items.AddRange(toolStripItems);
            gpb.ContextMenuStrip = contextMenuStrip;
            gpb.Controls.Add(new Label() { Text = "321", Location = new Point(20, 10), BackColor = Color.Red });
            return gpb;
        }

        private void listBoxProcess_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_Click(object sender, EventArgs e)
        {
            //SeniorTest seniorTest = new SeniorTest();
            //seniorTest
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            richTextBox1.Text += "Doubleclick";
        }

        private void Lodop_OnDeactivate()
        {
            MessageBox.Show("2");
        }

        private void Lodop_OnDestroy()
        {
            MessageBox.Show("2");
        }

        private string MyOpenFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "PDF文档(*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            else
            {
                return "";
            }
        }

        private void PrintEven()
        {
            for (int i = 0; i < 10; i += 2)
            {

            }
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tableLayoutPanel1.ColumnStyles[1].Width = 50;
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void stopProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string processName = listBoxProcess.SelectedItem.ToString();
            Process[] processes = Process.GetProcessesByName(processName);

            if (processes.Length > 0)
            {
                try
                {
                    foreach (var item in processes)
                    {
                        if (!item.HasExited)
                        {
                            item.Kill();
                            MessageBox.Show(item.ProcessName + "is Closed.");
                            processes = Process.GetProcesses();

                            listBoxProcess.Items.Clear();
                            foreach (var p in processes)
                            {
                                listBoxProcess.Items.Add(p);
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("this process cannot kill.");
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.richTextBox2.Text = (++key).ToString();
            //GetCode();
        }

        public class clss
        {
            public void a(RichTextBox etb)
            {
                etb.Text = 1.ToString();
            }

        }

        public class clss2 : clss
        {
            public new void a(RichTextBox rtb)
            {
                rtb.Text = 2.ToString();
            }
        }
        #region datagridview-combobox
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            string selectedItem = combo.Text;//拿到选择后的值
        }

        private DataTable CreateTable()
        {
            DataTable dt = new DataTable();                         //创建一个空表
            DataColumn column = new DataColumn();                    //创建一个空列     
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "id";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Name";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "Sex";
            dt.Columns.Add(column);
            DataRow row = dt.NewRow();                              //创建行
            row["id"] = 0;
            row["Name"] = "张三";
            row["Sex"] = 1;
            dt.Rows.Add(row);                                       //显示
            return dt;

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox combo = e.Control as ComboBox;
            if (combo != null)
            {
                combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            }
        }
        #endregion

        #region combobox 查询


        //得到Combobox的数据，返回一个List
        public List<string> getComboboxItems(ComboBox cb)
        {
            //初始化绑定默认关键词
            List<string> listOnit = new List<string>();
            //将数据项添加到listOnit中
            for (int i = 0; i < cb.Items.Count; i++)
            {
                listOnit.Add(cb.Items[i].ToString());
            }
            return listOnit;
        }
        //模糊查询Combobox
        public void selectCombobox(ComboBox cb, List<string> listOnit)
        {
            //输入key之后返回的关键词
            List<string> listNew = new List<string>();
            //清空combobox
            cb.Items.Clear();
            //清空listNew
            listNew.Clear();
            //遍历全部备查数据
            foreach (var item in listOnit)
            {
                if (item.Contains(cb.Text))
                {
                    //符合，插入ListNew
                    listNew.Add(item);
                }
            }
            //combobox添加已经查询到的关键字
            cb.Items.AddRange(listNew.ToArray());
            //设置光标位置，否则光标位置始终保持在第一列，造成输入关键词的倒序排列
            cb.SelectionStart = cb.Text.Length;
            //保持鼠标指针原来状态，有时鼠标指针会被下拉框覆盖，所以要进行一次设置
            Cursor = Cursors.Default;
            //自动弹出下拉框
            cb.DroppedDown = true;
        }
        #endregion

        #region Excel导入导出
        public void dataTableToListview(ListView listView1, DataTable dt)
        {
            if (dt != null)
            {
                listView1.Items.Clear();
                listView1.Columns.Clear();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ColumnHeader c1 = new ColumnHeader
                    {
                        Width = 100,
                        Text = dt.Rows[0][i].ToString()
                    };
                    listView1.Columns.Add(c1);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    string header = dr[0].ToString();
                    if (header == "姓名")
                    {

                        continue;
                    }
                    ListViewItem lvi = new ListViewItem();
                    lvi.SubItems[0].Text = dr[0].ToString();

                    for (int i = 1; i < dt.Columns.Count; i++)
                    {
                        lvi.SubItems.Add(dr[i].ToString());
                    }

                    listView1.Items.Add(lvi);
                }
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        public DataTable listViewToDataTable(ListView listView1, DataTable dt)
        {
            int i, j;
            DataRow dr;
            dt.Clear();
            dt.Columns.Clear();
            //生成DataTable列头
            for (i = 0; i < listView1.Columns.Count; i++)
            {
                dt.Columns.Add(listView1.Columns[i].Text.Trim(), typeof(String));
            }
            //每行内容
            for (i = 0; i < listView1.Items.Count; i++)
            {
                dr = dt.NewRow();
                for (j = 0; j < listView1.Columns.Count; j++)
                {
                    dr[j] = listView1.Items[i].SubItems[j].Text.Trim();
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// 导出datatable到excel 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = listViewToDataTable(listView1, dt);
            if (dt != null)
            {
                //dt = dataSet1.Tables[0];
            }
            else
            {
                MessageBox.Show("没有数据记录", "*^_^* 温馨提示信息", MessageBoxButtons.OK);
                return;
            }
            //上面只是取datatable，你自己diy 

            saveFileDialog1.Filter = "导出Excel (*.xls)|*.xls|Word (*.doc)|*.doc";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.CreatePrompt = true;
            saveFileDialog1.Title = "导出文件保存路径";
            //saveFileDialog1.ShowDialog(); 
            //string strName = saveFileDialog1.FileName; 
            //设置默认文件类型显示顺序 
            //saveFileDialog1.FilterIndex = 2; 
            //保存对话框是否记忆上次打开的目录 
            saveFileDialog1.RestoreDirectory = true;
            //点了保存按钮进入 
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //获得文件路径 
                string localFilePath = saveFileDialog1.FileName.ToString();
                //获取文件名，不带路径 
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                //获取文件路径，不带文件名 
                string FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                //给文件名前加上时间 
                string newFileName = DateTime.Now.ToString("yyyyMMdd") + fileNameExt;
                //在文件名里加字符 
                //saveFileDialog1.FileName.Insert(1,"dameng"); 
                saveFileDialog1.FileName = FilePath + "\\" + newFileName;
            }
            AsposeExcel tt = new AsposeExcel(saveFileDialog1.FileName, "");//不用模板, saveFileDialog1是什么？上面已经说过 
            bool OK_NO = tt.DatatableToExcel(dt);
            if (OK_NO)
            {
                MessageBox.Show("导出成功", "*^_^* 温馨提示信息", MessageBoxButtons.OK);
            }
            else
            {
            }
        }

        /// <summary>
        /// Excel导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string localFilePath = "";
            //点了保存按钮进入 
            if (openFileDialog1.ShowDialog() == DialogResult.OK)// openFileDialog1不要再问我这是什么！ 
            {
                //获得文件路径 
                localFilePath = openFileDialog1.FileName.ToString();
            }
            AsposeExcel tt = new AsposeExcel(localFilePath);
            DataTable dt;
            try
            {
                dt = tt.ExcelToDatatalbe();
            }
            catch (Exception)
            {
                return;
            }
            dataTableToListview(listView1, dt);


            //有了datatable你自己就可以DIY啦,下面是我自己的你不用理 
            //if (ddlResidence.SelectedValue == "违章确认")
            //{
            //    if (dt.Rows[0][9].ToString() != "违章确认")
            //    {
            //        return;
            //    }
            //    row = dt.Rows.Count;
            //    if (row <= 0) return;
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        bllistView1iola.Up_Confirmed_ByVnum(dt.Rows[i][6].ToString(), dt.Rows[i][9].ToString());
            //    }
            //    this.GridView1.DataSource = dt;
            //    GridView1.DataBind();
            //}
        }
        #endregion
        #region LOdop

        public string GetCode()
        {
            string lb = this.webBrowser1.Document.GetElementById("lbPrintCode").InnerText;
            richTextBox1.Text = lb;
            return lb;
        }

        //save as template
        private void BtnSaveTemp_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(this.richTextBox1.Text.Trim()))
            //{
            //    string templateText = richTextBox1.Text.Trim();
            //    //save templateText with SQL
            //}
            List<string> list = new List<string>();
            string param_1 = richTextBox1.Text;
            while (param_1.IndexOf('@') > 0)
            {
                int first_code = param_1.IndexOf('@');
                param_1 = param_1.Substring(first_code + 1);
                int end_code = param_1.IndexOf('"');
                string param_2 = param_1.Substring(0, end_code);
                list.Add(param_2);
            }
            //richTextBox2.Text = list[0] + "/n" + list[1];

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //PrintData printdata = new PrintData();
            //for (int i = 0; i < 3; i++)
            //{
            //    printdata.PrintBarcode("", "Template1");
            //}


            //this.webBrowser1.Navigate(new Uri(LodopURI));

            webBrowser1.Document.InvokeScript("prn_Design");
            //this.timer1.Interval = 1000;
            //this.timer1.Start();

            //lodop.PRINT_DESIGN();
            //lodop.OnDestroy += Lodop_OnDestroy; 
            //lodop.OnDeactivate += Lodop_OnDeactivate;

            //lodop.OnDestroy += Lodop_OnDestroy;
            //Lodop.LodopXClass MyLodop = new Lodop.LodopXClass();

            //MyLodop.ADD_PRINT_TEXT(10, 10, 100, 20, "新加文本1");
            //MyLodop.PREVIEW();

        }
        private void button4_Click(object sender, EventArgs e)
        {
            //GetCode();
        }
        private void Button5_Click(object sender, EventArgs e)
        {
            string printcode = richTextBox1.Text.Trim();
            //预览
            HtmlElement element = this.webBrowser1.Document.CreateElement("script");
            element.SetAttribute("type", "text/javascript");
            element.SetAttribute("text", "function print_design_2(){" + printcode +
                "LODOP.PRINT_DESIGN(); " +
                "LODOP.On_Return = function (TaskID, Value) {" +
                   " if (Value)" +
                "{" +
                   " code = Value;" +
                "}" +
                "else" +
                "{" +
                   " code = '';" +
                "}" +
                "document.getElementById('lbPrintCode').innerText = code;" + "window.external.GetCode();"+
          "  };};");
            webBrowser1.Document.Body.AppendChild(element);
            webBrowser1.Document.InvokeScript("print_design_2");
        }
        #endregion
        #region 双色球


        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object Num_Lock = new object();

        /// <summary>
        /// 标识是否开始摇奖
        /// </summary>
        private static bool IsBegin = true;
        private string[] BuleNum = {
            "01","02","03","04","05","06","07","08","09","10","11","12","13",
            "14","15","16"
        };

        private string[] RedNum = {
            "01","02","03","04","05","06","07","08","09","10","11","12","13",
            "14","15","16","17","18","19","20","21","22","23","24","25","26",
            "27","28","29","30","31","32","33"
        };
        private void button6_Click(object sender, EventArgs e)
        {
            TaskFactory taskFactory = new TaskFactory();
            List<Task> taskList = new List<Task>();
            IsBegin = true;
            this.btnStart.Enabled = false;

            // Thread.Sleep(1000);
            this.btnEnd.Enabled = true;
            foreach (Control item in gBox.Controls)
            {
                if (item is Label)
                {
                    Label lbl = (Label)item;
                    taskList.Add(taskFactory.StartNew(
                     () =>
                     {
                         while (IsBegin)
                         {
                             this.UpdateNum(lbl);
                         }

                     }));
                }
            }
            taskFactory.ContinueWhenAll(taskList.ToArray(), tList => this.ShowNumber());//等所有线程操作完毕后才显示中奖号。
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!this.GetLabelList().Contains("00"))
            {
                IsBegin = false;
                this.btnEnd.Enabled = false;
                this.btnStart.Enabled = true;

            }
            else
            {
                MessageBox.Show("请慢一点，稍后再试");
            }
        }

        /// <summary>
        /// 获取当前已经抽出的双色球，防止重复
        /// </summary>
        /// <returns>所有控件的值</returns>
        private List<string> GetLabelList()
        {
            List<string> strList = new List<string>();
            foreach (Control item in gBox.Controls)
            {
                if (item is Label)
                {
                    Label label = (Label)item;
                    strList.Add(label.Text);
                }
            }
            return strList;
        }

        private void ShowNumber()
        {
            MessageBox.Show(string.Format("结果是 {0} {1} {2} {3} {4} {5}  {6}",
                  lbRed1.Text, lbRed2.Text, lbRed3.Text, lbRed4.Text, lbRed5.Text, lbRed6.Text, lbBlue.Text));
        }

        /// <summary>
        /// 通过主线程修改UI
        /// </summary>
        /// <param name="lbl">修改的Label</param>
        /// <param name="text">修改的值</param>
        private void UpdateLbl(Label lbl, string text)
        {
            if (lbl.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    lbl.Text = text;
                    //Thread.Sleep(2000);
                    //Console.WriteLine($"当前UpdateLbl线程id{Thread.CurrentThread.ManagedThreadId}");
                }));//交给UI线程去更新
            }
            else
            {
                lbl.Text = text;
            }
        }

        private void UpdateNum(Label lbl)
        {
            RandomHelper randomHelper = new RandomHelper();

            if (lbl.Name.Contains("Blue"))
            {
                int num = randomHelper.GetNum(0, 16);
                string blueText = BuleNum[num];
                this.UpdateLbl(lbl, blueText);
            }
            else
            {
                int num = randomHelper.GetNum(0, 33);
                string redText = RedNum[num];
                lock (Num_Lock)
                {
                    List<string> list = this.GetLabelList();
                    if (list.Contains(redText))
                    {
                        return;
                    }
                    else
                    {
                        this.UpdateLbl(lbl, redText);
                    }
                }
            }
        }
        #endregion
        #region comboboclistview
        private ListViewItem lvItem;
        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the user presses ESC.
            switch (e.KeyChar)
            {
                case (char)(int)Keys.Escape:
                    {
                        // Reset the original text value, and then hide the ComboBox.
                        this.comboBox2.Text = lvItem.Text;
                        this.comboBox2.Visible = false;
                        break;
                    }

                case (char)(int)Keys.Enter:
                    {
                        // Hide the ComboBox.
                        this.comboBox2.Visible = false;
                        break;
                    }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set text of ListView item to match the ComboBox.
            lvItem.Text = this.comboBox2.Text;

            // Hide the ComboBox.
            this.comboBox2.Visible = false;
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            // Set text of ListView item to match the ComboBox.
            lvItem.Text = this.comboBox2.Text;

            // Hide the ComboBox.
            this.comboBox2.Visible = false;
        }
        private void comboListView1_Click(object sender, EventArgs e)
        {
            //this.myListView1.Select();
            this.comboBox2.Visible = false;
        }

        private void comboListView1_MouseUp(object sender, MouseEventArgs e)
        {
            lvItem = this.comboListView1.GetItemAt(e.X, e.Y);

            // Make sure that an item is clicked.
            if (lvItem != null)
            {

                // Get the bounds of the item that is clicked.
                Rectangle ClickedItem = lvItem.Bounds;

                //单击ListView第一列显示ComBoBox控件
                if (e.X > this.comboListView1.Columns[0].Width)
                {
                    return;
                }
                // Verify that the column is completely scrolled off to the left.
                if ((ClickedItem.Left + this.comboListView1.Columns[0].Width) < 0)
                {
                    // If the cell is out of view to the left, do nothing.
                    return;
                }                 // Verify that the column is partially scrolled off to the left.
                else if (ClickedItem.Left < 0)
                {
                    // Determine if column extends beyond right side of ListView.
                    if ((ClickedItem.Left + this.comboListView1.Columns[0].Width) > this.comboListView1.Width)
                    {
                        // Set width of column to match width of ListView.
                        ClickedItem.Width = this.comboListView1.Width;
                        ClickedItem.X = 0;
                    }
                    else
                    {
                        // Right side of cell is in view.
                        ClickedItem.Width = this.comboListView1.Columns[0].Width + ClickedItem.Left;
                        ClickedItem.X = 2;
                    }
                }
                else if (this.comboListView1.Columns[0].Width > this.comboListView1.Width)
                {
                    ClickedItem.Width = this.comboListView1.Width;
                }
                else
                {
                    ClickedItem.Width = this.comboListView1.Columns[0].Width;
                    ClickedItem.X = 2;
                }

                // Adjust the top to account for the location of the ListView.
                ClickedItem.Y += this.comboListView1.Top;
                ClickedItem.X += this.comboListView1.Left;

                // Assign calculated bounds to the ComboBox.
                this.comboBox2.Bounds = ClickedItem;

                // Set default text for ComboBox to match the item that is clicked.
                this.comboBox2.Text = lvItem.Text;

                // Display the ComboBox, and make sure that it is on top with focus.
                this.comboBox2.Visible = true;
                this.comboBox2.BringToFront();
                this.comboBox2.Focus();


            }
        }
        #endregion
        #region 获取文件名
        public static SortedList GetDirectoryContents(string url, bool deep)

        {

            //Retrieve the File

            HttpWebRequest Request = (HttpWebRequest)HttpWebRequest.Create(url);

            Request.Headers.Add("Translate: f");

            Request.Credentials = CredentialCache.DefaultCredentials;



            string requestString = "<?xml version=/‘1.0 /’ encoding=/‘utf-8/’?>" +

                  "<a:propfind xmlns:a=/‘DAV:/ ‘>" +

                  "<a:prop>" +

                  "<a:displayname/>" +

                  "<a:iscollection/>" +

                  "<a:getlastmodified/>" +

                  "</a:prop>" +

                  "</a:propfind>";



            Request.Method = "PROPFIND";

            if (deep == true)

                Request.Headers.Add("Depth: infinity");

            else

                Request.Headers.Add("Depth: 1");

            Request.ContentLength = requestString.Length;

            Request.ContentType = "text/xml";



            Stream requestStream = Request.GetRequestStream();

            requestStream.Write(Encoding.ASCII.GetBytes(requestString), 0, Encoding.ASCII.GetBytes(requestString).Length);

            requestStream.Close();



            HttpWebResponse Response;

            StreamReader respStream;

            try

            {

                Response = (HttpWebResponse)Request.GetResponse();

                respStream = new StreamReader(Response.GetResponseStream());

            }

            catch (WebException e)

            {

                Debug.WriteLine("错误" + url);

                throw e;

            }



            StringBuilder SB = new StringBuilder();



            char[] respChar = new char[1024];

            int BytesRead = 0;



            BytesRead = respStream.Read(respChar, 0, 1024);



            while (BytesRead > 0)

            {

                SB.Append(respChar, 0, BytesRead);

                BytesRead = respStream.Read(respChar, 0, 1024);

            }

            respStream.Close();



            XmlDocument XmlDoc = new XmlDocument();

            XmlDoc.LoadXml(SB.ToString());



            XmlNamespaceManager nsmgr = new XmlNamespaceManager(XmlDoc.NameTable);

            nsmgr.AddNamespace("a", "DAV:");



            XmlNodeList NameList = XmlDoc.SelectNodes("//a:prop/a:displayname", nsmgr);

            XmlNodeList isFolderList = XmlDoc.SelectNodes("//a:prop/a:iscollection", nsmgr);

            XmlNodeList LastModList = XmlDoc.SelectNodes("//a:prop/a:getlastmodified", nsmgr);

            XmlNodeList HrefList = XmlDoc.SelectNodes("//a:href", nsmgr);



            SortedList ResourceList = new SortedList();

            Resource tempResource;



            for (int i = 0; i < NameList.Count; i++)

            {

                if (HrefList[i].InnerText.ToLower(new CultureInfo("en-US")).TrimEnd(new char[] { '/' }) != url.ToLower(new CultureInfo("en-US")).TrimEnd(new char[] { '/' }))

                {

                    tempResource = new Resource();

                    tempResource.Name = NameList[i].InnerText;

                    tempResource.IsFolder = Convert.ToBoolean(Convert.ToInt32(isFolderList[i].InnerText));

                    tempResource.Url = HrefList[i].InnerText;

                    tempResource.LastModified = Convert.ToDateTime(LastModList[i].InnerText);

                    ResourceList.Add(tempResource.Url, tempResource);

                }

            }

            return ResourceList;

        }

        public class Resource

        {

            public bool IsFolder;
            public DateTime LastModified;
            public string Name;
            public string Url;
        }
        #endregion

        #region 文件上传

      

        public void DownloadByHttp(string URL, string Dir)
        {
            WebClient client = new WebClient();
            //string fileName = URL.Substring(URL.LastIndexOf("/") + 1); //被下载的文件名  

            //string Path = Dir + fileName;   //另存为的绝对路径＋文件名  

            try
            {
                client.DownloadFile(URL, Dir);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private string BuildQuery(IDictionary<string, string> parameters, string encode)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
                if (!string.IsNullOrEmpty(name))
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }
                    postData.Append(name);
                    postData.Append("=");
                    if (encode == "gb2312")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.GetEncoding("gb2312")));
                    }
                    else if (encode == "utf8")
                    {
                        postData.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
                    }
                    else
                    {
                        postData.Append(value);
                    }
                    hasParam = true;
                }
            }
            return postData.ToString();
        }

        private string DeleteRequest(Dictionary<string, string> parameters, string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "post";    //设置为post请求
            request.ReadWriteTimeout = 5000;
            request.KeepAlive = false;
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters, "utf8"));   //使用utf-8格式组装post参数
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            StreamReader sr = new StreamReader(s);
            String sReturnString = sr.ReadLine();
            s.Close();
            sr.Close();
            return sReturnString;
        }

        /// <summary>   
        /// 将本地文件上传到指定的服务器(HttpWebRequest方法)   
        /// </summary>   
        /// <param name="address">文件上传到的服务器</param>   
        /// <param name="fileNamePath">要上传的本地文件（全路径）</param>   
        /// <param name="saveName">文件上传后的名称</param>   
        /// <returns>成功返回1，失败返回0</returns>   
        private int Upload_Request2(string address, string fileNamePath, string saveName)
        {
            int returnValue = 0;     // 要上传的文件   
            FileStream fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);     //时间戳   
            string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + strBoundary + "\r\n");     //请求头部信息   
            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(strBoundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append("file");
            sb.Append("\"; filename=\"");
            sb.Append(saveName);
            sb.Append("\";");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append("application/octet-stream");
            sb.Append("\r\n");
            sb.Append("\r\n");
            string strPostHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(strPostHeader);     
            // 根据uri创建HttpWebRequest对象   
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(address));
            httpReq.Method = "POST";       
            //对发送的数据不使用缓存 
            httpReq.AllowWriteStreamBuffering = false;    
            //设置获得响应的超时时间（300秒）
            httpReq.Timeout = 300000;    
            httpReq.ContentType = "multipart/form-data; boundary=" + strBoundary;
            long length = fs.Length + postHeaderBytes.Length + boundaryBytes.Length;
            httpReq.ContentLength = length;
            try
            {
                //每次上传4k  
                int bufferLength = 4096;
                byte[] buffer = new byte[bufferLength]; //已上传的字节数   
                DateTime startTime = DateTime.Now;
                int size = r.Read(buffer, 0, bufferLength);
                Stream postStream = httpReq.GetRequestStream();         //发送请求头部消息   
                postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                while (size > 0)
                {
                    postStream.Write(buffer, 0, size);
                    Application.DoEvents();
                    size = r.Read(buffer, 0, bufferLength);
                }
                //添加尾部的时间戳   
                postStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                postStream.Close();         //获取服务器端的响应   
                WebResponse webRespon = httpReq.GetResponse();
                Stream s = webRespon.GetResponseStream();
                //读取服务器端返回的消息  
                StreamReader sr = new StreamReader(s);
                String sReturnString = sr.ReadLine();
                s.Close();
                sr.Close();
                if (sReturnString == "Success")
                {
                    returnValue = 1;
                }
                else if (sReturnString == "Error")
                {
                    returnValue = 0;
                }
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
                returnValue = 0;
            }
            finally
            {
                fs.Close();
                r.Close();
            }
            return returnValue;
        }
        #endregion

        private void btnLinklist_Click(object sender, EventArgs e)
        {
            LinkList<int> list = new LinkList<int>();
            list.AddHead(1);
            list.AddEnd(2);
            list.AddHead(3);
            //list.AddEnd(4);
            //list.AddEnd(5);
            //list.AddEnd(6);
            //list.AddEnd(7);
            //list.DeleteHead();
            //list.DeleteEnd();
            //list.DeleteEnd();

            for (int i = 0; i < list.Count(); i++)
            {
                rtbLinkList.Text += list.GetEle(i).ToString()+ "\n";
            }
        }

        #region 进度条

        delegate void AsynUpdateUI(int step);//建立一个委托来实现非创建控件的线程更新控件

        private void btnTestProcessBar_Click(object sender, EventArgs e)
        {
            int taskCount = 10000;
            this.pgbWrite.Maximum = taskCount;
            this.pgbWrite.Value = 0;

            DataWrite dataWrite = new DataWrite();
            dataWrite.UpdateUIDelegate += UpdateUIStatus;
            dataWrite.TaskCallBack += Accomplish;

            Thread thread = new Thread(new ParameterizedThreadStart(dataWrite.Write));
            thread.IsBackground = true;
            thread.Start(taskCount);
        }

        //更新UI
        private void UpdateUIStatus(int step)
        {
            if (InvokeRequired)
            {
                this.Invoke(new AsynUpdateUI(delegate (int s) 
                {
                    this.pgbWrite.Value += s;
                    this.lblWriteStatus.Text = this.pgbWrite.Value.ToString() + "/" + this.pgbWrite.Maximum.ToString();
                }), step);
            }
            else
            {
                this.pgbWrite.Value += step;
                this.lblWriteStatus.Text = this.pgbWrite.Value.ToString() + "/" + this.pgbWrite.Maximum.ToString();
            }
        }

        //完成任务时需要
        private void Accomplish()
        {
            MessageBox.Show("Completed！");
        }
        #endregion

        #region 进度条--BackgroundWorker
        
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            startAssembly(e);
            //for (int i = 0; i < 100; i++)
            //{
            //    if (backgroundWorker1.CancellationPending)
            //    {
            //        e.Cancel = true;
            //        break;
            //    }
            //    Thread.Sleep(20);
            //    backgroundWorker1.ReportProgress((i + 1), (i + 1).ToString() + "/100");
            //}
        }

        private void startAssembly(DoWorkEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                Thread.Sleep(20);
                backgroundWorker1.ReportProgress((i + 1), (i + 1).ToString() + "/100");
            }
        }
        
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
            }else if (e.Cancelled)
            {

            }
            else
            {

            }
        }
        private void btnProgressBarWorker_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.RunWorkerAsync();
            progressForm process = new progressForm(this.backgroundWorker1);
            process.ShowDialog();
        }
        #endregion


    }
}
