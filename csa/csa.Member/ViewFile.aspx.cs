using csa.DataLogic.Library;
using csa.Member.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace csa.Member
{
    public partial class ViewFile : System.Web.UI.Page
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
                string filePath = FileHelper.GetUploadPhysicalFullPath(AppSettings.UploadPath, fileDir, file);
                byte[] fileBytes;

                // Read the file into a byte array
                if (File.Exists(filePath))
                {
                    fileBytes = File.ReadAllBytes(filePath);

                    // Clear the response
                    Response.Clear();
                    Response.ContentType = GetMimeType(filePath); // Set the content type (e.g., image/jpeg)
                    Response.AddHeader("Content-Disposition", "inline; filename=" + Path.GetFileName(filePath)); // Inline for viewing
                    Response.BinaryWrite(fileBytes); // Write the byte array to the response
                    Response.End(); // End the response
                }
                else
                {
                    // Handle the case where the file doesn't exist
                    Response.StatusCode = 404; // Not Found
                    Response.Write("File not found.");
                    Response.End();
                }
            }
        }

        private string GetMimeType(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".pdf":
                    return "application/pdf";
                case ".doc":
                case ".docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xls":
                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".txt":
                    return "text/plain";
                case ".zip":
                    return "application/zip";
                // Add more cases as needed
                default:
                    return "application/octet-stream"; // Default for unknown types
            }
        }
    }
}