using LawTest.Model;
using LawTest.BLL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace LawTest
{
    public partial class Form1 : Form
    {
        TestUnit TestUnit;
        Answers Answers;
        TestProcesser TestProcessor;
        int taskNumber;
        int font = 9;
        DataTable dt = new DataTable();
        Student student;
        long StartTime;
        Stopwatch Stopwatch;

        public Form1(TestUnit testUnit, int _font, string studentFio, string studentGroup)
        {
            Stopwatch = new Stopwatch();
            StartTime = Stopwatch.ElapsedMilliseconds;
            TestUnit = testUnit;
            InitializeComponent();
            font = _font;
            student = new Student
            {
                FIO = studentFio,
                Group = studentGroup,
                TestName = testUnit.TestName
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TestProcessor = new TestProcesser();
            Answers = new Answers(TestUnit);
            setTaskSettings();
            label1.Font = new Font(label1.Font.FontFamily, font);
            label2.Font = new Font(label2.Font.FontFamily, font);
            label3.Font = new Font(label3.Font.FontFamily, font);
            radioButton1.Font = new Font(radioButton1.Font.FontFamily, font);
            radioButton2.Font = new Font(radioButton2.Font.FontFamily, font);
            radioButton3.Font = new Font(radioButton3.Font.FontFamily, font);
            radioButton4.Font = new Font(radioButton4.Font.FontFamily, font);
            groupBox1.Font = new Font(groupBox1.Font.FontFamily, font);
            groupBox2.Font = new Font(groupBox2.Font.FontFamily, font);
            button1.Font = new Font(button1.Font.FontFamily, font);
            button2.Font = new Font(button2.Font.FontFamily, font);

            dt.Columns.Add("Вопрос");
            dt.Columns.Add("Варианты ответов");
            dt.Columns.Add("Правильный ответ");
            dt.Columns.Add("Комментарий");
            dt.Columns.Add("+");
        }

        private void setTaskSettings()
        {
            if (taskNumber < TestUnit.Tasks.Count)
            {
                var currentTask = TestUnit.Tasks[taskNumber];
                var chosenAnswer = Answers.AnswersList[taskNumber].ChosenAnswer;
                label1.Text = taskNumber + 1 + " " + currentTask.TaskDescription;

                // Установка текста вариантов ответа
                for (int i = 0; i < currentTask.Choices.Count; i++)
                {
                    GetButtonRef(i).Text = currentTask.Choices[i];
                    GetButtonRef(i).Visible = true;
                }
                for (int i = currentTask.Choices.Count; i < 4; i++)
                {
                    GetButtonRef(i).Visible = false;
                }

                // Установка состояний ответов
                if (chosenAnswer == -1)
                {
                    for (int i = 0; i < currentTask.Choices.Count; i++)
                        GetButtonRef(i).Checked = false;
                }
                else
                {
                    for (int i = 0; i < currentTask.Choices.Count; i++)
                        if (i == chosenAnswer)
                        {
                            GetButtonRef(i).Checked = true;
                        }
                        else GetButtonRef(i).Checked = false;
                }

                // Проверка на окончание теста
                if (taskNumber + 1 == TestUnit.Tasks.Count)
                {
                    button1.Text = "Финиш";
                }
            }
            else
            {
                var mark = TestProcessor.GetResult(Answers);
                var correctAnswers = TestProcessor.CalculateAnswers(Answers);
                var incorrectAnswers = TestUnit.Tasks.Count - correctAnswers;
                var tasksNum = TestUnit.Tasks.Count;
                //MessageBox.Show($"Тестирование завершено \r\nВсего вопросов: {tasksNum} \r\nПравильно: {correctAnswers} ", "Информация");
                TestProcessor.SaveResultToFile(student, TestUnit, Answers, Stopwatch.ElapsedMilliseconds - StartTime);


                FormResult fr = new FormResult(dt, font, tasksNum, correctAnswers, TestProcessor.GetResult(Answers));
                fr.ShowDialog();

                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int answer = setAns();
            if (answer != -1)
            {
                var currentTask = TestUnit.Tasks[taskNumber];
                //результат, по которому красится
                string flag = "";
                if(currentTask.Choices[answer].ToString() == currentTask.Choices[currentTask.CorrectAnswer].ToString())
                {
                    flag = "+";
                } else
                {
                    flag = "-";
                }
                //добавление вариантов
                string answers = "";
                for (int i = 0; i< currentTask.Choices.Count;i++) {
                    if (currentTask.Choices[currentTask.CorrectAnswer].ToString() == currentTask.Choices[i].ToString())
                    {
                        answers += "+";
                    }
                    else
                    {
                        // answers += "\u2022";
                        answers += "-";
                    }
                    answers += currentTask.Choices[i];
                    if (i+1 != currentTask.Choices.Count)
                    {
                        answers += "\r\n";
                    }
                }
                //dt.Rows.Add(label1.Text, currentTask.Choices[currentTask.CorrectAnswer], currentTask.Choices[answer], currentTask.Tip,flag);
                dt.Rows.Add(label1.Text, answers, currentTask.Choices[answer], currentTask.Tip, flag);

                Answers.EditAnswer(taskNumber, answer);
                taskNumber += 1;
                setTaskSettings();
                label2.Text = "";
                label3.Text = "";


            }
            else
            {
                MessageBox.Show("Выберите вариант ответа", "Информация");
            }
        }

        private RadioButton GetButtonRef(int index)
        {
            switch (index)
            {
                case 0: return radioButton1;
                case 1: return radioButton2;
                case 2: return radioButton3;
                case 3: return radioButton4;
                default: throw new Exception("Could not get reference to button");
            }
        }

        private int setAns()
        {
            for (int i = 0; i < TestUnit.Tasks[taskNumber].Choices.Count; i++)
                if (GetButtonRef(i).Checked) return i;
            return -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var answer = setAns();
            button1.Text = "Далее";
            if (taskNumber - 1 != -1)
            {
                if (answer != -1)
                    Answers.EditAnswer(taskNumber, answer);
                taskNumber--;
                setTaskSettings();

                dt.Rows.RemoveAt(dt.Rows.Count - 1);

            }
            else
            {
                MessageBox.Show("Вы уже вернулись в начало теста", "Информация");
            }
        }
    }
}
