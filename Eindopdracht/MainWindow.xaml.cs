﻿using Eindopdracht.Classes;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Eindopdracht
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database db = new Database();
        DataView names;
        public MainWindow()
        {
            InitializeComponent();

            names = db.GetLanden();
            foreach (var name in names)
            {
                lbCountries.Items.Add(name);
            }
        }

        private void btnCustomerPlus_Click(object sender, RoutedEventArgs e)
        {
            AddUser au = new AddUser();
            au.Show();
        }
    }
}
