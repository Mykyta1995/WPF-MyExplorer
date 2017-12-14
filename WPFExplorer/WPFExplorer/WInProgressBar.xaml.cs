using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.IO;

namespace WPFExplorer
{
    /// <summary>
    /// Логика взаимодействия для WInProgressBar.xaml
    /// </summary>
    public partial class WInProgressBar : Window
    {
        Delete delcont = new Delete();
        Copy copyCont = new Copy();
        bool flagDelete = false;
        DirectoryInfo root = null;
        Thread thread = null;
        /// <summary>
        /// default constructor
        /// </summary>
        public WInProgressBar()
        {
            InitializeComponent();
            MediatorProgressBar.Items = (x, flag) =>
                {
                    this.flagDelete = flag;
                    this.delcont.Del = x;
                };
            MediatorProgressBar.Root = (x) => this.root = x;
            MediatorProgressBar.ActCopy = (x) => this.copyCont = x;
            MediatorProgressBar.Count = this.ShowDialog;
            MediatorProgressBar.Name = this.Show;
            MediatorProgressBar.Close = this.ShowClose;
            MediatorProgressBar.Ask = this.Ask;
        }

        /// <summary>
        /// method for delete content
        /// </summary>
        private void DeleteContend()
        {
            this.thread = new Thread((object y) =>
               {
                   try
                   {
                       this.delcont.AddContent();
                       this.delcont.StartDelete();
                       this.delcont.Reset();
                       MediatorProgressBar.GetRoot(this.root);
                       MediatorProgressBar.Count(100);
                       MediatorProgressBar.Close();
                   }
                   catch (Exception e)
                   {
                       try
                       {
                           MessageBox.Show(string.Format("Access is denied {0}", e.Message.Remove(0, e.Message.IndexOf('"'))), "Delete", MessageBoxButton.OK, MessageBoxImage.Error);
                       }
                       catch (Exception ex)
                       {

                       }
                       MediatorProgressBar.Count(100);
                       MediatorProgressBar.Close();

                   }
               });
            thread.Start();
        }

        /// <summary>
        /// method for copy content
        /// </summary>
        private void CopyContent()
        {
            this.thread = new Thread((object y) =>
               {
                   try
                   {
                       this.copyCont.AddContent();
                       this.copyCont.StartCopy();
                       MediatorProgressBar.GetRoot(this.root);
                       MediatorProgressBar.Count(100);
                       MediatorProgressBar.Close();
                   }
                   catch (Exception e)
                   {
                       this.copyCont.ErrorDelete();
                       MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                       MediatorProgressBar.GetRoot(this.root);
                       MediatorProgressBar.Count(100);
                       MediatorProgressBar.Close();
                   }
               });
            thread.Start();
        }

        /// <summary>
        /// method for ask user cahnge file or no
        /// </summary>
        /// <returns></returns>
        private bool Ask()
        {
            bool flag = false;
            this.Dispatcher.Invoke(new Action(() =>
                {
                    var answer = MessageBox.Show(string.Format("This content already exists, replace it?"), "Ask change content", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (answer == MessageBoxResult.Yes)
                    {
                        flag = true;
                    }
                }));
            return flag;
        }
        /// <summary>
        /// method if user click yes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (flagDelete)
                this.DeleteContend();
            else
                this.CopyContent();
        }

        /// <summary>
        /// method for user click no
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// method for look at name file
        /// </summary>
        /// <param name="value"></param>
        void Show(string value)
        {
            this.Dispatcher.Invoke(new Action(() => this.NameFile.Content = string.Format("Name: {0}", value)));
        }

        /// <summary>
        /// method for value progress bar
        /// </summary>
        /// <param name="value"></param>
        void ShowDialog(int value)
        {
            this.Dispatcher.Invoke(new Action(() => this.PB.Value = value));
        }

        /// <summary>
        /// method for close
        /// </summary>
        void ShowClose()
        {
            this.Dispatcher.Invoke(new Action(() => this.Button_Click_1(this, null)));
        }

        /// <summary>
        /// method for loaded after render window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            if (flagDelete)
                this.DeleteContend();
            else
                this.CopyContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.thread.IsAlive && this.PB.Value != 100)
            {
                var q = MessageBox.Show("Content is working.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Cancel = true;
            }
        }


    }
}
