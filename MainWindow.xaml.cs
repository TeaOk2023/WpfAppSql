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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppSql1
{
    public partial class MainWindow : Window
    {
        ApplicationContext db = new ApplicationContext();
        private int count = 1;
        public MainWindow()
        {
            InitializeComponent();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureCreated();
                Loaded += MainWindow_Loaded;
            }
        }


        // при загрузке окна
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            // загружаем данные из БД
            db.Cars.Load();
            db.Companies.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Cars.Local.ToObservableCollection(); 
            usersList.Items.Refresh();
        }

        // добавление
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddWindow AddWindow = new AddWindow(new Car());
            if (AddWindow.ShowDialog() == true)
            {
                Car Car = AddWindow.Car;
                db.Cars.Add(Car);
                db.SaveChanges();
                usersList.Items.Refresh();
            }
        }
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Car? car = usersList.SelectedItem as Car;
            // если ни одного объекта не выделено, выходим
            if (car is null) return;
            db.Cars.Remove(car);
            db.SaveChanges();
            usersList.Items.Refresh();
        }

        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Car? car = usersList.SelectedItem as Car;
            // если ни одного объекта не выделено, выходим
            if (car is null) return;

            AddWindow AddWindow = new AddWindow(new Car
            {
                CarId = car.CarId,
                year = car.year,
                Name = car.Name,
                probeg = car.probeg,
                CompanyId = car.CompanyId,
                Company = car.Company,
            });

            if (AddWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                car = db.Cars.Find(AddWindow.Car.CarId);
                if (car != null)
                {
                    car.year = AddWindow.Car.year;
                    car.Name = AddWindow.Car.Name;
                    car.probeg = AddWindow.Car.probeg;
                    car.CompanyId = AddWindow.Car.CompanyId;
                    usersList.Items.Refresh();
                }
            }
        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        
        private void Add_company(object sender, RoutedEventArgs e)
        {
            CompanyAdd CompanyAdd = new CompanyAdd(new Company());

            if (CompanyAdd.ShowDialog() == true)
            {
                Company Company = CompanyAdd.Company;
                db.Companies.Add(Company);
                db.SaveChanges(); //foring key
            }
        }
        //sort
        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            if (count % 2 == 0)
            {
                int a = this.TabIndex;
                using (ApplicationContext db = new ApplicationContext())
                {
                    usersList.DataContext = db.Cars.OrderBy(p => p.year).ToList();
                    db.Companies.Load();
                }
            }
            else
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    usersList.DataContext = db.Cars.OrderByDescending(p => p.year).ToList();
                    db.Companies.Load();
                }
            }
            usersList.Items.Refresh();
            count++;

        }
        //search
        private void Searchbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(Searchbox.Text, "[^0-9]"))
            {
                MessageBox.Show("Пожалуйста, вводите только цифры.");
                Searchbox.Text = Searchbox.Text.Remove(Searchbox.Text.Length - 1);
            }
            int search;
            bool success = int.TryParse(Searchbox.Text, out search);
            if(success)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    usersList.DataContext = db.Cars.Where(p => p.probeg == search).ToList();
                    db.Companies.Load();
                }
            }
            if (Searchbox.Text == string.Empty)
            {
                usersList.DataContext = db.Cars.ToList();
                usersList.Items.Refresh();
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
            MessageBox.Show("Данные сохранены!");
        }
        private void exti_click(object sender, RoutedEventArgs e)
        {
            CloseWin CloseWin = new CloseWin();
            if (CloseWin.ShowDialog() == true)
            {
                    db.SaveChanges();
            }
            Close(); //Предложить сохранить изменеия
        }
    }
}
