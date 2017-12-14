using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WPFExplorer
{
    /// <summary>
    /// class for mediator WinProgressBar
    /// </summary>
    static class MediatorProgressBar
    {
        /// <summary>
        /// list selected items
        /// </summary>
        public static Action<List<DirectoryInfo>,bool> Items { set; get;}
        
        /// <summary>
        /// object my class
        /// </summary>
        public static Action<Copy> ActCopy { set; get; }
        /// <summary>
        /// for look at file name 
        /// </summary>
        public static Action<string> Name { set; get; }

        /// <summary>
        /// value progress bar
        /// </summary>
        public static Action<int> Count { set; get; }
        /// <summary>
        /// for close window
        /// </summary>
        public static Action Close{set;get;}

        /// <summary>
        /// for root
        /// </summary>
        public static Action<DirectoryInfo> Root { set; get;}

        /// <summary>
        /// method for get root
        /// </summary>
        public static Action<DirectoryInfo> GetRoot { set; get; }


        /// <summary>
        /// method for ask user
        /// </summary>
        public static Func<bool> Ask { set; get; }

    }
}
