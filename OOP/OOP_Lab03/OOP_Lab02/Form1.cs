using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Lab02
{
    public partial class Form1 : Form
    {
        string jsonPath = "data.json";
        string lastSavedJsonTxtPath = "last_save_path.txt";

        public Form1()
        {
            InitializeComponent();

            var fd = CreateCurrentTeachersUI();
            var p = CreateExpander(fd, "Список Преподавателей", teachersIsExpanded, teachersExpanderContentPanel);

            tabControl1.TabPages[0].Controls.Add(p);
            tabControl1.TabPages[0].Controls.Add(CreateAddTeacherToCourseUI());

            tabControl1.TabPages[1].Controls.Add(CreateAddCoursesUI());

            this.FormClosing += Form1_FormClosing;
            this.AutoScroll = true;
            tabPage1.AutoScroll = true;
            tabPage2.AutoScroll = true;
            tabPage3.AutoScroll = true;

            try
            {
                if (File.Exists(lastSavedJsonTxtPath))
                {
                    var path = File.ReadAllText(lastSavedJsonTxtPath);
                    jsonPath = File.Exists(path) ? path : jsonPath;
                }
            }
            catch
            {

            }

            jsonPathtextBox.Text = jsonPath;

            LoadCoursesFromJson(jsonPath);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveCoursesToJson(jsonPath);
        }

        private void LoadCoursesFromJson(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    var jsonString = File.ReadAllText(path);
                    var json = JsonSerializer.Deserialize<ObservableCollection<ProgrammingCourse>>(jsonString);

                    foreach (var item in json)
                    {
                        var sameNameCourse = newCourses.FirstOrDefault(c => c.Name.Equals(item.Name));
                        if (sameNameCourse != null)
                        {
                            newCourses.Remove(sameNameCourse);
                        }
                        newCourses.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorDialog($"Ошибка при загрузке данных из '{path}': {ex.Message}");
                }
            }
        }

        private void SaveCoursesToJson(string path)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(newCourses);

                using (var sw = new StreamWriter(path))
                {
                    sw.Write(jsonString);
                }
            }
            catch (Exception ex)
            {
                ShowErrorDialog($"Ошибка при сохранение данных в '{path}': {ex.Message}");
            }
        }

        private TableLayoutPanel CreateControlWithLabel(string header, Control control)
        {
            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
            };

            Label label = new Label
            {
                Text = header,
                AutoSize = true,
            };

            control.Dock = DockStyle.Fill;
            panel.Controls.Add(label, 0, 0);
            panel.Controls.Add(control, 0, 1);

            return panel;
        }

        private void ShowErrorDialog(string message)
        {
            using (var dlg = new ErrorDialog(message))
            {
                dlg.ShowDialog(this);
            }
        }

        private void ShowErrorDialog(Exception ex)
        {
            using (var dlg = new ErrorDialog(ex))
            {
                dlg.ShowDialog(this);
            }
        }

        private void saveToJsonButon_Click(object sender, EventArgs e)
        {
            SaveCoursesToJson(this.jsonPath);
        }

        private void loadFromJsonButton_Click(object sender, EventArgs e)
        {
            LoadCoursesFromJson(this.jsonPath);
        }

        private void jsonPathtextBox_TextChanged(object sender, EventArgs e)
        {
            jsonPath = jsonPathtextBox.Text;

            try
            {
                File.WriteAllText(lastSavedJsonTxtPath, jsonPath);
            }
            catch (Exception ex)
            {
                ShowErrorDialog(ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FlowLayoutPanel flpCards = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true
            };

            foreach (var course in newCourses)
            {
                double bonusFactor = (course.AudienceAge < 18 || course.AudienceAge > 60) ? 1.3 : 1.0;

                double complexityFactor = 1.0;
                switch (course.Complexity)
                {
                    case CourseComplexity.Begginer:
                        complexityFactor = 1.0;
                        break;
                    case CourseComplexity.Medium:
                        complexityFactor = 1.2;
                        break;
                    case CourseComplexity.Advanced:
                        complexityFactor = 1.5;
                        break;
                }

                double controlFactor = 1.0;
                switch (course.ControlType)
                {
                    case FinalsType.None:
                        controlFactor = 1.0;
                        break;
                    case FinalsType.Midterm:
                        controlFactor = 1.1;
                        break;
                    case FinalsType.Exam:
                        controlFactor = 1.2;
                        break;
                }

                int teacherCount = (course.Teachers != null) ? course.Teachers.Count : 0;
                double teacherCost = teacherCount * 500;

                double baseCost = (course.LecturesCount * 100 + course.LabsCount * 150);

                double finalBudget = (baseCost * bonusFactor * complexityFactor * controlFactor) + teacherCost;

                string formulaText = "\nФормула расчёта:\n" +
                    $"((Лекций: {course.LecturesCount} * 100) + (Лабораторных: {course.LabsCount} * 150)) = {baseCost}\n" +
                    $"* (бонус за возраст: {(course.AudienceAge < 18 || course.AudienceAge > 60 ? "1.3" : "1.0")}) = {bonusFactor}\n" +
                    $"* (сложность: {complexityFactor})\n" +
                    $"* (контроль: {controlFactor})\n" +
                    $"+ (Преподаватели: {teacherCount} * 500 = {teacherCost})";

                FlowLayoutPanel cardPanel = new FlowLayoutPanel()
                {
                    FlowDirection = FlowDirection.TopDown,
                    AutoSize = true,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(10),
                    Padding = new Padding(10)
                };

                Label lblPrimaryInfo = new Label()
                {
                    AutoSize = true,
                    Text = $"Курс: {course.Name}\nАудитория: {course.AudienceAge} годиков\nБюджет: {finalBudget:C}"
                };

                Label lblFormula = new Label()
                {
                    AutoSize = true,
                    Text = formulaText,
                };

                cardPanel.Controls.Add(lblPrimaryInfo);
                cardPanel.Controls.Add(lblFormula);

                flpCards.Controls.Add(cardPanel);
            }

            panel1.Controls.Clear();
            panel1.Controls.Add(flpCards);
        }

        private void сортировкаПоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newnewCourses = newCourses.OrderBy(x => x.Name).ToList();
            //var newnewCoursess = newCourses.OrderByDescending(x => x.Name).ToList();
            newCourses.Clear();

            foreach (var item in newnewCourses)
            {
                newCourses.Add(item);
            }
        }
    }
}
