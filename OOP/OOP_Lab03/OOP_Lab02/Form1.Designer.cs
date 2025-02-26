using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OOP_Lab02
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region курсы

        TextBox newCourseNameTextBox = new TextBox();
        TrackBar newCourseAudienceAgeSlider = new TrackBar();
        ComboBox newCourseComplexityComboBox = new ComboBox() { Margin = new Padding(0, 0, 0, 10) };
        NumericUpDown newCourseLecturesCountUpDown = new NumericUpDown();
        NumericUpDown newCourseLabsCountUpDown = new NumericUpDown();
        ComboBox newCourseControlTypeComboBox = new ComboBox() { Margin = new Padding(0, 0, 0, 10) };
        Button addNewCourseButton = new Button() { Text = "Добавить курс", AutoSize = true };

        ObservableCollection<ProgrammingCourse> newCourses = new ObservableCollection<ProgrammingCourse>();

        private Control CreateCurrentCoursesUI()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0)
            };

            return tableLayoutPanel;
        }

        private Control CreateAddCoursesUI()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0)
            };

            var titleLabel = new Label()
            {
                Text = "Добавление курсов",
                AutoSize = true,
                TextAlign = ContentAlignment.TopCenter,
                Dock = DockStyle.Fill
            };

            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            tableLayoutPanel.Controls.Add(titleLabel, 0, 0);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Название:", newCourseNameTextBox), 0, 1);

            newCourseAudienceAgeSlider.Minimum = 1;
            newCourseAudienceAgeSlider.Maximum = 100;
            newCourseAudienceAgeSlider.TickFrequency = 1;
            newCourseAudienceAgeSlider.Value = 1;
            newCourseAudienceAgeSlider.Dock = DockStyle.Fill;

            Label labelCurrentValue = new Label();
            labelCurrentValue.Location = new Point(newCourseAudienceAgeSlider.Right + 10, newCourseAudienceAgeSlider.Top);
            labelCurrentValue.Text = newCourseAudienceAgeSlider.Value.ToString();

            newCourseAudienceAgeSlider.ValueChanged += (object sender, EventArgs e) =>
            {
                labelCurrentValue.Text = newCourseAudienceAgeSlider.Value.ToString();
            };

            TableLayoutPanel panel = new TableLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
            };

            panel.Controls.Add(labelCurrentValue);
            panel.Controls.Add(newCourseAudienceAgeSlider);

            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Возраст аудитории:", panel), 0, 2);

            newCourseComplexityComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            newCourseComplexityComboBox.Items.AddRange(Enum.GetNames(typeof(CourseComplexity)));
            newCourseComplexityComboBox.SelectedIndex = 0;
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Сложность курса:", newCourseComplexityComboBox), 0, 3);

            newCourseLecturesCountUpDown.Minimum = 1;
            newCourseLecturesCountUpDown.Maximum = 100;
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Количество лекций:", newCourseLecturesCountUpDown), 0, 4);

            newCourseLabsCountUpDown.Minimum = 0;
            newCourseLabsCountUpDown.Maximum = 100;
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Количество лабораторных:", newCourseLabsCountUpDown), 0, 5);

            newCourseControlTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            newCourseControlTypeComboBox.Items.AddRange(Enum.GetNames(typeof(FinalsType)));
            newCourseControlTypeComboBox.SelectedIndex = 0;
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Вид контроля:", newCourseControlTypeComboBox), 0, 6);

            tableLayoutPanel.Controls.Add(addNewCourseButton, 0, 7);


            tableLayoutPanel.Controls.Add(CreateExpander(CreateCurrentCoursesUI(), "Список Курсов", coursesIsExpanded, coursesExpanderContentPanel), 0, 8);

            addNewCourseButton.Click += AddNewCourseButton_Click;
            newCourseNameTextBox.KeyPress += NewCourseNameTextBox_KeyPress;

            return tableLayoutPanel;
        }

        private void NewCourseNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '-' && e.KeyChar != '.' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private GroupBox CreateCourseCard(ProgrammingCourse course)
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0)
            };

            var deleteButton = new Button() { AutoSize = true, Text = "Удалить" };

            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Название курса:", new Label() { Text = course.Name }), 0, 0);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Возраст аудитории:", new Label() { Text = course.AudienceAge.ToString() }), 0, 1);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Сложность:", new Label() { Text = course.Complexity.ToString() }), 0, 2);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Количество лекций:", new Label() { Text = course.LecturesCount.ToString() }), 0, 3);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Количество лабораторных:", new Label() { Text = course.LabsCount.ToString() }), 0, 4);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Тип итогового контроля:", new Label() { Text = course.ControlType.ToString() }), 0, 5);
            tableLayoutPanel.Controls.Add(new Label() { Text = "Преподаватели доступны на вкладке 'Преподаватели'", AutoSize = true }, 0, 6);
            tableLayoutPanel.Controls.Add(deleteButton, 0, 7);

            var groupBox = new GroupBox()
            {
                Name = Guid.NewGuid().ToString(),
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0),
                Text = "Информация о курсе"
            };

            groupBox.Controls.Add(tableLayoutPanel);

            deleteButton.Click += (sender, e) =>
            {
                if (newCourses.Contains(course))
                {
                    newCourses.Remove(course);
                }
                if (coursesExpanderContentPanel.Controls.Contains(groupBox))
                {
                    coursesExpanderContentPanel.Controls.Remove(groupBox);
                }
            };

            newCourses.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Remove
                    && e.OldItems?.Count == 1 && e.OldItems[0].Equals(course)
                    && coursesExpanderContentPanel.Controls.Contains(groupBox))
                {
                    coursesExpanderContentPanel.Controls.Remove(groupBox);
                }

                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    coursesExpanderContentPanel.Controls.Clear();
                }
            };

            return groupBox;
        }

        private void AddNewCourseButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(newCourseNameTextBox.Text))
            {
                ShowErrorDialog("Поле 'Название' не может быть пустым.");
                return;
            }
            string courseNamePattern = @"^[A-Za-zА-Яа-я0-9\s\-.]+$";
            if (!Regex.IsMatch(newCourseNameTextBox.Text, courseNamePattern))
            {
                ShowErrorDialog("Неверный формат названия курса.");
                return;
            }

            if (newCourseAudienceAgeSlider.Value <= 0)
            {
                ShowErrorDialog("Возраст аудитории должен быть больше 0.");
                return;
            }

            if (newCourseLecturesCountUpDown.Value <= 0)
            {
                ShowErrorDialog("Количество лекций должно быть больше 0.");
                return;
            }
            var newCourse = new ProgrammingCourse()
            {
                Name = newCourseNameTextBox.Text,
                AudienceAge = (int)newCourseAudienceAgeSlider.Value,
                Complexity = (CourseComplexity)Enum.Parse(typeof(CourseComplexity), newCourseComplexityComboBox.SelectedItem.ToString()),
                LecturesCount = (int)newCourseLecturesCountUpDown.Value,
                LabsCount = (int)newCourseLabsCountUpDown.Value,
                ControlType = (FinalsType)Enum.Parse(typeof(FinalsType), newCourseControlTypeComboBox.SelectedItem.ToString()),
                Teachers = new ObservableCollection<Teacher>()
            };

            newCourseNameTextBox.Text = string.Empty;
            newCourseAudienceAgeSlider.Value = newCourseAudienceAgeSlider.Minimum;
            newCourseComplexityComboBox.SelectedIndex = 0;
            newCourseLecturesCountUpDown.Value = newCourseLecturesCountUpDown.Minimum;
            newCourseLabsCountUpDown.Value = newCourseLabsCountUpDown.Minimum;
            newCourseControlTypeComboBox.SelectedIndex = 0;

            newCourses.Add(newCourse);
        }

        #endregion

        #region добавление преподов в курс

        TextBox courseTeacherNameTextBox = new TextBox();
        TextBox courseTeacherDepartmentTextBox = new TextBox();
        MaskedTextBox courseTeacherAuditoriumTextBox = new MaskedTextBox();
        Button addTeacherToCourseButton = new Button() { Text = "Добавить преподавателя в курс", AutoSize = true };
        ComboBox selectCourseComboBox = new ComboBox() { Margin = new Padding(0, 0, 0, 10) };

        BindingSource coursesBindingSource;

        private Control CreateAddTeacherToCourseUI()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0)
            };

            var titleLabel = new Label()
            {
                Text = "Добавление преподавателя в курс",
                AutoSize = true,
                TextAlign = ContentAlignment.TopCenter,
                Dock = DockStyle.Fill
            };

            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            tableLayoutPanel.Controls.Add(titleLabel, 0, 0);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Выберите курс:", selectCourseComboBox), 0, 1);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("ФИО преподавателя:", courseTeacherNameTextBox), 0, 2);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Кафедра преподавателя:", courseTeacherDepartmentTextBox), 0, 3);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Аудитория:", courseTeacherAuditoriumTextBox), 0, 4);
            tableLayoutPanel.Controls.Add(addTeacherToCourseButton, 0, 5);

            selectCourseComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            selectCourseComboBox.DataSource = newCourses;
            newCourses.CollectionChanged += NewCourses_CollectionChanged;

            courseTeacherAuditoriumTextBox.Mask = "000-L";

            selectCourseComboBox.SelectedValueChanged += SelectCourseComboBox_SelectedValueChanged;

            courseTeacherNameTextBox.KeyPress += CourseTeacherNameTextBox_KeyPress;
            courseTeacherDepartmentTextBox.KeyPress += CourseTeacherDepartmentTextBox_KeyPress;

            addTeacherToCourseButton.Click += AddTeacherToCourseButton_Click;

            return tableLayoutPanel;
        }

        private void SelectCourseComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            var ss = selectCourseComboBox.SelectedItem as ProgrammingCourse;

            teachersExpanderContentPanel.Controls.Clear();

            if (ss != null && ss.Teachers != null)
            {
                foreach (var item in ss.Teachers)
                {
                    teachersExpanderContentPanel.Controls.Add(CreateTeacherCard(item));
                }
            }
        }

        private void NewCourses_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            selectCourseComboBox.DataSource = null;
            selectCourseComboBox.DataSource = newCourses;
            selectCourseComboBox.DisplayMember = "Name";
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var newItem = e.NewItems[0] as ProgrammingCourse;
                if (newItem != null)
                {
                    newItem.Teachers.CollectionChanged += NewTeachers_CollectionChanged;

                    foreach (var item in newItem.Teachers)
                    {
                        if (newTeachers.FirstOrDefault(tt => tt.Equals(item)) == default)
                        {
                            newTeachers.Add(item);
                        }
                    }
                }

                var t = e.NewItems[0] as ProgrammingCourse;
                if (t != null)
                {
                    coursesExpanderContentPanel.Controls.Add(CreateCourseCard(t));
                }
            }

        }

        private void CourseTeacherNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '-' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void CourseTeacherDepartmentTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '(' &&
                e.KeyChar != ')' && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void AddTeacherToCourseButton_Click(object sender, EventArgs e)
        {
            if (selectCourseComboBox.SelectedItem == null)
            {
                ShowErrorDialog("Выберите курс, в который будет добавлен преподаватель.");
                return;
            }

            if (string.IsNullOrWhiteSpace(courseTeacherNameTextBox.Text))
            {
                ShowErrorDialog("Поле 'ФИО преподавателя' не может быть пустым.");
                return;
            }
            string teacherNamePattern = @"^[A-Za-zА-Яа-яЁё]+([ -][A-Za-zА-Яа-яЁё]+)+$";
            if (!Regex.IsMatch(courseTeacherNameTextBox.Text, teacherNamePattern))
            {
                ShowErrorDialog("Неверный формат ФИО преподавателя.");
                return;
            }

            if (string.IsNullOrWhiteSpace(courseTeacherDepartmentTextBox.Text))
            {
                ShowErrorDialog("Поле 'Кафедра преподавателя' не может быть пустым.");
                return;
            }

            if (!courseTeacherAuditoriumTextBox.MaskCompleted)
            {
                ShowErrorDialog("Поле 'Аудитория' заполнено некорректно. Формат должен быть: 000-L.");
                return;
            }

            ProgrammingCourse selectedCourse = selectCourseComboBox.SelectedItem as ProgrammingCourse;

            Teacher newTeacher = new Teacher()
            {
                FullName = courseTeacherNameTextBox.Text,
                Department = courseTeacherDepartmentTextBox.Text,
                Auditorium = courseTeacherAuditoriumTextBox.Text
            };

            selectedCourse?.Teachers.Add(newTeacher);

            courseTeacherNameTextBox.Text = string.Empty;
            courseTeacherDepartmentTextBox.Text = string.Empty;
            courseTeacherAuditoriumTextBox.Text = string.Empty;
        }

        #endregion

        ObservableCollection<Teacher> newTeachers = new ObservableCollection<Teacher>();

        private Control CreateCurrentTeachersUI()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0)
            };

            newTeachers.CollectionChanged += NewTeachers_CollectionChanged;

            return tableLayoutPanel;
        }

        private GroupBox CreateTeacherCard(Teacher teacher)
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0)
            };

            var button = new Button() { AutoSize = true, Text = "Удалить" };

            tableLayoutPanel.Controls.Add(CreateControlWithLabel("ФИО:", new Label() { Text = teacher.FullName }), 0, 1);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Кафедра:", new Label() { Text = teacher.Department }), 0, 2);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Аудитория:", new Label() { Text = teacher.Auditorium }), 0, 3);
            tableLayoutPanel.Controls.Add(button, 0, 4);

            var gb = new GroupBox()
            {
                Name = Guid.NewGuid().ToString(),
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0),
                Text = "Информация о преподавателе"
            };

            gb.Controls.Add(tableLayoutPanel);

            button.Click += (object sender, EventArgs e) =>
            {
                if ((selectCourseComboBox.SelectedItem as ProgrammingCourse)?.Teachers.Contains(teacher) == true)
                {
                    (selectCourseComboBox.SelectedItem as ProgrammingCourse)?.Teachers.Remove(teacher);
                }
                if (teachersExpanderContentPanel.Controls.Contains(gb))
                {
                    teachersExpanderContentPanel.Controls.Remove(gb);
                }
            };

            return gb;
        }

        private void NewTeachers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var t = e.NewItems[0] as Teacher;
                if (t != null)
                {
                    teachersExpanderContentPanel.Controls.Add(CreateTeacherCard(t));
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                teachersExpanderContentPanel.Controls.Clear();
            }
        }

        #region Expander

        private Panel teachersExpanderContentPanel = new Panel();
        private bool teachersIsExpanded = false;

        private Panel coursesExpanderContentPanel = new Panel();
        private bool coursesIsExpanded = false;

        private Panel CreateExpander(Control contentControl, string label, bool isExpanded, Panel expanderContentPanel)
        {
            Panel expanderPanel = new Panel();

            Button expanderToggleButton = new Button()
            {
                Text = $"Развернуть {label}",
                Dock = DockStyle.Top,
                AutoSize = true,
            };

            expanderToggleButton.Click += (object sender, EventArgs e) =>
            {
                if (isExpanded)
                {
                    expanderContentPanel.Visible = false;
                    expanderPanel.Height = expanderToggleButton.PreferredSize.Height + 2;
                    expanderToggleButton.Text = $"Развернуть {label}";
                    isExpanded = false;
                }
                else
                {
                    expanderContentPanel.Visible = true;
                    expanderPanel.Height = expanderToggleButton.Height + expanderContentPanel.PreferredSize.Height;
                    expanderToggleButton.Text = $"Свернуть {label}";
                    isExpanded = true;
                }
            };

            expanderContentPanel.Dock = DockStyle.Fill;
            expanderContentPanel.Visible = false;
            expanderContentPanel.Padding = new Padding(5, 0, 5, 10);
            expanderContentPanel.Controls.Add(contentControl);

            expanderPanel.BorderStyle = BorderStyle.FixedSingle;
            expanderPanel.Dock = DockStyle.Top;
            expanderPanel.Height = expanderToggleButton.PreferredSize.Height + 10;

            expanderPanel.Controls.Add(expanderContentPanel);
            expanderPanel.Controls.Add(expanderToggleButton);

            return expanderPanel;
        }

        #endregion


        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.jsonPathtextBox = new System.Windows.Forms.TextBox();
            this.loadFromJsonButton = new System.Windows.Forms.Button();
            this.saveToJsonButon = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.сортировкаПоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.tabControl1.Location = new System.Drawing.Point(19, 34);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(10, 10, 10, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 742);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage1.Size = new System.Drawing.Size(776, 705);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Преподаватели";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage2.Size = new System.Drawing.Size(776, 643);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Дисциплины";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.jsonPathtextBox);
            this.tabPage3.Controls.Add(this.loadFromJsonButton);
            this.tabPage3.Controls.Add(this.saveToJsonButon);
            this.tabPage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPage3.Location = new System.Drawing.Point(4, 33);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(776, 643);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Импорт/Экспорт";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // jsonPathtextBox
            // 
            this.jsonPathtextBox.Location = new System.Drawing.Point(8, 163);
            this.jsonPathtextBox.Multiline = true;
            this.jsonPathtextBox.Name = "jsonPathtextBox";
            this.jsonPathtextBox.Size = new System.Drawing.Size(760, 108);
            this.jsonPathtextBox.TabIndex = 2;
            this.jsonPathtextBox.TextChanged += new System.EventHandler(this.jsonPathtextBox_TextChanged);
            // 
            // loadFromJsonButton
            // 
            this.loadFromJsonButton.Location = new System.Drawing.Point(277, 365);
            this.loadFromJsonButton.Name = "loadFromJsonButton";
            this.loadFromJsonButton.Size = new System.Drawing.Size(220, 28);
            this.loadFromJsonButton.TabIndex = 1;
            this.loadFromJsonButton.Text = "Загрузить из json";
            this.loadFromJsonButton.UseVisualStyleBackColor = true;
            this.loadFromJsonButton.Click += new System.EventHandler(this.loadFromJsonButton_Click);
            // 
            // saveToJsonButon
            // 
            this.saveToJsonButon.Location = new System.Drawing.Point(277, 306);
            this.saveToJsonButon.Name = "saveToJsonButon";
            this.saveToJsonButon.Size = new System.Drawing.Size(220, 28);
            this.saveToJsonButon.TabIndex = 0;
            this.saveToJsonButon.Text = "Сохранить в json";
            this.saveToJsonButon.UseVisualStyleBackColor = true;
            this.saveToJsonButon.Click += new System.EventHandler(this.saveToJsonButon_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panel1);
            this.tabPage4.Controls.Add(this.button1);
            this.tabPage4.Location = new System.Drawing.Point(4, 33);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1061, 705);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Расчет прибыли";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Location = new System.Drawing.Point(33, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(735, 547);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(228, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(286, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "Расчитать ожидаемый доход";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.сортировкаПоToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(54, 20);
            this.toolStripMenuItem1.Text = "Поиск";
            // 
            // сортировкаПоToolStripMenuItem
            // 
            this.сортировкаПоToolStripMenuItem.Name = "сортировкаПоToolStripMenuItem";
            this.сортировкаПоToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.сортировкаПоToolStripMenuItem.Text = "Сортировка по";
            this.сортировкаПоToolStripMenuItem.Click += new System.EventHandler(this.сортировкаПоToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(800, 810);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "Редактор курса по программирования";
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Button saveToJsonButon;
        private Button loadFromJsonButton;
        private TextBox jsonPathtextBox;
        private TabPage tabPage4;
        private Button button1;
        private Panel panel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem сортировкаПоToolStripMenuItem;
        private ToolStripMenuItem сохранитьToolStripMenuItem;
        private ToolStripMenuItem оПрограммеToolStripMenuItem;
    }
}

