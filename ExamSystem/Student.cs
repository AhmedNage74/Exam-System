using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSystem
{
    public class Student
    {
        public string Name { get; set; }
        public List<Subject> EnrolledSubjects { get; set; } = new();

        public Student(string name)
        {
            Name = name;
        }

        public void SubscribeToExam(Exam<Question> exam)
        {
            if (EnrolledSubjects.Contains(exam.Subject))
            {
                exam.OnModeChanged += OnExamStart;  
                Console.WriteLine($"{Name} subscribed to {exam.Subject.Name} exam.");
            }
            else
            {
                Console.WriteLine($"{Name} is not enrolled in {exam.Subject.Name}.");
            }
        }

        private void OnExamStart(string message)
        {
            Console.WriteLine($"{Name} notified: {message}");
        }
    }


}
