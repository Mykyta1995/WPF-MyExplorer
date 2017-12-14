using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;

namespace WPFExplorer
{
    /// <summary>
    /// class copy content
    /// </summary>
    class Copy : BaseActContent
    {
        List<string> dirs = new List<string>();
        List<string> parent = new List<string>();
        List<string> files = new List<string>();
        List<string> copyFiles = new List<string>();
        List<string> copyDirectory = new List<string>();
        bool flag = false;
        bool flagEx = false;
        List<DirectoryInfo> copy = new List<DirectoryInfo>();
        DirectoryInfo inset = null;
        int count = 0;

        /// <summary>
        /// for to copy
        /// </summary>
        public DirectoryInfo To
        {
            set
            {
                this.copy.Add(value);
            }
            get
            {
                try
                {
                    return this.copy[0];
                }
                catch(Exception ex)
                {
                    return null;
                }
               
            }
        }

        /// <summary>
        /// for in copy
        /// </summary>
        public DirectoryInfo In
        {
            set
            {
                this.inset = value;
            }
        }

        /// <summary>
        /// search copy files
        /// </summary>
        public void AddContent()
        {
            AddContent(this.copy, this.dirs, this.files, ref this.count, this.parent);
        }

        public void StartCopy()
        {
            StartCopy(this.inset, this.dirs, this.parent, this.files, false,this);
        }


        public static void CopyByte(FileStream FileIn, FileStream FileTo)
        {
            byte[] buf = new byte[4096];
            while (true)
            {
                int count = FileTo.Read(buf, 0, buf.Length);
                if (count == 0)
                    break;
                FileIn.Write(buf, 0, count);
            }
        }
        /// <summary>
        /// method for copy files
        /// </summary>
        /// <param name="copy"></param>
        /// <param name="newPath"></param>
        public static void CopyFile(string copy, string newPath, ref bool flag,Copy cp)
        {
            if(cp.flagEx)
            {
                while(true)
                {

                }
            }
            using (FileStream FileTo = new FileStream(copy, FileMode.Open))
            {
                try
                {
                    using (FileStream FileIn = new FileStream(newPath, FileMode.CreateNew))
                    {
                        CopyByte(FileIn, FileTo);
                        cp.copyFiles.Add(newPath);
                    }
                }
                catch (Exception ex)
                {
                    if (!flag)
                    {
                        flag = MediatorProgressBar.Ask();
                    }
                    if (flag == true)
                    {
                        using (FileStream FileIn = new FileStream(newPath, FileMode.Create))
                        {
                            CopyByte(FileIn, FileTo);
                            cp.copyFiles.Add(newPath);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// method reset
        /// </summary>
        public void Reset()
        {
            this.files.Clear();
            this.dirs.Clear();
            this.copy.Clear();
        }

        public List<string> Files
        {
            get
            {
                return this.copyFiles;
            }
        }

        public string Dirs
        {
            set
            {
                this.copyDirectory.Add(value);
            }
        }

        public List<string> GetCopyDirs
        {
            get
            {
                return this.copyDirectory;
            }
        }

        public void ErrorDelete()
        {
            foreach (var file in this.copyFiles)
            {
                Delete.DeleteFile(file);
            }
            foreach (var dir in this.copyDirectory)
            {
                Delete.DeleteDirectory(dir);
            }
        }


    }
}
