namespace ExamSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Subject math = new("Math", "Algebra");

            QuestionList questions = new("MathQuestions");
            questions.Add(new TrueFalseQuestion("Is 2+2=4?", 1, "Q1", true));
            questions.Add(new ChooseOneQuestion("What is 2*3?", 2, "Q2", new[] { "5", "6", "7" }, 1));

            PracticeExam practice = new(60, questions.Count, math);
            FinalExam final = new(90, questions.Count, math);

            foreach (var q in questions)
            {
                AnswerList answers = new(q);
                answers.Add(new Answer("User Answer", false));  
                practice.QuestionAnswers[q] = answers;
                final.QuestionAnswers[q] = answers;
            }

            Student s1 = new("Ali");
            s1.EnrolledSubjects.Add(math);
            s1.SubscribeToExam(practice);  // ربط

            Console.WriteLine("Choose Exam Type: 1=Practice, 2=Final");
            int choice = int.Parse(Console.ReadLine());

            Exam<Question> selected = choice == 1 ? practice : final;


            selected.SetMode(ExamMode.Starting);  // الإشعار
            selected.TakeExam();
        }
    }
}
