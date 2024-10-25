using System;
using static System.Console;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;  // Пакет Newtonsoft.Json нужно установить через NuGet

namespace Mod9
{
    internal class Program
    {
        static List<Student> students = new List<Student>();
        static string filePath = "students.json";

        static void Main(string[] args)
        {
            LoadFromFile();
            Clear();
            while (true)
            {
                WriteLine("Меню:");
                WriteLine("1. Добавить студента");
                WriteLine("2. Редактировать студента");
                WriteLine("3. Удалить студента");
                WriteLine("4. Показать всех студентов");
                WriteLine("5. Сохранить и выйти");

                Write("\nВыберите действие: ");
                string choice = ReadLine();
                WriteLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        EditStudent();
                        break;
                    case "3":
                        DeleteStudent();
                        break;
                    case "4":
                        ShowStudents();
                        break;
                    case "5":
                        SaveToFile();
                        return;
                    default:
                        WriteLine("Неверный выбор, попробуйте снова.\n");
                        break;
                }
            }
        }

        static void AddStudent()
        {
            Write("Введите ID: ");
            int id = int.Parse(ReadLine());

            Write("Введите имя: ");
            string name = ReadLine();

            Write("Введите возраст: ");
            int age = int.Parse(ReadLine());

            Write("Введите курс: ");
            string course = ReadLine();

            students.Add(new Student { Id = id, Name = name, Age = age, Course = course });
            WriteLine("Студент добавлен.\n");
        }

        static void EditStudent()
        {
            Write("Введите ID студента для редактирования: ");
            int id = int.Parse(ReadLine());

            Student student = students.Find(s => s.Id == id);
            if (student != null)
            {
                Write("Введите новое имя: ");
                student.Name = ReadLine();

                Write("Введите новый возраст: ");
                student.Age = int.Parse(ReadLine());

                Write("Введите новый курс: ");
                student.Course = ReadLine();

                WriteLine("Информация о студенте обновлена.\n");
            }
            else
                WriteLine("Студент с таким ID не найден.\n");
        }

        static void DeleteStudent()
        {
            Write("Введите ID студента для удаления: ");
            int id = int.Parse(ReadLine());

            Student student = students.Find(s => s.Id == id);
            if (student != null)
            {
                students.Remove(student);
                WriteLine("Студент удален.\n");
            }
            else
                WriteLine("Студент с таким ID не найден.\n");
        }

        static void ShowStudents()
        {
            WriteLine("\nСписок студентов:");
            foreach (var student in students)
            {
                WriteLine($"ID: {student.Id}, Имя: {student.Name}, Возраст: {student.Age}, Курс: {student.Course}");
            }
            WriteLine();
        }

        static void SaveToFile()
        {
            string json = JsonConvert.SerializeObject(students, Newtonsoft.Json.Formatting.Indented); // Указано полное имя
            File.WriteAllText(filePath, json);
            WriteLine("Данные сохранены.\n");
        }


        static void LoadFromFile()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                students = JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();
            }
        }
        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string Course { get; set; }
        }
    }
}