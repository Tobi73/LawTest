using LawTest.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LawTest
{
    public partial class StartForm : Form
    {

        public TestUnit TestUnit;
        int font=9;

        public StartForm()
        {
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            TestUnit = GetTestUnit();
        }

        private TestUnit GetTestUnit()
        {
            var file = "./Tests/Test1.csv";
            var lines = File.ReadAllLines(file, Encoding.UTF8);
            var testUnit = new TestUnit("Тест");
            var rand = new Random();
            var tasks = new List<Model.Task>();
            foreach (var line in lines)
            {
                var cells = line.Split(';');
                var task = new Model.Task
                {
                    TaskDescription = cells[0]
                };
                var choises = new Dictionary<int, string>();
                for (var i = 1; i < cells.Length - 2; i++)
                {
                    choises.Add(i - 1, cells[i]);
                }
                var correctAnswer = cells[cells.Length - 2];
                task.CorrectAnswer = int.Parse(correctAnswer);
                task.Tip = cells[cells.Length - 1];
                task.Choices = choises;
                tasks.Add(task);
            }
            var testTasks = GenerateTasks(tasks);
            testUnit.Tasks = testTasks;
            return testUnit;
        }

        private List<Model.Task> GenerateTasks(List<Model.Task> allTasks)
        {
            var rand = new Random();
            var testTasks = new List<Model.Task>();

            for (var i = 0; i < 15;)
            {
                var task = allTasks[rand.Next(allTasks.Count)];
                if (!testTasks.Contains(task))
                {
                    testTasks.Add(task);
                    i++;
                }
            }

            return testTasks;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var studentFIO = textFIO.Text;
            var studentGroup = textGroup.Text;
            if (string.IsNullOrEmpty(studentFIO))
            {
                MessageBox.Show("Введите имя");
            }
            else if (string.IsNullOrEmpty(studentGroup))
            {
                MessageBox.Show("Введите группу");
            }
            else
            {
                Form1 f = new Form1(TestUnit, font, studentFIO, studentGroup);
                f.ShowDialog();
                this.Close();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            font = Convert.ToInt32( numericUpDown1.Value);
            label1.Font = new Font(label1.Font.FontFamily, font);
            label3.Font = new Font(label3.Font.FontFamily, font);
            numericUpDown1.Font = new Font(numericUpDown1.Font.FontFamily, font);
            button1.Font = new Font(button1.Font.FontFamily, font);
            label4.Font = new Font(label4.Font.FontFamily, font);
            label5.Font = new Font(label5.Font.FontFamily, font);
            textFIO.Font = new Font(textFIO.Font.FontFamily, font);
            textGroup.Font = new Font(textGroup.Font.FontFamily, font);
        }
    }
}
