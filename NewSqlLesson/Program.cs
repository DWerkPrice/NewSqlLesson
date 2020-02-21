using System;
using SqlLibrary;

namespace SqlLesson
{

    class Program
    {

        static void Main(string[] args) {

            var sqllib = new BcConnection();
            sqllib.Connect(@"localhost\sqlexpress" , "EdDb" , "trusted_connection=true");
           
            MajorController.bcConnection = sqllib;
            
            var Major = MajorController.GetMajorByPk(1);
            Console.WriteLine(Major);

            var majors = MajorController.GetAllMajors();
            foreach(var major in majors) {
                Console.WriteLine(major);
            }

            // insert new major
//            var newMajor = new Major {
//                Id = 11 ,
//                Description = "Graphics Design" ,
//                MinSAT = 1100
//            };
//            var successmjins = MajorController.InsertMajor(newMajor);


            StudentController.bcConnection = sqllib; // setting property to instance

            var newStudent = new Student {
            Id = 885,
            Firstname = "Crazy",
            Lastname = "Eights",
            SAT = 600,
            GPA = 0.00,
            MajorId = 1
            };
          //  var success = StudentController.InsertStudent(newStudent);

            var student = StudentController.GetStudentByPK(newStudent.Id);
            if(student == null) {
                Console.WriteLine("Student with primary key not found");

            }else {
                Console.WriteLine(student);
            }



            student.Firstname = "Charlie";
            student.Lastname = "Chan";
            var success = StudentController.UpdateStudent(student);
/*
            var studentToDelete = new Student {
                Id = 885
            };
            success = StudentController.DeleteStudent(885);
*/

            var students = StudentController.GetAllStudents();
            foreach(var student0 in students) {
                Console.WriteLine(student0);
            }
            sqllib.Disconnect();
        }
    }
}
