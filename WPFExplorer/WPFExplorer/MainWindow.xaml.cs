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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;

namespace WPFExplorer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpecialFolders specFolders = new SpecialFolders();
        CreateContent newContent = new CreateContent();
        DirectoryInfo saveRott = null;
        Copy copyContent = new Copy();
        //string Name = string.Empty;
        byte indexView = 1;
        bool horizont = false;
        Dictionary<int, Action<DirectoryInfo, string>> view = new Dictionary<int, Action<DirectoryInfo, string>>();
        Elemnts element = new Dirs();
        Thread thread = null;
        int countElement = 0;
        List<FileInfo> lstSearchFile = new List<FileInfo>();
        List<DirectoryInfo> lstSearchDir = new List<DirectoryInfo>();
        bool flagSearch = false;


        /// <summary>
        /// default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.ItemComputer.IsSelected = true;
            this.ItemComputer.IsExpanded = true;
            this.ItemFavorites.IsExpanded = true;
            MediatorWinRename.NameFile = (path) =>
            {
                this.Name = path;
            };
            MediatorWinRename.EnableBtnBack = (flag) =>
                {
                    this.btnBack.IsEnabled = flag;
                };
            MediatorWinRename.EnabledBtnFront = (flag) =>
            {
                this.btnFront.IsEnabled = flag;
            };
            MediatorSearch.AddDir = this.AddSearchDir;
            MediatorSearch.AddFile = this.AddSearchFile;
            this.view.Add(0, this.FillListView);
            this.view.Add(1, this.FillListViewList);
            MediatorProgressBar.GetRoot = this.Show;
            this.Path.Text = "My Computer";
        }

        /// <summary>
        /// method for click View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnView_Click(object sender, RoutedEventArgs e)
        {
            this.btnComputer.Background = Brushes.White;
            this.btnComputer.BorderBrush = Brushes.White;
            this.btnView.Background = Brushes.LightGray;
            this.btnView.BorderBrush = Brushes.LightGray;
            this.WarpComputer.Visibility = Visibility.Hidden;
            this.WarpView.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// method for click Computer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnComputer_Click(object sender, RoutedEventArgs e)
        {
            this.btnComputer.Background = Brushes.LightGray;
            this.btnComputer.BorderBrush = Brushes.LightGray;
            this.btnView.Background = Brushes.White;
            this.btnView.BorderBrush = Brushes.White;
            this.WarpComputer.Visibility = Visibility.Visible;
            this.WarpView.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// method for clock button Info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            PropertyWindows pw = new PropertyWindows();
            pw.Show();
        }

        /// <summary>
        /// method for add new items to My Favorites
        /// </summary>
        public void AddSpecFoldersFavorites()
        {
            foreach (var info in this.specFolders.GetSpecFoldersFavorites)
            {
                TreeViewItem item = this.AddItemTreeVew(new DirectoryInfo(info.Path), info.PathImage,
                   info.Path.Remove(0, info.Path.LastIndexOf('\\') + 1));
                this.ItemFavorites.Items.Add(item);
            }
        }
        /// <summary>
        /// method for add new items to My Computer 
        /// </summary>
        public void AddSpecFoldersMyComp()
        {
            foreach (var info in this.specFolders.GetSpecFoldersMyComp)
            {
                TreeViewItem item = this.AddItemTreeVew(new DirectoryInfo(info.Path), info.PathImage,
                    info.Path.Remove(0, info.Path.LastIndexOf('\\') + 1));
                this.TestFolder(new DirectoryInfo(info.Path), item);
                this.ItemComputer.Items.Add(item);
            }
        }
        /// <summary>
        /// method for Create new TreeViewItem
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info">path folder</param>
        /// <param name="pathImage">path to image</param>
        /// <returns>new item</returns>
        private TreeViewItem AddItemTreeVew(DirectoryInfo info, string pathImage, string Name)
        {
            TreeViewItem item = new TreeViewItem();
            item.FontSize = 15;
            StackPanel pan = new StackPanel();
            pan.Orientation = Orientation.Horizontal;
            Image image = new Image();
            var imagePlayUri = new Uri(pathImage, UriKind.Absolute);
            image.Source = new BitmapImage(new Uri(pathImage));
            image.Width = 16;
            pan.Children.Add(image);
            pan.Children.Add(new TextBlock(new Run(Name)));
            item.Tag = info;
            item.Header = pan;
            item.Expanded += new RoutedEventHandler(TVRoot_Expanded);
            return item;
        }

        /// <summary>
        /// method for view Drive 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int count = 6;
            this.AddSpecFoldersFavorites();
            this.AddSpecFoldersMyComp();
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                TreeViewItem item = this.AddItemTreeVew(new DirectoryInfo(drive.Name), @"pack://application:,,,/Resources/icons8-hdd-16.png",
                    drive.ToString());
                this.TestFolder(new DirectoryInfo(drive.Name), item);
                this.ItemComputer.Items.Add(item);
                count++;
            }
            this.lbElemet.Content = string.Format("{0}:{1}",
              (this.lbElemet.Content.ToString().Remove(this.lbElemet.Content.ToString().LastIndexOf(':')))
              , count.ToString());
        }

        /// <summary>
        /// method for recurs walk dir
        /// </summary>
        /// <param name="root"></param>
        public void WalkToParent(DirectoryInfo root)
        {
            //if (root == null)
            //    return;
            //WalkToParent(root.Parent);
            var buf = this.element.GetRoot;
            Elemnts el = null;
            if (buf == null)
                el = new Dirs { Parent = this.element, Root = root };
            else
                el = new Dirs { Parent = buf, Root = root };
            this.element.Add(el);

        }

        /// <summary>
        /// method for view filder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TVRoot_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)e.OriginalSource;
            item.Items.Clear();
            DirectoryInfo dir = null;
            dir = (DirectoryInfo)item.Tag;
            try
            {
                this.saveRott = dir;
                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    TreeViewItem newItem = this.AddItemTreeVew(subDir, @"pack://application:,,,/Resources/icons8-folder-invoices-16.png",
                        subDir.ToString());
                    this.TestFolder(subDir, newItem);
                    item.Items.Add(newItem);
                }
                this.TVRoot.Focus();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// method for test empty folder
        /// </summary>
        /// <param name="root"></param>
        /// <param name="item"></param>
        void TestFolder(DirectoryInfo root, TreeViewItem item)
        {
            try
            {
                foreach (var i in root.GetDirectories())
                {
                    item.Items.Add("*");
                    return;
                }
            }
            catch (Exception e)
            {

            }

        }

        /// <summary>
        /// method for add item dir ListView
        /// </summary>
        /// <param name="newFile"></param>
        /// <param name="dir"></param>
        public void AddDirectoryFillListView(string newFile, DirectoryInfo dir)
        {
            ViewList mydata = new ViewList();
            mydata.FillDirectory(dir, dir.Name, @"pack://application:,,,/Resources/icons8-folder-invoices-16.png");
            ListViewItem item = AddFoldresListView(new DirectoryInfo(dir.FullName), mydata);
            this.LVInfo.Items.Add(item);
            this.TestSelected(item, dir);
            if (newFile != null)
            {
                if (dir.FullName == newFile)
                    item.IsSelected = true;
            }
        }

        public void AddFileFileListView(string newFile, FileInfo file)
        {
            ViewList mydata = new ViewList();
            mydata.FillFile(file);
            ListViewItem item = AddFoldresListView(new DirectoryInfo(file.FullName), mydata);
            this.LVInfo.Items.Add(item);
            if (newFile != null)
            {
                if (file.FullName == newFile)
                    item.IsSelected = true;
            }
        }

        /// <summary>
        /// method for filling ListView
        /// </summary>
        /// <param name="root"></param>
        private void FillListView(DirectoryInfo root, string newFile = null)
        {
            try
            {
                this.TestTread();
                int count = 0;
                if (root.Name.Contains("Searching results"))
                {
                    this.LVInfo.Items.Clear();
                    foreach (var dir in this.lstSearchDir)
                    {
                        this.AddDirectoryFillListView(newFile, dir);
                        count++;
                    }
                    foreach (var file in this.lstSearchFile)
                    {
                        this.AddFileFileListView(newFile, file);
                        count++;
                    }
                }
                else
                {
                    this.saveRott = root;
                    var test = root.GetDirectories();
                    this.LVInfo.Items.Clear();
                    foreach (var dir in root.GetDirectories())
                    {

                        this.AddDirectoryFillListView(newFile, dir);
                        count++;
                    }
                    foreach (var file in root.GetFiles())
                    {
                        this.AddFileFileListView(newFile, file);
                        count++;
                    }
                }
                this.lbElemet.Content = string.Format("{0}:{1}",
                    (this.lbElemet.Content.ToString().Remove(this.lbElemet.Content.ToString().LastIndexOf(':')))
                    , count.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Access is denied {0}", ex.Message.Remove(0, ex.Message.IndexOf('"'))), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// method for change selected
        /// </summary>
        private void TVRoot_SelectedItemChanged()
        {
            try
            {
                this.MenuDelete.Visibility = Visibility.Visible;
                this.TestTread();
                TreeViewItem i = (TreeViewItem)this.TVRoot.SelectedItem;
                DirectoryInfo root = (DirectoryInfo)i.Tag;
                this.element.Parent = null;
                this.element.Root = root;
                this.element.Reset();
                this.WalkToParent(root);
                this.btnFront.IsEnabled = false;
                this.Path.Text = this.element.GetActionNode.FullName;
                this.view[this.indexView](root, null);
            }
            catch (Exception ex)
            {
            }
        }


        /// <summary>
        /// method for add new items in TreeView
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="pathImage"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        private ListViewItem AddFoldresListView(DirectoryInfo path, ViewList mydata)
        {
            ListViewItem item = new ListViewItem();
            item.Content = mydata;
            item.Tag = path;
            return item;
        }

        /// <summary>
        /// method for save path
        /// </summary>
        /// <param name="root">content</param>
        private void AddSavePath(DirectoryInfo root, bool flag = false)
        {
            try
            {
                if (!flag)
                {
                    foreach (var file in root.GetFiles())
                    {
                        break;
                    }
                }
                if (root.Parent == null)
                {
                    this.element.Reset();
                    this.btnFront.IsEnabled = false;
                }
                var buf = this.element.GetRoot;
                Elemnts el = null;
                if (buf == null)
                    el = new Dirs { Parent = this.element, Root = root };
                else
                    el = new Dirs { Parent = buf, Root = root };
                this.element.Add(el);
                if (!flag)
                    this.Path.Text = this.element.GetActionNode.FullName;
                else
                    this.Path.Text = this.element.GetActionNode.Name;
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// method for test active thread
        /// </summary>
        private void TestTread()
        {
            try
            {
                if (this.thread.IsAlive)
                {
                    this.thread.Abort();
                    this.thread = null;
                    this.countElement = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// method for new information on ListView info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LVInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (this.copyContent.To != null)
                {
                    this.MenuInsert.Visibility = Visibility.Visible;
                }
                this.btnFile.IsEnabled = true;
                LVInfo_ContextMenuClosing(this, null);
                ListViewItem item = (ListViewItem)this.LVInfo.SelectedItem;
                this.search.IsEnabled = true;
                this.search.IsEnabled = true;
                if (this.Path.Text.Contains("My Computer"))
                {
                    this.element.Reset();
                    this.btnFront.IsEnabled = false;
                }
                try
                {
                    if (this.LVInfo.SelectedIndex == -1)
                        return;
                    DirectoryInfo root = (DirectoryInfo)item.Tag;
                    this.saveRott = root;
                    this.AddSavePath(root);
                    if (root.Attributes.ToString().Contains("Directory"))
                    {
                        this.TestTread();
                        this.view[this.indexView](root, null);
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(root.FullName);
                    }
                    this.MenuDelete.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    FileInfo root = (FileInfo)item.Tag;
                    System.Diagnostics.Process.Start(root.FullName);
                }
            }
            catch (Exception exx)
            {
                this.LVInfo.Items.Clear();
            }
        }

        /// <summary>
        /// method for change selected TreeView
        /// </summary>
        /// <param name="info"></param>
        private void ChangeSelectedTreeView(ListViewItem info)
        {
            foreach (TreeViewItem i in this.ItemComputer.Items)
            {
                if (i.Tag.ToString() == info.Tag.ToString())
                {
                    i.IsSelected = true;
                    i.IsExpanded = true;
                    break;
                }
                //else
                //    this.WalkTreeView(i, info);
            }
        }

        //private void WalkTreeView(TreeViewItem item, ListViewItem info)
        //{
        //    try
        //    {
        //        foreach (TreeViewItem i in item.Items)
        //        {
        //            DirectoryInfo dir = (DirectoryInfo)i.Tag;
        //            if (dir.FullName == info.Tag.ToString())
        //            {
        //                i.IsSelected = true;
        //                i.IsExpanded = true;
        //                break;
        //            }
        //            else
        //                this.WalkTreeView(i, info);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
        /// <summary>
        /// method for open button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            this.LVInfo_MouseDoubleClick(this, null);
        }


        /// <summary>
        /// method for open selected item TreeView ItemComputre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemComputer_Selected(object sender, RoutedEventArgs e)
        {
            this.SelectedTreeView(this.ItemComputer);

        }


        /// <summary>
        /// method for selected item TreeView ItemFavorites
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemFavorites_Selected(object sender, RoutedEventArgs e)
        {
            this.SelectedTreeView(this.ItemFavorites);
        }

        /// <summary>
        /// method for active seclected root TreeView
        /// </summary>
        /// <param name="root"></param>
        private void SelectedTreeView(TreeViewItem root)
        {
            this.LVInfo.IsEnabled = true;
            this.search.IsEnabled = true;
            this.btnFile.IsEnabled = true;
            try
            {
                if (root.IsSelected)
                {
                    this.btnFile.IsEnabled = false;
                    this.search.IsEnabled = false;
                    this.MenuDelete.Visibility = Visibility.Collapsed;
                    this.MenuCriate.Visibility = Visibility.Collapsed;
                    this.MenuRename.Visibility = Visibility.Collapsed;
                    this.LVInfo.Items.Clear();
                    foreach (var info in this.specFolders.GetSpecFoldersMyComp)
                    {
                        ListViewItem item = null;
                        string Name = info.Path.Remove(0, info.Path.LastIndexOf('\\') + 1);
                        if (this.indexView == 0)
                        {
                            ViewList mydata = new ViewList();
                            mydata.FillDirectory(new DirectoryInfo(info.Path), Name, info.PathImage);
                            item = this.AddFoldresListView(new DirectoryInfo(info.Path), mydata);
                        }
                        else if (this.indexView == 1)
                        {
                            item = this.AddListViewListFolder<DirectoryInfo>(new DirectoryInfo(info.Path), info.PathImage, Name);
                        }
                        else if (this.indexView == 2)
                        {

                        }
                        this.LVInfo.Items.Add(item);
                        this.TestSelected(item, new DirectoryInfo(info.Path));
                    }
                    foreach (var drive in DriveInfo.GetDrives())
                    {
                        ListViewItem item = null;
                        if (this.indexView == 0)
                        {
                            ViewList mydata = new ViewList();
                            mydata.FillDrive(drive, @"pack://application:,,,/Resources/icons8-hdd-16.png");
                            item = this.AddFoldresListView(new DirectoryInfo(drive.Name), mydata);
                        }
                        else if (this.indexView == 1)
                        {
                            item = this.AddListViewListFolder<DirectoryInfo>(new DirectoryInfo(drive.Name), @"pack://application:,,,/Resources/icons8-hdd-16.png", drive.Name);
                        }
                        this.LVInfo.Items.Add(item);
                        this.TestSelected(item, new DirectoryInfo(drive.Name));
                    }
                    this.MenuCopy.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.TVRoot_SelectedItemChanged();
                    this.MenuCriate.Visibility = Visibility.Visible;
                    this.MenuRename.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// method for change size ListView Path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Path.Width = this.ActualWidth - 300;
        }


        /// <summary>
        /// method for create new content
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            ListViewItem item = (ListViewItem)this.LVInfo.SelectedItem;
            DirectoryInfo root = null;
            MenuItem s = (MenuItem)e.OriginalSource;
            string key = s.Header.ToString();
            if (item == null)
            {
                TreeViewItem buf = (TreeViewItem)this.TVRoot.SelectedItem;
                root = this.saveRott;
                if (root == null)
                    root = (DirectoryInfo)buf.Tag;
                flag = true;
            }
            else
            {
                try
                {
                    root = (DirectoryInfo)item.Tag;
                }
                catch (Exception ex)
                {
                    FileInfo file = (FileInfo)item.Tag;
                    root = new DirectoryInfo(file.FullName);
                }
                root = root.Parent;
            }
            try
            {
                string Fullname = string.Empty;
                if (root.Parent == null)
                {
                    Fullname = root.FullName.Remove(root.FullName.IndexOf('\\'), 1);
                }
                else
                {
                    Fullname = root.FullName;
                }
                if (root.Attributes.ToString().Contains("Directory"))
                {

                    this.newContent.Create(key, Fullname);
                    this.view[this.indexView](new DirectoryInfo(root.FullName), this.newContent.NewFile);
                }
                else
                {
                    this.newContent.Create(key, root.FullName.Remove(root.FullName.LastIndexOf("\\")));
                    this.view[this.indexView](new DirectoryInfo(root.FullName), this.newContent.NewFile);
                }
                this.MenuItem_Click_1(this, null);
            }
            catch (Exception exx)
            {
                MessageBox.Show(string.Format("Access is denied {0}", exx.Message.Remove(0, exx.Message.IndexOf('"'))), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        /// method for open contex menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LVInfo_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            int j = 0;
            try
            {
                if (this.element.GetActionNode.FullName.Contains("Searching results"))
                    return;
            }
            catch (Exception ex)
            {

            }

            this.MenuCopy.Visibility = Visibility.Collapsed;
            foreach (var i in this.LVInfo.SelectedItems)
            {
                j++;
                this.MenuCopy.Visibility = Visibility.Visible;
                this.MenuRename.Visibility = Visibility.Visible;
                this.MenuDelete.Visibility = Visibility.Visible;
                this.MenuCriate.Visibility = Visibility.Visible;
                if (j > 1)
                {
                    this.MenuCriate.Visibility = Visibility.Collapsed;
                    this.MenuRename.Visibility = Visibility.Collapsed;
                    return;
                }
            }
            if (this.Path.Text == "My Computer")
            {
                this.MenuCopy.Visibility = Visibility.Collapsed;
                this.MenuInsert.Visibility = Visibility.Collapsed;
                this.MenuDelete.Visibility = Visibility.Collapsed;
                this.MenuRename.Visibility = Visibility.Collapsed;
                this.MenuCriate.Visibility = Visibility.Collapsed;
            }
        }

        ///<summary>
        ///method for close context menu
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        private void LVInfo_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            this.MenuCriate.Visibility = Visibility.Visible;
            this.MenuRename.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// method for rename content
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ListViewItem item = (ListViewItem)this.LVInfo.SelectedItem;
            string newPath = null;
            if (item != null)
            {
                WindowsRename wr = new WindowsRename();
                wr.ShowDialog();
                if (this.Name.Length != 0)
                {
                    try
                    {
                        DirectoryInfo dir = (DirectoryInfo)item.Tag;
                        Rename.ReName(dir.FullName, this.Name, ref newPath);
                        this.newContent.NewFile = newPath;
                        this.view[this.indexView](new DirectoryInfo(dir.Parent.FullName), this.newContent.NewFile);
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            FileInfo file = (FileInfo)item.Tag;
                            Rename.ReName(file.FullName, this.Name, ref newPath);
                            this.newContent.NewFile = newPath;
                            this.view[this.indexView](new DirectoryInfo(file.Directory.FullName), this.newContent.NewFile);
                        }
                        catch(Exception exx)
                        {
                            MessageBox.Show(string.Format("Access is denied {0}", ex.Message.Remove(0, ex.Message.IndexOf('"'))), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                       
                    }
                }
            }
        }

        /// <summary>
        /// method for delete files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.LVInfo.SelectedIndex == -1)
                return;
            List<DirectoryInfo> content = new List<DirectoryInfo>();
            foreach (ListViewItem item in this.LVInfo.SelectedItems)
            {
                try
                {
                    DirectoryInfo dir = (DirectoryInfo)item.Tag;
                    content.Add(dir);
                }
                catch (Exception ex)
                {
                    FileInfo file = (FileInfo)item.Tag;
                    content.Add(new DirectoryInfo(file.FullName));
                }
            }
            MessageBoxResult answer = new MessageBoxResult();
            answer = MessageBox.Show("Are you sure you want to delete this content?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                WInProgressBar pb = new WInProgressBar();
                MediatorProgressBar.Items(content, true);
                MediatorProgressBar.Root(this.saveRott);
                this.element.DeletePath(content);
                pb.ShowDialog();
                this.view[this.indexView](this.saveRott, null);
            }
        }

        /// <summary>
        /// method for copy files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuCopy_Click(object sender, RoutedEventArgs e)
        {
            this.copyContent.Reset();
            this.MenuInsert.Visibility = Visibility.Visible;
            foreach (ListViewItem item in this.LVInfo.SelectedItems)
            {
                try
                {
                    this.copyContent.To = (DirectoryInfo)item.Tag;
                }
                catch (Exception ex)
                {
                    FileInfo file = (FileInfo)item.Tag;
                    this.copyContent.To = new DirectoryInfo(file.FullName);
                }
            }
        }


        /// <summary>
        /// method for insert files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuInsert_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem item = (ListViewItem)this.LVInfo.SelectedItem;
            if (item != null)
            {
                try
                {
                    DirectoryInfo dir = (DirectoryInfo)item.Tag;
                    this.copyContent.In = dir.Parent;
                }
                catch (Exception ex)
                {
                    FileInfo file = (FileInfo)item.Tag;
                    this.copyContent.In = file.Directory;
                }

            }
            else
                this.copyContent.In = this.saveRott;
            var answer = MessageBox.Show("Are you sure you want to copy this content?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
            {
                WInProgressBar pb = new WInProgressBar();
                MediatorProgressBar.ActCopy(this.copyContent);
                MediatorProgressBar.Root(this.saveRott);
                pb.Show();
            }
        }

        /// <summary>
        /// method for button Medium
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMedium_Click(object sender, RoutedEventArgs e)
        {
            this.LVInfo.Style = null;
            this.indexView = 1;
            this.horizont = false;
            if (this.saveRott != null)
                this.view[this.indexView](this.element.GetActionNode, null);
            else
            {
                this.SelectedTreeView((TreeViewItem)this.TVRoot.SelectedItem);
            }
        }

        /// <summary>
        /// method for 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="pathImage"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public ListViewItem AddListViewListFolder<T>(T content, string pathImage, string Name)
        {
            ListViewItem item = new ListViewItem();
            StackPanel pan = new StackPanel();
            if (!horizont)
                pan.Orientation = Orientation.Horizontal;
            else
            {
                pan.Width = 90;
                pan.Height = 45;
                pan.HorizontalAlignment = HorizontalAlignment.Left;
            }
            Image image = new Image();
            image.Width = 16;
            image.Source = new BitmapImage(new Uri(pathImage));
            TextBlock tex = new TextBlock();
            tex.FontSize = 14;
            tex.TextTrimming = TextTrimming.WordEllipsis;
            tex.Text = Name;
            tex.HorizontalAlignment = HorizontalAlignment.Center;
            pan.Children.Add(image);
            pan.Children.Add(tex);
            item.Tag = content;
            item.Content = pan;
            if (horizont)
            {
                item.HorizontalAlignment = HorizontalAlignment.Left;
                item.VerticalAlignment = VerticalAlignment.Top;
                item.Width = 90;
                item.Height = 45;
            }

            return item;
        }

        /// <summary>
        /// method for testing selected
        /// </summary>
        /// <param name="item">item list view</param>
        /// <param name="dir">folder</param>
        private void TestSelected(ListViewItem item, DirectoryInfo dir)
        {
            try
            {
                if (this.element.GetActionFolder.FullName == dir.FullName)
                {
                    this.LVInfo.SelectedItem = item;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// method for add new Item dir
        /// </summary>
        /// <param name="newFile"></param>
        /// <param name="dir"></param>
        public void AddDirectoryFillListViewList(string newFile, DirectoryInfo dir)
        {
            ListViewItem item = this.AddListViewListFolder<DirectoryInfo>(dir, @"pack://application:,,,/Resources/icons8-folder-invoices-16.png", dir.Name);
            this.LVInfo.Items.Add(item);
            this.TestSelected(item, dir);
            if (newFile != null)
            {
                if (dir.FullName == newFile)
                    item.IsSelected = true;
            }
        }

        /// <summary>
        /// method for add file to ListView
        /// </summary>
        /// <param name="newFile"></param>
        /// <param name="file"></param>
        public void AddFileFillListViewList(string newFile, FileInfo file)
        {
            ListViewItem item = this.AddListViewListFolder<FileInfo>(file, @"pack://application:,,,/Resources/icons8-file-16.png", file.Name);
            this.LVInfo.Items.Add(item);
            if (newFile != null)
            {
                if (file.FullName == newFile)
                    item.IsSelected = true;
            }
        }
        /// <summary>
        /// method for filling listView
        /// </summary>
        /// <param name="root"></param>
        /// <param name="newFile"></param>
        private void FillListViewList(DirectoryInfo root, string newFile = null)
        {
            try
            {
                int count = 0;
                if (root.Name.Contains("Searching results"))
                {
                    this.LVInfo.Items.Clear();
                    foreach (var dir in this.lstSearchDir)
                    {
                        this.AddDirectoryFillListViewList(newFile, dir);
                        count++;
                    }
                    foreach (var file in this.lstSearchFile)
                    {
                        this.AddFileFillListViewList(newFile, file);
                        count++;
                    }
                }
                else
                {
                    var test = root.GetDirectories();
                    this.LVInfo.Items.Clear();
                    this.saveRott = root;
                    foreach (var dir in root.GetDirectories())
                    {
                        this.AddDirectoryFillListViewList(newFile, dir);
                        count++;
                    }
                    foreach (var file in root.GetFiles())
                    {
                        this.AddFileFillListViewList(newFile, file);
                        count++;
                    }
                }
                this.lbElemet.Content = string.Format("{0}:{1}",
                    (this.lbElemet.Content.ToString().Remove(this.lbElemet.Content.ToString().LastIndexOf(':')))
                    , count.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Access is denied {0}", ex.Message.Remove(0, ex.Message.IndexOf('"'))), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// method for click buttn Big
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBig_Click(object sender, RoutedEventArgs e)
        {
            this.LVInfo.Style = (Style)this.FindResource("List");
            this.indexView = 0;
            this.horizont = false;
            if (this.saveRott != null)
                this.view[this.indexView](this.element.GetActionNode, null);
            else
            {
                this.SelectedTreeView((TreeViewItem)this.TVRoot.SelectedItem);
            }
        }

        /// <summary>
        /// method for click button Medium
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMedium_Click_1(object sender, RoutedEventArgs e)
        {
            this.LVInfo.Style = (Style)this.FindResource("Horizont");
            this.indexView = 1;
            this.horizont = true;
            if (this.saveRott != null)
                this.view[this.indexView](this.element.GetActionNode, null);
            else
            {
                this.SelectedTreeView((TreeViewItem)this.TVRoot.SelectedItem);
            }
        }

        /// <summary>
        /// method for show
        /// </summary>
        /// <param name="root"></param>
        private void Show(DirectoryInfo root)
        {
            this.Dispatcher.Invoke(new Action(() =>
             {
                 if (root == this.saveRott)
                     this.view[this.indexView](this.saveRott, null);
             }));
        }

        /// <summary>
        /// method for click button back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dirs buf = (Dirs)this.element;
                buf--;
                this.view[this.indexView](buf.GetActionNode, null);
                if (buf.GetIndex() == 0)
                {
                    this.ItemComputer.IsSelected = true;
                    this.Path.Text = "My Computer";
                    SelectedTreeView((TreeViewItem)this.TVRoot.SelectedItem);
                    this.btnFront.IsEnabled = true;
                    return;
                }
                this.Path.Text = buf.GetActionNode.FullName;
                this.btnFront.IsEnabled = true;
            }
            catch (Exception ex)
            {
                //this.ItemComputer.IsSelected = true;
                //this.Path.Text = "My Computer";
                //SelectedTreeView((TreeViewItem)this.TVRoot.SelectedItem);
                //this.btnFront.IsEnabled = true;
                Dirs buf = (Dirs)this.element;
                this.Path.Text = buf.GetActionNode.FullName;
            }
        }

        /// <summary>
        /// method for click in button front
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Dirs buf = (Dirs)this.element;
                buf++;
                this.view[this.indexView](buf.GetActionNode, null);
                this.btnBack.IsEnabled = true;
                if (!this.element.GetActionNode.FullName.Contains("Searching results"))
                    this.Path.Text = buf.GetActionNode.FullName;
                else
                    this.Path.Text = buf.GetActionNode.Name;
            }
            catch (Exception ex)
            {
                Dirs buf = (Dirs)this.element;
                this.Path.Text = buf.GetActionNode.FullName;
            }
        }

        /// <summary>
        /// method for key down button enter for my TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            this.countElement = 0;
            if (e.Key == Key.Enter && this.search.Text.Length > 0)
            {
                this.btnFile.Visibility = Visibility.Collapsed;
                this.MenuCopy.Visibility = Visibility.Collapsed;
                this.MenuCriate.Visibility = Visibility.Collapsed;
                this.MenuDelete.Visibility = Visibility.Collapsed;
                this.MenuInsert.Visibility = Visibility.Collapsed;
                this.MenuRename.Visibility = Visibility.Collapsed;
                this.lstSearchDir.Clear();
                this.lstSearchFile.Clear();
                this.flagSearch = true;
                if (this.element.GetActionNode.FullName.Contains("Searching results"))
                {
                    List<DirectoryInfo> lst = new List<DirectoryInfo>();
                    lst.Add(this.element.GetActionNode);
                    this.element.DeletePath(lst);
                }
                DirectoryInfo node = this.element.GetActionNode;
                try
                {
                    this.AddSavePath(new DirectoryInfo(string.Format("Searching results {0}", this.element.GetActionNode.Name)), true);
                }
                catch (Exception ex)
                {
                    this.AddSavePath(new DirectoryInfo("Searching results info"), true);
                }
                this.thread = new Thread((object x) =>
                 {
                     string mask = this.GetMask();
                     this.ClearListView();
                     Search.SearchContent(node, mask);
                 });
                thread.Start();
            }
        }

        /// <summary>
        /// method for get mask
        /// </summary>
        /// <returns></returns>
        private string GetMask()
        {
            string test = string.Empty;
            this.Dispatcher.Invoke(new Action(() =>
                {
                    test = this.search.Text;
                }));
            return test;
        }

        /// <summary>
        /// method for get clear listview
        /// </summary>
        private void ClearListView()
        {
            this.Dispatcher.Invoke(new Action(() => this.LVInfo.Items.Clear()));
        }

        /// <summary>
        /// method for add search file
        /// </summary>
        /// <param name="file"></param>
        private void AddSearchFile(FileInfo file)
        {
            this.Dispatcher.Invoke(new Action(() =>
                {
                    //this.lstSearchDir.Clear();
                    //this.lstSearchFile.Clear();
                    ListViewItem item = null;
                    if (this.indexView == 1)
                        item = AddListViewListFolder<FileInfo>(file, @"pack://application:,,,/Resources/icons8-file-16.png", file.Name);
                    else
                    {
                        ViewList mydata = new ViewList();
                        mydata.FillFile(file);
                        item = AddFoldresListView(new DirectoryInfo(file.FullName), mydata);
                    }
                    this.LVInfo.Items.Add(item);
                    try
                    {
                        this.lstSearchFile.Add((FileInfo)item.Tag);
                    }
                    catch (Exception ex)
                    {
                        DirectoryInfo dir = (DirectoryInfo)item.Tag;
                        this.lstSearchFile.Add(new FileInfo(dir.FullName));
                    }
                    this.countElement++;
                    this.lbElemet.Content = string.Format("Elements: {0}", this.countElement.ToString());
                }));
        }

        /// <summary>
        /// method for add search dir
        /// </summary>
        /// <param name="dir"></param>
        private void AddSearchDir(DirectoryInfo dir)
        {
            this.Dispatcher.Invoke(new Action(() =>
                {
                    ListViewItem item = null;
                    if (this.indexView == 1)
                        item = AddListViewListFolder<DirectoryInfo>(dir, @"pack://application:,,,/Resources\icons8-folder-invoices-16.png", dir.Name);
                    else
                    {
                        ViewList mydata = new ViewList();
                        mydata.FillDirectory(dir, dir.Name, @"pack://application:,,,/Resources\icons8-folder-invoices-16.png");
                        item = AddFoldresListView(new DirectoryInfo(dir.FullName), mydata);
                    }
                    this.LVInfo.Items.Insert(0, item);
                    this.lstSearchDir.Add((DirectoryInfo)item.Tag);
                    this.countElement++;
                    this.lbElemet.Content = string.Format("Elements: {0}", this.countElement.ToString());
                }));
        }

        /// <summary>
        /// method for stop search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.lbElemet.Content = string.Format("Elements: {0}", this.countElement.ToString());
            this.TestTread();
        }

        /// <summary>
        /// method for about programm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.Show();
        }
    }
}
