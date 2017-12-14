using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;

namespace WPFExplorer
{
    /// <summary>
    /// base class for Act content
    /// </summary>
    class BaseActContent
    {
        /// <summary>
        /// method search content
        /// </summary>
        /// <param name="root">root</param>
        /// <param name="AllDirs">save path disr</param>
        /// <param name="AllFiles">save path files</param>
        /// <param name="flag">flag delete or not</param>
        public static void SearchDirectory(DirectoryInfo root, List<string> AllDirs, List<string> AllFiles, bool flag, ref int count)
        {
            DirectoryInfo[] dirs = null;
            FileInfo[] files = null;
            try
            {
                files = root.GetFiles();
            }
            catch (UnauthorizedAccessException ex)
            {

            }
            catch (DirectoryNotFoundException e)
            {

            }
            if (files != null)
            {
                foreach (var file in files)
                {
                    count++;
                    if (flag)
                        Delete.DeleteFile(file.FullName);
                    else
                        AllFiles.Add(file.FullName);
                }
                dirs = root.GetDirectories();
                foreach (var dir in dirs)
                {
                    SearchDirectory(dir, AllDirs, AllFiles, flag, ref count);
                    if (flag)
                        Directory.Delete(dir.FullName);
                    else
                        AllDirs.Add(dir.FullName);
                }
            }
        }

        /// <summary>
        /// method for add content
        /// </summary>
        /// <param name="copy"></param>
        /// <param name="dirs"></param>
        /// <param name="files"></param>
        /// <param name="count"></param>
        /// <param name="parent"></param>
        public static void AddContent(List<DirectoryInfo> copy, List<string> dirs, List<string> files, ref int count, List<string> parent)
        {
            foreach (var content in copy)
            {
                if (content.Attributes.ToString().Contains("Directory"))
                {
                    SearchDirectory(content, dirs, files, false, ref count);
                    dirs.Add(content.FullName);
                }
                else
                {
                    count++;
                    files.Add(content.FullName);
                }
                if (!parent.Contains(content.Parent.FullName))
                {
                    if (content.Parent.Parent == null)
                    {
                        parent.Add(content.Parent.FullName.Remove(content.Parent.FullName.IndexOf('\\', 1)));
                    }
                    else
                        parent.Add(content.Parent.FullName);
                }
                    
            }
        }

        /// <summary>
        /// method start copy
        /// </summary>
        /// <param name="insert"></param>
        /// <param name="dirs"></param>
        /// <param name="parent"></param>
        /// <param name="files"></param>
        /// <param name="flag"></param>
        /// <param name="cp"></param>
        public void StartCopy(DirectoryInfo insert, List<string> dirs, List<string> parent, List<string> files, bool flag,Copy cp)
        {
            int index = 0;
            string newPath = string.Empty;
            if (!flag)
            {
                if (insert.Attributes.ToString().Contains("Directory"))
                {
                    if (insert.Parent == null)
                        newPath = insert.FullName;
                    else
                        newPath = String.Format("{0}\\", insert.FullName);
                }
                else
                    newPath = insert.Parent.FullName;
            }
            if (!flag)
                this.ChangeDirectory(parent, dirs, flag, newPath,cp);
            this.ChangeFiles(files, newPath, parent, flag,cp);
            if (flag)
                this.ChangeDirectory(parent, dirs, flag, newPath,cp);
        }

        /// <summary>
        /// method for change or delete dirs 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="dirs"></param>
        /// <param name="flag"></param>
        /// <param name="newPath"></param>
        /// <param name="cp"></param>
        public void ChangeDirectory(List<string> parent, List<string> dirs, bool flag, string newPath,Copy cp)
        {
            int index = 0;
            string path = string.Empty;
            foreach (var dir in dirs)
            {
                if (!dir.Contains(parent[index]))
                    index++;
                if (!flag)
                {
                    path = string.Format("{0}{1}", newPath, dir.Remove(0, parent[index].Length + 1));
                    Directory.CreateDirectory(path);
                    if(cp!=null)cp.Dirs = path;
                }
                else
                    Delete.DeleteDirectory(dir);

            }
        }

        /// <summary>
        /// method for change file
        /// </summary>
        /// <param name="files"></param>
        /// <param name="newPath"></param>
        /// <param name="parent"></param>
        /// <param name="flag"></param>
        /// <param name="cp"></param>
        public void ChangeFiles(List<string> files, string newPath, List<string> parent,bool flag,Copy cp)
        {
            bool flagchange=false;
            int index = 0;
            int count = 1;
            string path = string.Empty;
            int value = 0;
            foreach (var file in files)
            {
                if (!file.Contains(parent[index]))
                    index++;
                MediatorProgressBar.Name(file.Remove(0, file.LastIndexOf('\\') + 1));
                if (!flag)
                {
                    path = string.Format("{0}{1}", newPath, file.Remove(0, parent[index].Length + 1));
                    Copy.CopyFile(file, path,ref flagchange,cp);
                }
                else
                    Delete.DeleteFile(file);
                value = count * 100 / files.Count;
                MediatorProgressBar.Count(value);
                count++;
            }
        }
    }
}
