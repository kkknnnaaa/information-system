using System;

namespace Lab03
{
    public class ListStudentFactory
    {
        public ListStudent Create(string type, ListStudent students = null)
        {
            switch (type.ToLower())
            {
                case "json":
                    students = new ListStudentJson(students);
                    break;
                case "csv":
                    students = new ListStudentCsv(students);
                    break;
                case "xml":
                    students = new ListStudentXml(students);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(type);
            }
            return students;
        }
    }
}
