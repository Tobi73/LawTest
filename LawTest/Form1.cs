using LawTest.Model;
using LawTest.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LawTest
{
    public partial class Form1 : Form
    {
        TestUnit testUnit;
        Answers answers;
        TestProcesser ts;
        int taskNumber;
        int font = 9;

        public Form1(TestUnit testUnit, int _font)
        {
            this.testUnit = testUnit;
            InitializeComponent();
            font = _font;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ts = new TestProcesser();
            answers = new Answers(testUnit);
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
            if (taskNumber < testUnit.Tasks.Count) {
                var currentTask = testUnit.Tasks[taskNumber];
                var chosenAnswer = answers.AnswersList[taskNumber].ChosenAnswer;
                label1.Text = taskNumber + " " + currentTask.TaskDescription;

                // Установка текста вариантов ответа
                for(int i = 0; i < currentTask.Choices.Count; i++)
                {
                    GetButtonRef(i).Text = currentTask.Choices[i];
                }
                for(int i = currentTask.Choices.Count; i < 4; i++)
                {
                    GetButtonRef(i).Visible = false;
                }

                // Установка состояний ответов
                if (chosenAnswer == -1)
                {
                    for (int i = 0; i < currentTask.Choices.Count; i++)
                        GetButtonRef(i).Checked = false;
                } else
                {
                    for (int i = 0; i < currentTask.Choices.Count; i++)
                        if (i == chosenAnswer)
                        {
                            GetButtonRef(i).Checked = true;
                        }
                        else GetButtonRef(i).Checked = false;
                }
                
                // Проверка на окончание теста
                if(taskNumber +1 == testUnit.Tasks.Count)
                {
                    button1.Text = "Финиш";
                }
            } else
            {
                var mark = ts.GetResult(answers);
                var correctAnswers = ts.CalculateAnswers(answers);
                var incorrectAnswers = testUnit.Tasks.Count - correctAnswers;
                var tasksNum = testUnit.Tasks.Count;
                MessageBox.Show($"Тестирование завершено \r\nВсего вопросов: {tasksNum} \r\nПравильно: {correctAnswers} ", "Информация");
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int answer = setAns();
            if (answer != -1) {
                answers.EditAnswer(taskNumber, answer);
                taskNumber += 1;
                setTaskSettings();
                label2.Text = "";
                label3.Text = "";
            } else
            {
                MessageBox.Show("Выберите вариант ответа","Информация");
            }
        }

        private RadioButton GetButtonRef(int index)
        {
            switch(index)
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
            for (int i = 0; i < testUnit.Tasks[taskNumber].Choices.Count; i++)
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
                    answers.EditAnswer(taskNumber, answer);
                taskNumber--;
                setTaskSettings();
            } else
            {
                MessageBox.Show("Вы уже вернулись в начало теста", "Информация");
            }
        }
    }
}
