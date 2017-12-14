using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WPFExplorer
{
    /// <summary>
    /// class for search content
    /// </summary>
    static class Search
    {
        /// <summary>
        /// method for search content
        /// </summary>
        /// <param name="root">where are search file</param>
        /// <param name="mask">mask need's content</param>
        public static void SearchContent(DirectoryInfo root, string mask)
        {
            string maskfile = string.Empty;
            string maskfilecon = string.Empty;
            if (mask.Contains('.'))
            {
                mask=mask.Remove(mask.IndexOf('.'),1);
            }
            maskfile = string.Format("{0}*.*", mask);
            maskfilecon = string.Format("*.{0}", mask);
            List<DirectoryInfo> dirs = new List<DirectoryInfo>();
            List<FileInfo> files = new List<FileInfo>();
            try
            {
                if(mask!="*")
                {
                    files.AddRange(root.GetFiles(maskfile));
                    files.AddRange(root.GetFiles(maskfilecon));
                    files.AddRange(root.GetFiles(mask));
                }
                else
                {
                    files.AddRange(root.GetFiles("*.*"));
                }
                
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
                    MediatorSearch.AddFile(file);
                }
                files.Clear();
                try
                {
                    dirs.AddRange(root.GetDirectories());
                }
                catch(Exception ex)
                {

                }
                foreach (var dir in dirs)
                {
                    SearchContent(dir, mask);
                    if (dir.Name.Contains(mask) || mask.Contains('*'))
                    {
                        MediatorSearch.AddDir(dir);
                    }
                }
            }
        }
    }
}
