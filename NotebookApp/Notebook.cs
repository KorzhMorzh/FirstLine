using System;
using System.Collections.Generic;

namespace NotebookApp
{
    class Notebook
    {
        static void Main(string[] args)
        {
            ProgramLogic.Greeting();
            ProgramLogic.Start();
        }

        internal static List<Note> listNotes = new List<Note>(); 

        internal static void CreateNewNote()
        {
            Console.WriteLine("Вы выбрали создать запись");
            Note note = new Note();
            EditNote(ref note);
            listNotes.Add(note);
            Console.WriteLine("Заметка успешно добавлена!");
            EndFunction();
        }

        internal static void EditNote()
        {
            if (listNotes.Count == 0)
            {
                Console.WriteLine("Записи отстутствуют\n" +
                                  "Для выхода в главное меню нажмите любую клавишу");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            Console.WriteLine("Вы выбрали редактировать запись\n");
            Dictionary<string, string> info = GetDataForSearch();
            List<int> indexList = GetIndexesOfNotes(info); 
            if (indexList.Count == 0)
            {
                Console.WriteLine("Совпадения не найдены");
            }
            else if (indexList.Count == 1)
            {
                Console.WriteLine("Найденная запись:");
                ShowShortInfo(listNotes[indexList[0]]);
                Note note = listNotes[indexList[0]];
                Console.WriteLine("\nВведите новые данные:");
                EditNote(ref note);
                Console.WriteLine("Запись отредактирована");
            }
            else
            {
                Console.WriteLine($"\nКоличество найденных записей {indexList.Count}\n\n");
                for (int i = 0; i < indexList.Count; i++)
                {
                    ShowShortInfo(listNotes[indexList[i]]);
                }
                Console.WriteLine($"Выберете дальнейшее действие:\n" +
                                  $"1 - Редактировать все найденные записи\n" +
                                  $"2 - Редактировать запись по номеру телефона\n" +
                                  $"3 - Редактировать запись по ID\n" +
                                  $"Вернуться в главное меню - нажать enter\n");
                switch (Console.ReadLine())
                {
                    case "1":
                        for (int i = 0; i < indexList.Count; i++)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nЗапись {i + 1}:");
                            Note note = listNotes[indexList[i]];
                            Console.WriteLine("\nВведите новые данные:");
                            EditNote(ref note);
                            Console.Clear();
                        }
                        Console.WriteLine($"Количество отредактированных записей: {indexList.Count}");
                        break;
                    case "2":
                        info = CollectDataFromUser("phone");
                        indexList = GetIndexesOfNotes(info);
                        if (indexList.Count == 0)
                        {
                            Console.WriteLine("Совпадений не найдено");
                        }
                        else
                        {
                            Note note = listNotes[indexList[0]];
                            Console.WriteLine("\nВведите новые данные:");
                            EditNote(ref note);
                            Console.WriteLine($"Запись успешно отредактирована");
                        }
                        break;
                    case "3":
                        info = CollectDataFromUser("id");
                        indexList = GetIndexesOfNotes(info);
                        if (indexList.Count == 0)
                        {
                            Console.WriteLine("Совпадений не найдено");
                        }
                        else
                        {
                            Note note = listNotes[indexList[0]];
                            Console.WriteLine("\nВведите новые данные:");
                            EditNote(ref note);
                            Console.WriteLine($"Запись успешно отредактирована");
                        }
                        break;
                }
            }
            EndFunction();
        }

        internal static void DeleteNote() 
        {
            if (listNotes.Count == 0)
            {
                Console.WriteLine("Записи отстутствуют\n" +
                                  "Для выхода в главное меню нажмите любую клавишу");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            Console.WriteLine("Вы выбрали удалить запись\n");
            Dictionary<string, string> info = GetDataForSearch();
            List<int> indexList = GetIndexesOfNotes(info);

            if (indexList.Count == 0)
            {
                Console.WriteLine("Совпадения не найдены");
            }
            else if (indexList.Count == 1)
            {
                Console.WriteLine("Найденная запись:");
                ShowShortInfo(listNotes[indexList[0]]);
                listNotes.RemoveAt(indexList[0]);
                Console.WriteLine("Запись удалена");
            }
            else
            {
                Console.WriteLine($"\nКоличество найденных записей {indexList.Count}\n\n");
                for (int i = 0; i < indexList.Count; i++)
                {
                    ShowShortInfo(listNotes[indexList[i]]);
                }
                Console.WriteLine($"Выберете дальнейшее действие:\n" +
                                  $"1 - Удалить все найденные записи\n" +
                                  $"2 - Удалить запись по номеру телефона\n" +
                                  $"3 - Удалить запись по ID\n" +
                                  $"Вернуться в главное меню - нажать enter");
                switch (Console.ReadLine())
                {
                    case "1":
                        for(int i = 0; i < indexList.Count; i++)
                        {
                        listNotes.RemoveAt(indexList[i] - i);
                        }
                        Console.WriteLine($"Количество удаленных записей: {indexList.Count}");
                        break;
                    case "2":
                        info = CollectDataFromUser("phone");
                        indexList = GetIndexesOfNotes(info);
                        if (indexList.Count == 0)
                        {
                            Console.WriteLine("Совпадений не найдено");
                        }
                        else
                        {
                            listNotes.RemoveAt(indexList[0]);
                            Console.WriteLine($"Запись успешно удалена");
                        }
                        break;
                    case "3":
                        info = CollectDataFromUser("id");
                        indexList = GetIndexesOfNotes(info);
                        if (indexList.Count == 0)
                        {
                            Console.WriteLine("Совпадений не найдено");
                        }
                        else
                        {
                            listNotes.RemoveAt(indexList[0]);
                            Console.WriteLine($"Запись успешно удалена");
                        }
                        break;
                }
            }
            EndFunction();
        }

        internal static void ReadNote()
        {
            if (listNotes.Count == 0)
            {
                Console.WriteLine("Записи отстутствуют\n" +
                                  "Для выхода в главное меню нажмите любую клавишу");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            bool isContain = false;
            void DisplayUser(Note item)
            {
                Console.WriteLine($"\n{item.Surname} {item.Name} {item?.Lastname ?? ""}\n" +
                                  $"Id: {item.Id}\n" +
                                  $"Номер телефона: {item.PhoneNumber}\n" +
                                  $"Страна: {item.Country}\n" +
                                  $"Дата рождения: {item.Birthday}\n" +
                                  $"Организация: {item?.Organization ?? "не указана"}\n" +
                                  $"Должность: {item?.Post ?? "не указана"}\n" +
                                  $"Прочие заметки: {item?.Other ?? "не указаны"}\n\n");
            }
            void IfNotContain()
            {
                if (!isContain)
                {
                    Console.WriteLine("Совпадения не найдены");
                }
            }
            Console.WriteLine("Вы выбрали посмотреть запись\n");
            Dictionary<string, string> info = GetDataForSearch();
            switch (info["choice"])
            {
                case "fullName":
                    foreach (var item in listNotes)
                    {
                        if (item.Name.ToLower().Contains(info["name"]) && item.Surname.ToLower().Contains(info["surname"]))
                        {
                            isContain = true;
                            DisplayUser(item);
                        }
                    }
                    IfNotContain();
                    break;
                case "phone":
                    foreach (var item in listNotes)
                    {
                        if (item.PhoneNumber.ToLower() == info["phone"])
                        {
                            isContain = true;
                            DisplayUser(item);
                        }
                    }
                    IfNotContain();
                    break;
                case "id":
                    foreach (var item in listNotes)
                    {
                        if (item.Id == (info["id"]))
                        {
                            isContain = true;
                            DisplayUser(item);
                        }
                    }
                    IfNotContain();
                    break;
            }
            EndFunction();
        }

        internal static void ShowAllNotes()
        {
            if (listNotes.Count == 0)
            {
                Console.WriteLine("Записи отстутствуют\n" +
                                  "Для выхода в главное меню нажмите любую клавишу");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            Console.WriteLine("Вы выбрали посмотреть все записи\n");
            foreach (var item in listNotes)
            {
                ShowShortInfo(item);
            }
            EndFunction();
        }

        //Далее следуют вспомогательные функции

        /// <summary>
        ///Редактирует поля записи
        /// </summary>
        /// <param name="note">Изменяемая запись</param>
        private static void EditNote(ref Note note)
        {
            Console.WriteLine("\nВведите фамилию:");
            note.Surname = Console.ReadLine();

            Console.WriteLine("Введите имя:");
            note.Name = Console.ReadLine();

            Console.WriteLine("Введите отчество (необязательно):");
            note.Lastname = Console.ReadLine();

            Console.WriteLine("Введите номер телефона:");
            note.PhoneNumber = Console.ReadLine();

            Console.WriteLine("Введите страну:");
            note.Country = Console.ReadLine();

            Console.WriteLine("Введите дату рождения (необязательно):");
            note.Birthday = Console.ReadLine();

            Console.WriteLine("Введите организацию (необязательно):");
            note.Organization = Console.ReadLine();

            Console.WriteLine("Введите должность (необязательно):");
            note.Post = Console.ReadLine();

            Console.WriteLine("Введите другие заметки (необязательно):");
            note.Other = Console.ReadLine();
        }

        /// <summary>
        /// Собирает данные от пользователя для поиска записи
        /// </summary>
        /// <param name="choice">Параметр поиска</param>
        /// <returns>Словарь с данными</returns>
        internal static Dictionary<string, string> CollectDataFromUser(string choice)
        {
            Dictionary<string, string> info = new Dictionary<string, string>();
            if (choice == "fullName")
            {
                info.Add("choice", "fullName");
                Console.WriteLine("\nВведите фамилию");
                info.Add("surname", Console.ReadLine().ToLower());
                Console.WriteLine("Введите имя");
                info.Add("name", Console.ReadLine().ToLower());
            }
            else if (choice == "phone")
            {
                info.Add("choice", "phone");
                Console.WriteLine("\nВведите номер телефона");
                info.Add("phone", Console.ReadLine());
            }
            else if (choice == "id")
            {
                info.Add("choice", "id");
                Console.WriteLine("\nВведите ID");
                info.Add("id", Console.ReadLine());
            }
            return info;
        }

        /// <summary>
        /// Проверяет необязательные поля
        /// </summary>
        /// <param name="field">Проверяемое поле</param>
        internal static void CheckUnnecessaryField(ref string field)
        {
            if (field =="")
            {
                field = null;
            }
        }

        /// <summary>
        /// Проверяет обязательные поля
        /// </summary>
        /// <param name="field">Проверяемое поле</param>
        /// <param name="isCorrect">Флаг корректности</param>
        internal static void CheckNecessaryField(string field, out bool isCorrect)
        {
            if (field == "")
            {
                Console.WriteLine("Поле не может быть пустым. Попробуйте еще раз");
                isCorrect = false;
            }
            else
            {
                isCorrect = true;
            }
        }

        /// <summary>
        /// Окончание метода
        /// </summary>
        private static void EndFunction()
        {
            Console.WriteLine("Нажмите любую клавишу для продолжения");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Вы вернулись в главное меню");
        }

        /// <summary>
        /// Собирает данные, по которым будет осуществляться поиск записи
        /// </summary>
        /// <returns>Данные для поиска</returns>
        private static Dictionary<string, string> GetDataForSearch()
        {
            Console.WriteLine($"Выберете параметр поиска:\n" +
                              $"1 - По фамилии и имени\n" +
                              $"2 - По номеру телефона\n" +
                              $"3 - По id\n");
            while (true)
            {
                Dictionary<string, string> info = new Dictionary<string, string>();
                switch (Console.ReadLine())
                {
                    case "1":
                        info = CollectDataFromUser("fullName");
                        return info;
                    case "2":
                        info = CollectDataFromUser("phone");
                        return info;
                    case "3":
                        info = CollectDataFromUser("id");
                        return info;
                    default:
                        Console.WriteLine("Введена некорректная команда. Попробуйте еще раз.");
                        break;
                }
            }
        }

        /// <summary>
        /// Получает список индексов записей, удолетворяющих условию поиска
        /// </summary>
        /// <param name="info">Условие поиска</param>
        /// <returns>Лист индексов</returns>
        private static List<int> GetIndexesOfNotes(Dictionary<string, string> info)
        {
            List<int> indexList = new List<int>();
            switch (info["choice"])
            {
                case "fullName":
                    for (int i = 0; i < listNotes.Count; i++)
                    {
                        if (listNotes[i].Name.ToLower().Contains(info["name"]) && listNotes[i].Surname.ToLower().Contains(info["surname"]))
                        {
                            indexList.Add(i);
                        }
                    }
                    break;
                case "phone":
                    for (int i = 0; i < listNotes.Count; i++)
                    {
                        if (listNotes[i].PhoneNumber == info["phone"])
                        {
                            indexList.Add(i);
                        }
                    }
                    break;
                case "id":
                    for (int i = 0; i < listNotes.Count; i++)
                    {
                        if (listNotes[i].Id == (info["id"]))
                        {
                            indexList.Add(i);
                        }
                    }
                    break;
            }
            return indexList;
        }

        /// <summary>
        /// Показывает короткую информацию о записи
        /// </summary>
        /// <param name="note">Запись</param>
        private static void ShowShortInfo(Note note)
        {
            Console.WriteLine($"Id: {note.Id}\n" +
                              $"    Фамилия: {note.Surname}\n" +
                              $"    Имя: {note.Name}\n" +
                              $"    Номер телефона: {note.PhoneNumber}\n\n");
        }
    }
}
