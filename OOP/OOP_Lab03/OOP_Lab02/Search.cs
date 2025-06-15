using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OOP_Lab02
{
    public partial class Search : Form
    {
        private ObservableCollection<ProgrammingCourse> newCourses = new ObservableCollection<ProgrammingCourse>();

        private TextBox txtName, txtTeacher;
        private NumericUpDown numAudienceAge, numLecturesCount, numLabsCount;
        private Button btnSearch;
        private Panel searchResultsPanel;

        public Search(ObservableCollection<ProgrammingCourse> newCourses)
        {
            this.newCourses = newCourses;
            InitializeComponent();
            CreateUI();
            this.AutoScroll = true;
            toolStrip1.MouseDown += toolStrip1_MouseDown;
            toolStrip1.MouseMove += toolStrip1_MouseMove;
            toolStrip1.MouseUp += toolStrip1_MouseUp;
        }

        private List<ProgrammingCourse> programmingCourses;

        private void CreateUI()
        {
            TableLayoutPanel mainLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            Panel criteriaPanel = new Panel()
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Padding = new Padding(10)
            };

            TableLayoutPanel опдаво = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6,
                AutoSize = true
            };
            опдаво.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            опдаво.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            опдаво.Controls.Add(new Label() { Text = "Название курса:" }, 0, 0);
            txtName = new TextBox() { Dock = DockStyle.Fill };
            опдаво.Controls.Add(txtName, 1, 0);

            опдаво.Controls.Add(new Label() { Text = "Преподаватель:" }, 0, 1);
            txtTeacher = new TextBox() { Dock = DockStyle.Fill };
            опдаво.Controls.Add(txtTeacher, 1, 1);

            опдаво.Controls.Add(new Label() { Text = "Возраст аудитории:" }, 0, 2);
            numAudienceAge = new NumericUpDown() { Dock = DockStyle.Fill, Minimum = 0, Maximum = 100, Value = 0 };
            опдаво.Controls.Add(numAudienceAge, 1, 2);

            опдаво.Controls.Add(new Label() { Text = "Количество лекций:" }, 0, 3);
            numLecturesCount = new NumericUpDown() { Dock = DockStyle.Fill, Minimum = 0, Maximum = 200, Value = 0 };
            опдаво.Controls.Add(numLecturesCount, 1, 3);

            опдаво.Controls.Add(new Label() { Text = "Количество лабораторных:" }, 0, 4);
            numLabsCount = new NumericUpDown() { Dock = DockStyle.Fill, Minimum = 0, Maximum = 200, Value = 0 };
            опдаво.Controls.Add(numLabsCount, 1, 4);

            btnSearch = new Button() { Text = "Найти", Dock = DockStyle.Fill };
            btnSearch.Click += BtnSearch_Click;
            опдаво.Controls.Add(btnSearch, 0, 5);
            опдаво.SetColumnSpan(btnSearch, 2);

            criteriaPanel.Controls.Add(опдаво);

            searchResultsPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BorderStyle = BorderStyle.FixedSingle
            };

            mainLayout.Controls.Add(criteriaPanel, 0, 0);
            mainLayout.Controls.Add(searchResultsPanel, 0, 1);

            toolStripContainer1.ContentPanel.Controls.Add(mainLayout);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Form1.Current.LastAction = "Найти результаты";
            SearchCourses();
        }

        private void SearchCourses()
        {
            searchResultsPanel.Controls.Clear();

            var filteredCourses = newCourses.Where(course =>
            {
                bool matches = true;

                if (!string.IsNullOrWhiteSpace(txtName.Text))
                {
                    string pattern = ".*" + Regex.Escape(txtName.Text) + ".*";
                    matches &= Regex.IsMatch(course.Name, pattern, RegexOptions.IgnoreCase);
                }

                if (!string.IsNullOrWhiteSpace(txtTeacher.Text))
                    matches &= course.Teachers != null && course.Teachers.Any(t =>
                        t.FullName.IndexOf(txtTeacher.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                if (numAudienceAge.Value > 0)
                    matches &= course.AudienceAge == (int)numAudienceAge.Value;

                if (numLecturesCount.Value > 0)
                    matches &= course.LecturesCount == (int)numLecturesCount.Value;

                if (numLabsCount.Value > 0)
                    matches &= course.LabsCount == (int)numLabsCount.Value;

                return matches;
            }).ToList();

            foreach (var course in filteredCourses)
            {
                var courseCard = CreateCourseCard(course);
                searchResultsPanel.Controls.Add(courseCard);
            }

            if (!filteredCourses.Any())
                searchResultsPanel.Controls.Add(new Label() { Text = "Ничего не найдено", AutoSize = true });
            else
            {
                programmingCourses = filteredCourses;
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void вXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.Current.LastAction = "Сохранить в xml";
            MessageBox.Show("Какой xml, только в Json!");
        }

        private void вJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.Current.LastAction = "Сохранить в json";
            string path = "бабабуй.json";
            try
            {
                string jsonString = JsonSerializer.Serialize(programmingCourses);

                using (var sw = new StreamWriter(path))
                {
                    sw.Write(jsonString);
                }
                MessageBox.Show($"Сохранено в {path}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранение данных в '{path}': {ex.Message}");
            }
        }

        private void алфавитныйПорядокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.Current.LastAction = "Сортировка по имени";
            Form1.алфавитныйПорядокToolStripMenuItem_Click(sender, e);
        }

        private void обратныйАлфавитныйПорядокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.Current.LastAction = "Сортировка по числу преподавателей";
            Form1.обратныйАлфавитныйПорядокToolStripMenuItem_Click(sender, e);
        }

        private void возрастаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.Current.LastAction = "Сортировка по имени";
            Form1.алфавитныйПорядокToolStripMenuItem_Click(sender, e);
        }

        private void убываниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.Current.LastAction = "Сортировка по числу преподавателей";
            Form1.обратныйАлфавитныйПорядокToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Form1.Current.LastAction = "Отчистить";
            searchResultsPanel.Controls.Clear();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Form1.Current.LastAction = "Удалить";
            if (programmingCourses.Count <= 0)
            {
                return;
            }

            foreach (var item in programmingCourses)
            {
                if (newCourses.Contains(item))
                {
                    newCourses.Remove(item);
                }
            }

            toolStripButton3_Click(sender, e);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Form1.Current.LastAction = "Назад";
            this.Close();
            this.Dispose();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1.Current.LastAction = "О программе";
            Form1.оПрограммеToolStripMenuItem_Click(sender, e);
        }

        private void панельИнструментовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip1.Visible = !toolStrip1.Visible;
            (sender as ToolStripMenuItem).Text = toolStrip1.Visible ? "Скрыть" : "Показать";
        }

        private bool isPinned = true;
        private bool dragging = false;
        private Point dragStartPoint = Point.Empty;
        private Point originalLocation = Point.Empty;

        private void закрепитьПанельИнструментовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isPinned)
            {
                Form1.Current.LastAction = "Открепить";
                toolStrip1.GripStyle = ToolStripGripStyle.Visible;
                toolStrip1.AllowDrop = true;
                (sender as ToolStripMenuItem).Text = "Закрепить";
                isPinned = false;
            }
            else
            {
                Form1.Current.LastAction = "Закрепить";
                toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
                toolStrip1.AllowDrop = false;
                (sender as ToolStripMenuItem).Text = "Открепить";
                isPinned = true;
            }
        }

        private void toolStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isPinned && e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragStartPoint = e.Location;
                originalLocation = toolStrip1.Location;
            }
        }

        private void toolStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isPinned && dragging)
            {
                Point diff = new Point(e.X - dragStartPoint.X, e.Y - dragStartPoint.Y);
                toolStrip1.Location = new Point(originalLocation.X + diff.X, originalLocation.Y + diff.Y);
            }
        }

        private void toolStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private GroupBox CreateCourseCard(ProgrammingCourse course)
        {
            TableLayoutPanel courseInfoPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0)
            };

            courseInfoPanel.Controls.Add(CreateControlWithLabel("Название курса:", new Label() { Text = course.Name }), 0, 0);
            courseInfoPanel.Controls.Add(CreateControlWithLabel("Возраст аудитории:", new Label() { Text = course.AudienceAge.ToString() }), 0, 1);
            courseInfoPanel.Controls.Add(CreateControlWithLabel("Сложность:", new Label() { Text = course.Complexity.ToString() }), 0, 2);
            courseInfoPanel.Controls.Add(CreateControlWithLabel("Количество лекций:", new Label() { Text = course.LecturesCount.ToString() }), 0, 3);
            courseInfoPanel.Controls.Add(CreateControlWithLabel("Количество лабораторных:", new Label() { Text = course.LabsCount.ToString() }), 0, 4);
            courseInfoPanel.Controls.Add(CreateControlWithLabel("Тип итогового контроля:", new Label() { Text = course.ControlType.ToString() }), 0, 5);

            FlowLayoutPanel teachersPanel = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };

            if (course.Teachers != null)
            {
                foreach (var teacher in course.Teachers)
                {
                    var teacherCard = CreateTeacherCard(teacher);
                    teachersPanel.Controls.Add(teacherCard);
                }
            }

            TableLayoutPanel container = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 1,
                RowCount = 2
            };
            container.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            container.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            container.Controls.Add(courseInfoPanel, 0, 0);
            container.Controls.Add(teachersPanel, 0, 1);

            var groupBox = new GroupBox()
            {
                Name = Guid.NewGuid().ToString(),
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(5),
                Text = "Информация о курсе"
            };

            groupBox.Controls.Add(container);

            return groupBox;
        }

        private GroupBox CreateTeacherCard(Teacher teacher)
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
            };

            tableLayoutPanel.Controls.Add(CreateControlWithLabel("ФИО:", new Label() { Text = teacher.FullName }), 0, 0);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Кафедра:", new Label() { Text = teacher.Department }), 0, 1);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Аудитория:", new Label() { Text = teacher.Auditorium }), 0, 2);

            var gb = new GroupBox()
            {
                Name = Guid.NewGuid().ToString(),
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(20, 0, 0, 0),
                Text = "Информация о преподавателе"
            };

            gb.Controls.Add(tableLayoutPanel);

            return gb;
        }

        private Control CreateControlWithLabel(string labelText, Control control)
        {
            FlowLayoutPanel panel = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true
            };
            panel.Controls.Add(new Label() { Text = labelText, AutoSize = true });
            panel.Controls.Add(control);
            return panel;
        }
    }
}
