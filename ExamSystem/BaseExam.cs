using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSystem
{
    public enum ExamMode { Starting, Queued, Finished }

    public delegate void ExamNotification(string message);

    public abstract class Exam<T> : ICloneable, IComparable<Exam<T>> where T : Question  
    {
        public int Time { get; set; }  
        public int NumQuestions { get; set; }
        public Dictionary<T, AnswerList> QuestionAnswers { get; set; } = new();
        public Subject Subject { get; set; } 
        public ExamMode Mode { get; set; } = ExamMode.Queued;

        public event ExamNotification OnModeChanged;

        protected Exam() : this(0, 0, null) { }

        protected Exam(int time, int numQuestions, Subject subject)
        {
            Time = time;
            NumQuestions = numQuestions;
            Subject = subject;
        }

        public virtual void TakeExam()  
        {
            Console.WriteLine($"Starting {Subject.Name} Exam - Time: {Time} min, Questions: {NumQuestions}");
            int totalScore = 0;

            foreach (var kvp in QuestionAnswers)
            {
                Question q = kvp.Key;
                AnswerList answers = kvp.Value;

                q.Display();  

                Console.Write("Enter your answer: ");
                string userInput = Console.ReadLine();  
                answers.AddUserAnswer(userInput);  

                if (this is PracticeExam)
                {
                    int qScore = answers.CalculateScore();
                    totalScore += qScore;
                    Console.WriteLine($"Your score for this question: {qScore}/{q.Marks}");
                    if (qScore == 0)
                    {
                        Console.WriteLine("Correct answer: " + GetCorrectAnswer(q));  
                    }
                }
            }

            if (this is PracticeExam)
            {
                Console.WriteLine($"\nTotal Score: {totalScore}/{NumQuestions * /* افتراض marks max */ 3}");  // عدل الـmax
            }
            SetMode(ExamMode.Finished); 
        }

        private string GetCorrectAnswer(Question q)
        {
            if (q is TrueFalseQuestion tf) return tf.CorrectAnswer.ToString();
            if (q is ChooseOneQuestion co) return co.Options[co.CorrectIndex];
            return "Multiple correct";
        }

        public void SetMode(ExamMode mode)
        {
            Mode = mode;
            if (mode == ExamMode.Starting)
            {
                OnModeChanged?.Invoke($"Exam starting for {Subject.Name}!");  
            }
        }

        public object Clone() => MemberwiseClone();

        public int CompareTo(Exam<T> other) => NumQuestions.CompareTo(other.NumQuestions);

        public override string ToString() => $"Exam: {Subject.Name} ({NumQuestions} questions, {Time} min)";

        public override bool Equals(object obj) => obj is Exam<T> e && NumQuestions == e.NumQuestions && Subject == e.Subject;

        public override int GetHashCode() => HashCode.Combine(NumQuestions, Subject);
    }
}
