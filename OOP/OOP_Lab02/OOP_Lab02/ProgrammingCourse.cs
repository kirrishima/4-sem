using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab02
{
    public class ProgrammingCourse
    {
        public string Name { get; set; }

        public int AudienceAge { get; set; }

        public CourseComplexity Complexity { get; set; }

        public int LexturesCount { get; set; }

        public int LabsCount { get; set; }

        public FinalsType ControlType { get; set; }

        public Teacher Teacher { get; set; }
    }

    public enum CourseComplexity
    {
        Begginer,
        Medium,
        Advanced
    }

    public enum FinalsType
    {
        None,
        Midterm,
        Exam
    }

    public class Teacher
    {
        public string FullName { get; set; }

        public string Department { get; set; }

        public string Auditorium { get; set; }
    }
}
