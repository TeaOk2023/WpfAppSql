using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private List<Company> cam;

        public Car Car { get; private set; }
              

        public AddWindow(Car car)
        {
            InitializeComponent();

            
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureCreated();
                cam = db.Companies.ToList();
                company_box.ItemsSource = cam;
            }
                Car = car;
            DataContext = Car;
        }




        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void company_box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int pos;//company_box.
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureCreated();
                pos = db.Companies.Where(p => p.Title == company_box.SelectedItem.ToString()).ToList()[0].Id;
            }
            //this.Car.Company = company_box.SelectedItem.ToString();
            this.Car.CompanyId = pos;/*(company_box.SelectedIndex+1);*/
        }
    }
}
