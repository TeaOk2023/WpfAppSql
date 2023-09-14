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

namespace WpfAppSql1
{
    /// <summary>
    /// Interaction logic for Company.xaml
    /// </summary>
    public partial class CompanyAdd : Window
    {
        public Company Company { get; private set; }

        public CompanyAdd(Company company)
        {
            InitializeComponent();

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureCreated();
                Company = company;
                DataContext = Company;
            }

        }




        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
