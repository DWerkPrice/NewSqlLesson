using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SqlLibrary
{
    public class MajorController
    {
        public static BcConnection bcConnection { get; set; }

        private static Major LoadMajorInstance(SqlDataReader reader) {// passing what we need to create code block usually need parameters when creating this type of method
            var major = new Major();
            major.Id = Convert.ToInt32(reader["Id"]);
            major.Description = reader["Description"].ToString();
            major.MinSAT = Convert.ToInt32(reader["MinSat"]);
            return major;
        }

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
                var major = LoadMajorInstance(reader);
                // next 3 lines are getting a row from sequal(property) and are stuffing the values into the instance class of major
//                major.Id = Convert.ToInt32(reader["Id"]);
//                major.Description = reader["Description"].ToString();
//                major.MinSAT = Convert.ToInt32(reader["MinSat"]);
                majors.Add(major);
            }
            reader.Close();
            reader = null;
            return majors;
        }

        public static Major GetMajorByPk(int id) {
            var sql = "SELECT * from Major Where Id = @Id; ";
            var command =new SqlCommand(sql , bcConnection.Connection);
            command.Parameters.AddWithValue("@id" , id);
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
                return null;
            }
            reader.Read();
            var major = LoadMajorInstance(reader);
//          next 4 lines in LoadMajorInstance(reader) by refactoring
//            var major=new Major();
//            major.Id = Convert.ToInt32(reader["Id"]);
//            major.Description = reader["Description"].ToString();
//            major.MinSAT = Convert.ToInt32(reader["MinSat"]);
            reader.Close();
            reader = null;
            return major;
        }

        public static bool InsertMajor(Major major) {
            var sql = $"INSERT into Major (Id, Description, MinSAT) "
            + $"Values(@Id, @Description, @MinSAT); ";


            var command = new SqlCommand(sql , bcConnection.Connection);
            command.Parameters.AddWithValue("@Id" , major.Id);
            command.Parameters.AddWithValue("@Description" , major.Description);
            command.Parameters.AddWithValue("@MinSAT" , major.MinSAT);
            //recs plural because it will be used in other methods that can affect more than one 
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                Console.WriteLine("Here");
                throw new Exception("Insert Failed"); // most likely failure is in the data passed
                // no return needed because throw exits the method on its own
            }
            return true;
        }

        public static bool UpdateMajor(Major major) {
            var sql = "UPDATE MAJOR SET " +
                "Id = @Id, " +
                "Description = @Description, " +
                "MinSAT = @MinSAT " +
                "Where Id = @Id;";
            var command = new SqlCommand(sql , bcConnection.Connection);
            command.Parameters.AddWithValue("@ID", major.Id);
            command.Parameters.AddWithValue("@Description" , major.Description);
            command.Parameters.AddWithValue("@MinSAT" , major.MinSAT);
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Update Failed"); // most likely failure is in the data passed
                // no return needed because throw exits the method on its own
            }
            return true;
        }


    }
}