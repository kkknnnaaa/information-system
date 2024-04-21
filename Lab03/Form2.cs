using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03
{
    public partial class Form2 : Form
    {
        Student _st;

        public Form2(Student st, Form1 mf, bool is_edit = false)
        {
            InitializeComponent();
            _st = st;

            if (is_edit)
            {
                textBoxCode.Text = st.Code.ToString();
                textBoxFIO.Text = st.FIO;
                textBoxGroup.Text = st.Group;
                numericUpDownCourse.Value = st.Course;
                dateTimePicker1.Value = st.BrDate;
                numericUpDownBall.Value = (decimal)st.Ball;
            }
            else
            {
                if (mf.studentList.bd.Count() > 0)
                {
                    textBoxCode.Text =
                            (mf.studentList.bd.Max(x => x.Code) + 1).ToString();
                }
                else
                {
                    textBoxCode.Text = "0";
                }
            }
        }

       

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBoxAge.Text =
                (DateTime.Now.Year - dateTimePicker1.Value.Year).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                $"Сохранить элемент с кодом: {textBoxCode.Text} ?",
                "Сохранение элемента элемента.",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _st.Code = Convert.ToInt32(textBoxCode.Text);
                _st.FIO = textBoxFIO.Text;
                _st.Group = textBoxGroup.Text;
                _st.Course = (int)numericUpDownCourse.Value;
                _st.BrDate = dateTimePicker1.Value;
                _st.Ball = (double)numericUpDownBall.Value;
                DialogResult = DialogResult.OK;

                Close();
            }
        }
    }
}
