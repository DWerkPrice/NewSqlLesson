using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SqlLibrary
{
    public class StudentController {
        // establish connection

        public static BcConnection bcConnection { get; set; }


        public static List<Student> GetAllStudents() { // pass the database an sql command
            var sql = "SELECT * From Student s left join Major m on m.Id= s.MajorId;";// sql syntax command to be passed
            var command = new SqlCommand(sql , bcConnection.Connection);
            // reads the database and if statement checks for actual row of result
            var reader = command.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                reader = null;
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
                if (Convert.IsDBNull(reader["Description"])) {

                    student.Major = null;


                } else {
                    var major = new Major {
                        Description = reader["Description"].ToString() ,
                        MinSAT = Convert.ToInt32(reader["MinSat"])

                    };
                }
                //         student.MajorId = Convert.IsDBNull(reader["MajorId"])) ? null: Conve;
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
                reader.Close();
                reader = null;
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

        public static bool InsertStudent(Student student) {
            var majorid = "";
            if (student.MajorId == null) {
                majorid = "NULL";
            } else {
                majorid = student.MajorId.ToString();
            }


            //var sql = $"INSERT into Student (Id, Firstname, Lastname, SAT, GPA, MajorId) " +
            //$"VALUES ({student.Id} , '{student.Firstname}', '{student.Lastname}', {student.SAT}, {student.GPA}, {majorid};";
            //  @region SQL with parameters
            var sql = $"INSERT into Student (Id, Firstname, Lastname, SAT, GPA, MajorId) "
            + $"Values(@Id, @Firstname, @Lastname, @SAT, @GPA, @MajorId); ";


            var command = new SqlCommand(sql , bcConnection.Connection);
            command.Parameters.AddWithValue("@Id" , student.Id);
            command.Parameters.AddWithValue("@Firstname" , student.Firstname);
            command.Parameters.AddWithValue("@Lastname" , student.Lastname);
            command.Parameters.AddWithValue("@SAT" , student.SAT);
            command.Parameters.AddWithValue("@GPA" , student.GPA);
            command.Parameters.AddWithValue("@MajorId" , student.MajorId ?? Convert.DBNull);
            //recs plural because it will be used in other methods that can affect more than one 
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                Console.WriteLine("Here");
                throw new Exception("Insert Failed"); // most likely failure is in the data passed
                // no return needed because throw exits the method on its own
            }
            return true;
        }

        public static bool UpdateStudent(Student student) {
            var sql = "Update Student Set" +
                " Firstname = @Firstname', " +
                " Lastname = @Lastname, " +
                " SAT= @SAT" +
                " GPA = @GPA" +
                " MajorId = @MajorID " +
                " Where Id = @Id; ";
            var command = new SqlCommand(sql , bcConnection.Connection);
            command.Parameters.AddWithValue("@Id" , student.Id);
            command.Parameters.AddWithValue("@Firstname" , student.Firstname);
            command.Parameters.AddWithValue("@Lastname" , student.Lastname);
            command.Parameters.AddWithValue("@SAT" , student.SAT);
            command.Parameters.AddWithValue("@GPA" , student.GPA);
            command.Parameters.AddWithValue("@MajorId" , student.MajorId ?? Convert.DBNull);
            //recs plural because it will be used in other methods that can affect more than one 
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Update Failed"); // most likely failure is in the data passed
                // no return needed because throw exits the method on its own
            }
            return true;
        }

        public static bool DeleteStudent(Student student) {
            var sql = "delete from student where id = @Id";
            var command = new SqlCommand(sql , bcConnection.Connection);
            command.Parameters.AddWithValue("@Id" , student.Id);
            var recsAffected = command.ExecuteNonQuery();
            if (recsAffected != 1) {
                throw new Exception("Delete Failed");
            }
            return true;
        }

        public static bool DeleteStudent(int id) {
            var student = GetStudentByPK(id);
            if (student == null) {
                return false;
            }
            var success = DeleteStudent(student);
            return true;
        }
    }
}