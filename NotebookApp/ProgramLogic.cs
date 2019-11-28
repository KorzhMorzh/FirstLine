using System;

namespace NotebookApp
{
    static class ProgramLogic
    {
        /// <summary>
        /// Выводит приветствие на экран
        /// </summary>
        internal static void Greeting()
        {
            Console.WriteLine("Добро пожаловать в записную книжку!");
        }

        /// <summary>
        /// Реализует основную логику программы
        /// </summary>
        internal static void Start()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Выберете дальнейшее действие:\n" +
                                  "- Создать новую запись (Введите 1)\n" +
                                  "- Редактировать существующую запись (Введите 2)\n" +
                                  "- Удалить запись (Введите 3)\n" +
                                  "- Просмотреть запись (Введите 4)\n" +
                                  "- Просмотреть все записи (Введите 5)\n" +
                                  "- Выйти из программы (Введите 6)");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Notebook.CreateNewNote();
                        break;
                    case "2":
                        Console.Clear();
                        Notebook.EditNote();
                        break;
                    case "3":
                        Console.Clear();
                        Notebook.DeleteNote();
                        break;
                    case "4":
                        Console.Clear();
                        Notebook.ReadNote();
                        break;
                    case "5":
                        Console.Clear();
                        Notebook.ShowAllNotes();
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("Были рады вас видеть");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверно выбрана команда. Попробуйте еще раз\n");
                        break;
                }
            }
        } 
    }
}
