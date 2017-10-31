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

        public List<TestUnit> TestUnits;
        int font=9;
        public StartForm()
        {
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            TestUnits = GetTestUnits();
            comboBox1.Items.AddRange(TestUnits.ToArray());
        }

        private List<TestUnit> GetTestUnits()
        {
            var testUnits = new List<TestUnit>();

            foreach (var file in Directory.GetFiles("./Tests"))
            {
                var lines = File.ReadAllLines(file);
                var filename = (file.Split('\\').Last());
                // Переписать эту хуевую строчку
                var testUnit = new TestUnit(filename.Remove(filename.LastIndexOf('.')));
                foreach (var line in lines)
                {
                    var cells = line.Split(',');
                    testUnit.AddTask(new Model.Task
                    {
                        TaskDescription = cells[0],
                        // Сделать через for, если вариантов ответа не всегда будет 4
                        Choices = new Dictionary<int, string>
                        {
                            { 0, cells[1] },
                            { 1, cells[2] },
                            { 2, cells[3] },
                            { 3, cells[4] }
                        },
                        CorrectAnswer = int.Parse(cells[5])
                    });
                }
                testUnits.Add(testUnit);
            }
            return testUnits;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex !=-1) {
                Form1 f = new LawTest.Form1(comboBox1.SelectedItem as TestUnit, font);
                f.ShowDialog();
            } else
            {
                MessageBox.Show("Выберите тест", "Информация");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            font = Convert.ToInt32( numericUpDown1.Value);
            label1.Font = new Font(label1.Font.FontFamily, font);
            label2.Font = new Font(label2.Font.FontFamily, font);
            label3.Font = new Font(label3.Font.FontFamily, font);
            numericUpDown1.Font = new Font(numericUpDown1.Font.FontFamily, font);
            button1.Font = new Font(button1.Font.FontFamily, font);
            comboBox1.Font = new Font(comboBox1.Font.FontFamily, font);
        }
    }
}
