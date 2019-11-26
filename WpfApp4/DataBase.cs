using System.Data.Sql;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
    class DataBase
    {
        SqlConnection connection = new SqlConnection("Data Source=PROVOD;Initial Catalog=Test0;Integrated Security=True");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }


        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }


        public SqlConnection getConnection()
        {
            return connection;
        }
    }
}
