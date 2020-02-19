using Eindopdracht.Classes;
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
        DataView landen;
        string selectedName;
        public MainWindow()
        {
            InitializeComponent();

            lbNames.UpdateLayout();
            lbNames.ItemsSource = db.GetUser();
            lbNames.DisplayMemberPath = "FLnames";

            landen = db.GetLanden();
            foreach (var land in landen)
            {
                lbCountries.Items.Add(land);
            }

        }

        private void btnCustomerPlus_Click(object sender, RoutedEventArgs e)
        {
            AddUser au = new AddUser();
            au.Show();
            this.Close();
        }

        private void btnCustomerMinus_Click(object sender, RoutedEventArgs e)
        {
            db.Removeuser(lbNames.SelectedItem.ToString());
        }

        private void lbNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedName = lbNames.SelectedItem.ToString();
            MessageBox.Show(selectedName);
            lbSingleName.Items.Add(selectedName);
        }
    }
}
