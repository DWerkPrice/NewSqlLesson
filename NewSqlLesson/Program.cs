using System;
using SqlLibrary;

namespace SqlLesson
{

    class Program
    {

        static void Main(string[] args) {

            var sqllib = new BcConnection();
            sqllib.Connect(@"localhost\sqlexpress" , "EdDb" , "trusted_connection=true");
            StudentController.bcConnection = sqllib; // setting property to instance

            var student = StudentController.GetStudentByPK(100);
            if(student == null) {
                Console.WriteLine("Student with primary key not found");

            }else {
                Console.WriteLine(student);
            }

            var students = StudentController.GetAllStudents();
            foreach(var student0 in students) {
                Console.WriteLine(student0);
            }
            sqllib.Disconnect();
        }
    }
}
