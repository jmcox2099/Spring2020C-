using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
namespace Justin.CodeLou.ExerciseProject
{
    class Program
    {
        static string _studentRepositoryPath = $"{AppDomian.CurrentDomain.BaseDirectory}\\students.json";


        static List<Student> studentsList = File.Exists(_studentRepositoryPath) ? Read : new List<Student>();
        static void Save()
        {
             var serializedStudents = JsonSerializer.Serialize(studentsList);
             using (var writer = File.CreateText(_studentRepositoryPath))
             {
                 writer.Write(serializedStudents);
             }
        }

        static List<Student> Read()
        {
            return JsonSerializer.Deserialize<List<Student>>(File.ReadAllText(_studentRepositoryPath));

        } 
        static void Main(string[] args)

        {            
            var inputtingStudent = true;

            while (inputtingStudent)
            {
                DisplayMenu();
                var option = Console.ReadLine();

                switch (int.Parse(option))
                {
                    case 1:
                        InputStudent();
                        break;
                    case 2:
                        DisplayStudents();
                        break;
                    case 3:
                        SearchStudents();
                        break;
                    case 4:
                        inputtingStudent = false;
                        break;
                }
                Console.ReadLine();
            }
        } 

        private static void DisplayStudents(List<Student> students)
        {
            if(students.Any())
            {
                Console.WriteLine($"Name | Student ID | Class");
                studentsList.ForEach(x => {Console.WriteLine(x.StudentDisplay);});
            }
            else
            {
                System.Console.WriteLine("No students found.");
            }
        }

        private static void DisplayStudents() => DisplayStudents(studentsList);
       
        private static void SearchStudents()
        {
            Console.WriteLine("Search student name: ");
            var studentSearch = Console.ReadLine();
            var students = studentsList.Where(x => x.FullName.ToLower().Contains(studentSearch.ToLower()) || (x.ClassName.ToLower().Contains(studentSearch.ToLower())));
            DisplayStudents(students.ToList());
        }
        
        private static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Select from the following operations:");
            Console.WriteLine("1: Enter new student");
            Console.WriteLine("2: List all students");
            Console.WriteLine("3: Search for student by name");
            Console.WriteLine("4: Exit");
        }   

        static void InputStudent()
        {
            var student = new Student();
            while(true)
            {
                Console.WriteLine("Please enter student ID: ");
                var studentIDSuccessful = int.TryParse(Console.ReadLine(), out var studentID);
                if(studentIDSuccessful)
                {
                    student.StudentId = studentID;
                    break; 
                }    
            }

            while(true)
            {
                Console.WriteLine("Please enter the last class completed (MM/DD/YYYY): ");
                var lastClassCompletedOnSuccessful = DateTimeOffset.TryParse(Console.ReadLine(), out var lastClassCompletedOn);
                if(lastClassCompletedOnSuccessful)
                {
                    student.LastClassCompletedOn = lastClassCompletedOn;
                    break; 
                }    
            }

            while(true)
            {
                Console.WriteLine("Please enter the date the class was started on (MM/DD/YYYY): ");
                var startDateSuccessful = DateTime.TryParse(Console.ReadLine(), out var startDate);
                if(startDateSuccessful)
                {
                    student.StartDate = startDate;
                    break; 
                }    
            }
        
            Console.WriteLine("Enter First Name");
            var studentFirstName = Console.ReadLine();
            Console.WriteLine("Enter Last Name");
            var studentLastName = Console.ReadLine();
            Console.WriteLine("Enter Class Name");
            var className = Console.ReadLine();
            Console.WriteLine("Enter Last Class Completed");
            var lastClass = Console.ReadLine();
            
            studentsList.Add(student);
            Save();
        }    
  
    }
}
