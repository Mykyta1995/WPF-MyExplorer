using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace WPFExplorer
{
    /// <summary>
    /// class delete content
    /// </summary>
    class Delete : BaseActContent
    {
        List<string> dirs = new List<string>();
        List<string> files = new List<string>();
        int count = 0;
        List<string> parent = new List<string>();
        List<DirectoryInfo> del = new List<DirectoryInfo>();
        public void AddContent()
        {
            AddContent(this.del, this.dirs, this.files, ref this.count, this.parent);
        }

        /// <summary>
        ///  list delete
        /// </summary>
        public List<DirectoryInfo> Del
        {
            set
            {
                this.del = value;
            }
        }

        /// <summary>
        /// method fro start delete
        /// </summary>
        public void StartDelete()
        {
            StartCopy(null, this.dirs, this.parent, this.files, true,null);
        }
        /// <summary>
        /// method for delete file
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFile(string path)
        {
            try
            {
                File.SetAttributes(path, FileAttributes.Normal);
                File.Delete(path);
            }
            catch (Exception ef)
            {

            }
        }


        /// <summary>
        /// method for delete directory
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteDirectory(string path)
        {
            Directory.Delete(path);
        }

        /// <summary>
        /// method for reset info
        /// </summary>
        public void Reset()
        {
            this.files.Clear();
            this.dirs.Clear();
            this.del.Clear();
        }

        /// <summary>
        /// method for add files
        /// </summary>
        public List<string> Files
        {
            set
            {
                this.files = value;
            }
        }

        /// <summary>
        /// method for return del
        /// </summary>
        public List<DirectoryInfo> Dellited
        {
            get
            {
                return this.del;
            }
        }
    }

}
