using System;
using System.Collections.Generic;

namespace CyberSecurityAwarenessBot
{
    public class DatabaseHelper
    {
        private List<TaskItem> memoryTasks = new List<TaskItem>();
        private int taskIdCounter = 1;

        public DatabaseHelper()
        {
            Console.WriteLine("Task storage initialized - In-Memory Mode");
            AddSampleTasks();
        }

        private void AddSampleTasks()
        {
            var task1 = new TaskItem
            {
                Title = "Enable Two-Factor Authentication",
                Description = "Set up 2FA on all important accounts including email and banking.",
                Reminder = "Reminder set for 7 days from now",
                CreatedAt = DateTime.Now,
                IsCompleted = false
            };

            var task2 = new TaskItem
            {
                Title = "Review Privacy Settings",
                Description = "Check and update privacy settings on social media accounts.",
                Reminder = "Reminder set for 3 days from now",
                CreatedAt = DateTime.Now,
                IsCompleted = false
            };

            AddTask(task1);
            AddTask(task2);
        }

        public void AddTask(TaskItem task)
        {
            task.Id = taskIdCounter++;
            memoryTasks.Add(task);
            Console.WriteLine("Task added: " + task.Title);
        }

        public List<TaskItem> GetAllTasks()
        {
            return new List<TaskItem>(memoryTasks);
        }

        public void UpdateTask(TaskItem task)
        {
            var existing = memoryTasks.Find(t => t.Id == task.Id);
            if (existing != null)
            {
                existing.IsCompleted = task.IsCompleted;
                existing.Title = task.Title;
                existing.Description = task.Description;
                existing.Reminder = task.Reminder;
                Console.WriteLine("Task updated: " + task.Title);
            }
        }

        public void DeleteTask(int id)
        {
            var task = memoryTasks.Find(t => t.Id == id);
            if (task != null)
            {
                memoryTasks.Remove(task);
                Console.WriteLine("Task deleted: " + task.Title);
            }
        }
    }
}