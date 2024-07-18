using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Major { get; set; }
    public int Year { get; set; }
}

class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public int Credits { get; set; }
}

class Enrollment
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public string Grade { get; set; }
}

class UniversitySystem
{
    private List<Student> students = new List<Student>();
    private List<Course> courses = new List<Course>();
    private List<Enrollment> enrollments = new List<Enrollment>();
    private int nextStudentId = 1;
    private int nextCourseId = 1;

    public void Run()
    {
        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStudent();
                    break;
                case "2":
                    AddCourse();
                    break;
                case "3":
                    EnrollStudent();
                    break;
                case "4":
                    AssignGrade();
                    break;
                case "5":
                    ViewStudents();
                    break;
                case "6":
                    ViewCourses();
                    break;
                case "7":
                    ViewEnrollments();
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    private void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("=== University Students Database System ===");
        Console.WriteLine("1. Add Student");
        Console.WriteLine("2. Add Course");
        Console.WriteLine("3. Enroll Student in Course");
        Console.WriteLine("4. Assign Grade");
        Console.WriteLine("5. View All Students");
        Console.WriteLine("6. View All Courses");
        Console.WriteLine("7. View All Enrollments");
        Console.WriteLine("8. Exit");
        Console.Write("Enter your choice (1-8): ");
    }

    private void AddStudent()
    {
        Console.Write("Enter student name: ");
        string name = Console.ReadLine();
        Console.Write("Enter student major: ");
        string major = Console.ReadLine();
        Console.Write("Enter student year (1-4): ");
        if (int.TryParse(Console.ReadLine(), out int year) && year >= 1 && year <= 4)
        {
            students.Add(new Student { Id = nextStudentId++, Name = name, Major = major, Year = year });
            Console.WriteLine("Student added successfully.");
        }
        else
        {
            Console.WriteLine("Invalid year. Student not added.");
        }
    }

    private void AddCourse()
    {
        Console.Write("Enter course name: ");
        string name = Console.ReadLine();
        Console.Write("Enter course department: ");
        string department = Console.ReadLine();
        Console.Write("Enter course credits: ");
        if (int.TryParse(Console.ReadLine(), out int credits) && credits > 0)
        {
            courses.Add(new Course { Id = nextCourseId++, Name = name, Department = department, Credits = credits });
            Console.WriteLine("Course added successfully.");
        }
        else
        {
            Console.WriteLine("Invalid credits. Course not added.");
        }
    }

    private void EnrollStudent()
    {
        Console.Write("Enter student ID: ");
        if (int.TryParse(Console.ReadLine(), out int studentId) && students.Any(s => s.Id == studentId))
        {
            Console.Write("Enter course ID: ");
            if (int.TryParse(Console.ReadLine(), out int courseId) && courses.Any(c => c.Id == courseId))
            {
                if (!enrollments.Any(e => e.StudentId == studentId && e.CourseId == courseId))
                {
                    enrollments.Add(new Enrollment { StudentId = studentId, CourseId = courseId, Grade = "N/A" });
                    Console.WriteLine("Student enrolled successfully.");
                }
                else
                {
                    Console.WriteLine("Student is already enrolled in this course.");
                }
            }
            else
            {
                Console.WriteLine("Invalid course ID. Enrollment failed.");
            }
        }
        else
        {
            Console.WriteLine("Invalid student ID. Enrollment failed.");
        }
    }

    private void AssignGrade()
    {
        Console.Write("Enter student ID: ");
        if (int.TryParse(Console.ReadLine(), out int studentId))
        {
            Console.Write("Enter course ID: ");
            if (int.TryParse(Console.ReadLine(), out int courseId))
            {
                Enrollment enrollment = enrollments.FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);
                if (enrollment != null)
                {
                    Console.Write("Enter grade: ");
                    string grade = Console.ReadLine();
                    enrollment.Grade = grade;
                    Console.WriteLine("Grade assigned successfully.");
                }
                else
                {
                    Console.WriteLine("Enrollment not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid course ID.");
            }
        }
        else
        {
            Console.WriteLine("Invalid student ID.");
        }
    }

    private void ViewStudents()
    {
        Console.WriteLine("Students:");
        foreach (var student in students)
        {
            Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Major: {student.Major}, Year: {student.Year}");
        }
    }

    private void ViewCourses()
    {
        Console.WriteLine("Courses:");
        foreach (var course in courses)
        {
            Console.WriteLine($"ID: {course.Id}, Name: {course.Name}, Department: {course.Department}, Credits: {course.Credits}");
        }
    }

    private void ViewEnrollments()
    {
        Console.WriteLine("Enrollments:");
        foreach (var enrollment in enrollments)
        {
            Student student = students.First(s => s.Id == enrollment.StudentId);
            Course course = courses.First(c => c.Id == enrollment.CourseId);
            Console.WriteLine($"Student: {student.Name}, Course: {course.Name}, Grade: {enrollment.Grade}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        UniversitySystem universitySystem = new UniversitySystem();
        universitySystem.Run();
    }
}