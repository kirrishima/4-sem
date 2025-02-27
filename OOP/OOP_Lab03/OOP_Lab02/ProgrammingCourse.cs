using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OOP_Lab02
{
    public class ProgrammingCourse
    {
        [Required(ErrorMessage = "Название курса обязательно")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Название должно содержать от 3 до 100 символов")]
        public string Name { get; set; }

        [Range(10, 100, ErrorMessage = "Возрастная аудитория должна быть в диапазоне от 10 до 100 лет")]
        public int AudienceAge { get; set; }

        [Required(ErrorMessage = "Необходимо указать сложность курса")]
        public CourseComplexity Complexity { get; set; }

        [Range(1, 100, ErrorMessage = "Количество лекций должно быть в диапазоне от 1 до 100")]
        public int LecturesCount { get; set; }

        [Range(0, 100, ErrorMessage = "Количество лабораторных должно быть в диапазоне от 0 до 100")]
        public int LabsCount { get; set; }

        [Required(ErrorMessage = "Необходимо указать тип итоговой аттестации")]
        public FinalsType ControlType { get; set; }

        public ObservableCollection<Teacher> Teachers { get; set; }
    }

    public enum CourseComplexity
    {
        Beginner,
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
        [Required(ErrorMessage = "ФИО преподавателя обязательно")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "ФИО должно содержать от 5 до 100 символов")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Необходимо указать кафедру")]
        [StringLength(50, ErrorMessage = "Название кафедры не должно превышать 50 символов")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Необходимо указать аудиторию")]
        [RegularExpression(@"^[A-ZА-Я]-\d{3}$", ErrorMessage = "Формат аудитории должен быть вида A-101 или А-302")]
        public string Auditorium { get; set; }
    }
}
