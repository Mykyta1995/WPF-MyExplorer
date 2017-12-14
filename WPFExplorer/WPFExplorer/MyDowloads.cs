using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFExplorer
{
    /// <summary>
    ///class for special folder MyDowloads
    /// </summary>
    class MyDowloads:BaseSepcialFilders
    {
        readonly string path = null;
        readonly string pathImage = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public MyDowloads()
        {
            this.path = string.Format("{0}\\Downloads",Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            this.pathImage = @"pack://application:,,,/Resources/icons8-downloads-folder-16.png";
        }

        /// <summary>
        /// property for return path 
        /// </summary>
        public override string Path
        {
            get
            {
                return this.path;
            }
        }

        /// <summary>
        /// property for return path image
        /// </summary>
        public override string PathImage
        {
            get
            {
                return this.pathImage;
            }
        }
    }
}
