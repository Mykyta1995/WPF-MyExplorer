using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFExplorer
{
    /// <summary>
    /// class common class special folder
    /// </summary>
    class SpecialFolders
    {
        List<BaseSepcialFilders> lstSpecFoldersMyComp = new List<BaseSepcialFilders>();
        List<BaseSepcialFilders> lstSpecFoldersFavorites = new List<BaseSepcialFilders>();

        /// <summary>
        /// default constructor
        /// </summary>
        public SpecialFolders()
        {
            this.lstSpecFoldersMyComp.Add(new MyVideo());
            this.lstSpecFoldersMyComp.Add(new MyDocuments());
            this.lstSpecFoldersMyComp.Add(new MyDowloads());
            this.lstSpecFoldersMyComp.Add(new MyPicture());
            this.lstSpecFoldersMyComp.Add(new MyMusic());
            this.lstSpecFoldersMyComp.Add(new Desktop());
            this.lstSpecFoldersFavorites.Add(new MyLinks());
            this.lstSpecFoldersFavorites.Add(this.lstSpecFoldersMyComp[2]);
        }


        /// <summary>
        /// property return list special folders
        /// </summary>
        public List<BaseSepcialFilders> GetSpecFoldersMyComp
        {
            get
            {
                return this.lstSpecFoldersMyComp;
            }
        }

        /// <summary>
        /// property return list specFolderFavorite
        /// </summary>
        public List<BaseSepcialFilders> GetSpecFoldersFavorites
        {
            get
            {
                return this.lstSpecFoldersFavorites;
            }
        }
    }
}
