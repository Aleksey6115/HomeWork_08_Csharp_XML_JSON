using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HomeWork_08
{
    class Menu
    {
        /// <summary>
        /// Метод запускает основные части программы
        /// </summary>
        public void Start()
        {
            #region Необходимые переменные
            List<Department> departament_list = new List<Department>();
            List<Workers> workers_list = new List<Workers>();
            int select = 1;
            Input input = new Input();
            Department departament = new Department();
            Workers workers = new Workers();
            int select_case = 1;
            string departament_name;
            bool check = false;
            int workers_count;
            Workers workeradd = new Workers();
            #endregion

            while (select != 5)
            {
                Console.Clear();
                Console.WriteLine("Выберите то, что Вы хотите сделать.\n" +
                    "1. Работа со списком департаментов      " +
                    "2. Работа со списком сотрудников\n" +
                    "3. Сохранить БД     " +
                    "4. Загрузить БД     " +
                    "5. Закрыть программу");

                Console.Write("Ваш выбор: ");
                select = input.UserСhoiceInt(1, 5);

                switch (select)
                {
                    case 1: // Работа со списком департаментов
                        #region
                        select_case = 1;
                        while (select_case != 5)
                        {
                            Console.Clear();
                            Console.WriteLine("Какую операцию Вы хотите выполнить?\n" +
                                "1. Создать департамент                " +
                                "2. Удалить департамент     \n" +
                                "3. Изменить название департамента     " +
                                "4. Просмотреть список департаментов\n" +
                                "5. Выход в главное меню");

                            Console.Write("Ваш выбор: ");
                            select_case = input.UserСhoiceInt(1, 5);

                            switch (select_case)
                            {
                                case 1: //Создать департамент
                                    #region
                                    Console.Clear();
                                    Console.WriteLine("Каким способом Вы хотите создать департамент?\n" +
                                        "1. Вручную     2.Автоматически");

                                    Console.Write("Ваш выбор: ");
                                    select_case = input.UserСhoiceInt(1, 2);

                                    switch (select_case)
                                    {
                                        case 1: // Создать вручную
                                            Console.Clear();
                                            departament_list = departament.Add(departament_list);
                                            break;

                                        case 2: // Создать автоматически
                                            Console.Clear();
                                            departament_list = departament.AutoAdd(departament_list);
                                            break;
                                    }
                                    #endregion
                                    break;

                                case 2: //Удалить департамент
                                    #region
                                    Console.Clear();

                                    if (departament_list.Count == 0)
                                    {
                                        Console.WriteLine("Список департамнтов пуст! Нажмите на любую клавишу...");
                                        Console.ReadKey();
                                    }

                                    else departament_list = departament.Delete(departament_list, workers_list, out workers_list);
                                    #endregion
                                    break;

                                case 3: //Изменить название департамента
                                    #region
                                    Console.Clear();

                                    if (departament_list.Count == 0)
                                    {
                                        Console.WriteLine("Список департамнтов пуст! Нажмите на любую клавишу...");
                                        Console.ReadKey();
                                    }

                                    else departament_list = departament.EditName(departament_list, workers_list, out workers_list);
                                    #endregion
                                    break;

                                case 4: //Просмотреть список департаментов
                                    #region
                                    Console.Clear();

                                    if (departament_list.Count == 0)
                                    {
                                        Console.WriteLine("Список департамнтов пуст! Нажмите на любую клавишу...");
                                        Console.ReadKey();
                                    }

                                    else
                                    {
                                        Console.WriteLine("Выберите способ просмотра списка департаментов.\n" +
                                            "1. Просмотреть весь список     " +
                                            "2. Отсортировать по дате");

                                        Console.Write("Ваш выбор: ");
                                        select_case = input.UserСhoiceInt(1, 2);

                                        switch (select_case)
                                        {
                                            case 1: // Просмотреть весь список
                                                Console.Clear();
                                                Console.WriteLine("Название          Дата     Кол-во сотрудников");
                                                foreach (var dl in departament_list)
                                                    dl.Show();
                                                Console.WriteLine("\nНажмите любую клавишу...");
                                                Console.ReadKey();
                                                break;

                                            case 2: // Отсортировать по дате
                                                Console.Clear();
                                                Console.WriteLine("Название          Дата     Кол-во сотрудников");

                                                var sort_departament_list = from dl in departament_list
                                                                            orderby dl.Date
                                                                            select dl;

                                                foreach (var sdl in sort_departament_list) sdl.Show();
                                                Console.WriteLine("\nНажмите любую клавишу...");
                                                Console.ReadKey();
                                                break;
                                        }
                                    }
                                    #endregion
                                    break;
                            }
                        }
                        #endregion
                        break;

                    case 2: // Работа со списком сотрудников
                        #region
                        if (departament_list.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Сначала нужно создать департаменты! Нажмите на любую клавишу...");
                            Console.ReadKey();
                        }

                        else
                        {
                            select_case = 1;
                            while (select_case != 5)
                            {
                                Console.Clear();
                                Console.WriteLine("Какую операцию Вы хотите выполнить?\n" +
                                    "1. Создать сотрудника           " +
                                    "2. Удалить сотрудника     \n" +
                                    "3. Редактировать сотрудника     " +
                                    "4. Просмотреть список сотрудников\n" +
                                    "5. Выход в главное меню");

                                Console.Write("Ваш выбор: ");
                                select_case = input.UserСhoiceInt(1, 5);

                                switch (select_case)
                                {
                                    case 1: //Создать сотрудника
                                        #region
                                        Console.Clear();
                                        Console.WriteLine("Каким способом Вы хотите создать сотрудника?\n" +
                                            "1. Вручную     2.Автоматически");

                                        Console.Write("Ваш выбор: ");
                                        select_case = input.UserСhoiceInt(1, 2);

                                        switch (select_case)
                                        {
                                            case 1: // Создать вручную
                                                #region
                                                Console.Clear();
                                                Console.WriteLine("Укажите название департамента в который нужно добавить сотрудника.");
                                                Console.Write("Департамент: ");

                                                departament_name = input.UserChoiceString();
                                                check = departament.HaveDepartament(departament_name, departament_list);

                                                if (!check)
                                                {
                                                    workers_list = workers.Add(workers_list, departament_name);
                                                    departament_list = departament.EditCount(departament_name, departament_list, true, 1);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Такого департамента не существует! Нажмите на любую клавишу...");
                                                    Console.ReadKey();
                                                }
                                                #endregion
                                                break;

                                            case 2: // Создать автоматически
                                                #region
                                                Console.Clear();
                                                Console.Write("Укажите какое колличество сотрудников Вы хотите добавить: ");
                                                workers_count = input.UserСhoiceInt(1, 1000);

                                                for (int i = 0; i<workers_count; i++)
                                                {
                                                    workeradd = workers.AutoAdd(departament_list, workers_list);
                                                    departament_list = departament.EditCount(workeradd.Departament_Name, departament_list, true, 1);
                                                    workers_list.Add(workeradd);
                                                }

                                                Console.WriteLine($"{workers_count} сотрудников успешно добавлены. " +
                                                    $"Нажмите любую клавишу...");
                                                Console.ReadKey();
                                                #endregion
                                                break;
                                        }
                                        #endregion
                                        break;

                                    case 2: //Удалить сотрудника
                                        #region
                                        Console.Clear();

                                        if (workers_list.Count == 0)
                                        {
                                            Console.WriteLine("Список сотрудниов пуст! Нажмите на любую клавишу...");
                                            Console.ReadKey();
                                        }

                                        else workers_list = workers.Delete(workers_list, departament_list, out departament_list);
                                        #endregion
                                        break;

                                    case 3: //Редактировать сотрудника
                                        #region
                                        Console.Clear();

                                        if (workers_list.Count == 0)
                                        {
                                            Console.WriteLine("Список сотрудниов пуст! Нажмите на любую клавишу...");
                                            Console.ReadKey();
                                        }

                                        else workers_list = workers.Edit(workers_list, departament_list, out departament_list);
                                        #endregion
                                        break;

                                    case 4: //Просмотреть список сотрудников
                                        #region
                                        Console.Clear();

                                        if (workers_list.Count == 0)
                                        {
                                            Console.WriteLine("Список сотрудниов пуст! Нажмите на любую клавишу...");
                                            Console.ReadKey();
                                        }

                                        else
                                        {
                                            Console.WriteLine("Выберите способ просмотра списка сотрудников.\n" +
                                                "1. Показать весь список        " +
                                                "2. Отсортировать по возрасту\n" +
                                                "3. Отсортировать по зарплате");

                                            Console.Write("Ваш выбор: ");
                                            select_case = input.UserСhoiceInt(1, 3);
                                            Console.Clear();

                                            Console.Write("№"); Console.SetCursorPosition(5, Console.CursorTop);
                                            Console.Write("Фамилия"); Console.SetCursorPosition(20, Console.CursorTop);
                                            Console.Write("Имя"); Console.SetCursorPosition(27, Console.CursorTop);
                                            Console.Write("Возраст"); Console.SetCursorPosition(37, Console.CursorTop);
                                            Console.Write("Департамент"); Console.SetCursorPosition(52, Console.CursorTop);
                                            Console.Write("Зарплата"); Console.WriteLine();

                                            switch (select_case)
                                            {
                                                case 1: // Показать весь список
                                                    #region
                                                    foreach (var wl in workers_list)
                                                        wl.Show();
                                                    Console.WriteLine("\nНажмите на любую клавишу...");
                                                    Console.ReadKey();
                                                    #endregion
                                                    break;

                                                case 2: // Отсортировать по возрасту
                                                    #region
                                                    var wl_sort_age = from wl in workers_list
                                                                      orderby wl.Age
                                                                      select wl;

                                                    foreach (var wl in wl_sort_age)
                                                        wl.Show();

                                                    Console.WriteLine("\nНажмите на любую клавишу...");
                                                    Console.ReadKey();
                                                    #endregion
                                                    break;

                                                case 3: // Отсортировать по зарплате
                                                    #region
                                                    var wl_sort_wage = from wl in workers_list
                                                                       orderby wl.Wage
                                                                       select wl;

                                                    foreach (var wl in wl_sort_wage)
                                                        wl.Show();

                                                    Console.WriteLine("\nНажмите на любую клавишу...");
                                                    Console.ReadKey();
                                                    #endregion
                                                    break;
                                            }
                                        }
                                        #endregion
                                        break;
                                }

                            }
                        }
                        #endregion
                        break;

                    case 3: // Сохранить БД
                        #region
                        Console.Clear();
                        Console.WriteLine("Каким способом сохранить БД?\n" +
                            "1. XML     2. JSON");

                        Console.Write("Ваш выбор: ");
                        select_case = input.UserСhoiceInt(1, 2);

                        switch (select_case)
                        {
                            case 1: // XML
                                #region
                                Console.Clear();
                                departament.SerializationXML(departament_list);
                                workers.SerializationXML(workers_list);
                                Console.WriteLine("БД сохранена. Нажмите на любую клавишу...");
                                Console.ReadKey();
                                #endregion
                                break;

                            case 2: // JSON
                                #region
                                Console.Clear();
                                departament.SerializationJSON(departament_list);
                                workers.SerializationJSON(workers_list);
                                Console.WriteLine("БД сохранена. Нажмите на любую клавишу...");
                                Console.ReadKey();
                                #endregion
                                break;
                        }
                        #endregion
                        break; 

                    case 4: // Загрузить БД
                        #region
                        Console.Clear();
                        Console.WriteLine("Каким способом загрузить БД?\n" +
                            "1. XML     2. JSON");

                        Console.Write("Ваш выбор: ");
                        select_case = input.UserСhoiceInt(1, 2);

                        switch (select_case)
                        {
                            case 1: // XML
                                #region
                                Console.Clear();
                                departament_list = departament.DeSerializationXML();
                                workers_list = workers.DeSerializationXML();
                                Console.WriteLine("БД загружена. Нажмите на любую клавишу...");
                                Console.ReadKey();
                                #endregion
                                break;

                            case 2: // JSON
                                #region
                                Console.Clear();
                                departament_list = departament.DeSerializationJSON();
                                workers_list = workers.DeSerializationJSON();
                                Console.WriteLine("БД загружена. Нажмите на любую клавишу...");
                                Console.ReadKey();
                                #endregion
                                break;
                        }
                        #endregion
                        break;
                }
                Console.ReadKey();
            }
        }
    }
}
