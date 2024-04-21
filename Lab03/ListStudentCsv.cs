using System;
using System.IO;

namespace Lab03
{
    public class ListStudentCsv : ListStudent
    {
        private char Delimiter = '$';
        public ListStudentCsv(ListStudent students = null) : base(students)
        {
        }
        public override string WriteToFile(string fileName)
        {
            string result = "";
            string[] str = new string[bd.Count];

            for (var i = 0; i < bd.Count; i++)
            {
                string s =
                    bd[i].Code.ToString() + Delimiter +
                    bd[i].FIO + Delimiter +
                    bd[i].Group + Delimiter +
                    bd[i].Course + Delimiter +
                    bd[i].BrDate + Delimiter +
                    bd[i].Age + Delimiter +
                    bd[i].Ball;
                str[i] = s;
            }

            try
            {
                File.WriteAllLines(fileName, str);
            }
            catch (Exception er)
            {
                result = er.Message;
            }

            return result;
        }

        public override string ReadFromFile(string fileName)
        {
            string result = "";
            try
            {
                bd.Clear();
                string[] str = File.ReadAllLines(fileName);
                for (var i = 0; i < str.Length; i++)
                {
                    var values = str[i].Split(Delimiter);
                    var student = new Student();
                    student.Code = Convert.ToInt32(values[0]);
                    student.FIO = values[1];
                    student.Group = values[2];
                    student.Course = Convert.ToInt32(values[3]);
                    student.BrDate = Convert.ToDateTime(values[4]);
                    student.Ball = Convert.ToDouble(values[6]);
                    bd.Add(student);
                }
            }
            catch (Exception er)
            {
                result = er.Message;
            }
            return result;
        }
    }
}
