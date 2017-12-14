using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFExplorer
{
    /// <summary>
    /// class-delegate for fill PropertyWindows
    /// </summary>
    static class MediatreInfoWin
    {
        /// <summary>
        /// method for info Windows
        /// </summary>
        public static Action<string> Windows { set; get; }
        /// <summary>
        /// method for info Processor
        /// </summary>
        public static Action<string> Proccesor { set; get; }
        /// <summary>
        /// method for info RAM
        /// </summary>
        public static Action<string> RAM { set; get; }
        /// <summary>
        /// method for info Video Controller
        /// </summary>
        public static Action<string> VideoController { set; get; }
    }
}
