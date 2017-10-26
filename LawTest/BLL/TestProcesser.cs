using LawTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawTest.BLL
{
    class TestProcesser
    {
        public Mark GetResult(Answers answers)
        {
            var correctAnswersCount = CalculateAnswers(answers);
            return GetMark(correctAnswersCount, answers.AnswersList.Count);
        }

        int CalculateAnswers(Answers answers)
        {
            return answers.AnswersList.
                        FindAll(answer => answer.ChosenAnswer == answer.CorrectAnswer).Count;
        }

        Mark GetMark(double correctAnswers, double tasksCount)
        {
            var correctPercentage = correctAnswers / (tasksCount / 100.0);
            if (correctPercentage < 30.0) return Mark.UNSATISFACTORY;
            if (correctPercentage < 50.0) return Mark.SATISFACTORY;
            if (correctPercentage < 80.0) return Mark.GOOD;
            return Mark.EXCELLENT;
        }

    }
}
