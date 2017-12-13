using LawTest.Model;
using System;
using System.IO;

namespace LawTest.BLL
{
    class TestProcesser
    {
        public Mark GetResult(Answers answers)
        {
            var correctAnswersCount = CalculateAnswers(answers);
            return GetMark(correctAnswersCount, answers.AnswersList.Count);
        }

        public int CalculateAnswers(Answers answers)
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

        public void SaveResultToFile(Student student, TestUnit testUnit, Answers answers, long time, int mark)
        {   
            string fileText = $"Выполнил студент {student.FIO} из группы {student.Group}\r\n";
            var i = 1;
            foreach (var task in testUnit.Tasks)
            {
                fileText += $"{i}. {task.TaskDescription} - {answers.AnswersList[i-1].ChosenAnswer}\r\n";
                i++;
            }
            fileText += $"Оценка - {mark}";
            Directory.CreateDirectory("./StudentsTests");
            File.WriteAllText($"./StudentsTests/{DateTime.Now.ToString().Replace(":", "-")} {student.FIO}.txt", fileText);
        }

    }
}
