using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using SWArchitecture.Models;
using SWArchitecture.VLPI;

namespace SWArchitecture.UserControls
{
    public class Singleton
    {
        private static Singleton instance;

        private Singleton() { }
        public int cur=-1;
        public int count=0;
        public int corans=0;
        public TaskType type=TaskType.Refactor;
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
        public SystemUser User { get; set; }
        public List<Task> Tasks { get; set; } //вообще всё что у нас в бд завалялось
        private List<Task> CurrentTasks { get; set; } //список тасков отсортированых по теме

        public void Save()
        {
            SaverLoader.SaveProgress(new TaskState(Singleton.Instance.corans,0,
                Singleton.Instance.cur,0, TextBoxTaskAnswer.Text, CurrentTasks) { } );
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
            catch(Exception ex)
            {
                CurrentTasks = Tasks.Where(t => t.Type == TaskType.Refactor).ToList();
            }
            Singleton.Instance.count = CurrentTasks.Count;
            this.CorrectAns.Content = Singleton.Instance.corans.ToString();
            this.TaskCount.Content = Singleton.Instance.count.ToString();
            ShowNext();
        }

        private async void buttonCheckTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (CurrentTasks.Count < Singleton.Instance.cur)
            {
                var task = CurrentTasks[++Singleton.Instance.cur];
                task.TaskCode = TextBoxTaskAnswer.Text;
                var Res = new TaskChecker().CheckTask(task);
                if(Res.Result)
                {
                    Singleton.Instance.corans++;
                    this.CorrectAns.Content = Singleton.Instance.corans.ToString();
                }
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
            this.CurTask.Content = (Singleton.Instance.cur+1).ToString();
            if (CurrentTasks.Count > 0)
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

        private void buttonStats_Copy_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Save();
        }
    }
}
