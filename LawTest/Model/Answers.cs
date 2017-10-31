using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawTest.Model
{
    public class Answers
    {
        List<Answer> answers;

        public Answers(TestUnit testUnit) 
        {
            answers = new List<Answer>();
            answers.AddRange(testUnit.Tasks.
                                        Select(task => new Answer{ CorrectAnswer = task.CorrectAnswer,
                                                                   ChosenAnswer = -1}));
        }

        public List<Answer> AnswersList { get
            {
                return answers;
            }
        }

        public void EditAnswer(int answerIndex, int chosenAnswer)
        {
            answers[answerIndex].ChosenAnswer = chosenAnswer;
        }

    }

    public class Answer
    {
        public int ChosenAnswer { get; set; }
        public int CorrectAnswer { get; set; }
    }
}
