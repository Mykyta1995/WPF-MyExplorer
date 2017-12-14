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
    /// Логика взаимодействия для PropertyWindows.xaml
    /// </summary>
    public partial class PropertyWindows : Window
    {
        /// <summary>
        /// constructor default
        /// </summary>
        public PropertyWindows()
        {
            InitializeComponent();
            MediatreInfoWin.Windows = (info) => this.lbWindows.Content = string.Format("{0}{1}", this.lbWindows.Content.ToString(), info);
            MediatreInfoWin.Proccesor = (info) => this.lbProccesor.Content = string.Format("{0}    {1}", this.lbProccesor.Content.ToString(), info);
            MediatreInfoWin.RAM = (info) => this.lbRAM.Content = string.Format("{0}    {1}", this.lbRAM.Content.ToString(), info);
            MediatreInfoWin.VideoController = (info) => this.lbVideoController.Content = string.Format("{0}    {1}", this.lbVideoController.Content.ToString(), info);
            CharecterPC.Windows();
            CharecterPC.Proccesor();
            CharecterPC.RAM();
            CharecterPC.VideoController();
            this.Image.Source = new BitmapImage(new Uri(@"pack://application:,,,/Resources/icons8-windows8-96.png"));
        }
    }
}
