using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFExplorer
{
    /// <summary>
    /// class Video
    /// </summary>
    class Video
    {
        /// <summary>
        /// path ro folder myVideo
        /// </summary>
        readonly string path = null;

        public Video()
        {
            this.path = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        }
    }
}
