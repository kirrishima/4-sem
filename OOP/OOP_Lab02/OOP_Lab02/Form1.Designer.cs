using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace OOP_Lab02
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        NumericUpDown newCourseAudienceAgeUpDown = new NumericUpDown();
        ComboBox newCourseComplexityComboBox = new ComboBox();
        NumericUpDown newCourseLecturesCountUpDown = new NumericUpDown();
        NumericUpDown newCourseLabsCountUpDown = new NumericUpDown();
        ComboBox newCourseControlTypeComboBox = new ComboBox();
        Button addNewCourseButton = new Button() { Text = "Добавить курс", AutoSize = true };

        ObservableCollection<ProgrammingCourse> newCourses = new ObservableCollection<ProgrammingCourse>();

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

            tableLayoutPanel.Controls.Add(titleLabel, 0, 0);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Название:", newCourseNameTextBox), 0, 1);

            // Настройка NumericUpDown для возраста аудитории
            newCourseAudienceAgeUpDown.Minimum = 1;
            newCourseAudienceAgeUpDown.Maximum = 100;
            newCourseAudienceAgeUpDown.Value = 18;
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Возраст аудитории:", newCourseAudienceAgeUpDown), 0, 2);

            // Настройка ComboBox для сложности курса
            newCourseComplexityComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            newCourseComplexityComboBox.Items.AddRange(Enum.GetNames(typeof(CourseComplexity)));
            newCourseComplexityComboBox.SelectedIndex = 0;
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Сложность курса:", newCourseComplexityComboBox), 0, 3);

            // Настройка NumericUpDown для количества лекций
            newCourseLecturesCountUpDown.Minimum = 1;
            newCourseLecturesCountUpDown.Maximum = 100;
            newCourseLecturesCountUpDown.Value = 10;
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Количество лекций:", newCourseLecturesCountUpDown), 0, 4);

            // Настройка NumericUpDown для количества лабораторных
            newCourseLabsCountUpDown.Minimum = 0;
            newCourseLabsCountUpDown.Maximum = 100;
            newCourseLabsCountUpDown.Value = 5;
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Количество лабораторных:", newCourseLabsCountUpDown), 0, 5);

            // Настройка ComboBox для вида контроля
            newCourseControlTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            newCourseControlTypeComboBox.Items.AddRange(Enum.GetNames(typeof(FinalsType)));
            newCourseControlTypeComboBox.SelectedIndex = 0;
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Вид контроля:", newCourseControlTypeComboBox), 0, 6);

            tableLayoutPanel.Controls.Add(addNewCourseButton, 0, 7);

            addNewCourseButton.Click += AddNewCourseButton_Click;
            // Можно добавить фильтрацию ввода для названия курса (если необходимо)
            newCourseNameTextBox.KeyPress += NewCourseNameTextBox_KeyPress;

            return tableLayoutPanel;
        }

        // Пример обработчика для ограничения ввода символов в названии курса
        private void NewCourseNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем буквы, цифры, пробелы, дефис и точку, а также клавишу Backspace
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '-' && e.KeyChar != '.' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void AddNewCourseButton_Click(object sender, EventArgs e)
        {
            // Валидация поля "Название" с использованием регулярного выражения:
            // Допустим, название курса может состоять из букв, цифр, пробелов, дефисов и точек.
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

            // Проверка возраста аудитории (> 0)
            if (newCourseAudienceAgeUpDown.Value <= 0)
            {
                ShowErrorDialog("Возраст аудитории должен быть больше 0.");
                return;
            }

            // Проверка количества лекций (> 0)
            if (newCourseLecturesCountUpDown.Value <= 0)
            {
                ShowErrorDialog("Количество лекций должно быть больше 0.");
                return;
            }
            // Количество лабораторных может быть 0 и больше, поэтому дополнительная проверка не требуется

            // Создание нового курса
            var newCourse = new ProgrammingCourse()
            {
                Name = newCourseNameTextBox.Text,
                AudienceAge = (int)newCourseAudienceAgeUpDown.Value,
                Complexity = (CourseComplexity)Enum.Parse(typeof(CourseComplexity), newCourseComplexityComboBox.SelectedItem.ToString()),
                LexturesCount = (int)newCourseLecturesCountUpDown.Value,
                LabsCount = (int)newCourseLabsCountUpDown.Value,
                ControlType = (FinalsType)Enum.Parse(typeof(FinalsType), newCourseControlTypeComboBox.SelectedItem.ToString()),
                Teachers = new ObservableCollection<Teacher>()
            };

            // Очистка полей после добавления
            newCourseNameTextBox.Text = string.Empty;
            newCourseAudienceAgeUpDown.Value = newCourseAudienceAgeUpDown.Minimum;
            newCourseComplexityComboBox.SelectedIndex = 0;
            newCourseLecturesCountUpDown.Value = newCourseLecturesCountUpDown.Minimum;
            newCourseLabsCountUpDown.Value = newCourseLabsCountUpDown.Minimum;
            newCourseControlTypeComboBox.SelectedIndex = 0;

            newCourses.Add(newCourse);
        }

        #endregion

        #region djsakdjsakjdhkas
        // Элементы для добавления преподавателя в курс:
        TextBox courseTeacherNameTextBox = new TextBox();
        TextBox courseTeacherDepartmentTextBox = new TextBox();
        MaskedTextBox courseTeacherAuditoriumTextBox = new MaskedTextBox();
        Button addTeacherToCourseButton = new Button() { Text = "Добавить преподавателя в курс", AutoSize = true };
        ComboBox selectCourseComboBox = new ComboBox();

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

            // Заголовок
            var titleLabel = new Label()
            {
                Text = "Добавление преподавателя в курс",
                AutoSize = true,
                TextAlign = ContentAlignment.TopCenter,
                Dock = DockStyle.Fill
            };

            tableLayoutPanel.Controls.Add(titleLabel, 0, 0);
            // Контрол для выбора курса
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Выберите курс:", selectCourseComboBox), 0, 1);
            // Поля для данных преподавателя
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("ФИО преподавателя:", courseTeacherNameTextBox), 0, 2);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Кафедра преподавателя:", courseTeacherDepartmentTextBox), 0, 3);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Аудитория:", courseTeacherAuditoriumTextBox), 0, 4);
            tableLayoutPanel.Controls.Add(addTeacherToCourseButton, 0, 5);

            // Настройка комбобокса для выбора курса:
            selectCourseComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            // Привязываем список курсов; DisplayMember указывает, что показывать в списке – имя курса
            // Объявляем BindingSource на уровне класса:
            BindingSource coursesBindingSource = new BindingSource();

            // При инициализации UI:
            selectCourseComboBox.DataSource = newCourses;
            newCourses.CollectionChanged += NewCourses_CollectionChanged;
            // Настройка MaskedTextBox для аудитории
            courseTeacherAuditoriumTextBox.Mask = "000-L";

            selectCourseComboBox.SelectedValueChanged += SelectCourseComboBox_SelectedValueChanged;

            // Добавляем фильтрацию ввода для ФИО и кафедры
            courseTeacherNameTextBox.KeyPress += CourseTeacherNameTextBox_KeyPress;
            courseTeacherDepartmentTextBox.KeyPress += CourseTeacherDepartmentTextBox_KeyPress;

            // Назначаем обработчик нажатия на кнопку
            addTeacherToCourseButton.Click += AddTeacherToCourseButton_Click;

            return tableLayoutPanel;
        }

        private void SelectCourseComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            expanderContentPanel.Controls.Clear();

            foreach (var item in ((ProgrammingCourse)selectCourseComboBox.SelectedItem).Teachers)
            {
                expanderContentPanel.Controls.Add(CreateTeacherCard(item));
            }
        }

        private void NewCourses_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            selectCourseComboBox.DataSource = null;
            selectCourseComboBox.DataSource = newCourses;
            selectCourseComboBox.DisplayMember = "Name";
        }

        private void CourseTeacherNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем буквы, пробелы, дефис и Backspace
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '-' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void CourseTeacherDepartmentTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем буквы, пробелы и некоторые знаки (как в предыдущей реализации)
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '(' &&
                e.KeyChar != ')' && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void AddTeacherToCourseButton_Click(object sender, EventArgs e)
        {
            // Проверяем, выбран ли курс
            if (selectCourseComboBox.SelectedItem == null)
            {
                ShowErrorDialog("Выберите курс, в который будет добавлен преподаватель.");
                return;
            }

            // Валидация ФИО преподавателя с использованием регулярного выражения:
            if (string.IsNullOrWhiteSpace(courseTeacherNameTextBox.Text))
            {
                ShowErrorDialog("Поле 'ФИО преподавателя' не может быть пустым.");
                return;
            }
            // Пример регулярного выражения: минимум два слова, состоящих из букв (кириллица или латиница)
            string teacherNamePattern = @"^[A-Za-zА-Яа-яЁё]+([ -][A-Za-zА-Яа-яЁё]+)+$";
            if (!Regex.IsMatch(courseTeacherNameTextBox.Text, teacherNamePattern))
            {
                ShowErrorDialog("Неверный формат ФИО преподавателя.");
                return;
            }

            // Валидация кафедры преподавателя
            if (string.IsNullOrWhiteSpace(courseTeacherDepartmentTextBox.Text))
            {
                ShowErrorDialog("Поле 'Кафедра преподавателя' не может быть пустым.");
                return;
            }

            // Проверка заполненности поля аудитории
            if (!courseTeacherAuditoriumTextBox.MaskCompleted)
            {
                ShowErrorDialog("Поле 'Аудитория' заполнено некорректно. Формат должен быть: 000-L.");
                return;
            }

            // Получаем выбранный курс
            ProgrammingCourse selectedCourse = (ProgrammingCourse)selectCourseComboBox.SelectedItem;

            // Создаём нового преподавателя
            Teacher newTeacher = new Teacher()
            {
                FullName = courseTeacherNameTextBox.Text,
                Department = courseTeacherDepartmentTextBox.Text,
                Auditorium = courseTeacherAuditoriumTextBox.Text
            };

            // Добавляем преподавателя в коллекцию выбранного курса
            selectedCourse.Teachers.Add(newTeacher);

            // Очищаем поля ввода преподавателя
            courseTeacherNameTextBox.Text = string.Empty;
            courseTeacherDepartmentTextBox.Text = string.Empty;
            courseTeacherAuditoriumTextBox.Text = string.Empty;
        }


        #endregion


        TextBox newTeacherNameTextBox = new TextBox();
        TextBox newTeacherDepartmentTextBox = new TextBox();
        MaskedTextBox newTeacherAuditoriumTextBox = new MaskedTextBox();
        Button addNewTeacherButton = new Button() { Text = "Добавить", AutoSize = true };

        private Panel expanderPanel = new Panel();
        private Button expanderToggleButton = new Button();
        private Panel expanderContentPanel = new Panel();
        private bool isExpanded = false;

        ObservableCollection<Teacher> newTeachers = new ObservableCollection<Teacher>();

        private void LoadFromJson(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    var jsonString = File.ReadAllText(path);
                    var json = JsonSerializer.Deserialize<ObservableCollection<Teacher>>(jsonString);

                    foreach (var item in json)
                    {
                        newTeachers.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorDialog($"Ошибка при загрузке данных из '{path}': {ex.Message}");
                }
            }
        }

        private void SaveToJson(string path)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(newTeachers);

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

        private Control CreateAddTeachersUI()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0)
            };

            var label = new Label()
            {
                Text = "Добавление преподавателей",
                AutoSize = true,
                TextAlign = System.Drawing.ContentAlignment.TopCenter,
                Dock = DockStyle.Fill
            };

            tableLayoutPanel.Controls.Add(label, 0, 0);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("ФИО:", newTeacherNameTextBox), 0, 1);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Кафедра:", newTeacherDepartmentTextBox), 0, 2);
            tableLayoutPanel.Controls.Add(CreateControlWithLabel("Аудитория:", newTeacherAuditoriumTextBox), 0, 3);
            tableLayoutPanel.Controls.Add(addNewTeacherButton);

            addNewTeacherButton.Click += AddNewTeacherButton_Click;
            newTeacherNameTextBox.KeyPress += NewTeacherNameTextBox_KeyPress;
            newTeacherDepartmentTextBox.KeyPress += NewTeacherDepartmentTextBox_KeyPress;
            newTeacherAuditoriumTextBox.Mask = "000-L";
            return tableLayoutPanel;
        }

        private void NewTeacherDepartmentTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '(' &&
             e.KeyChar != ')' && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void NewTeacherNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '-' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void AddNewTeacherButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(newTeacherNameTextBox.Text))
            {
                ShowErrorDialog("Поле 'ФИО' не может быть пустым.");
                return;
            }

            if (string.IsNullOrWhiteSpace(newTeacherDepartmentTextBox.Text))
            {
                ShowErrorDialog("Поле 'Кафедра' не может быть пустым.");
                return;
            }

            if (!newTeacherAuditoriumTextBox.MaskCompleted)
            {
                ShowErrorDialog("Поле 'Аудитория' заполнено некорректно. Формат должен быть: 000-L.");
                return;
            }

            var newTeacher = new Teacher()
            {
                FullName = newTeacherNameTextBox.Text,
                Department = newTeacherDepartmentTextBox.Text,
                Auditorium = newTeacherAuditoriumTextBox.Text
            };

            newTeacherNameTextBox.Text = string.Empty;
            newTeacherDepartmentTextBox.Text = string.Empty;
            newTeacherAuditoriumTextBox.Text = string.Empty;

            newTeachers.Add(newTeacher);
        }

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
            LoadFromJson(jsonPath);
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
                Margin = new Padding(0)
            };

            gb.Controls.Add(tableLayoutPanel);

            button.Click += (object sender, EventArgs e) =>
            {
                if (newTeachers.Contains(teacher))
                {
                    newTeachers.Remove(teacher);
                }
                if (expanderContentPanel.Controls.Contains(gb))
                {
                    expanderContentPanel.Controls.Remove(gb);
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
                    expanderContentPanel.Controls.Add(CreateTeacherCard(t));
                }
            }
        }

        #region Expander
        private Panel CreateExpander(Control contentControl, string label)
        {
            expanderToggleButton.Text = $"Развернуть {label}";
            expanderToggleButton.Dock = DockStyle.Top;
            expanderToggleButton.AutoSize = true;
            expanderToggleButton.Click += ToggleButton_Click;

            expanderContentPanel.Dock = DockStyle.Fill;
            expanderContentPanel.Visible = false;
            expanderContentPanel.Padding = new Padding(5, 0, 5, 10);
            expanderContentPanel.Controls.Add(contentControl);

            expanderPanel.BorderStyle = BorderStyle.FixedSingle;
            expanderPanel.Dock = DockStyle.Top;
            expanderPanel.Height = expanderToggleButton.PreferredSize.Height + 10;
            expanderPanel.Tag = label;

            expanderPanel.Controls.Add(expanderContentPanel);
            expanderPanel.Controls.Add(expanderToggleButton);

            return expanderPanel;
        }

        private void ToggleButton_Click(object sender, EventArgs e)
        {
            if (isExpanded)
            {
                expanderContentPanel.Visible = false;
                expanderPanel.Height = expanderToggleButton.PreferredSize.Height + 2;
                expanderToggleButton.Text = $"Развернуть {expanderPanel.Tag as string}";
                isExpanded = false;
            }
            else
            {
                expanderContentPanel.Visible = true;
                expanderPanel.Height = expanderToggleButton.Height + expanderContentPanel.PreferredSize.Height;
                expanderToggleButton.Text = $"Свернуть {expanderPanel.Tag as string}";
                isExpanded = true;
            }
        }

        #endregion


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(10, 10, 10, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 680);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tabPage1.Size = new System.Drawing.Size(776, 643);
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
            this.tabPage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabPage3.Location = new System.Drawing.Point(4, 33);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(776, 643);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Импорт/Экспорт";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(784, 694);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "Редактор курса по программирования";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
    }
}

