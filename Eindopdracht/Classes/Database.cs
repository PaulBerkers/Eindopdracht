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
            command.CommandText = "select concat(FirstName, ' ', LastName) as FLnames from Mensen";

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

        public void Removeuser(string firstName)
        {
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandText = "DELETE FROM Mensen WHERE FirstName = @voornaam";
            command.Parameters.AddWithValue("@voornaam", firstName);

            command.ExecuteNonQuery();

            conn.Close();
        }
    }
}
