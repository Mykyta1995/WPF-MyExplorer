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
    class MyPicture:BaseSepcialFilders
    {
        readonly string path = null;
        readonly string pathImage = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public MyPicture()
        {
            this.path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            this.pathImage = @"pack://application:,,,/Resources/icons8-pictures-folder-16.png";
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
