using LawTest.Model;
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
        public TestUnit TestUnit { get; set; }
        public int TaskNumber { get; set; }
        public List<Model.Task> Tasks;

        public Form1(TestUnit testUnit)
        {
            TestUnit = testUnit;
            TaskNumber = 0;
            Tasks = TestUnit.Tasks;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setTaskSettings();
        }

        private void setTaskSettings()
        {
            if (TaskNumber < Tasks.Count) {
                var currentTask = Tasks[TaskNumber];
                label1.Text = $"{TaskNumber + 1}. {currentTask.TaskDescription}";
                radioButton1.Text = currentTask.Choices[0];
                radioButton2.Text = currentTask.Choices[1];
                radioButton3.Text = currentTask.Choices[2];
                radioButton4.Text = currentTask.Choices[3];

                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                if(TaskNumber +1 == Tasks.Count)
                {
                    button1.Text = "Finish";
                }
            } else
            {
                MessageBox.Show("Тестирование завершено \r\nВсего вопросов: " + Tasks.Count + " \r\nПравильно: 0 ", "Информация");
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (setAns()) {
                TaskNumber += 1;
                setTaskSettings();
            } else
            {
                MessageBox.Show("Выберите вариант ответа","Информация");
            }
        }

        private bool setAns()
        {
            if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked || radioButton4.Checked)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
