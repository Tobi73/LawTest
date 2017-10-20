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
            var currentTask = Tasks[TaskNumber];
            label1.Text = $"{TaskNumber + 1}. {currentTask.TaskDescription}";
            radioButton1.Text = currentTask.Choices[0];
            radioButton2.Text = currentTask.Choices[1];
            radioButton3.Text = currentTask.Choices[2];
            radioButton4.Text = currentTask.Choices[3];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TaskNumber += 1;
            setTaskSettings();
        }
    }
}
