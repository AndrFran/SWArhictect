﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SWArchitecture.Models;

namespace SWArchitecture.UserControls
{
    /// <summary>
    /// Interaction logic for TaskForm.xaml
    /// </summary>
    public class Singleton
    {
        public static Singleton instance;

        private Singleton() { }
        public int cur = -1;
        public int count = 0;
        public int corans = 0;
        public TaskType type = TaskType.Refactor;
        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
    /// <summary>
    /// Interaction logic for TaskForm.xaml
    /// </summary>
    public partial class TaskForm : UserControl
    {
        private Caretaker SaverLoader;
        public List<Task> Tasks { get; set; } //вообще всё что у нас в бд завалялось
        private List<Task> CurrentTasks { get; set; } //список тасков отсортированых по теме

        public void Save()
        {
            SaverLoader.SaveProgress(new TaskState(Singleton.Instance.corans, 0,
                Singleton.Instance.cur, 0, TextBoxTaskAnswer.Text, CurrentTasks)
            { });
        }
        public TaskForm()
        {
            InitializeComponent();
            using (var context = new ApplicationDbContext())
            {
                Tasks = context.Task.ToList();
            }
            SaverLoader = new Caretaker();
            SaverLoader.BeginTest();
            try
            {
                TaskState tmp = SaverLoader.LoadProgress();
                Singleton.Instance.corans = tmp.CountOfPossitiveAnswers;
                Singleton.Instance.cur = tmp.NumberOfCurrentTask - 1;
                this.TextBoxTaskAnswer.Text = tmp.CurrentAnswer;
                CurrentTasks = tmp._currentTestSet;
            }
            catch (Exception ex)
            {
                CurrentTasks = Tasks.Where(t => t.Type == TaskType.Refactor).ToList();
            }
            Singleton.Instance.count = CurrentTasks.Count;
            this.CorrectAns.Content = Singleton.Instance.corans.ToString();
            this.TaskCount.Content = Singleton.Instance.count.ToString();
            ShowNext();
        }

        private void buttonCheckTask_Click(object sender, RoutedEventArgs e)
        {
            if (Singleton.Instance.cur<CurrentTasks.Count )
            {
                var task = CurrentTasks[Singleton.Instance.cur];
                task.TaskCode = TextBoxTaskAnswer.Text;
                var Res = new TaskChecker().CheckTask(task);
                if (Res.Result)
                {
                    Singleton.Instance.corans++;
                    this.CorrectAns.Content = Singleton.Instance.corans.ToString();
                }
                
                using (var context = new ApplicationDbContext())
                {
                    var statistic = new Statistic
                    {
                        Date = DateTime.Now,
                        Task = task,
                        Mark = Res.Result ? 1 : 0,
                        User = MainWindow.User
                    };
                    context.Statistics.Add(statistic);
                }
            }
        }

        private void ShowNext()
        {
            this.CurTask.Content = (Singleton.Instance.cur + 1).ToString();
            if (CurrentTasks.Count == Singleton.Instance.cur+1)
            {
                buttonStats_Click(null,null);
            }
            else if (CurrentTasks.Count > 0)
            {
                var task = CurrentTasks[++Singleton.Instance.cur];
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

        #region task types buttons

        private void buttonCodeStyleTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Singleton.Instance.cur = -1;
            CurrentTasks = Tasks.Where(t => t.Type == TaskType.CodeStyle).ToList();
            ShowNext();
        }

        private void buttonRefactorTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Singleton.Instance.cur = -1;
            CurrentTasks = Tasks.Where(t => t.Type == TaskType.Refactor).ToList();
            ShowNext();
        }

        private void buttonCodeWrite_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Singleton.Instance.cur = -1;
            CurrentTasks = Tasks.Where(t => t.Type == TaskType.WriteCode).ToList();
            ShowNext();
        }

        #endregion

        private void buttonStats_Copy_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Save();
        }

        private void buttonStats_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            var entities = new ApplicationDbContext();
            var User = MainWindow.User;
            //var list=entities.SystemUsers.FirstOrDefault(x => x.Id == User.Id).Statistics;
            var list = entities.Statistics.Where(x => x.User.Id == User.Id );
            var windstats = new StatsWindow();
            windstats.Data.ItemsSource = list.ToList();
            windstats.Show();
        }
    }
}
