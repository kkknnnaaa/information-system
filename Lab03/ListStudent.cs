using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03
{

    public abstract class ListStudent
    {
        public List<Student> bd;

        public ListStudent(ListStudent students = null)
        {
            if (students != null)
            {
                bd = students.bd;
            }
            else
            {
                bd = new List<Student>();
            }
        }

        public abstract string WriteToFile(string fileName);
        public abstract string ReadFromFile(string fileName);
    }
}
