using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextCalculator
{
    public partial class Form1 : Form
    {

        // Делегат для уведомления о выполненной операции


        private Calculator calculator;

        // Элементы управления (создаются через Designer или программно)
        private TextBox txtInput;
        private TextBox txtOldSubstring;
        private TextBox txtNewSubstring;
        private TextBox txtIndex;
        private Label lblResult;
        private Button btnReplace;
        private Button btnDelete;
        private Button btnGetChar;
        private Button btnLength;
        private Button btnCountVowels;
        private Button btnCountConsonants;
        private Button btnCountSentences;
        private Button btnCountWords;

        public Form1()
        {
            InitializeComponent();
            calculator = new Calculator();
            // Подписка на событие выполнения операции
            calculator.OperationCompleted += Calculator_OperationCompleted;
        }

        private void Header_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form1_SizeChanged(sender, e);
        }

        // Обработчик события, получающего результаты из Calculator
        private void Calculator_OperationCompleted(object sender, OperationEventArgs e)
        {
            lblResult.Text = e.Result;
        }

        // Обработчики кликов по кнопкам
        private void BtnReplace_Click(object sender, EventArgs e)
        {
            // Тестовый блок с try-catch для проверки ввода (пример позитивного/негативного тестирования)
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
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txtInput.Text;
                // Здесь можно использовать, например, значение из txtOldSubstring для удаления
                calculator.DeleteSubstring(input, txtOldSubstring.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении подстроки: " + ex.Message);
            }
        }

        private void BtnGetChar_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txtInput.Text;
                if (!int.TryParse(txtIndex.Text, out int index))
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
        }

        private void BtnLength_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txtInput.Text;
                calculator.GetLength(input);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при вычислении длины строки: " + ex.Message);
            }
        }

        private void BtnCountVowels_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txtInput.Text;
                calculator.CountVowels(input);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подсчёте гласных: " + ex.Message);
            }
        }

        private void BtnCountConsonants_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txtInput.Text;
                calculator.CountConsonants(input);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подсчёте согласных: " + ex.Message);
            }
        }

        private void BtnCountSentences_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txtInput.Text;
                calculator.CountSentences(input);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подсчёте предложений: " + ex.Message);
            }
        }

        private void BtnCountWords_Click(object sender, EventArgs e)
        {
            try
            {
                string input = txtInput.Text;
                calculator.CountWords(input);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подсчёте слов: " + ex.Message);
            }
        }
    }
}
