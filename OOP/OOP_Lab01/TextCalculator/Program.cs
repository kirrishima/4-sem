using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextCalculator
{
    // Класс Calculator, реализующий все операции над строкой

    public delegate void OperationPerformedEventHandler(object sender, OperationEventArgs e);

    // Класс для передачи результатов операции
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
        // Событие, вызываемое по завершении операции
        public event OperationPerformedEventHandler OperationCompleted;

        // Вспомогательный метод для вызова события
        protected virtual void OnOperationCompleted(string result)
        {
            OperationCompleted?.Invoke(this, new OperationEventArgs(result));
        }

        // Замена подстроки на другую подстроку
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
            finally
            {
                // Можно добавить логирование или освобождение ресурсов
            }
        }

        // Удаление заданной подстроки (символов)
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
            finally
            {
                // Завершающие действия
            }
        }

        // Получение символа по индексу
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
            finally
            {
                // Дополнительные действия, если необходимо
            }
        }

        // Определение длины строки
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
            finally
            {
                // Финальные действия
            }
        }

        // Подсчёт количества гласных
        public void CountVowels(string input)
        {
            try
            {
                if (input == null)
                    throw new ArgumentNullException(nameof(input), "Входная строка не может быть null.");

                int count = input.Count(c => "aeiouAEIOU".Contains(c));
                OnOperationCompleted("Количество гласных: " + count);
            }
            catch (Exception ex)
            {
                OnOperationCompleted("Ошибка в методе CountVowels: " + ex.Message);
            }
            finally
            {
                // Дополнительные действия
            }
        }

        // Подсчёт количества согласных
        public void CountConsonants(string input)
        {
            try
            {
                if (input == null)
                    throw new ArgumentNullException(nameof(input), "Входная строка не может быть null.");

                int count = input.Count(c => char.IsLetter(c) && !"aeiouAEIOU".Contains(c));
                OnOperationCompleted("Количество согласных: " + count);
            }
            catch (Exception ex)
            {
                OnOperationCompleted("Ошибка в методе CountConsonants: " + ex.Message);
            }
            finally
            {
                // Финальные действия
            }
        }

        // Подсчёт количества предложений (предполагается, что предложения заканчиваются символами . ! ?)
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
            finally
            {
                // Завершающие действия
            }
        }

        // Подсчёт количества слов
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
            finally
            {
                // Финальные действия
            }
        }
    }

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
