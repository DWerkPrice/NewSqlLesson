using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SqlLibrary
{
    public class StudentController
    {
        // establish connection
   
        public static BcConnection bcConnection { get; set; }

       
        public static List<Student> GetAllStudents() { // pass the database an sql command
            var sql = "SELECT * From Student";// sql syntax command to be passed
            var command = new SqlCommand(sql , bcConnection.Connection);
        // reads the database and if statement checks for actual row of result
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                Console.WriteLine("No rows from GetAllStudents()");
                return new List<Student>();
            }
            // reader.Read() reads the actual data base
            // while student creates the instance of the data by reading the data
            // everything comes back as a string so non-strings have to 
            // reader.[] sends back an object => class object which has the
            // methods that include ToString() which converts object to a string
            var students = new List<Student>();
            while (reader.Read()) {
                var student = new Student();
                student.Id = Convert.ToInt32(reader["Id"]);
                student.Firstname = reader["Firstname"].ToString();
                student.Lastname = reader["Lastname"].ToString();
                student.SAT = Convert.ToInt32(reader["SAT"]);
                student.GPA = Convert.ToDouble(reader["GPA"]);
                //student.MajorId = Convert.ToInt32(reader["MajorId"]);
                students.Add(student);  // adds student to collection of students
            }// checks for row beyond bottom
            reader.Close();
            reader = null;
            return students;// returns collection of students
        }

        public static Student GetStudentByPK(int id) { // could call it just GetByPK()
            var sql = $"SELECT * from Student Where ID = {id}";// if it was a string you have to enclose id with '' and should be tested some people would just put this in command directly without generating a variable
            var command = new SqlCommand(sql , bcConnection.Connection); // gets command ready to go
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                return null;
            }
            reader.Read();
            var student = new Student();
            student.Id = Convert.ToInt32(reader["Id"]);
            student.Firstname = reader["Firstname"].ToString();
            student.Lastname = reader["Lastname"].ToString();
            student.SAT = Convert.ToInt32(reader["SAT"]);
            student.GPA = Convert.ToDouble(reader["GPA"]);
            //student.MajorId = Convert.ToInt32(reader["MajorId"]);
            reader.Close();
            reader = null;
            return student;              

        }
    }
}
