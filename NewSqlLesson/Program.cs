﻿using System;
using SqlLibrary;

namespace SqlLesson
{

    class Program
    {

        static void Main(string[] args) {

            var sqllib = new BcConnection();
            sqllib.Connect(@"localhost\sqlexpress" , "EdDb" , "trusted_connection=true");
            Student.bcConnection = sqllib; // setting property to instance
            var students = student.GetAllStudents();
            sqllib.Disconnect();
        }
    }
}
