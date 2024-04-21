using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Lab03
{
    public class ListStudentXml : ListStudent
    {
        public ListStudentXml(ListStudent students = null) : base(students)
        {
        }
        public override string WriteToFile(string fileName)
        {
            string result = "";

            try
            {
                //create the serialiser to create the xml
                XmlSerializer serialiser = new XmlSerializer(typeof(List<Student>));

                // Create the TextWriter for the serialiser to use
                TextWriter filestream = new StreamWriter(fileName);

                //write to the file
                serialiser.Serialize(filestream, bd);

                // Close the file
                filestream.Close();
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
                //create the serialiser to create the xml
                XmlSerializer serialiser = new XmlSerializer(typeof(List<Student>));

                // Create the StreamReader for the serialiser to use
                StreamReader filestream = new StreamReader(fileName);

                //write to the file
                bd = (List<Student>)serialiser.Deserialize(filestream);

                // Close the file
                filestream.Close();
            }
            catch (Exception er)
            {
                result = er.Message;
            }
            return result;
        }
    }
}
