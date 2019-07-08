using MySql.Data.MySqlClient;
using QueueSimulator.Models;
using System.Collections.Generic;
using System;

namespace QueueSimulator
{
    public class DbContext
    {
        public string ConnectionString { get; set; }

        public DbContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public int ExecuteQuery(string sqlCommand)
        {
            int result;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
                result = cmd.ExecuteNonQuery();
            }
            return result;
        }

        public List<Patient> GetPatientsDb(string sqlCommand)
        {
            List<Patient> list = new List<Patient>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Patient()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            PatientName = reader["PatientName"].ToString(),
                            Status = reader["Status"].ToString(),
                            Piority = reader["Priority"].ToString(),
                            GSC = Convert.ToInt32(reader["GSC"]),
                            Inspection = Convert.ToInt32(reader["Inspection"]),
                            BP = Convert.ToInt32(reader["BP"]),
                            RR = Convert.ToInt32(reader["RR"]),
                            POX = Convert.ToInt32(reader["POX"]),
                            HR = Convert.ToInt32(reader["HR"]),
                            RLS = Convert.ToInt32(reader["RLS"])
                            //Temperature = Convert.ToDouble(reader["Temperature"])
                        });
                    }
                }
            }
            return list;
        }
    }
}
