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
        DataView favorites;
        DataRow selectedCountryToRemove;
        DataRow selectedUser;
        DataRow selectedCountry;
        List<int> selecteduserbyid;

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
            db.RemoveUser(int.Parse(selectedUser[0].ToString()));
            MessageBox.Show("We hebben de volgende gebruiker verwijderd " + selectedUser[1].ToString());
            lbNames.ItemsSource = db.GetUser();
        }

        private void lbNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lbSingleName.Items.Count > 0)
                {
                    lbSingleName.Items.Remove(selectedUser[1]);
                }
                selectedUser = ((DataRowView)lbNames.SelectedItem).Row;

                lbSingleName.Items.Add(selectedUser[1]);
                GetFavorites();

            }
            catch (Exception)
            {

            }
           
        }

        private void lbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCountry = ((DataRowView)lbCountries.SelectedItem).Row;
        }

        private void btnFavoritesPlus_Click(object sender, RoutedEventArgs e)
        {
            if (lbSingleName.Items.Count > 0)
            {
                if (lbFavorites.Items.Count < 3)
                { 
                    db.AddFavorite(int.Parse(selectedUser[0].ToString()), int.Parse(selectedCountry[0].ToString()));
                    MessageBox.Show(selectedUser[1].ToString() + " heeft het volgende favoriete land: " + selectedCountry[1].ToString());
                    GetFavorites();
                }
                else
                {
                    MessageBox.Show("U kunt maar 3 favorieten landen hebben haal er eerst eentje weg voordat je een nieuwe toevoegd");
                }
            }
            else
            {
                MessageBox.Show("Selecteer eerst een gebruiker");
            }
        }

        private void btnFavoritesMinus_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCountryToRemove != null)
            {
                db.RemoveFavorite(int.Parse(selectedUser[0].ToString()), int.Parse(selectedCountryToRemove[0].ToString()));
                MessageBox.Show("Het volgende favoriete land: " + selectedCountryToRemove[1].ToString() + " Is succesvol verwijderd van " + selectedUser[1].ToString());
                GetFavorites();
            }
            else
            {
                MessageBox.Show("Kies eerst een land");
            }
        }

        private void GetFavorites()
        {
            selecteduserbyid = db.GetCountryIDsByPersonID(int.Parse(selectedUser[0].ToString()));

            lbFavorites.Items.Clear();

            if (selecteduserbyid != null)
            {
                lbFavorites.DisplayMemberPath = "country";

                foreach (var land in selecteduserbyid)
                {
                    favorites = db.GetFavoritesCountry(land);
                    foreach (var favoriet in favorites)
                    {
                        lbFavorites.Items.Add(favoriet);
                    }
                }
              
            }
        }

        private void lbFavorites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedCountryToRemove = ((DataRowView)lbFavorites.SelectedItem).Row;
            }
            catch (Exception)
            {

            }
        }
    }
}
