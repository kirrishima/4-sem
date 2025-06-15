using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
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

        private static ObservableCollection<ProgrammingCourse> newCourses = new ObservableCollection<ProgrammingCourse>();

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

            newCourseAudienceAgeSlider.Minimum = 0;
            newCourseAudienceAgeSlider.Maximum = 100;
            newCourseAudienceAgeSlider.TickFrequency = 1;
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
            //newCourseNameTextBox.KeyPress += NewCourseNameTextBox_KeyPress;

            return tableLayoutPanel;
        }

        //private void NewCourseNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '-' && e.KeyChar != '.' && e.KeyChar != (char)Keys.Back)
        //    {
        //        e.Handled = true;
        //    }
        //}

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

            newCourses.CollectionChanged += (sender, e) =>
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
            LastAction = "Добавить курс";
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

            var context = new ValidationContext(newCourse, null, null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(newCourse, context, results, true))
            {
                string errors = string.Join("\n", results.Select(r => r.ErrorMessage));
                ShowErrorDialog(errors);
                return;
            }
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

            //courseTeacherNameTextBox.KeyPress += CourseTeacherNameTextBox_KeyPress;
            //courseTeacherDepartmentTextBox.KeyPress += CourseTeacherDepartmentTextBox_KeyPress;

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
            LastAction = "Добавить преподавателя";
            if (selectCourseComboBox.SelectedItem == null)
            {
                ShowErrorDialog("Выберите курс, в который будет добавлен преподаватель.");
                return;
            }

            Teacher newTeacher = new Teacher()
            {
                FullName = courseTeacherNameTextBox.Text,
                Department = courseTeacherDepartmentTextBox.Text,
                Auditorium = courseTeacherAuditoriumTextBox.Text
            };

            var validationContext = new ValidationContext(newTeacher, null, null);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(newTeacher, validationContext, validationResults, true);

            if (!isValid)
            {
                string errors = string.Join("\n", validationResults.Select(r => r.ErrorMessage));
                ShowErrorDialog(errors);
                return;
            }

            ProgrammingCourse selectedCourse = selectCourseComboBox.SelectedItem as ProgrammingCourse;
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

            expanderContentPanel.Dock = DockStyle.Fill;
            expanderContentPanel.Padding = new Padding(5, 0, 5, 10);
            expanderContentPanel.Controls.Add(contentControl);

            expanderPanel.BorderStyle = BorderStyle.FixedSingle;
            expanderPanel.Dock = DockStyle.Top;

            expanderPanel.Controls.Add(expanderContentPanel);

            expanderContentPanel.ControlAdded += (s, e) => expanderPanel.Height = expanderContentPanel.PreferredSize.Height;
            expanderContentPanel.ControlRemoved += (s, e) => expanderPanel.Height = expanderContentPanel.PreferredSize.Height;

            return expanderPanel;
        }

        #endregion

        #region Шелуха 

        private void InitializeШелуха()
        {

        }

        #endregion


        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.сортировкаПоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.алфавитныйПорядокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обратныйАлфавитныйПорядокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вJsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вXmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.saveToJsonButon = new System.Windows.Forms.Button();
            this.loadFromJsonButton = new System.Windows.Forms.Button();
            this.jsonPathtextBox = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(54, 20);
            this.toolStripMenuItem1.Text = "Поиск";
            // 
            // сортировкаПоToolStripMenuItem
            // 
            this.сортировкаПоToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.алфавитныйПорядокToolStripMenuItem,
            this.обратныйАлфавитныйПорядокToolStripMenuItem});
            this.сортировкаПоToolStripMenuItem.Name = "сортировкаПоToolStripMenuItem";
            this.сортировкаПоToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.сортировкаПоToolStripMenuItem.Text = "Сортировка по";
            // 
            // алфавитныйПорядокToolStripMenuItem
            // 
            this.алфавитныйПорядокToolStripMenuItem.Name = "алфавитныйПорядокToolStripMenuItem";
            this.алфавитныйПорядокToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.алфавитныйПорядокToolStripMenuItem.Text = "Названию";
            // 
            // обратныйАлфавитныйПорядокToolStripMenuItem
            // 
            this.обратныйАлфавитныйПорядокToolStripMenuItem.Name = "обратныйАлфавитныйПорядокToolStripMenuItem";
            this.обратныйАлфавитныйПорядокToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.обратныйАлфавитныйПорядокToolStripMenuItem.Text = "Числу преподавателей";
            this.обратныйАлфавитныйПорядокToolStripMenuItem.Click += new System.EventHandler(this.обратныйАлфавитныйПорядокToolStripMenuItem_Click_1);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вJsonToolStripMenuItem,
            this.вXmlToolStripMenuItem});
            this.сохранитьToolStripMenuItem.Enabled = false;
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // вJsonToolStripMenuItem
            // 
            this.вJsonToolStripMenuItem.Name = "вJsonToolStripMenuItem";
            this.вJsonToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.вJsonToolStripMenuItem.Text = "В Json";
            // 
            // вXmlToolStripMenuItem
            // 
            this.вXmlToolStripMenuItem.Name = "вXmlToolStripMenuItem";
            this.вXmlToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.вXmlToolStripMenuItem.Text = "в Xml";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
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
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panel1);
            this.tabPage4.Controls.Add(this.button1);
            this.tabPage4.Location = new System.Drawing.Point(4, 33);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(792, 570);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Расчет прибыли";
            this.tabPage4.UseVisualStyleBackColor = true;
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
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Location = new System.Drawing.Point(33, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(735, 547);
            this.panel1.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.jsonPathtextBox);
            this.tabPage3.Controls.Add(this.loadFromJsonButton);
            this.tabPage3.Controls.Add(this.saveToJsonButon);
            this.tabPage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPage3.Location = new System.Drawing.Point(4, 33);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(792, 570);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Импорт/Экспорт";
            this.tabPage3.UseVisualStyleBackColor = true;
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
            // jsonPathtextBox
            // 
            this.jsonPathtextBox.Location = new System.Drawing.Point(8, 163);
            this.jsonPathtextBox.Multiline = true;
            this.jsonPathtextBox.Name = "jsonPathtextBox";
            this.jsonPathtextBox.Size = new System.Drawing.Size(760, 108);
            this.jsonPathtextBox.TabIndex = 2;
            this.jsonPathtextBox.TextChanged += new System.EventHandler(this.jsonPathtextBox_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage2.Size = new System.Drawing.Size(792, 570);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Дисциплины";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage1.Size = new System.Drawing.Size(792, 570);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Преподаватели";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.tabControl1.Location = new System.Drawing.Point(0, 49);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(10, 10, 10, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 607);
            this.tabControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(800, 656);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "Редактор курса по программирования";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem сортировкаПоToolStripMenuItem;
        private ToolStripMenuItem сохранитьToolStripMenuItem;
        private ToolStripMenuItem оПрограммеToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem вJsonToolStripMenuItem;
        private ToolStripMenuItem вXmlToolStripMenuItem;
        private ToolStripMenuItem алфавитныйПорядокToolStripMenuItem;
        private ToolStripMenuItem обратныйАлфавитныйПорядокToolStripMenuItem;
        private Label label1;
        private TabPage tabPage4;
        private Panel panel1;
        private Button button1;
        private TabPage tabPage3;
        private TextBox jsonPathtextBox;
        private Button loadFromJsonButton;
        private Button saveToJsonButon;
        private TabPage tabPage2;
        private TabPage tabPage1;
        private TabControl tabControl1;
    }
}

