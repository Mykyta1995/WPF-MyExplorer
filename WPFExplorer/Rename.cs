using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WPFExplorer
{
    /// <summary>
    /// class for Rename
    /// </summary>
    static class Rename
    {
        /// <summary>
        /// method for rename comtent
        /// </summary>
        /// <param name="path"></param>
        /// <param name="newName"></param>
        public static void ReName(string path,string newName,ref string newPath)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if(dir.Attributes.ToString().Contains("Directory"))
            {
                newPath=string.Format("{0}\\{1}",path.Remove(path.LastIndexOf('\\')),newName);
                Directory.Move(path, newPath);
            }
            else
            {
                string mask = path.Remove(0, path.LastIndexOf('.'));
                newPath = string.Format("{0}\\{1}{2}", path.Remove(path.LastIndexOf('\\')), newName, mask);
                try
                {
                    File.Move(path, newPath);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
