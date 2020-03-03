

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
        DataView _landen;
        DataView _personen;
        DataView _favourites;
        DataRow selectedCountryToRemove;
        DataRow selectedUser;
        DataRow selectedCountry;

        public DataView Landen { get => _landen; set { _landen = value; NotifyPropertyChanged();  } }
        public DataView Personen { get => _personen; set { _personen = value; NotifyPropertyChanged(); } }
        public DataView Favourit { get => _favourites; set { _favourites = value; NotifyPropertyChanged(); } }

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
            if (db.GetFavoritesCountry(int.Parse(selectedUser[0].ToString())).Count < 3)
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

        private void btnFavoritesMinus_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCountryToRemove != null)
            {

                db.RemoveFavorite(int.Parse(selectedUser[0].ToString()), int.Parse(selectedCountryToRemove[1].ToString()));
                MessageBox.Show("Het volgende favoriete land: " + selectedCountryToRemove[0].ToString() + " Is succesvol verwijderd van " + selectedUser[1].ToString());
                GetFavorites();
            }
            else
            {
                MessageBox.Show("Kies eerst een land");
            }
        }

        private void GetFavorites()
        {
            Favourit = db.GetFavoritesCountry(int.Parse(selectedUser[0].ToString()));
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
