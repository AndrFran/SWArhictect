using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using SWArchitecture.Models;
using System.Windows;

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
            if (CurrentTasks.Count < current)
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

        private void buttonCodeStyleTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            current = -1;
            CurrentTasks = Tasks.Where(t => t.Type == TaskType.CodeStyle).ToList();
            ShowNext();
        }

        private void buttonRefactorTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            current = -1;
            CurrentTasks = Tasks.Where(t => t.Type == TaskType.Refactor).ToList();
            ShowNext();
        }

        private void buttonCodeWrite_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            current = -1;
            CurrentTasks = Tasks.Where(t => t.Type == TaskType.WriteCode).ToList();
            ShowNext();
        }

        private void buttonStats_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            var entities = new ApplicationDbContext();
            //var list=entities.SystemUsers.FirstOrDefault(x => x.Id == User.Id).Statistics;
            var list = entities.Statistics.Where(x => x.User.Id == User.Id);
            var windstats = new StatsWindow();
            windstats.Data.ItemsSource = list.ToList();
            windstats.Show();
        }
    }
}
