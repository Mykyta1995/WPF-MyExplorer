using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WPFExplorer
{
    /// <summary>
    /// class mediator for serach
    /// </summary>
    static class MediatorSearch
    {
        /// <summary>
        /// method for add file in listview
        /// </summary>
        public static Action<FileInfo> AddFile { set; get; }

        /// <summary>
        /// method for add file in listview
        /// </summary>
        public static Action<DirectoryInfo> AddDir { set; get; }
    }
}
