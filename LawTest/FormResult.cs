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
    public partial class FormResult : Form
    {
        int font = 9;
        int correctAnswers = 0;
        int tasksNum = 0;
        DataTable dt;
        Model.Mark Mark;

        public FormResult(DataTable _dt, int _font, int _tasksNum, int _correctAnswers, Model.Mark mark)
        {
            InitializeComponent();
            dataGridView1.DataSource = _dt;
            dt = _dt;
            font = _font;
            correctAnswers = _correctAnswers;
            tasksNum = _tasksNum;
            Mark = mark;
        }

        private void FormResult_Load(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font.FontFamily, font);
            label2.Font = new Font(label2.Font.FontFamily, font);
            dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, font);
            ColorDT();
            label2.Text = "Всего вопросов: " + tasksNum + Environment.NewLine + "Правильно: " + correctAnswers;
            int m = (int)Mark;
            labelMark.Text = m.ToString();


            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 20;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ColorDT();
        }

        private void ColorDT()
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // if (dt.Rows[i][1].ToString() == dt.Rows[i][2].ToString())
                if (dt.Rows[i][4].ToString() == "+")
                {
                    dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.LightGreen;
                    dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.LightGreen;
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.LightGreen;
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.LightGreen;
                    dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.LightGreen;
                }
                else
                {
                    dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.LightCoral;
                    dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.LightCoral;
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.LightCoral;
                    dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.LightCoral;
                    dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.LightCoral;
                }
               

            }
        }
    }
}
