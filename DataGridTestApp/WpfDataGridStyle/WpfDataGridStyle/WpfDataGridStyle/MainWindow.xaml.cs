using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfDataGridStyle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            customerDataGrid.ItemsSource = LoadCollectionData();
        }

        private List<Customer> LoadCollectionData()
        {
            List<Customer> customer = new List<Customer>();
            customer.Add(new Customer()
            {
                ID = 101,
                Name = "ABC",
                Title = "Mr.",
                DOB = new DateTime(1900, 1, 1),
                IsNew = false
            });

            customer.Add(new Customer()
            {
                ID = 201,
                Name = "XYZ",
                Title = "Mr.",
                DOB = new DateTime(1982, 2, 20),
                IsNew = true
            });

            customer.Add(new Customer()
            {
                ID = 244,
                Name = "JKL",
                Title = "Dr.",
                DOB = new DateTime(1985, 5, 15),
                IsNew = true
            });
            customer.Add(new Customer()
            {
                ID = 101,
                Name = "ABC",
                Title = "Mr.",
                DOB = new DateTime(1900, 1, 1),
                IsNew = false
            });

            customer.Add(new Customer()
            {
                ID = 201,
                Name = "XYZ",
                Title = "Mr.",
                DOB = new DateTime(1982, 2, 20),
                IsNew = true
            });

            customer.Add(new Customer()
            {
                ID = 244,
                Name = "JKL",
                Title = "Dr.",
                DOB = new DateTime(1985, 5, 15),
                IsNew = true
            });
            customer.Add(new Customer()
            {
                ID = 101,
                Name = "ABC",
                Title = "Mr.",
                DOB = new DateTime(1900, 1, 1),
                IsNew = false
            });

            customer.Add(new Customer()
            {
                ID = 201,
                Name = "XYZ",
                Title = "Mr.",
                DOB = new DateTime(1982, 2, 20),
                IsNew = true
            });

            customer.Add(new Customer()
            {
                ID = 244,
                Name = "JKL",
                Title = "Dr.",
                DOB = new DateTime(1985, 5, 15),
                IsNew = true
            });
            return customer;

        }
    }

    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Title { get; set; }
        public bool IsNew { get; set; }
    }
}