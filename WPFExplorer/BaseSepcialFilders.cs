using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFExplorer
{
    /// <summary>
    /// base class for special filder video,
    /// docutemt also music,picture,desktop too
    /// </summary>
    abstract class BaseSepcialFilders
    {
        /// <summary>
        /// property for return path 
        /// </summary>
        public abstract string Path
        {
            get;
        }

        /// <summary>
        /// property for return path image
        /// </summary>
        public abstract string PathImage
        {
            get;
        }
    }
}
