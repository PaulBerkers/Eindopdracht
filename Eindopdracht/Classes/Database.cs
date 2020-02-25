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
                                                Initial Catalog=Countries;Integrated Security=True");

        public DataView GetLanden()
        {
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select * from tblLanden";

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
            command.CommandText = "select PersonId, concat(FirstName, ' ', LastName) as FLnames from Mensen";

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
            command.CommandText = "INSERT INTO Mensen (FirstName, LastName) VALUES (@voornaam, @achternaam); ";
            command.Parameters.AddWithValue("@voornaam", firstName);
            command.Parameters.AddWithValue("@achternaam", lastName);

            command.ExecuteNonQuery();

            conn.Close();
        }

        public void RemoveUser(int ID)
        {
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandText = "DELETE FROM Mensen WHERE PersonId = @id";
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

        public DataView GetFavorites(int PersonID)
        {
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select CountryId from Favourites where PersonId = @persoonsid";
            command.Parameters.AddWithValue("@persoonsid", PersonID);

            SqlDataReader reader = command.ExecuteReader();

            DataTable dtData = new DataTable();
            dtData.Load(reader);

            conn.Close();

            return dtData.DefaultView;
        }
        public DataView GetFavoritesCountry(int CountryID)
        {
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select * from tbllanden where code = @landid";
            command.Parameters.AddWithValue("@landid", CountryID);

            SqlDataReader reader = command.ExecuteReader();

            DataTable dtData = new DataTable();
            dtData.Load(reader);

            conn.Close();

            return dtData.DefaultView;
        }
    }
}
