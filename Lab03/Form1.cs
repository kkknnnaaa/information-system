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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Serilog;

namespace Lab03
{
    public partial class Form1 : Form
    {
        Serilog.Core.Logger log = new LoggerConfiguration().WriteTo.File("log.txt").CreateLogger();

        int indCol = 1;
        string nameField = "FIO";
        SortOrder typeSort = SortOrder.Ascending;

        public ListStudent studentList = new ListStudentJson();

        public Student currentSt = null;

        public BindingSource bindingSource = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            log.Information("Приложение запущено.");
        }

        public void RefreshGrid()
        {
            dataGridView1.DataSource = null;

            switch (nameField)
            {
                case "Code":
                    if (typeSort == SortOrder.Ascending)
                        studentList.bd = studentList.bd.OrderBy(x => x.Code).ToList();
                    else
                        studentList.bd = studentList.bd.OrderByDescending(x => x.Code).ToList();
                    break;
                case "FIO":
                    if (typeSort == SortOrder.Ascending)
                        studentList.bd = studentList.bd.OrderBy(x => x.FIO).ToList();
                    else
                        studentList.bd = studentList.bd.OrderByDescending(x => x.FIO).ToList();
                    break;
                case "Group":
                    if (typeSort == SortOrder.Ascending)
                        studentList.bd = studentList.bd.OrderBy(x => x.Group).ToList();
                    else
                        studentList.bd = studentList.bd.OrderByDescending(x => x.Group).ToList();
                    break;
                case "Course":
                    if (typeSort == SortOrder.Ascending)
                        studentList.bd = studentList.bd.OrderBy(x => x.Course).ToList();
                    else
                        studentList.bd = studentList.bd.OrderByDescending(x => x.Course).ToList();
                    break;
                case "BrDate":
                    if (typeSort == SortOrder.Ascending)
                        studentList.bd = studentList.bd.OrderBy(x => x.BrDate).ToList();
                    else
                        studentList.bd = studentList.bd.OrderByDescending(x => x.BrDate).ToList();
                    break;
                case "Age":
                    if (typeSort == SortOrder.Ascending)
                        studentList.bd = studentList.bd.OrderBy(x => x.Age).ToList();
                    else
                        studentList.bd = studentList.bd.OrderByDescending(x => x.Age).ToList();
                    break;
                case "Ball":
                    if (typeSort == SortOrder.Ascending)
                        studentList.bd = studentList.bd.OrderBy(x => x.Ball).ToList();
                    else
                        studentList.bd = studentList.bd.OrderByDescending(x => x.Ball).ToList();
                    break;
            }

            bindingSource.DataSource = studentList.bd;
            dataGridView1.DataSource = bindingSource;
            dataGridView1.AutoGenerateColumns = false;
            bindingSource.Position = bindingSource.IndexOf(currentSt);
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns[indCol].HeaderCell.SortGlyphDirection =
                    typeSort;
            }
        }



        private void загрузитьИзФайлаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (studentList.bd.Count() > 0)
            {
                DialogResult result = MessageBox.Show(
                "Текущий список студентов будет потярян. Продложить?",
                "Загрузка списка",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "json files (*.json)|*.json|csv files (*.csv)|*.csv|xml files (*.xml)|*.xml",
                RestoreDirectory = true,
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ListStudentFactory studentFactory = new ListStudentFactory();
                toolStripTextBox1.Text = openFileDialog1.FileName;
                string fileName = toolStripTextBox1.Text;
                string type = Path.GetExtension(fileName).TrimStart('.');
                studentList = studentFactory.Create(type);
                if (studentList != null)
                {
                    string errorText = studentList.ReadFromFile(fileName);

                    if (!String.IsNullOrEmpty(errorText))
                    {
                        MessageBox.Show(errorText, "Ошибка чтения файла",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        RefreshGrid();
                        log.Information($"Выполнена загрузка из файла {fileName}");
                    }
                }

            }
        }
        private void сохранитьСписокВФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (studentList.bd.Count() != 0)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog
                {
                    Filter = "json files (*.json)|*.json|csv files (*.csv)|*.csv|xml files (*.xml)|*.xml",
                    RestoreDirectory = true,
                };

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ListStudentFactory studentFactory = new ListStudentFactory();
                    string fileName = saveFileDialog1.FileName;
                    string type = Path.GetExtension(fileName).TrimStart('.');
                    ListStudent students = studentFactory.Create(type, studentList);
                    string errorText = students.WriteToFile(fileName);
                    if (!String.IsNullOrEmpty(errorText))
                    {
                        MessageBox.Show(errorText, "Ошибка записи файла",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                        return;
                    }
                    MessageBox.Show("Сохранение прошла успешно", "Информация",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    log.Information($"Выполнено сохранение в файл {fileName}");
                } 
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            currentSt = new Student();

            Form2 addEdit = new Form2(currentSt, this);
            addEdit.ShowDialog();
            if (addEdit.DialogResult == DialogResult.OK)
            {
                log.Information($"Дабавление студента в список: {currentSt.ToString()}");
                studentList.bd.Add(currentSt);
                RefreshGrid();
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            indCol = e.ColumnIndex;
            nameField = dataGridView1.Columns[indCol].Name;
            typeSort = dataGridView1.Columns[indCol].HeaderCell.SortGlyphDirection;
            if (typeSort == SortOrder.None)
                typeSort = SortOrder.Ascending;
            if (typeSort == SortOrder.Ascending)
                typeSort = SortOrder.Descending;
            else
                typeSort = SortOrder.Ascending;
            dataGridView1.Columns[indCol].HeaderCell.SortGlyphDirection =
                typeSort;
            RefreshGrid();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            currentSt = (Student)bindingSource.Current;

            Form2 addEdit = new Form2(currentSt, this, true);
            addEdit.ShowDialog();
            if (addEdit.DialogResult == DialogResult.OK)
            {
                RefreshGrid();
                log.Information($"Изменения студента в списоке: {currentSt.ToString()}");
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            currentSt = (Student)bindingSource.Current;
            DialogResult result = MessageBox.Show(
                $"Удалить элемент с кодом: {currentSt.Code} ?",
                "Удаление элемента.",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                studentList.bd.Remove(currentSt);
                RefreshGrid();
                log.Information($"Удаление студента из списка: {currentSt.ToString()}");
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            log.Information("Выполнен выход из приложения");
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
