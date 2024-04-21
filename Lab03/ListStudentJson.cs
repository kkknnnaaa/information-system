using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.IO;
using System.Text.Encodings.Web;

namespace Lab03
{
    public class ListStudentJson : ListStudent
    {
        public ListStudentJson(ListStudent students = null) : base(students)
        {
        }
        public override string WriteToFile(string fileName)
        {
            string result = "";

            try
            {
                var options = new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.LetterlikeSymbols, UnicodeRanges.Cyrillic),
                    WriteIndented = true
                };
                string json = JsonSerializer.Serialize(bd, options);
                File.WriteAllText(fileName, json, Encoding.UTF8);
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
                string jsonString = File.ReadAllText(fileName, Encoding.UTF8);
                bd = JsonSerializer.Deserialize<List<Student>>(jsonString);

            }
            catch (Exception er)
            {
                result = er.Message;
            }
            return result;
        }
    }
}
