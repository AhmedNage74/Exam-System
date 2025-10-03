using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSystem
{
    public abstract class Question : ICloneable, IComparable<Question>
    {
        public string Body { get; set; }  
        public int Marks { get; set; }   
        public string Header { get; set; } 

        protected Question() : this("", 0, "") { }  

        protected Question(string body, int marks, string header)
        {
            Body = body;
            Marks = marks;
            Header = header;
        }

        public abstract void Display();  

        public object Clone() => MemberwiseClone();

        public int CompareTo(Question other) => Marks.CompareTo(other.Marks);

        public override string ToString() => $"{Header}: {Body} ({Marks} marks)";

        public override bool Equals(object obj) => obj is Question q && Body == q.Body && Marks == q.Marks;

        public override int GetHashCode() => HashCode.Combine(Body, Marks);
    }

    public class TrueFalseQuestion : Question
    {
        public bool CorrectAnswer { get; set; }  

        public TrueFalseQuestion(string body, int marks, string header, bool correct)
            : base(body, marks, header)
        {
            CorrectAnswer = correct;
        }

        public override void Display()
        {
            Console.WriteLine($"{Header}: {Body}\nOptions: True or False");
        }
    }

    public class ChooseOneQuestion : Question
    {
        public string[] Options { get; set; } = new string[0];
        public int CorrectIndex { get; set; }  // فهرس الإجابة الصح

        public ChooseOneQuestion(string body, int marks, string header, string[] options, int correctIndex)
            : base(body, marks, header)
        {
            Options = options;
            CorrectIndex = correctIndex;
        }

        public override void Display()
        {
            Console.WriteLine($"{Header}: {Body}");
            for (int i = 0; i < Options.Length; i++)
                Console.WriteLine($"\t{i + 1}. {Options[i]}");
        }
    }

    public class ChooseAllQuestion : Question
    {
        public string[] Options { get; set; } = new string[0];
        public bool[] CorrectIndices { get; set; } = new bool[0];  

        public ChooseAllQuestion(string body, int marks, string header, string[] options, bool[] correct)
            : base(body, marks, header)
        {
            Options = options;
            CorrectIndices = correct;
        }

        public override void Display()
        {
            Console.WriteLine($"{Header}: {Body} (Choose all that apply)");
            for (int i = 0; i < Options.Length; i++)
                Console.WriteLine($"\t{i + 1}. {Options[i]}");
        }
    }
}
