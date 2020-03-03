using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.Classes
{
    public class Database
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb; 
                                                Initial Catalog=FavouriteCountries;Integrated Security=True");

        public DataView GetLanden()
        {
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select * from Countries";

            SqlDataReader reader = command.ExecuteReader();

            DataTable dtData = new DataTable();
            dtData.Load(reader);

            conn.Close();

            return dtData.DefaultView;
        }

        public DataView GetUser()
        {
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select PersonId, concat(FirstName, ' ', LastName) as FLnames from People";

            SqlDataReader reader = command.ExecuteReader();

            DataTable dtData = new DataTable();
            dtData.Load(reader);

            conn.Close();

            return dtData.DefaultView;
        }

        public void AddUser(string firstName, string lastName)
        {
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandText = "INSERT INTO People (FirstName, LastName) VALUES (@voornaam, @achternaam); ";
            command.Parameters.AddWithValue("@voornaam", firstName);
            command.Parameters.AddWithValue("@achternaam", lastName);

            command.ExecuteNonQuery();

            conn.Close();
        }

        public void RemoveUser(int ID)
        {
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandText = "DELETE FROM People WHERE PersonId = @id";
            command.Parameters.AddWithValue("@id", ID);

            command.ExecuteNonQuery();

            conn.Close();
        }
        
        public void AddFavorite(int PersonID, int CountryID)
        {
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandText = "INSERT INTO Favourites (PersonId, CountryId) VALUES (@persoonid, @landid); ";
            command.Parameters.AddWithValue("@persoonid", PersonID);
            command.Parameters.AddWithValue("@landid", CountryID);

            command.ExecuteNonQuery();

            conn.Close();
        }

        public void RemoveFavorite(int PersonID, int CountryID)
        {
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandText = "DELETE FROM Favourites WHERE PersonId = @persoonid and CountryId = @countryid; ";
            command.Parameters.AddWithValue("@persoonid", PersonID);
            command.Parameters.AddWithValue("@countryid", CountryID);

            command.ExecuteNonQuery();

            conn.Close();
        }

        public DataView GetFavoritesCountry(int PersonID)
        {
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select Countries.country, Countries.id from Favourites INNER JOIN Countries ON Countries.id = Favourites.CountryId where PersonId = @personid;";
            command.Parameters.AddWithValue("@personid", PersonID);

            SqlDataReader reader = command.ExecuteReader();

            DataTable dtData = new DataTable();
            dtData.Load(reader);

            conn.Close();

            return dtData.DefaultView;
        }
    }
}
