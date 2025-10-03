using System;
using System.Collections.Generic;
using System.Text;

namespace ExamSystem
{
    public class Answer
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public Answer(string text, bool isCorrect)
        {
            Text = text;
            IsCorrect = isCorrect;
        }

        public override string ToString() => $"{Text} ({(IsCorrect ? "Correct" : "Wrong")})";
    }

    public class AnswerList : List<Answer>
    {
        public Question AssociatedQuestion { get; set; }
        public List<string> UserAnswers { get; set; } = new();  

        public AnswerList(Question question)
        {
            AssociatedQuestion = question;
        }

        public void AddUserAnswer(string userInput)
        {
            UserAnswers.Add(userInput);
        }

        public int CalculateScore()
        {
            int score = 0;
            
            if (AssociatedQuestion is TrueFalseQuestion tf)
            {
                bool userBool = bool.Parse(UserAnswers[0]); 
                if (userBool == tf.CorrectAnswer) score = AssociatedQuestion.Marks;
            }
            else if (AssociatedQuestion is ChooseOneQuestion co)
            {
                int userChoice = int.Parse(UserAnswers[0]) - 1;  
                if (userChoice == co.CorrectIndex) score = AssociatedQuestion.Marks;
            }
            return score;
        }
    }
}
