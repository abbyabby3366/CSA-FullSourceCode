using System;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace csa.DataLogic.Library
{
    public static class FileHelper
    {
        public enum FileDir
        {
            IcFileDir,
            ProfileFileDir,
            ApplicationPreCheckingDir,
            ApplicationDocumentDir,
            ApplicationPreparationDir,
            ApplicationZoomAcceptanceDir,
            ApplicationSettlementDir,
            ApplicationCcrisDir,
            ApplicationQueueDir,
            ApplicationReloanDir,
            ApplicationCollectionDir,
            ApplicationDir,
            MemberDir,
        }

        public static string GetUploadPhysic(FileDir dir)
        {
            return GetUploadPhysicalFullPath(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "uploads", dir, string.Empty);
        }

        public static string GetUploadPhysic(string physicalPath,FileDir dir)
        {
            return GetUploadPhysicalFullPath(physicalPath, dir, string.Empty);
        }

        private static string GetDirByFileDir(FileDir dir)
        {
            switch (dir)
            {
                case FileDir.IcFileDir:
                    return "/ic_file";
                case FileDir.ProfileFileDir:
                    return "/profile_file";
                case FileDir.ApplicationPreCheckingDir:
                    return "/application_prechecking";
                case FileDir.ApplicationDocumentDir:
                    return "/application_document";
                case FileDir.ApplicationPreparationDir:
                    return "/application_preparation";
                case FileDir.ApplicationZoomAcceptanceDir:
                    return "/application_zoomatteptance";
                case FileDir.ApplicationSettlementDir:
                    return "/application_settlement";
                case FileDir.ApplicationCcrisDir:
                    return "/application_ccris";
                case FileDir.ApplicationQueueDir:
                    return "/application_queue";
                case FileDir.ApplicationReloanDir:
                    return "/application_reloan";
                case FileDir.ApplicationCollectionDir:
                    return "/application_collection";
                case FileDir.ApplicationDir:
                    return "/application";
                case FileDir.MemberDir:
                    return "/member";
                default:
                    return "/";
            }
        }
        public static string GetUploadFullPath(FileDir dir, string filename)
        {
            return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/uploads" + GetDirByFileDir(dir) + "/" + filename;
        }

        public static string GetUploadFullPath(FileDir dir)
        {
            return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/uploads" + GetDirByFileDir(dir) + "/";
        }

        public static string GetUploadFullPath(string virtualPath, FileDir dir, string filename)
        {
            return virtualPath + "/uploads" + GetDirByFileDir(dir) + "/" + filename;
        }

        private static string GetPhysicalDirByFileDir(FileDir dir)
        {
            switch (dir)
            {
                case FileDir.IcFileDir:
                    return "\\ic_file";
                case FileDir.ProfileFileDir:
                    return "\\profile_file";
                case FileDir.ApplicationPreCheckingDir:
                    return "\\application_prechecking";
                case FileDir.ApplicationDocumentDir:
                    return "\\application_document";
                case FileDir.ApplicationPreparationDir:
                    return "\\application_preparation";
                case FileDir.ApplicationZoomAcceptanceDir:
                    return "\\application_zoomatteptance";
                case FileDir.ApplicationSettlementDir:
                    return "\\application_settlement";
                case FileDir.ApplicationCcrisDir:
                    return "\\application_ccris";
                case FileDir.ApplicationQueueDir:
                    return "\\application_queue";
                case FileDir.ApplicationReloanDir:
                    return "\\application_reloan";
                case FileDir.ApplicationCollectionDir:
                    return "\\application_collection";
                case FileDir.ApplicationDir:
                    return "\\application";
                case FileDir.MemberDir:
                    return "\\member";
                default:
                    return "\\";
            }
        }

        public static void CheckDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        public static string GetUploadPhysicalFullPath(string basePath, FileDir dir, string file)
        {
            if (!Directory.Exists(basePath + GetPhysicalDirByFileDir(dir)))
            {
                try
                {
                    Directory.CreateDirectory(basePath + GetPhysicalDirByFileDir(dir));
                    Console.WriteLine("Directory created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating directory: {ex.Message}");
                }

            }

            return basePath + GetPhysicalDirByFileDir(dir) + "\\" + file;
        }
    }

    public static class ImageThumbnailGenerator
    {
        public static void GenerateThumbnail(HttpPostedFile httpPostedFile, string thumbnailPath, int width)
        {
            try
            {
                using (var originalImage = Image.FromStream(httpPostedFile.InputStream))
                {
                    NormalizeOrientation(originalImage);
                    // Calculate height to maintain aspect ratio
                    int height = originalImage.Height * width / originalImage.Width;

                    using (var thumbnail = new Bitmap(width, height))
                    {

                        using (var graphics = Graphics.FromImage(thumbnail))
                        {
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                            // Draw the original image onto the thumbnail with specified width and calculated height
                            graphics.DrawImage(originalImage, 0, 0, width, height);

                            // Save thumbnail to file
                            thumbnail.Save(thumbnailPath, ImageFormat.Png);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine("Error generating thumbnail: " + ex.Message);
            }
        }

        public static void GenerateThumbnail(string imagePath, string thumbnailPath, int width)
        {
            try
            {
                using (var originalImage = Image.FromFile(imagePath))
                {
                    NormalizeOrientation(originalImage);
                    // Calculate height to maintain aspect ratio
                    int height = originalImage.Height * width / originalImage.Width;

                    using (var thumbnail = new Bitmap(width, height))
                    {
                        using (var graphics = Graphics.FromImage(thumbnail))
                        {
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                            // Draw the original image onto the thumbnail with specified width and calculated height
                            graphics.DrawImage(originalImage, 0, 0, width, height);

                            // Save thumbnail to file
                            thumbnail.Save(thumbnailPath, ImageFormat.Png);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine("Error generating thumbnail: " + ex.Message);
            }
        }

        public static void NormalizeOrientation(Image image)
        {
            //The EXIF id 0x0112 is for Orientation.This is a helpful EXIF id reference http://www.sno.phy.queensu.ca/~phil/exiftool/TagNames/EXIF.html
            //0x0112 is the hex equivalent of 274.The data type of a PropertyItem.Id is an int, meaning 274 is what is useful here.
            //Additionally, 5029 likely was supposed to be 0x5029 or 20521 which correlates to ThumbnailOrientation, though is likely not what is desired here.

            int ExifOrientationTagId = 274;

            if (Array.IndexOf(image.PropertyIdList, ExifOrientationTagId) > -1)
            {
                int orientation;

                orientation = image.GetPropertyItem(ExifOrientationTagId).Value[0];

                if (orientation >= 1 && orientation <= 8)
                {
                    switch (orientation)
                    {
                        case 2:
                            image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            break;
                        case 3:
                            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            break;
                        case 4:
                            image.RotateFlip(RotateFlipType.Rotate180FlipX);
                            break;
                        case 5:
                            image.RotateFlip(RotateFlipType.Rotate90FlipX);
                            break;
                        case 6:
                            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            break;
                        case 7:
                            image.RotateFlip(RotateFlipType.Rotate270FlipX);
                            break;
                        case 8:
                            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            break;
                    }

                    image.RemovePropertyItem(ExifOrientationTagId);
                }
            }
        }

        public static void NormalizeOrientation(Bitmap image)
        {
            //The EXIF id 0x0112 is for Orientation.This is a helpful EXIF id reference http://www.sno.phy.queensu.ca/~phil/exiftool/TagNames/EXIF.html
            //0x0112 is the hex equivalent of 274.The data type of a PropertyItem.Id is an int, meaning 274 is what is useful here.
            //Additionally, 5029 likely was supposed to be 0x5029 or 20521 which correlates to ThumbnailOrientation, though is likely not what is desired here.

            int ExifOrientationTagId = 274;

            if (Array.IndexOf(image.PropertyIdList, ExifOrientationTagId) > -1)
            {
                int orientation;

                orientation = image.GetPropertyItem(ExifOrientationTagId).Value[0];

                if (orientation >= 1 && orientation <= 8)
                {
                    switch (orientation)
                    {
                        case 2:
                            image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            break;
                        case 3:
                            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            break;
                        case 4:
                            image.RotateFlip(RotateFlipType.Rotate180FlipX);
                            break;
                        case 5:
                            image.RotateFlip(RotateFlipType.Rotate90FlipX);
                            break;
                        case 6:
                            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            break;
                        case 7:
                            image.RotateFlip(RotateFlipType.Rotate270FlipX);
                            break;
                        case 8:
                            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            break;
                    }

                    image.RemovePropertyItem(ExifOrientationTagId);
                }
            }
        }
    }
}
