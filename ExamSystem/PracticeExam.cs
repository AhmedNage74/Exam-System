using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSystem
{
    public class PracticeExam : Exam<Question>
    {
        public PracticeExam(int time, int numQuestions, Subject subject) : base(time, numQuestions, subject) { }

        public override void TakeExam()  // override للـbase
        {
            base.TakeExam();  // يدعي الـbase اللي بيحسب الدرجة
        }
    }

    public class FinalExam : Exam<Question>
    {
        public FinalExam(int time, int numQuestions, Subject subject) : base(time, numQuestions, subject) { }

        public override void TakeExam()         
        {
            foreach (var kvp in QuestionAnswers)
            {
                Question q = kvp.Key;
                AnswerList answers = kvp.Value;

                q.Display();
                Console.Write("Enter your answer: ");
                string userInput = Console.ReadLine();
                answers.AddUserAnswer(userInput);
            }
            Console.WriteLine("\nExam Finished! Answers saved.");
            SetMode(ExamMode.Finished);
        }
    }
}
