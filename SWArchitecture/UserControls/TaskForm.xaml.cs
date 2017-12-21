﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using SWArchitecture.Models;

namespace SWArchitecture.UserControls
{
    /// <summary>
    /// Interaction logic for TaskForm.xaml
    /// </summary>
    public partial class TaskForm : UserControl
    {
        public SystemUser User { get; set; }
        public List<Task> Tasks { get; set; } //вообще всё что у нас в бд завалялось
        private List<Task> CurrentTasks { get; set; } //список тасков отсортированых по теме
        private int current = -1;

        public TaskForm()
        {
            InitializeComponent();
            using (var context = new ApplicationDbContext())
            {
                Tasks = context.Task.ToList();
            }
            CurrentTasks = Tasks.Where(t => t.Type == TaskType.Refactor).ToList();
            ShowNext();
        }

        private async void buttonCheckTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            var task = CurrentTasks[current];
            task.TaskCode = TextBoxTaskAnswer.Text;
            var Res = new TaskChecker().CheckTask(task);

            using (var context = new ApplicationDbContext())
            {
                var statistic = new Statistic()
                {
                    Date = DateTime.Now,
                    Task = task,
                    Mark = Res.Result ? 1 : 0,
                    User = User
                };
                context.Statistics.Add(statistic);
                await context.SaveChangesAsync();
            }

        }

        private void ShowNext()
        {
            if (CurrentTasks.Count > 0)
            {
                var task = CurrentTasks[++current];
                TextBlockDescription.Text = $"{task.Description}\n{task.RuleForTask}";
                TextBoxTaskUp.Text = task.UpCode;
                TextBoxTaskDown.Text = task.DownCode;
            }
        }

        private void buttonNextTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            buttonCheckTask_Click(sender, e);
            ShowNext();
        }
    }
}
