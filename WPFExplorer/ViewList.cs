using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WPFExplorer
{
    /// <summary>
    /// class for view list view
    /// </summary>
    class ViewList
    {
        public string PathImage { set; get; }
        public string Name { set; get; }
        public string DataChange { set; get; }
        public string Type { set; get; }
        public string Size { set; get; }

        /// <summary>
        /// method for add icon file
        /// </summary>
        /// <param name="file"></param>
        public void FillFile(FileInfo file)
        {
            this.DataChange = file.CreationTime.ToString();
            this.PathImage = @"pack://application:,,,/Resources/icons8-file-16.png";
            this.Size = string.Format("{0} KB", (file.Length / 1024).ToString());
            this.Type = file.Extension.ToString();
            try
            {
                this.Name = file.Name.Remove(file.Name.LastIndexOf('.'));
            }
            catch
            {
                this.Name = file.Name;
            }

        }

        /// <summary>
        /// for add directory ico
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="Name"></param>
        /// <param name="ImagePath"></param>
        public void FillDirectory(DirectoryInfo dir, string Name, string ImagePath)
        {
            this.DataChange = dir.CreationTime.ToString();
            this.PathImage = ImagePath;
            this.Size = "";
            this.Type = dir.Attributes.ToString();
            this.Name = Name;
        }

        /// <summary>
        /// for add drive ico
        /// </summary>
        /// <param name="drive"></param>
        /// <param name="ImagePath"></param>
        public void FillDrive(DriveInfo drive, string ImagePath)
        {
            try
            {
                this.DataChange = "";
                this.PathImage = ImagePath;
                this.Type = drive.DriveType.ToString();
                this.Name = drive.Name;
                this.Size = string.Format("{0} GB", (drive.TotalSize / (1024 * 1024 * 1024)).ToString());
            }
            catch (Exception e)
            {

            }
        }

    }
}
