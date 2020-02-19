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
    }
}
