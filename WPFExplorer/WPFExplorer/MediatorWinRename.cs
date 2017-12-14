using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFExplorer
{
    /// <summary>
    /// method for rename 
    /// </summary>
    static class MediatorWinRename
    {
        /// <summary>
        /// method for name file
        /// </summary>
        public static Action<string> NameFile { set; get; }
        /// <summary>
        /// method for visibiliyu button back
        /// </summary>
        public static Action<bool> EnableBtnBack { set; get; }
        /// <summary>
        /// method for visibility buttin font
        /// </summary>
        public static Action<bool> EnabledBtnFront { set; get; }
    }
}
