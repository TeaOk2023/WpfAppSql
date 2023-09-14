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

namespace WpfAppSql1.Windows
{
    /// <summary>
    /// Interaction logic for ExportWin.xaml
    /// </summary>
    public partial class ExportWin : Window
    {
        public ExportWin()
        {
            InitializeComponent();
        }
        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
