using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_08
{
    /// <summary>
    /// Класс содержит в себе методы проверки ввода
    /// </summary>
    class Input
    {
        #region Методы
        /// <summary>
        /// Проверяет правильность ввода переменной типа int
        /// </summary>
        /// <param name="min_select">Минимально возможное значение</param>
        /// <param name="max_select">Максимально возможное значение</param>
        /// <returns></returns>
        public int UserСhoiceInt(int min_select, int max_select)
        {
            bool input_value; // Проверка правильности ввода
            int select; // Выбор сделанный пользователем

            do // Пользователь делает выбор
            {
                input_value = int.TryParse(Console.ReadLine(), out select);
                if (select < min_select ^ select > max_select)
                {
                    Console.Write("Введите ещё раз: ");
                    input_value = false;
                }
                else if (!input_value) Console.Write("Введите ещё раз: ");
            }
            while (!input_value);
            return select;
        }

        /// <summary>
        /// Метод проверяет ввод на правильность 
        /// и возвращает либо "д" либо "н"
        /// </summary>
        /// <returns></returns>
        public char UserChoiceYesNo()
        {
            char yes_no; // Выбор пользователя

            do // Пользователь делает выбор
            {
                Console.Write("\nВведите н/д: ");
                yes_no = Console.ReadKey(false).KeyChar;
                yes_no = Char.ToLower(yes_no);
            }
            while (yes_no != 'н' && yes_no != 'д');
            Console.WriteLine();
            return yes_no;
        }

        /// <summary>
        /// Метод проверяет ввод даты
        /// </summary>
        /// <returns></returns>
        public DateTime UserChoiceDate()
        {
            bool input_value; // Проверка ввода
            DateTime date; // Результат ввода пользователя

            do // Ввод даты пользователем
            {
                Console.Write("Укажите дату в формате - чис.мес.год: ");
                input_value = DateTime.TryParse(Console.ReadLine(), out date);
            }
            while (!input_value);
            return date;
        }

        /// <summary>
        /// Метод проверяет вводимую строку, строка не может иметь пустое значение
        /// </summary>
        /// <returns></returns>
        public string UserChoiceString()
        {
            string result;
            do
            {
                result = Console.ReadLine();
                if (result.Length == 0) Console.Write("Введите ещё раз: ");
            }
            while (result.Length == 0);
            return result;

        }
        #endregion
    }

}
