using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Lab03
{
    [Serializable()]
    public class Student
    {
        [DisplayName("Код")]
        public int Code { get; set; }

        [DisplayName("ФИО")]
        public string FIO { get; set; }

        [DisplayName("Группа")]
        public string Group { get; set; }

        [DisplayName("Курс")]
        public int Course { get; set; }

        [DisplayName("Дата рождения")]
        public DateTime BrDate { get; set; }

        [DisplayName("Возраст")]
        public int Age
        {
            get => DateTime.Now.Year - BrDate.Year;
        }

        [DisplayName("Балл")]
        public double Ball { get; set; }

        public override string ToString()
        {
            return $"Code={Code} FIO={FIO} Group={Group} Course={Course} BrDate={BrDate} Ball={Ball}";
        }

    }
    
}
