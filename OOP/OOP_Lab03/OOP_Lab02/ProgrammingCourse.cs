using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OOP_Lab02
{
    public class ProgrammingCourse
    {
        [Required(ErrorMessage = "Название курса обязательно")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Название должно содержать от 3 до 100 символов")]
        [RegularExpression(@"^[A-Za-zА-Яа-я0-9\s\-.]+$", ErrorMessage = "Неверный формат названия курса.")]
        public string Name { get; set; }

        [Range(10, 100, ErrorMessage = "Возрастная аудитория должна быть в диапазоне от 10 до 100 лет")]
        public int AudienceAge { get; set; }

        [Required(ErrorMessage = "Необходимо указать сложность курса")]
        public CourseComplexity Complexity { get; set; }

        [Range(1, 200, ErrorMessage = "Количество лекций должно быть в диапазоне от 1 до 200")]
        public int LecturesCount { get; set; }

        [Range(0, 200, ErrorMessage = "Количество лабораторных должно быть в диапазоне от 0 до 200")]
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
        [RegularExpression(@"^[A-Za-zА-Яа-я]+([ -][A-Za-zА-Яа-я]+)+$", ErrorMessage = "Неверный формат ФИО преподавателя.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Необходимо указать кафедру")]
        [StringLength(50, ErrorMessage = "Название кафедры не должно превышать 50 символов")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Необходимо указать аудиторию")]
        [AuditoriumFormat(ErrorMessage = "Формат аудитории должен быть вида 101-A или 302-a")]
        public string Auditorium { get; set; }
    }

    public class AuditoriumFormatAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string auditorium = value as string;
            if (string.IsNullOrEmpty(auditorium))
            {
                return ValidationResult.Success;
            }
            if (Regex.IsMatch(auditorium, @"^\d{3}-[A-Za-zА-Яа-я]$"))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ?? "Неверный формат аудитории.");
        }
    }
}
