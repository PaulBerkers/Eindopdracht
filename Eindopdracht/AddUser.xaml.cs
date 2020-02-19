using Eindopdracht.Classes;
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

namespace Eindopdracht
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        Database db = new Database();
        public AddUser()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbFirstName.Text != null && tbLastName.Text != null)
                {
                    db.AddUser(tbFirstName.Text, tbLastName.Text);
                    MessageBox.Show("We hebben " + tbFirstName.Text + " " + tbLastName.Text + " toegevoegd aan de lijst");
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Voer aub beide velden in!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Er is iets mis gegaan probeer het opnieuw");
            }
        }
    }
}
