using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WPF___OOP
{
    public class SqlManagement
    {
        private string connectionString = @"Data Source=LAPTOP-S00DPV1P;Initial Catalog=MovieList;Integrated Security=true";
        private SqlConnection connection;
        private SqlCommand command;

        public SqlManagement()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void RunNonQuery(string query)
        {
            command = connection.CreateCommand();
            command.CommandText = query;
            command.ExecuteNonQuery();
            connection.Close();
        }

        public string RunQuery(string query)
        {
            string queryResult = "";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (reader.FieldCount > 1 && i < reader.FieldCount - 1)
                            {
                                queryResult += reader.GetValue(i) + ",";
                            }
                            else
                            {
                                queryResult += reader.GetValue(i);
                            }

                        }

                    }
                }
            }
            connection.Close();
            return queryResult;
        }

        public List<string> RunQueryList(string query)
        {
            List<string> queryResultList = new List<string>();
            string bufferQuery = "";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (reader.FieldCount > 1 && i < reader.FieldCount - 1)
                            {
                                bufferQuery += reader.GetValue(i) + ",";
                            }
                            else
                            {
                                bufferQuery += reader.GetValue(i);
                                queryResultList.Add(bufferQuery);
                                bufferQuery = "";
                            }

                        }

                    }
                }
            }
            connection.Close();
            return queryResultList;
        }
    }
}
