using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SqlLibrary
{
    public class MajorController
    {
        public static BcConnection bcConnection { get; set; }

        public static List<Major> GetAllMajors() {
            var sql = "Select * From Major; ";
            var command = new SqlCommand(sql , bcConnection.Connection); // can use cmd to abbreviate command
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                Console.WriteLine("No rows for GetAllMajors");
                return new List<Major>();
            }
            var majors = new List<Major>();
            while (reader.Read()) {
                var major = new Major();
                major.Id = Convert.ToInt32(reader["Id"]);
                major.Description = reader["Description"].ToString();
                major.MinSAT = Convert.ToInt32(reader["MinSat"]);
                majors.Add(major);
            }
            reader.Close();
            reader = null;
            return majors;
        }
    }
}