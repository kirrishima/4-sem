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

        private Size ActualSize => new Size(this.Width - this.Padding.Right - this.Padding.Left,
         this.Height - this.Padding.Top - this.Padding.Bottom);

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // Инициализация элементов, задание размеров, расположения и т.д.
            this.txtInput = new TextBox() { Width = 300, Top = 10, Left = 10 };
            this.txtOldSubstring = new TextBox() { Width = 100, Top = 40, Left = 10, Text = "Старый текст" };
            this.txtNewSubstring = new TextBox() { Width = 100, Top = 40, Left = 120, Text = "Новый текст" };
            this.txtIndex = new TextBox() { Width = 50, Top = 70, Left = 10, Text = "Индекс" };
            this.lblResult = new Label() { Width = 300, Top = 200, Left = 10, BorderStyle = BorderStyle.FixedSingle };

            // Кнопки для операций
            this.btnReplace = new Button() { Text = "Замена", Top = 100, Left = 10 };
            this.btnDelete = new Button() { Text = "Удаление", Top = 100, Left = 100 };
            this.btnGetChar = new Button() { Text = "Символ по индексу", Top = 100, Left = 190 };
            this.btnLength = new Button() { Text = "Длина строки", Top = 140, Left = 10 };
            this.btnCountVowels = new Button() { Text = "Гласные", Top = 140, Left = 100 };
            this.btnCountConsonants = new Button() { Text = "Согласные", Top = 140, Left = 190 };
            this.btnCountSentences = new Button() { Text = "Предложения", Top = 180, Left = 10 };
            this.btnCountWords = new Button() { Text = "Слова", Top = 180, Left = 100 };

            // Привязка событий к обработчикам
            this.btnReplace.Click += BtnReplace_Click;
            this.btnDelete.Click += BtnDelete_Click;
            this.btnGetChar.Click += BtnGetChar_Click;
            this.btnLength.Click += BtnLength_Click;
            this.btnCountVowels.Click += BtnCountVowels_Click;
            this.btnCountConsonants.Click += BtnCountConsonants_Click;
            this.btnCountSentences.Click += BtnCountSentences_Click;
            this.btnCountWords.Click += BtnCountWords_Click;

            // Добавляем элементы на форму
            this.Controls.Add(txtInput);
            this.Controls.Add(txtOldSubstring);
            this.Controls.Add(txtNewSubstring);
            this.Controls.Add(txtIndex);
            this.Controls.Add(lblResult);
            this.Controls.Add(btnReplace);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnGetChar);
            this.Controls.Add(btnLength);
            this.Controls.Add(btnCountVowels);
            this.Controls.Add(btnCountConsonants);
            this.Controls.Add(btnCountSentences);
            this.Controls.Add(btnCountWords);

            // Настройка параметров формы
            this.Text = "Текстовый калькулятор";
            this.Width = 350;
            this.Height = 300;
        }

        private void Form1_SizeChanged(object sender, System.EventArgs e)
        {

        }

        #endregion

        private System.Windows.Forms.Label Header;
        private System.Windows.Forms.TextBox userInputTextBox;
    }
}

