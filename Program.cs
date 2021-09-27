using BackTest.Models;
using System;
using System.Collections.Generic;

namespace BackTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Teacher!");
            Console.WriteLine("Please enter the total of students : ");
            String nEstudiantes = Console.ReadLine();

            Console.WriteLine("Please enter the grades for all the students");
            for(int i = 1; i < Convert.ToInt32(nEstudiantes) + 1; i++) {
                for(int n = 1; n < 4; n++)
                {
                    Console.WriteLine("Student No " + i + " - Grade No " + n + " : ");
                    String nota = Console.ReadLine();
                    Student estudiante = new Student(i, Convert.ToInt32(nota));
                    if (Student.saveStudentGrade(estudiante))
                    {
                        Console.WriteLine("Student No " + i  + " - Grade No " + n + " grade saved correctly");
                    }
                }
                Console.WriteLine("===============================================");
            }
            Console.WriteLine("============== AVERAGE GRADES =================");
            List<Student> students = Student.getStudentAverage();
            int sum = 0;
            foreach(Student student in students) { 
                Console.WriteLine("Student No " + student.IdEstudiante + " - Average Grade : " + student.Nota);
                sum += student.Nota;
            }

            Console.WriteLine("===============================================");
            int result = sum / students.Count;
            Console.WriteLine("Average grade for the whole class : " + result);

            Console.WriteLine("Press enter to exit the application, thank you");
            String resultF = Console.ReadLine();

        }
    }
}
