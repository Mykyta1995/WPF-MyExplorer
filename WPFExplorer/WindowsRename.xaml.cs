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

namespace WPFExplorer
{
    /// <summary>
    /// Логика взаимодействия для WindowsRename.xaml
    /// </summary>
    public partial class WindowsRename : Window
    {
        /// <summary>
        /// constructor default
        /// </summary>
        public WindowsRename()
        {
            InitializeComponent();
           
        }

        /// <summary>
        /// method for btnOk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MediatorWinRename.NameFile(this.BoxName.Text);
            this.Close();
        }

        /// <summary>
        /// method for enabled 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.BoxName.Text.Length == 0)
                this.btnOk.IsEnabled = false;
            else
                this.btnOk.IsEnabled = true;
        }
    }
}
