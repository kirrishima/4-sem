using System.Collections.Generic;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace TextCalculator
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

        #region Windows Form Designer generated code

        const string InputTextPlaceholder = "Введите строку текста";
        const string OldSubstringPlaceholder = "Введите строку для поиска";
        const string NewSubstringPlaceholder = "Введите строку для замены";
        const string IndexPlaceholder = "Индекс символа";

        private void ToggleButtons(bool state)
        {
            btnCountConsonants.Enabled = state;
            btnCountSentences.Enabled = state;
            btnCountVowels.Enabled = state;
            btnCountWords.Enabled = state;
            btnGetChar.Enabled = state;
            btnLength.Enabled = state;
        }

        private void InitializeComponent()
        {
            txtInput = new TextBox()
            {
                Text = InputTextPlaceholder,
                Tag = InputTextPlaceholder,
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                ScrollBars = ScrollBars.Vertical,
                WordWrap = true,
                Height = 100,
                Multiline = true
            };
            txtInput.Enter += TxtInput_Enter;
            txtInput.Leave += TxtInput_Leave;

            UserChangedFields += () =>
            {
                ToggleButtons(_userStringValid);
            };

            lblResult = new Label()
            {
                AutoSize = true,
                Dock = DockStyle.Top
            };

            TableLayoutPanel tablePanel = new TableLayoutPanel() { Dock = DockStyle.Top };
            tablePanel.ColumnCount = 1;
            tablePanel.RowCount = 4;
            tablePanel.AutoSize = true;
            tablePanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            tablePanel.Controls.Add(txtInput, 0, 0);
            tablePanel.Controls.Add(InitializeInputFields(), 0, 1);
            tablePanel.Controls.Add(InitializeButtons(), 0, 2);
            tablePanel.Controls.Add(lblResult, 0, 3);

            tablePanel.MouseDown += (s, e) => { this.ActiveControl = null; };

            this.Controls.Add(tablePanel);

            this.Padding = new Padding(20);
            this.Font = new Font(FontFamily.GenericSansSerif, 12);
            this.Text = "Текстовый калькулятор";
            this.Width = 800;
            this.Height = 400;

            this.MouseDown += Form1_MouseDown;
            this.Activated += Form1_Activated;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // Если клик произошёл вне активного текстового поля,
            // сбрасываем активный контрол:
            this.ActiveControl = null;
        }
        #endregion

        #region ui initializers
        private FlowLayoutPanel InitializeButtons()
        {
            btnLength = new Button() { Text = "Длина" };
            btnLength.Click += BtnLength_Click;

            btnCountConsonants = new Button() { Text = "Посчитать согласные" };
            btnCountConsonants.Click += BtnCountConsonants_Click;

            btnCountVowels = new Button() { Text = "Посчитать гласные" };
            btnCountVowels.Click += BtnCountVowels_Click;

            btnCountSentences = new Button() { Text = "Количество предложений" };
            btnCountSentences.Click += BtnCountSentences_Click;

            btnCountWords = new Button() { Text = "Посчитать слова" };
            btnCountWords.Click += BtnCountWords_Click;

            btnDelete = new Button() { Text = "Удалить подстроку" };
            btnDelete.Click += BtnDelete_Click;

            btnGetChar = new Button() { Text = "Получить символ по индексу" };
            btnGetChar.Click += BtnGetChar_Click;

            btnReplace = new Button() { Text = "Заменить подстроку" };
            btnReplace.Click += BtnReplace_Click;

            var flowPanel = new FlowLayoutPanel() { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, Margin = new Padding(0) };
            flowPanel.FlowDirection = FlowDirection.LeftToRight;
            flowPanel.WrapContents = true;

            flowPanel.Controls.Add(btnLength);
            flowPanel.Controls.Add(btnCountVowels);
            flowPanel.Controls.Add(btnCountConsonants);
            flowPanel.Controls.Add(btnCountSentences);
            flowPanel.Controls.Add(btnCountWords);
            flowPanel.Controls.Add(btnReplace);
            flowPanel.Controls.Add(btnDelete);
            flowPanel.Controls.Add(btnGetChar);

            flowPanel.MouseDown += (s, e) => { this.ActiveControl = null; };
            foreach (var item in flowPanel.Controls)
            {
                if (item is Button button)
                {
                    button.Enabled = false;
                    button.AutoSize = true;
                }
            }

            return flowPanel;
        }

        private TableLayoutPanel InitializeInputFields()
        {
            txtOldSubstring = new TextBox()
            {
                Text = OldSubstringPlaceholder,
                Dock = DockStyle.Fill,
                Tag = OldSubstringPlaceholder
            };

            txtNewSubstring = new TextBox()
            {
                Text = NewSubstringPlaceholder,
                Dock = DockStyle.Fill,
                Tag = NewSubstringPlaceholder
            };

            numIndex = new NumericUpDown()
            {
                Minimum = 0,
                Maximum = 0,
                Value = 0,
                Increment = 1,
                Dock = DockStyle.Fill,
            };

            UserStringChanged += (s) =>
            {
                numIndex.Value = s.Length - 1 >= numIndex.Value ? numIndex.Value : 0;
                numIndex.Maximum = s.Length - 1;
            };

            txtOldSubstring.Enter += TextBox_Enter;
            txtOldSubstring.Leave += TextBox_Leave;
            this.UserChangedFields += () =>
            {
                if (_oldSubstringValid && _newSubstringValid && _userStringValid)
                {
                    btnReplace.Enabled = true;
                    btnDelete.Enabled = true;
                }
                else if (_oldSubstringValid && _userStringValid)
                {
                    btnDelete.Enabled = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                    btnReplace.Enabled = false;
                }
            };
            txtNewSubstring.Enter += TextBox_Enter;
            txtNewSubstring.Leave += TextBox_Leave;

            var tableLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 3,
                Margin = new Padding(0)
            };

            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));

            tableLayout.Controls.Add(txtOldSubstring, 0, 0);
            tableLayout.Controls.Add(txtNewSubstring, 1, 0);
            tableLayout.Controls.Add(numIndex, 2, 0);

            tableLayout.MouseDown += (s, e) => { this.ActiveControl = null; };
            return tableLayout;
        }
        #endregion

        #region ui events
        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox tb && tb.Text == (string)tb.Tag)
            {
                tb.Text = string.Empty;
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                bool valid = !string.IsNullOrWhiteSpace(tb.Text);
                if (!valid)
                {
                    tb.Text = (string)tb.Tag;
                }

                switch (tb.Tag)
                {
                    case NewSubstringPlaceholder:
                        _newSubstringValid = valid;
                        break;
                    case OldSubstringPlaceholder:
                        _oldSubstringValid = valid;
                        break;
                }
                UserChangedFields?.Invoke();
            }
        }

        private void TxtInput_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox tb && tb.Text == (string)tb.Tag)
            {
                tb.Text = string.Empty;
            }
        }

        private void TxtInput_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (string.IsNullOrEmpty(tb.Text))
                {
                    tb.Text = (string)tb.Tag;
                    _userStringValid = false;
                }
                else
                {
                    _userStringValid = true;
                    UserStringChanged?.Invoke(tb.Text);
                }
                UserChangedFields?.Invoke();
            }
        }

        private void Calculator_OperationCompleted(object sender, OperationEventArgs e)
        {
            lblResult.Text = e.Result;
        }

        private void BtnReplace_Click(object sender, EventArgs e)
        {
            if (!_userStringValid)
            {
                MessageBox.Show("Некорректная строка ввода.");
                return;
            }

            try
            {
                string input = txtInput.Text;
                string oldSub = txtOldSubstring.Text;
                string newSub = txtNewSubstring.Text;

                calculator.ReplaceSubstring(input, oldSub, newSub);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при замене подстроки: " + ex.Message);
            }
            this.ActiveControl = null;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!_userStringValid)
            {
                MessageBox.Show("Некорректная строка ввода.");
                return;
            }

            try
            {
                string input = txtInput.Text;
                calculator.DeleteSubstring(input, txtOldSubstring.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении подстроки: " + ex.Message);
            }
            this.ActiveControl = null;
        }

        private void BtnGetChar_Click(object sender, EventArgs e)
        {
            if (!_userStringValid)
            {
                MessageBox.Show("Некорректная строка ввода.");
                return;
            }

            try
            {
                string input = txtInput.Text;
                if (!int.TryParse(numIndex.Text, out int index))
                {
                    MessageBox.Show("Введите корректный индекс (целое число).");
                    return;
                }
                calculator.GetCharAtIndex(input, index);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении символа: " + ex.Message);
            }
            this.ActiveControl = null;
        }

        private void BtnLength_Click(object sender, EventArgs e)
        {
            if (!_userStringValid)
            {
                MessageBox.Show("Некорректная строка ввода.");
                return;
            }

            try
            {
                string input = txtInput.Text;
                calculator.GetLength(input);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при вычислении длины строки: " + ex.Message);
            }
            this.ActiveControl = null;
        }

        private void BtnCountVowels_Click(object sender, EventArgs e)
        {
            if (!_userStringValid)
            {
                MessageBox.Show("Некорректная строка ввода.");
                return;
            }

            try
            {
                string input = txtInput.Text;
                calculator.CountVowels(input);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подсчёте гласных: " + ex.Message);
            }
            this.ActiveControl = null;
        }

        private void BtnCountConsonants_Click(object sender, EventArgs e)
        {
            if (!_userStringValid)
            {
                MessageBox.Show("Некорректная строка ввода.");
                return;
            }

            try
            {
                string input = txtInput.Text;
                calculator.CountConsonants(input);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подсчёте согласных: " + ex.Message);
            }
            this.ActiveControl = null;
        }

        private void BtnCountSentences_Click(object sender, EventArgs e)
        {
            if (!_userStringValid)
            {
                MessageBox.Show("Некорректная строка ввода.");
                return;
            }

            try
            {
                string input = txtInput.Text;
                calculator.CountSentences(input);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подсчёте предложений: " + ex.Message);
            }
            this.ActiveControl = null;
        }

        private void BtnCountWords_Click(object sender, EventArgs e)
        {
            if (!_userStringValid)
            {
                MessageBox.Show("Некорректная строка ввода.");
                return;
            }

            try
            {
                string input = txtInput.Text;
                calculator.CountWords(input);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подсчёте слов: " + ex.Message);
            }
            this.ActiveControl = null;
        }
        #endregion
    }
}

