

using Eindopdracht.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Eindopdracht
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        Database db = new Database();
        DataView landen;
        DataView _personen;
        DataView favorites;
        DataRow selectedCountryToRemove;
        DataRow selectedUser;
        DataRow selectedCountry;
        List<int> selecteduserbyid;

        public DataView Landen { get => landen; set { landen = value; NotifyPropertyChanged();  } }

        public DataView Personen { get => _personen; set { _personen = value; NotifyPropertyChanged(); } }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Personen = db.GetUser();
            Landen = db.GetLanden();
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
            Personen = db.GetUser();
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
