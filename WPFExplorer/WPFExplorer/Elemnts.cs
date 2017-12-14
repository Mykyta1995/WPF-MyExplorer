using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WPFExplorer
{
    abstract class Elemnts
    {
        /// <summary>
        /// property parent
        /// </summary>
        public Elemnts Parent { set; get; }
        /// <summary>
        /// property root
        /// </summary>
        public DirectoryInfo Root { set; get; }

        /// <summary>
        /// method for add new element
        /// </summary>
        /// <param name="el">new element</param>
        public abstract void Add(Elemnts el);

        /// <summary>
        /// method for get root
        /// </summary>
        public abstract Elemnts GetRoot { get; }

        //public abstract DirectoryInfo Get { get; }

        /// <summary>
        /// method for get new Node
        /// </summary>
        public abstract DirectoryInfo GetActionNode { get; }

        /// <summary>
        /// method for get action folder
        /// </summary>
        public abstract DirectoryInfo GetActionFolder { get;}

        /// <summary>
        /// method for reset
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// method for delete path
        /// </summary>
        /// <param name="?"></param>
        public abstract void DeletePath(List<DirectoryInfo> dirs);
    }



}
