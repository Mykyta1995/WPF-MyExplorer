using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFExplorer
{
    /// <summary>
    /// class fo special folder Desktop
    /// </summary>
    class Desktop:BaseSepcialFilders
    {
        readonly string path = null;
        readonly string pathImage = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public Desktop()
        {
            this.path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            this.pathImage = @"pack://application:,,,/Resources/icons8-Монитор-16.png";
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
