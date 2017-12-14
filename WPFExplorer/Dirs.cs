using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WPFExplorer
{
    /// <summary>
    /// class for save dirs choise user
    /// </summary>
    class Dirs : Elemnts
    {
        List<Elemnts> lst = new List<Elemnts>();
        int actionindex = 0;

        /// <summary>
        /// method for test 
        /// </summary>
        /// <param name="el">new element</param>
        /// <returns>choise</returns>
        private bool Test(Elemnts el)
        {
            foreach (var element in this.lst)
            {
                if (element.Root.FullName == el.Root.FullName)
                {
                    this.actionindex++;
                    if (this.actionindex == lst.Count)
                        MediatorWinRename.EnabledBtnFront(false);
                    return false;
                }

            }
            return true;
        }
        /// <summary>
        /// method for add
        /// </summary>
        /// <param name="el"></param>
        public override void Add(Elemnts el)
        {
            if (this.Test(el))
            {
                for (var i = 1; i < this.lst.Count; i++)
                {
                    if (this.lst[i].Root.Parent.FullName == el.Root.Parent.FullName)
                    {
                        this.lst.RemoveRange(i, (this.lst.Count) - i);
                        this.actionindex = i;
                        MediatorWinRename.EnabledBtnFront(false);
                    }
                    else if (this.lst[i].Root.FullName.Contains("Searching results"))
                    {
                        if(this.lst[i].Parent.Root.FullName==el.Root.Parent.FullName)
                        {
                            this.lst.RemoveRange(i, (this.lst.Count) - i);
                            this.actionindex = i;
                            MediatorWinRename.EnabledBtnFront(false);
                        }
                    }
                }
                this.actionindex++;
                this.lst.Add(el);
                MediatorWinRename.EnableBtnBack(true);
            }
        }

        /// <summary>
        /// proprety for get root
        /// </summary>
        public override Elemnts GetRoot
        {
            get
            {
                try
                {
                    return this.lst[lst.Count - 1];
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

       

        /// <summary>
        /// method for return action dir parent
        /// </summary>
        public override DirectoryInfo GetActionNode
        {
            get
            {
                try
                {
                    return this.lst[actionindex - 1].Root;
                }
                catch (Exception ex)
                {
                    return this.lst[this.lst.Count - 1].Root;
                }

            }
        }

        /// <summary>
        /// method for return action folder
        /// </summary>
        public override DirectoryInfo GetActionFolder
        {
            get
            {
                return this.lst[actionindex].Root;
            }
        }

        /// <summary>
        /// operator ++
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Dirs operator ++(Dirs d)
        {
            if (d.actionindex < d.lst.Count)
            {
                d.actionindex++;
            }
            if (d.actionindex == d.lst.Count)
            {
                MediatorWinRename.EnabledBtnFront(false);
            }
            return d;
        }

        /// <summary>
        /// operator --
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Dirs operator --(Dirs d)
        {
            if (d.actionindex > 0)
            {
                d.actionindex--;
            }
            if (d.actionindex == 0)
                MediatorWinRename.EnableBtnBack(false);
            return d;
        }

        /// <summary>
        /// method for Reset
        /// </summary>
        public override void Reset()
        {
            this.lst.Clear();
            this.actionindex = 0;
        }

        /// <summary>
        /// method for delete last element
        /// </summary>
        public void DeleteLast()
        {
            this.lst.RemoveAt(lst.Count - 1);
        }

        /// <summary>
        /// method for return index
        /// </summary>
        /// <returns></returns>
        public int GetIndex()
        {
            return this.actionindex;
        }

        public override void DeletePath(List<DirectoryInfo> dirs)
        {
            foreach (var dir in dirs)
            {
                for (var i = this.lst.Count - 1; i >= 0; i--)
                {
                    if (this.lst[i].Root.FullName == dir.FullName)
                    {
                        this.lst.RemoveAt(i);
                        this.actionindex--;
                        if (this.actionindex == this.lst.Count)
                        {
                            MediatorWinRename.EnabledBtnFront(false);
                        }
                    }
                }
            }
        }
    }
}
