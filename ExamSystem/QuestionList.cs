using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSystem
{
    public class QuestionList : List<Question>
    {
        private string _filePath;  

        public QuestionList(string fileName)
        {
            _filePath = $"./{fileName}.txt";  
        }

        public new void Add(Question item)
        {
            base.Add(item);  

            using (TextWriter writer = new StreamWriter(_filePath, true))  
            {
                writer.WriteLine($"Added: {item.ToString()}");  
            }
        }
    }
}
