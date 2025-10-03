using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSystem
{
    public class Subject
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Subject(string name, string description = "")
        {
            Name = name;
            Description = description;
        }

        public override string ToString() => $"{Name}: {Description}";
    }
}
