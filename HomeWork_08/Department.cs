using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace HomeWork_08
{
    struct Department
    {
        #region Свойства
        /// <summary>
        /// Название департамента
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Дата создания департамента
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Колличество сотрудников в департаменте
        /// </summary>
        public int Count { get; private set; }
        #endregion

        #region Конструкторы
        /// <summary>
        /// Конструктор класса Depsrtament
        /// </summary>
        /// <param name="n">Название департамента</param>
        /// <param name="d">Дата создания департамента</param>
        public Department (string n, DateTime d) { Name = n; Date = d; Count = 0;}

        /// <summary>
        /// Конструктор класса Depsrtament
        /// </summary>
        /// <param name="n">Название департамента</param>
        /// <param name="d">Дата создания департамента</param>
        /// <param name="c">Колличество сотрудников</param>
        public Department(string n, DateTime d, int c) { Name = n; Date = d; Count = c; }
        #endregion

        #region Методы
        /// <summary>
        /// Метод создаёт новый департамент
        /// </summary>
        /// <returns></returns>
        public List<Department> Add(List<Department> list_departament)
        {
            string name; // Название департамента
            DateTime date; // Дата создания департамента
            Char yes_no; // Ответ пользователя
            Input input = new Input(); // Для работы с вводом пользователя
            bool check = false; // Проверить создан ли такой же департамент

            Console.WriteLine("Давайте создадим департамент!");
            Console.Write("Укажите название департамента: ");

            do // Проверить создан ли такой департамент
            {
                name = input.UserChoiceString();
                check = HaveDepartament(name, list_departament);
                if (!check) Console.Write("Такой департамент существует! Введите ещё раз: ");
            }
            while (!check);

            Console.Write("\nЖелаете указать дату создания департамента? (В случае отказа поле заполнится автоматически)");
            yes_no = input.UserChoiceYesNo();
            if (yes_no == 'д') date = input.UserChoiceDate();
            else date = DateTime.Now;

            list_departament.Add(new Department(name, date));
            Console.WriteLine($"\nДепартамент {name} успешно создан! Нажмите на любую клавишу...");
            Console.ReadKey();
            return list_departament;
        }

        /// <summary>
        /// Метод проверяет существует ли отдел
        /// </summary>
        /// <param name="departament_name">Отдел который нужно найти</param>
        /// <param name="list_departament">Коллекция департаментов</param>
        /// <returns></returns>
        public bool HaveDepartament(string departament_name, List<Department> list_departament)
        {
            bool result = true;
            for (int i = 0; i < list_departament.Count; i++)
                if (departament_name.ToLower() == list_departament[i].Name.ToLower()) result = false;
            return result;
        }

        /// <summary>
        /// Метод изменяет Св-ва Count 
        /// </summary>
        /// <param name="departament_name">Название департамента которое нужно изменить</param>
        /// <param name="list_departament">Коллекция департаментов</param>
        /// <param name="action">Если true - то Count+, если False - то Count-</param>
        /// <param name="count">На сколько Count увеличивается или уменьшается</param>
        /// <returns></returns>
        public List<Department> EditCount(string departament_name, List<Department> list_departament, bool action, int count)
        {
            for (int i = 0; i<list_departament.Count; i++)
            {
                if (departament_name.ToLower() == list_departament[i].Name.ToLower() && action)
                    list_departament[i] = new Department(list_departament[i].Name, list_departament[i].Date,
                        list_departament[i].Count + count);

                else if(departament_name.ToLower() == list_departament[i].Name.ToLower() && !action)
                    list_departament[i] = new Department(list_departament[i].Name, list_departament[i].Date,
                       list_departament[i].Count - count);
            }

            return list_departament;
        }

        /// <summary>
        /// Метод генерирует департаменты
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Department> AutoAdd(List<Department> departament_list)
        {
            List<Department> result = new List<Department>(); 
            Random rand = new Random();
            int count; // Колличество департаментов 
            Input input = new Input(); // Для работы с вводом пользователя

            Console.Write("Сколько департаментов нужно создать?: ");
            count = input.UserСhoiceInt(1, 1000);

            for (int i = 0; i < count; i++) // Создаём департамент
            {
                Department dt = new Department
                {
                    Name = $"Отдел_{1 + departament_list.Count}",
                    Date = new DateTime(2021, rand.Next(1, 12), rand.Next(1, 29))
                };
                departament_list.Add(dt);
            }

            Console.WriteLine($"Создано {count} департаментов. Нажмите на любую клавишу...");
            Console.ReadKey();
            return departament_list;
        }

        /// <summary>
        /// Метод удаляет депортаменты по названию, либо по дате создания
        /// </summary>
        /// <param name="departament_list"></param>
        /// <returns></returns>
        public List<Department> Delete(List<Department> departament_list, List<Workers> list_workers, out List<Workers> workers_result)
        {
            int select; // Выбор пользователя
            Input input = new Input(); // Методы работы с вводом пользователя
            string delete_name; // Название которое будем удалять
            bool flag = true; // Переменная управления циклом
            int delete_count = 0; // Подсчёт удалений
            DateTime delete_date; // Дата для удаления
            Workers work = new Workers();

            Console.WriteLine("По какому полю Вы хотите произвести удаление?\n" +
                              "1. Название     " +
                              "2. Дата создания     ");

            Console.Write("Ваш выбор: ");
            select = input.UserСhoiceInt(1, 3); // Пользователь делает выбор

            switch (select)
            {
                case 1: // Удаление по названию
                    #region 
                    Console.Clear();
                    Console.WriteLine("Напишите название департамента, которого Вы хотите удалить.");
                    delete_name = input.UserChoiceString().ToLower();
                    Console.Clear();

                    do // Поиск и удаление департамента
                    {
                        flag = true;
                        foreach (var dl in departament_list)
                        {
                            if (dl.Name.ToLower() == delete_name.ToLower())
                            {
                                list_workers = work.EditDepartamentName(dl.Name, "Беспризорник!", list_workers);
                                Console.WriteLine($"{dl.Name} - удалён.");
                                departament_list.Remove(dl);
                                delete_count++;
                                break;
                            }
                        }
                    }
                    while (!flag);

                    if (delete_count == 0) Console.WriteLine("Департамента с таким названием не найдено!\n" +
                                                             "Нажмите любую клавишую...");
                    
                    else Console.WriteLine("Нажмите на любую клавишу...");

                    Console.ReadKey();
                    #endregion
                    break;

                case 2: // Удаление по дате создания
                    #region
                    Console.Clear();
                    delete_date = input.UserChoiceDate();
                    Console.Clear();

                    do // Поиск и удаление департамента
                    {
                        flag = true;
                        foreach (var dl in departament_list)
                        {
                            if (dl.Date.Year == delete_date.Year && dl.Date.Month == delete_date.Month && dl.Date.Day == delete_date.Day)
                            {
                                list_workers = work.EditDepartamentName(dl.Name, "Беспризорник!", list_workers);
                                Console.WriteLine($"{dl.Name} - удалён.");
                                departament_list.Remove(dl);
                                delete_count++;
                                break;
                            }
                        }
                    }

                    while (!flag);

                    if (delete_count == 0) Console.WriteLine("Департамента с такой датой создания не найдено!\n" +
                                                             "Нажмите любую клавишую...");

                    else Console.WriteLine("Нажмите на любую клавишу...");

                    Console.ReadKey();
                    #endregion
                    break;
            }
            workers_result = list_workers;
            return departament_list;
        }

        /// <summary>
        /// Метод изменяет название департамента
        /// </summary>
        /// <param name="departament_list"></param>
        /// <returns></returns>
        public List<Department> EditName(List<Department> departament_list, List<Workers> list_workers, out List<Workers> workers_result)
        {
            Department edit_departament = new Department();
            string search_name; // Название которое будем искать
            string edit_name; // Новое название
            int count = 0; // Считает было ли сделанно редактирование
            Input input = new Input(); // Для работы с вводом пользователя
            Workers work = new Workers();

            Console.WriteLine("Введите название департамента, которое нужно поменять: ");
            search_name = input.UserChoiceString().ToLower();

            for (int i = 0; i < departament_list.Count; i++)
            {
                if (departament_list[i].Name.ToLower() == search_name)
                {
                    Console.Write("Напишите новое название: ");
                    edit_name = input.UserChoiceString();
                    list_workers = work.EditDepartamentName(departament_list[i].Name, edit_name, list_workers);
                    edit_departament = new Department(edit_name, departament_list[i].Date, departament_list[i].Count);
                    departament_list[i] = edit_departament;
                    count++;
                }
            }

            if (count == 0) Console.WriteLine("Департамента с таким названием не найдено!\n" +
                                              "Нажмите на любую клавишу...");

            else Console.WriteLine("Название успешно изменено! Нажмите на любую клавишу...");

            Console.ReadKey();

            workers_result = list_workers;
            return departament_list;
        }

        /// <summary>
        /// Метод показывает информацию о департаменте
        /// </summary>
        public void Show()
        {
            Console.WriteLine();
            Console.Write(Name); Console.SetCursorPosition(15, Console.CursorTop);
            Console.Write(Date.ToShortDateString()); Console.SetCursorPosition(34, Console.CursorTop);
            Console.Write(Count);
        }

        /// <summary>
        /// Метод сериализирует список департаментов по средствам XML
        /// </summary>
        /// <param name="departament_list">Коллекция департаментов для сериализации</param>
        public void SerializationXML(List<Department> departament_list)
        {
            Input input = new Input(); // Работа с вводом пользователя

                    Console.Write("Укажите путь к файлу, для сохранения списка департаментов: ");
                    string path = input.UserChoiceString();

                    XDocument xdoc = new XDocument(); // Работа с файлом
                    XElement departaments = new XElement("departaments"); // корневой элемент (условно List)

                    for (int i = 0; i < departament_list.Count; i++)
                    {
                        XElement departament = new XElement("departament"); // Создаём элемент (экземпляр класса)

                        // Создаём атрибуты (Поля класса)
                        XAttribute name = new XAttribute("name", departament_list[i].Name);
                        XAttribute date = new XAttribute("date", departament_list[i].Date);
                        XAttribute count = new XAttribute("count", departament_list[i].Count);

                        // Добовляем атрибуты к элементу (Формируем объект)
                        departament.Add(name);
                        departament.Add(date);
                        departament.Add(count);

                        departaments.Add(departament); // Добавляем в корневой элемент
                    }
                    xdoc.Add(departaments);
                    xdoc.Save(path); // Записываем в файл
        }

        /// <summary>
        /// Метод десиреализирует список департаментов по средствам XML
        /// </summary>
        /// <returns></returns>
        public List<Department> DeSerializationXML()
        {
            Input input = new Input(); // Работа с вводом пользователя
            List<Department> result = new List<Department>();
            bool flag = false;
            XDocument xdoc = new XDocument();

            while (!flag)
            {
                try
                {
                    Console.Write("Укажите путь к файлу, для загрузки списка департаметов: ");
                    string path = input.UserChoiceString();

                    xdoc = XDocument.Load(path); // Загружаем файл
                    flag = true;
                }
                catch
                {
                    Console.WriteLine("Что то пошло не так...");
                }
            }

            foreach (var xe in xdoc.Element("departaments").Elements("departament")) // Перебераем все элементы
            {
                XAttribute name = xe.Attribute("name");
                XAttribute date = xe.Attribute("date");
                XAttribute count = xe.Attribute("count");

                if (name.Value != null)
                    result.Add(new Department(name.Value, DateTime.Parse(date.Value), int.Parse(count.Value)));
            }
            return result;
        }

        /// <summary>
        /// Метод сериализирует список департаментов по средствам JSON
        /// </summary>
        /// <param name="departament_list">Коллекция департаментов</param>
        public void SerializationJSON(List<Department> departament_list)
        {
            Input input = new Input(); // Работа с вводом пользователя

            Console.Write("Укажите путь к файлу, для сохранения списка департаментов: ");
            string path = input.UserChoiceString();

            string json = JsonConvert.SerializeObject(departament_list);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Метод десиреализирует список департаментов по средствам JSON
        /// </summary>
        /// <returns></returns>
        public List<Department> DeSerializationJSON()
        {
            Input input = new Input(); // Работа с вводом пользователя
            List<Department> result = new List<Department>();
            bool flag = false;

            while (!flag)
            {
                try
                {
                    Console.Write("Укажите путь к файлу, для загрузки списка департаметов: ");
                    string path = input.UserChoiceString();

                    string json = File.ReadAllText(path);
                    result = JsonConvert.DeserializeObject<List<Department>>(json);
                    flag = true;
                }
                catch
                {
                    Console.WriteLine("Что то пошло не так...");
                }
            }
            return result;
        }
        #endregion
    }
}
