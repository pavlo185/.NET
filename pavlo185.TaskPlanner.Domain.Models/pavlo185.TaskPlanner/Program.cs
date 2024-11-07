using System;
using System.Collections.Generic;
using pavlo185.TaskPlanner.Domain.Models;
using pavlo185.TaskPlanner.Domain.Models.Enums;
using pavlo185.TaskPlanner.Domain.Logic;


internal static class Program
{
    public static void Main(string[] args)
    {
        List<WorkItem> workItems = new List<WorkItem>();

        Console.WriteLine("Введіть задачі (WorkItem). Для завершення вводу залиште назву пустою і натисніть Enter.");

        while (true)
        {
            Console.Write("Введіть назву задачі (або залиште пустим для завершення): ");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
                break;

            Console.Write("Введіть дату завершення (формат dd.MM.yyyy): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine() ?? throw new InvalidOperationException("Дата не введена"));

            Console.Write("Введіть пріоритет (None, Low, Medium, High, Urgent): ");
            Priority priority = Enum.Parse<Priority>(Console.ReadLine() ?? throw new InvalidOperationException("Пріоритет не введений"), ignoreCase: true);

            Console.Write("Введіть складність (None, Minutes, Hours, Days, Weeks): ");
            Complexity complexity = Enum.Parse<Complexity>(Console.ReadLine() ?? throw new InvalidOperationException("Складність не введена"), ignoreCase: true);

            Console.Write("Введіть опис задачі: ");
            string description = Console.ReadLine();

            // Додаємо новий об'єкт WorkItem до списку
            workItems.Add(new WorkItem
            {
                Title = title,
                DueDate = dueDate,
                Priority = priority,
                Complexity = complexity,
                CreationDate = DateTime.Now,
                Description = description,
                IsCompleted = false
            });

            Console.WriteLine("Задача успішно додана.\n");
        }

        // Сортуємо задачі за допомогою SimpleTaskPlanner
        SimpleTaskPlanner planner = new SimpleTaskPlanner();
        WorkItem[] sortedItems = planner.CreatePlan(workItems.ToArray());

        // Виводимо результати
        Console.WriteLine("\nВідсортовані задачі:");
        foreach (var item in sortedItems)
        {
            Console.WriteLine(item.ToString());
        }
    }
}
