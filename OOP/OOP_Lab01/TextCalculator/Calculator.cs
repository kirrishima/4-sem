using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextCalculator
{
    public delegate void OperationPerformedEventHandler(object sender, OperationEventArgs e);

    public class OperationEventArgs : EventArgs
    {
        public string Result { get; }

        public OperationEventArgs(string result)
        {
            Result = result;
        }
    }

    public class Calculator
    {
        public event OperationPerformedEventHandler OperationCompleted;

        protected virtual void OnOperationCompleted(string result)
        {
            OperationCompleted?.Invoke(this, new OperationEventArgs(result));
        }

        public void ReplaceSubstring(string input, string oldSub, string newSub)
        {
            try
            {
                if (input == null)
                    throw new ArgumentNullException(nameof(input), "Входная строка не может быть null.");

                string result = input.Replace(oldSub, newSub);
                OnOperationCompleted("Результат замены: " + result);
            }
            catch (Exception ex)
            {
                OnOperationCompleted("Ошибка в методе ReplaceSubstring: " + ex.Message);
            }
        }

        public void DeleteSubstring(string input, string subToDelete)
        {
            try
            {
                if (input == null)
                    throw new ArgumentNullException(nameof(input), "Входная строка не может быть null.");

                string result = input.Replace(subToDelete, "");
                OnOperationCompleted("Результат удаления: " + result);
            }
            catch (Exception ex)
            {
                OnOperationCompleted("Ошибка в методе DeleteSubstring: " + ex.Message);
            }
        }

        public void GetCharAtIndex(string input, int index)
        {
            try
            {
                if (input == null)
                    throw new ArgumentNullException(nameof(input), "Входная строка не может быть null.");

                if (index < 0 || index >= input.Length)
                    throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона.");

                char result = input[index];
                OnOperationCompleted($"Символ по индексу {index}: {result}");
            }
            catch (Exception ex)
            {
                OnOperationCompleted("Ошибка в методе GetCharAtIndex: " + ex.Message);
            }
        }

        public void GetLength(string input)
        {
            try
            {
                if (input == null)
                    throw new ArgumentNullException(nameof(input), "Входная строка не может быть null.");

                int length = input.Length;
                OnOperationCompleted("Длина строки: " + length);
            }
            catch (Exception ex)
            {
                OnOperationCompleted("Ошибка в методе GetLength: " + ex.Message);
            }
        }

        public void CountVowels(string input)
        {
            try
            {
                if (input == null)
                    throw new ArgumentNullException(nameof(input), "Входная строка не может быть null.");

                int count = input.ToLower().Count(c => "aeiouаоуыияеэю".Contains(c));
                OnOperationCompleted("Количество гласных: " + count);
            }
            catch (Exception ex)
            {
                OnOperationCompleted("Ошибка в методе CountVowels: " + ex.Message);
            }
        }

        public void CountConsonants(string input)
        {
            try
            {
                if (input == null)
                    throw new ArgumentNullException(nameof(input), "Входная строка не может быть null.");

                int count = input.ToLower().Count(c => char.IsLetter(c) && !"aeiouаоуыияеэю".Contains(c));
                OnOperationCompleted("Количество согласных: " + count);
            }
            catch (Exception ex)
            {
                OnOperationCompleted("Ошибка в методе CountConsonants: " + ex.Message);
            }
        }

        public void CountSentences(string input)
        {
            try
            {
                if (input == null)
                    throw new ArgumentNullException(nameof(input), "Входная строка не может быть null.");

                int count = input.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
                OnOperationCompleted("Количество предложений: " + count);
            }
            catch (Exception ex)
            {
                OnOperationCompleted("Ошибка в методе CountSentences: " + ex.Message);
            }
        }

        public void CountWords(string input)
        {
            try
            {
                if (input == null)
                    throw new ArgumentNullException(nameof(input), "Входная строка не может быть null.");

                int count = input.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
                OnOperationCompleted("Количество слов: " + count);
            }
            catch (Exception ex)
            {
                OnOperationCompleted("Ошибка в методе CountWords: " + ex.Message);
            }
        }
    }
}
