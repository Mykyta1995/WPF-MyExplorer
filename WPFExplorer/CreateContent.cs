using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WPFExplorer
{
    /// <summary>
    /// class createContent
    /// </summary>
    class CreateContent
    {
        Dictionary<string, string> files = new Dictionary<string, string>();
        string newContent = null;

        /// <summary>
        /// default constructor
        /// </summary>
        public CreateContent()
        {
            this.files.Add("Microsoft Windows Word", ".docx");
            this.files.Add("Microsoft PowerPoint", ".pptx");
            this.files.Add("Text", ".txt");
        }

        /// <summary>
        /// method for create
        /// </summary>
        /// <param name="key"></param>
        /// <param name="path"></param>
        public void Create(string key, string path)
        {
            this.newContent = null;
            if (key.Contains("Directory"))
            {
                this.CreateDictionary(path);
                return;
            }
            foreach (var file in this.files.Keys)
            {
                if (key.Contains(file))
                {
                    for (var i = 0; ; i++)
                    {
                        this.newContent = string.Format("{0}\\New Document{1}({2}){3}", path, file, i.ToString(), this.files[file]);
                        if (!File.Exists(newContent))
                        {
                                using (FileStream fs = new FileStream(newContent, FileMode.CreateNew))
                                {

                                }
                                return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// method for create directory
        /// </summary>
        /// <param name="path"></param>
        private void CreateDictionary(string path)
        {
            string newContent = null;
            for (var i = 0; ; i++)
            {
                this.newContent = string.Format("{0}\\New Directory{1}", path, i.ToString());
                if (!Directory.Exists(this.newContent))
                {
                    Directory.CreateDirectory(this.newContent);
                    return;
                }

            }

        }

        /// <summary>
        /// method fro create file
        /// </summary>
        public string NewFile
        {
            get
            {
                return this.newContent;
            }
            set
            {
                this.newContent = value;
            }
        }
    }
}
