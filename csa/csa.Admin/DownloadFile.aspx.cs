using csa.DataLogic.Library;
using csa.Member.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csa.Admin
{
    public partial class DownloadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var name = Request.Params["n"];
                var file = Request.Params["f"];
                var type = Request.Params["t"];

                FileHelper.FileDir fileDir = FileHelper.FileDir.ApplicationPreCheckingDir;
                switch (type)
                {
                    case "application_prechecking":
                        fileDir = FileHelper.FileDir.ApplicationPreCheckingDir;
                        break;
                    case "application_preparation":
                        fileDir = FileHelper.FileDir.ApplicationPreparationDir;
                        break;
                    case "application_document":
                        fileDir = FileHelper.FileDir.ApplicationDocumentDir;
                        break;
                    case "application_zoomacceptance":
                        fileDir = FileHelper.FileDir.ApplicationZoomAcceptanceDir;
                        break;
                    case "application_settlement":
                        fileDir = FileHelper.FileDir.ApplicationSettlementDir;
                        break;
                    case "application_ccris":
                        fileDir = FileHelper.FileDir.ApplicationCcrisDir;
                        break;
                    case "application_queue":
                        fileDir = FileHelper.FileDir.ApplicationQueueDir;
                        break;
                    case "application_reloan":
                        fileDir = FileHelper.FileDir.ApplicationReloanDir;
                        break;
                    case "application_collection":
                        fileDir = FileHelper.FileDir.ApplicationCollectionDir;
                        break;
                    case "application":
                        fileDir = FileHelper.FileDir.ApplicationDir;
                        break;
                    case "member":
                        fileDir = FileHelper.FileDir.MemberDir;
                        break;
                    case "icFile":
                        fileDir = FileHelper.FileDir.IcFileDir;
                        break;
                    default:
                        break;
                }
                string path = FileHelper.GetUploadPhysicalFullPath(AppSettings.UploadPath, fileDir, file);

                if (!File.Exists(path))
                {
                    throw new FileNotFoundException("The specified file was not found.");
                }

                var byteArray = File.ReadAllBytes(path);

                Response.Clear();
                Response.AddHeader("Content-disposition", "attachment; filename=" + name);
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(byteArray);
                Response.End();
            }
        }
    }
}