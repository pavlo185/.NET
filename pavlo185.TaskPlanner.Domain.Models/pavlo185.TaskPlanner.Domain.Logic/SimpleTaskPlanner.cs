using System;
using System.Collections.Generic;
using System.Linq;
using pavlo185.TaskPlanner.Domain.Models;

namespace pavlo185.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        public WorkItem[] CreatePlan(WorkItem[] items)
        {
            var itemsAsList = items.ToList();
            itemsAsList.Sort(CompareWorkItems);
            return itemsAsList.ToArray();
        }

        private static int CompareWorkItems(WorkItem firstItem, WorkItem secondItem)
        {
            // Спочатку сортування за пріоритетом у зворотному порядку (спадання)
            int priorityComparison = secondItem.Priority.CompareTo(firstItem.Priority);
            if (priorityComparison != 0) return priorityComparison;

            // Потім сортування за DueDate у зростаючому порядку
            int dueDateComparison = firstItem.DueDate.CompareTo(secondItem.DueDate);
            if (dueDateComparison != 0) return dueDateComparison;

            // І нарешті сортування за Title в алфавітному порядку
            return string.Compare(firstItem.Title, secondItem.Title, StringComparison.Ordinal);
        }
    }
}
