using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace HomeWork_08
{
    struct Workers
    {
        #region Свойства
        /// <summary>
        /// Порядковый номер сотрудника
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string Last_Name { get; private set; }

        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string First_Name { get; private set; }

        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        public int Age { get; private set; }

        /// <summary>
        /// Название департамента в котором работает сотрудник
        /// </summary>
        public string Departament_Name { get; private set; }

        /// <summary>
        /// Заработная плата сотрудника
        /// </summary>
        public int Wage { get; private set; }
        #endregion

        #region Конструкторы
        /// <summary>
        /// Конструктор класса Workers
        /// </summary>
        /// <param name="number">Порядковый номер сотрудника</param>
        /// <param name="last_name">Фамилия</param>
        /// <param name="first_name">Имя</param>
        /// <param name="age">Возраст</param>
        /// <param name="departament_name">Название департамента в котором работает сотрудник</param>
        /// <param name="wage">Заработная плата</param>
        public Workers(int number, string last_name, string first_name, int age, string departament_name, int wage)
        {
            Number = number;
            Last_Name = last_name;
            First_Name = first_name;
            Age = age;
            Departament_Name = departament_name;
            Wage = wage;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Метод создаёт нового сотрудника
        /// </summary>
        /// <param name="list_workers">Коллекция которая хранит в себе сотрудников</param>
        /// <param name="departament">Департамент к которому относится сотруднки</param>
        /// <returns></returns>
        public List<Workers> Add(List<Workers> list_workers, string departament_name)
        {
            int number; // Порядковый номер сотрудника
            string last_name; // Фамилия сотрудника
            string first_name; // Имя сотрудника
            int age; // Возраст сотрудника
            int wage; // Заработная плата сотрудника
            Input input = new Input(); // Работа с вводом пользователя

            if (list_workers.Count == 0) number = 1;
            else number = list_workers[list_workers.Count - 1].Number + 1;

            Console.Write("Укажите фамилию сотрудника: ");
            last_name = input.UserChoiceString();
            Console.Clear();

            Console.Write("Укажите имя сотрудника: ");
            first_name = input.UserChoiceString();
            Console.Clear();

            Console.Write("Укажите возраст сотрудника (от 18 до 70): ");
            age = input.UserСhoiceInt(18, 70);
            Console.Clear();

            Console.Write("Укажите заработную плату сотрудника: ");
            wage = input.UserСhoiceInt(0, 100000);
            Console.Clear();

            Workers worker = new Workers(number, last_name, first_name, age, departament_name, wage);
            list_workers.Add(worker);
            Console.WriteLine($"Сотрудник {last_name} {first_name} успешно добавлен в департамент {departament_name}");
            Console.WriteLine("Нажмите на любую клавишу...");
            Console.ReadKey();
            return list_workers;
        }

        /// <summary>
        /// Метод создаёт сотрудников автоматичеки и прикрепляет их к департаментам
        /// </summary>
        /// <param name="departament_list">Коллекция которая хранит в себе департаменты</param>
        /// <returns></returns>
        public Workers AutoAdd(List<Department> departament_list, List<Workers> list_workers)
        {
            Random rand = new Random();// Генератор случайных чисел
            int number; // Порядковый номер сотрудника

            if (list_workers.Count == 0) number = 1;
            else number = list_workers[list_workers.Count - 1].Number + 1;

            Workers worker = new Workers
            {
                Number = number,
                Last_Name = $"Фамилия_{number}",
                First_Name = $"Имя_{number}",
                Age = rand.Next(18, 70),
                Departament_Name = departament_list[rand.Next(0, departament_list.Count - 1)].Name,
                Wage = rand.Next(1, 10) * 10000
            };
            return worker;
        }

        /// <summary>
        /// Метод удаляет сотрудников по номеру, департаменту или заработной плате
        /// </summary>
        /// <param name="list_workers">Коллекция с сотрудниками</param>
        /// <returns></returns>
        public List<Workers> Delete(List<Workers> list_workers, List<Department> list_departament, out List<Department> result_dep)
        {
            Input input = new Input(); // Работа с вводом пользователя
            int select; // Выбор пользователя
            bool flag = true; // Переменная управления циклом
            int delete_number; // Номер сотрудника для удаления
            int delete_count = 0; // Счётчик удалений
            string departament_delete; // Департамент для удаления
            int delete_wage; // Зарплата для удаления
            Department depar = new Department();

            Console.WriteLine("По какому полю Вы хотите произвести удаление?\n" +
                  "1. Номер     " +
                  "2. Департамент     " +
                  "3. Заработная плата");

            Console.Write("Ваш выбор: ");
            select = input.UserСhoiceInt(1, 3);

            switch (select)
            {
                case 1: // Удаление по порядковому номеру
                    #region
                    Console.Clear();
                    Console.Write("Укажите порядковый номер сотрудника, которого нужно удалить: ");
                    delete_number = input.UserСhoiceInt(1, list_workers[list_workers.Count - 1].Number);
                    Console.Clear();

                    do // Удаление сотрудника
                    {
                        flag = true;
                        foreach (var lw in list_workers)
                        {
                            if (lw.Number == delete_number)
                            {
                                list_departament = depar.EditCount(lw.Departament_Name, list_departament, false, 1);
                                list_workers.Remove(lw);
                                delete_count++;
                                break;
                            }
                        }
                    }
                    while (!flag);

                    if (delete_count == 0) Console.WriteLine("Сотрудника с таким порядковым номером не найдено!\n" +
                          "Нажмите любую клавишу...");


                    else Console.WriteLine("Сотрудник успешно удалён! " +
                                   "Нажмите любую клавишу...");

                    Console.ReadKey();
                    #endregion
                    break;

                case 2: // Удаление по департаменту
                    #region
                    Console.Clear();
                    Console.WriteLine("Напишите департамент и мы удалим всех сотрудников связанных с ним!");
                    Console.Write("Департамент: ");
                    departament_delete = input.UserChoiceString();
                    Console.Clear();

                    do // Удаление сотрудника
                    {
                        flag = true;
                        foreach (var lw in list_workers)
                        {
                            if (lw.Departament_Name.ToLower() == departament_delete.ToLower())
                            {
                                Console.WriteLine($"Сотрудник {lw.Last_Name} {lw.First_Name} удалён.");
                                list_departament = depar.EditCount(lw.Departament_Name, list_departament, false, 1);
                                list_workers.Remove(lw);
                                delete_count++;
                                flag = false;
                                break;
                            }
                        }
                    }
                    while (!flag);

                    if (delete_count == 0)
                        Console.WriteLine("Сотрудника работающего в таком департаменте не найдено!\n" +
                            "Нажмите любую клавишу...");

                    else Console.WriteLine("Нажмите на любую клавишу...");

                    Console.ReadKey();
                    #endregion
                    break;

                case 3: // Удаление по заработной плате
                    #region
                    Console.Clear();
                    Console.WriteLine("Укажите заработную плату и мы удалим всех сотрудников кто\n" +
                        "получает больше этого значения!");
                    Console.Write("Значение от 0 до 100000: ");
                    delete_wage = input.UserСhoiceInt(1, 100000);
                    Console.Clear();

                    do // Удаление сотрудника
                    {
                        flag = true;
                        foreach (var lw in list_workers)
                        {
                            if (lw.Wage > delete_wage)
                            {
                                Console.WriteLine($"Сотрудник {lw.Last_Name} {lw.First_Name} удалён.");
                                list_departament = depar.EditCount(lw.Departament_Name, list_departament, false, 1);
                                list_workers.Remove(lw);
                                delete_count++;
                                flag = false;
                                break;
                            }
                        }
                    }
                    while (!flag);

                    if (delete_count == 0)
                        Console.WriteLine("Сотрудников подходящих под требования не найдено!\n" +
                            "Нажмите любую клавишу...");

                    else Console.WriteLine("Нажмите на любую клавишу...");

                    Console.ReadKey();
                    #endregion
                    break;
            }
            result_dep = list_departament;
            return list_workers;
        }

        /// <summary>
        /// Метод меняет название департамента у сотрудника, сделан для удаления департамента
        /// </summary>
        /// <param name="departament_name">Имя департамента которое будет меняться</param>
        /// <param name="list_workers">Коллекция сотрудников</param>
        /// <returns></returns>
        public List<Workers> EditDepartamentName(string departament_name, string new_departament_name, List<Workers> list_workers)
        {
            for (int i = 0; i < list_workers.Count; i++)
            {
                if (list_workers[i].Departament_Name.ToLower() == departament_name.ToLower())
                    list_workers[i] = new Workers(list_workers[i].Number, list_workers[i].Last_Name, list_workers[i].First_Name,
                        list_workers[i].Age, new_departament_name, list_workers[i].Wage);
            }
            return list_workers;
        }

        /// <summary>
        /// Метод изменяет данные сотрудника
        /// </summary>
        /// <param name="list_workers">Коллекция сотрудников</param>
        /// <param name="departament_list">Коллекция департаментов</param>
        /// <param name="departament_result">Изменённая коллекция департаментов</param>
        /// <returns></returns>
        public List<Workers> Edit(List<Workers> list_workers, List<Department> departament_list, out List<Department> departament_result)
        {
            Department depar = new Department();
            Input input = new Input(); // Для работы с вводом пользователя
            int number_worker; // Номер сотрудника для поиска
            int search_count = 0; // Счётчик изменений
            int select = 1; // Выбор пользователя
            string last_name; // Новая фамилия сотрудника
            string first_name; // Новое имя сотрудника
            int age; // Новый возраст сотрудника
            int wage; // Новая зарплата
            string departament_name; // Новое имя департамента
            bool check = false;

            Console.Write("Укажите порядковый номер сотрудника, чьи данные Вы хотите изменить: ");
            number_worker = input.UserСhoiceInt(1, list_workers[list_workers.Count - 1].Number);

            for (int i = 0; i < list_workers.Count; i++)
            {
                if (list_workers[i].Number == number_worker)
                {
                    while (select != 4)
                    {
                        Console.Clear();
                        Console.WriteLine("Какое поле будем редактировать?\n" +
                                          "1. Фамилия/Имя/Возраст     " +
                                          "2. Зарплата     " +
                                          "3. Департамент     " +
                                          "4. Выход в главное меню\n");
                        Console.Write("Ваш выбор: ");
                        select = input.UserСhoiceInt(1, 4);

                        switch (select)
                        {
                            case 1: // Изменить личные данные сотрудника
                                #region
                                Console.Clear();

                                Console.Write("Укажите фамилию сотрудника: ");
                                last_name = input.UserChoiceString();
                                Console.Clear();

                                Console.Write("Укажите имя сотрудника: ");
                                first_name = input.UserChoiceString();
                                Console.Clear();

                                Console.Write("Укажите возраст сотрудника (от 18 до 70): ");
                                age = input.UserСhoiceInt(18, 70);

                                list_workers[i] = new Workers(list_workers[i].Number, last_name, first_name, age,
                                    list_workers[i].Departament_Name, list_workers[i].Wage);
                                #endregion
                                break;

                            case 2: // Изменить зарплату сотрудника
                                #region
                                Console.Clear();
                                Console.Write("Укажите заработную плату сотрудника: ");
                                wage = input.UserСhoiceInt(0, 100000);

                                list_workers[i] = new Workers(list_workers[i].Number, list_workers[i].Last_Name,
                                    list_workers[i].First_Name, list_workers[i].Age, list_workers[i].Departament_Name, wage);
                                #endregion
                                break;

                            case 3: // Изменить департамент
                                #region
                                Console.Clear();
                                Console.Write("Укажите новое имя департамента: ");

                                do
                                {
                                    departament_name = input.UserChoiceString();
                                    check = depar.HaveDepartament(departament_name, departament_list);

                                    if (!check)
                                    {
                                        departament_list = depar.EditCount(list_workers[i].Departament_Name, departament_list, false, 1);
                                        departament_list = depar.EditCount(departament_name, departament_list, true, 1);
                                        list_workers[i] = new Workers(list_workers[i].Number, list_workers[i].Last_Name,
                                            list_workers[i].First_Name, list_workers[i].Age, departament_name, list_workers[i].Wage);
                                    }

                                    else Console.Write("Такого департамента нет! Введите ещё раз: ");
                                }
                                while (check);
                                #endregion
                                break;
                        }

                    }
                }
            }
            departament_result = departament_list;
            return list_workers;
        }

        /// <summary>
        /// Метод отображает информацию о сотруднике
        /// </summary>
        public void Show()
        {
            Console.WriteLine();
            Console.Write(Number); Console.SetCursorPosition(5, Console.CursorTop);
            Console.Write(Last_Name); Console.SetCursorPosition(20, Console.CursorTop);
            Console.Write(First_Name); Console.SetCursorPosition(30, Console.CursorTop);
            Console.Write(Age); Console.SetCursorPosition(37, Console.CursorTop);
            Console.Write(Departament_Name); Console.SetCursorPosition(52, Console.CursorTop);
            Console.Write(Wage);
        }

        /// <summary>
        /// Метод сериализирует сотрудников по средствам XML
        /// </summary>
        /// <param name="workers_list">Коллекция сотрудников, которая будет сриализирована</param>
        public void SerializationXML(List<Workers> workers_list)
        {
            Input input = new Input(); // Раюота с вводом пользователя

            Console.Write("Укажите путь к файлу, для сохранения списка сотрудников: ");
            string path = input.UserChoiceString();

            XElement workers = new XElement("workers"); // Корневой элемент (Условно LIST)
            XDocument xdoc = new XDocument(); // Работа с файлом

            for (int i = 0; i < workers_list.Count; i++)
            {
                XElement worker = new XElement("worker"); // Создаём элемент (экземпляр класса)

                // создаём атрибуты (поля класса)
                XAttribute number = new XAttribute("number", workers_list[i].Number);
                XAttribute last_name = new XAttribute("last_name", workers_list[i].Last_Name);
                XAttribute first_name = new XAttribute("first_name", workers_list[i].First_Name);
                XAttribute age = new XAttribute("age", workers_list[i].Age);
                XAttribute departament_name = new XAttribute("departament_name", workers_list[i].Departament_Name);
                XAttribute wage = new XAttribute("wage", workers_list[i].Wage);

                // Добавляем атрибуты к элементу (формируем класс в XML файле)
                worker.Add(number);
                worker.Add(last_name);
                worker.Add(first_name);
                worker.Add(age);
                worker.Add(departament_name);
                worker.Add(wage);

                workers.Add(worker); // Добавление элемента в корневой элемент
            }
            xdoc.Add(workers);
            xdoc.Save(path);
        }

        /// <summary>
        /// Метод десериализирует список сотрудников по средствам XML
        /// </summary>
        /// <returns></returns>
        public List<Workers> DeSerializationXML()
        {
            Input input = new Input(); // Работа с вводом пользователя

            Console.Write("Укажите путь к файлу, для загрузки списка сотрудников: ");
            string path = input.UserChoiceString();

            List<Workers> result = new List<Workers>();
            XDocument xdoc = XDocument.Load(path); // Загружаем файл

            foreach (var xe in xdoc.Element("workers").Elements("worker")) // Перебераем все элементы
            {
                XAttribute number = xe.Attribute("number");
                XAttribute last_name = xe.Attribute("last_name");
                XAttribute first_name = xe.Attribute("first_name");
                XAttribute age = xe.Attribute("age");
                XAttribute departament_name = xe.Attribute("departament_name");
                XAttribute wage = xe.Attribute("wage");

                if (number != null)
                    result.Add(new Workers(int.Parse(number.Value), last_name.Value, first_name.Value,
                        int.Parse(age.Value), departament_name.Value, int.Parse(wage.Value)));
            }
            return result;
        }

        /// <summary>
        /// Метод сериализирует сотрудников по средствам JSON
        /// </summary>
        /// <param name="workers_list">Коллекция сотрудников</param>
        public void SerializationJSON(List<Workers> workers_list)
        {
            Input input = new Input(); // Раюота с вводом пользователя

            Console.Write("Укажите путь к файлу, для сохранения списка сотрудников: ");
            string path = input.UserChoiceString();

            string json = JsonConvert.SerializeObject(workers_list);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Метод десериализирует список сотрудников по средствам JSON
        /// </summary>
        /// <returns></returns>
        public List<Workers> DeSerializationJSON()
        {
            Input input = new Input(); // Работа с вводом пользователя
            List<Workers> result = new List<Workers>();

            Console.Write("Укажите путь к файлу, для загрузки списка сотрудников: ");
            string path = input.UserChoiceString();

            string json = File.ReadAllText(path);
            result = JsonConvert.DeserializeObject<List<Workers>>(json);
            return result;
        }
        #endregion
    }
}
