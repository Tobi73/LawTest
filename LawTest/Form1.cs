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
        Student student;
        Stopwatch Stopwatch;

        int taskNumber;
        int font = 9;
        long startTime;
        bool testFinished = false;

        Mark mark;
        int correctAnswers;
        int incorrectAnswers;

        public Form1(TestUnit testUnit, int _font, string studentFio, string studentGroup)
        {
            Stopwatch = new Stopwatch();
            startTime = Stopwatch.ElapsedMilliseconds;
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
        }

        private void setTaskSettings()
        {
            if (taskNumber < TestUnit.Tasks.Count)
            {
                button1.Visible = true;

                var currentTask = TestUnit.Tasks[taskNumber];
                int chosenAnswer = Answers.AnswersList[taskNumber].ChosenAnswer;
                label1.Text = taskNumber + 1 + " " + currentTask.TaskDescription;

                tipLabel.Visible = testFinished;

                // Установка текста вариантов ответа
                for (int i = 0; i < currentTask.Choices.Count; i++)
                {
                    GetButtonRef(i).Text = currentTask.Choices[i];
                    GetButtonRef(i).Visible = true;
                    GetPictureBoxRef(i).Visible = false;
                    if (testFinished)
                    {
                        GetButtonRef(i).Enabled = false;
                        if (i == chosenAnswer)
                        {
                            GetPictureBoxRef(i).BackColor = Color.Red;
                            GetPictureBoxRef(i).Visible = true;
                            tipLabel.Visible = true;
                            tipLabel.Text = currentTask.Tip;
                        }
                        if (i == currentTask.CorrectAnswer)
                        {
                            GetPictureBoxRef(i).BackColor = Color.Green;
                            GetPictureBoxRef(i).Visible = true;
                            tipLabel.Visible = false;
                        }

                    }
                    
                }
                for (int i = currentTask.Choices.Count; i < 4; i++)
                {
                    GetButtonRef(i).Visible = false;
                    GetPictureBoxRef(i).Visible = false;
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
                if (taskNumber + 1 == TestUnit.Tasks.Count && testFinished)
                {
                    button1.Text = "Закончить";
                }
            }
            else
            {
                testFinished = true;
                button1.Text = "Закончить";
                taskNumber--;
                var currentTask = TestUnit.Tasks[taskNumber];
                int chosenAnswer = Answers.AnswersList[taskNumber].ChosenAnswer;
                for (int i = 0; i < currentTask.Choices.Count; i++)
                {
                    GetButtonRef(i).Enabled = false;
                        if (i == chosenAnswer)
                        {
                            GetPictureBoxRef(i).BackColor = Color.Red;
                            GetPictureBoxRef(i).Visible = true;
                            tipLabel.Visible = true;
                            tipLabel.Text = currentTask.Tip;
                        }
                        if (i == currentTask.CorrectAnswer)
                        {
                            GetPictureBoxRef(i).BackColor = Color.Green;
                            GetPictureBoxRef(i).Visible = true;
                            tipLabel.Visible = false;
                        }

                }

                mark = TestProcessor.GetResult(Answers);
                correctAnswers = TestProcessor.CalculateAnswers(Answers);
                incorrectAnswers = TestUnit.Tasks.Count - correctAnswers;

                errorLabel.Text = $"{errorLabel.Text} {incorrectAnswers.ToString()}";
                errorLabel.Visible = true;

                correctAnswersLabel.Text = $"{correctAnswersLabel.Text} {correctAnswers.ToString()}";
                correctAnswersLabel.Visible = true;

                markHeaderLabel.Visible = true;
                markLabel.Visible = true;
                markLabel.Text = ((int)mark).ToString();
                markLabel.Height = 50;
                markLabel.Width = 30;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (testFinished && TestUnit.Tasks.Count - 1 == taskNumber)
            {
                var tasksNum = TestUnit.Tasks.Count;
                TestProcessor.SaveResultToFile(student, TestUnit, Answers, Stopwatch.ElapsedMilliseconds - startTime);
                Close();
            }
            int answer = setAns();
            if (answer != -1)
            {
                var currentTask = TestUnit.Tasks[taskNumber];
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

        private PictureBox GetPictureBoxRef(int index)
        {
            switch(index)
            {
                case 0: return answerIcon1;
                case 1: return answerIcon2;
                case 2: return answerIcon3;
                case 3: return answerIcon4;
                default: throw new Exception("Could not get reference to picture box");
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

            }
            else
            {
                MessageBox.Show("Вы уже вернулись в начало теста", "Информация");
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
