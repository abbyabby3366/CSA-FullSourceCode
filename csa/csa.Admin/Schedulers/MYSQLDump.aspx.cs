using csa.DataLogic;
using csa.Library;
using Devart.Data.MySql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csa.Admin.Schedulers
{
    public partial class MYSQLDump : System.Web.UI.Page
    {
        string appName = "CSA";
        string appPwd = "csa123";
        string connectionStringName = "CsaEntitiesConnectionString";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["pwd"] == appPwd)
                {
                    RunMySQLDump();
                }
                else
                    Response.Write("no action running");
            }
        }

        private void RunMySQLDump()
        {
            string host = "127.0.0.1";
            if(Request.Params["h"] != null) host = Request.Params["h"];
            string fileName = "bk";
            var adminEmail = ConfigurationManager.AppSettings["MySQLDumpAdminEmail"];
            try
            {
                CheckDisk();
                if (!adminEmail.IsEmpty()) EmailBiz.DirectEmail(new CsaModel.CsaEntities(), new EmailNewDirectData(adminEmail, $"{appName} - MySQLDump Start", $"Scheduler started at {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}"));

                DateTime dt = DateTime.Now;
                fileName = string.Format("{0} {1}.{2}.{3} {4}-{5}", fileName, dt.Day, dt.Month, dt.Year, dt.Hour, dt.Minute);

                string command = ConfigurationManager.AppSettings["MySQLDumpExePath"];

                string fullConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;

                // Isolate the provider connection string
                string[] parts = fullConnectionString.Split(new[] { "provider connection string=\"" }, StringSplitOptions.None);
                string connectionString = parts[1].TrimEnd('"');
                MySqlConnectionStringBuilder con = new MySqlConnectionStringBuilder(connectionString);
                string myUser = con.UserId;
                string myPass = con.Password;
                string myDB = con.Database;
                int myPort = con.Port;

                var fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory);
                string folderBackup = $"C:\\backup\\{fileInfo.Directory.Name}\\";
                string folderBackupArchive = $"C:\\backup\\archive\\{fileInfo.Directory.Name}\\";

                DataLogic.Library.FileHelper.CheckDirectory(folderBackup);
                DataLogic.Library.FileHelper.CheckDirectory(folderBackupArchive);

                string pathSource = "-u{user} -p{pass} -h{h} --port={port} {db} --events -r \"{folderBackup}{filename}.sql\"";
                pathSource = pathSource.Replace("{user}", myUser);
                pathSource = pathSource.Replace("{pass}", myPass);
                pathSource = pathSource.Replace("{h}", host);
                pathSource = pathSource.Replace("{db}", myDB);
                pathSource = pathSource.Replace("{folderBackup}", folderBackup);
                pathSource = pathSource.Replace("{filename}", fileName);
                pathSource = pathSource.Replace("{port}", myPort.ToString());

                string arguments = pathSource;

                //string.Format("-udump -pAAaa1111 -h172.26.9.46 live_partner --events -r \"C:\\database_backup\\partner.asiaemall.com\\{0}.sql\"", fileName);
                //string arguments = string.Format("-udump -pAAaa1111 -h127.0.0.1 LIVE* --events -r \"C:\\database_backup\\partner.asiaemall.com\\{0}.sql\"", fileName);                

                Process MySQLDump = new Process();
                ProcessStartInfo info = new ProcessStartInfo(command);// mysqldump is in environment path 
                info.Arguments = arguments;
                info.UseShellExecute = false;
                info.RedirectStandardInput = true;
                info.RedirectStandardError = true;

                Process p = Process.Start(info);
                p.WaitForExit();

                string fileNameZipExtension = $"{fileName}.zip";
                string filePathZip = $"{folderBackupArchive}{fileNameZipExtension}";

                // compress
                using (ZipArchive newFile = ZipFile.Open(filePathZip, ZipArchiveMode.Create))
                {
                    newFile.CreateEntryFromFile($"{folderBackup}{fileName}.sql", fileName + ".sql", CompressionLevel.Fastest);
                }

                // delete original sql file
                FileInfo fi = new FileInfo($"{folderBackup}{fileName}.sql");
                fi.Delete();

                if (!adminEmail.IsEmpty()) EmailBiz.DirectEmail(new CsaModel.CsaEntities(), new EmailNewDirectData(adminEmail, $"{appName} - MySQLDump End", $"Scheduler ended at {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}"));                

                // delete files older than 6 months
                string[] files = Directory.GetFiles(folderBackupArchive);
                int iCnt = 0;

                foreach (string file in files)
                {
                    FileInfo fiold = new FileInfo(file);

                    fiold.Refresh();

                    if (fiold.LastWriteTime <= DateTime.Now.AddMonths(-6))
                    {
                        fiold.Delete();
                        iCnt += 1;
                    }
                }

                if (iCnt > 0)
                    Response.Write(iCnt + " old files deleted.<br />");

                try
                {
                    Response.Write(fileName);
                }
                catch { }
                try
                {
                    Response.Write(p.StandardError.ReadToEnd());

                }
                catch { }

                //var systemEmails = settingBiz.Get("SystemEmails")?.Value;

                //Task.Run(async () =>
                //{
                //    Locust.AWS.AmazonUploader.Response res = await Locust.AWS.AmazonUploader.UploadDbZipFileAsync(bucketName: "xnergybucket", keyName: fileNameZipExtension, filePath: filePathZip);
                //    if (res.Success)
                //    {
                //        if (!systemEmails.IsEmpty())
                //            emailBiz.CreateEmail(systemEmails, $"{appName} - MySQLDump.Zip Uploaded To S3 - Done", $"Done at {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
                //    }
                //    else
                //    {
                //        if (!systemEmails.IsEmpty())
                //            emailBiz.CreateEmail(systemEmails, $"{appName} - MySQLDump.Zip Uploaded To S3 - Failed", $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}<br><br>{res.Message}");
                //    }
                //});
            }
            catch (Exception ex)
            {
                if (!adminEmail.IsEmpty()) EmailBiz.DirectEmail(new CsaModel.CsaEntities(), new EmailNewDirectData(adminEmail, $"{appName} - MySQLDump Exception", $"{ex.Message} at {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}"));                
            }

        }

        private void CheckDisk()
        {
            var adminEmail = ConfigurationManager.AppSettings["MySQLDumpAdminEmail"];
            bool sendAlertEmail = false;

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            List<string> listMessage = new List<string>();
            listMessage.Add($"Date: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");

            foreach (DriveInfo d in allDrives)
            {
                listMessage.Add("--------------------------------------------------");
                listMessage.Add(string.Format("Drive {0}", d.Name));
                listMessage.Add(string.Format("  Drive type: {0}", d.DriveType));
                if (d.IsReady == true)
                {
                    if (d.AvailableFreeSpace < 21474836480)
                        sendAlertEmail = true;


                    listMessage.Add(string.Format("  Volume label: {0}", d.VolumeLabel));
                    listMessage.Add(string.Format("  File system: {0}", d.DriveFormat));
                    listMessage.Add(string.Format(
                        "  Available space to current user: {0:N} GB",
                        d.AvailableFreeSpace / 1073741824));

                    listMessage.Add(string.Format(
                        "  Total available space:          {0:N} GB",
                        d.TotalFreeSpace / 1073741824));

                    listMessage.Add(string.Format(
                        "  Total size of drive:            {0:N} GB ",
                        d.TotalSize / 1073741824));
                }
            }

            if (sendAlertEmail)
            {
                string message = string.Join(System.Environment.NewLine, listMessage);
                if (!adminEmail.IsEmpty()) EmailBiz.DirectEmail(new CsaModel.CsaEntities(), new EmailNewDirectData(adminEmail, $"{appName} - Disk Warning", message));
            }

        }
    }
}