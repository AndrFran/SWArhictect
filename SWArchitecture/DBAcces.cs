using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace SWArchitecture
{
    public enum TaskType { Refactoring, Modify, Style };
    public class Task
    {
        public int id;
        public TaskType type;
        public int LanguageId { get; set; }
        public String UpCode;//code for Richtextbox in up
        public String TaskCode; //place for user answer
        public String DownCode;//code for RichTextbox
        public String Answer;//Pattern to compare
        public String Description;//what to do with code 
        public String RuleForTask;//Rules to use to complete task
    }
    interface IBDInterface
    {
        // use this interface to retrive or set task info 
        Task getCodeStyleTask(int id);
        Task getCodeModifyTask(int id);
        Task getCodeRefactorTask(int id);
        void setCodeStyleTask(Task task);
        void setCodeModifyTask(Task task);
        void setCodeRefactorTask(Task task);
    }
    class BDInterface : IBDInterface
    {
        public BDInterface()
        {
            LoadInst = new Loader();
            GetterInst = new Getter();
        }
        private Loader LoadInst = new Loader();
        private Getter GetterInst = new Getter();
        public Task getCodeModifyTask(int id)
        {
            return GetterInst.GetTaskById(TaskType.Modify, id);
        }

        public Task getCodeRefactorTask(int id)
        {
            return GetterInst.GetTaskById(TaskType.Refactoring, id);
        }

        public Task getCodeStyleTask(int id)
        {
            return GetterInst.GetTaskById(TaskType.Style, id);
        }

        public void setCodeModifyTask(Task task)
        {
            LoadInst.LoadTaskByType(TaskType.Modify, task);
        }

        public void setCodeRefactorTask(Task task)
        {
            LoadInst.LoadTaskByType(TaskType.Refactoring, task);
        }

        public void setCodeStyleTask(Task task)
        {
            LoadInst.LoadTaskByType(TaskType.Style, task);
        }
    }
    public class TaskDbContext : DbContext
    {
        //singleton from 14 lab here 
        // we need to set up connection to bd using entity framework
        public DbSet<Task> Tasks { get; set; }

    }
    public class Loader
    {
        public Loader()
        {

        }
        VLPIEntities2 entities = new VLPIEntities2();
        public void LoadTaskByType(TaskType typearg, Task t)
        {

            //using (var ctx = new TaskDbContext())
            //{
            //    Task tmp = t;
            //    tmp.type = typearg;
            //    ctx.Tasks.Add(tmp);
            //    ctx.SaveChanges();
            //}
            entities.Task1.Add(new Task1 {
                answer =t.Answer,
                descriptio =t.Description,
                downcode =t.DownCode,
                relefortask =typearg.ToString(),
                taskcode =t.TaskCode,
                upcode =t.UpCode,
                language_id=t.LanguageId
            });
        }
    }
    public class Getter
    {
        VLPIEntities2 entities = new VLPIEntities2();
        public Getter()
        {

        }
        public Task GetTaskById(TaskType typearg, int id)
        {
            using (var context = new TaskDbContext())
            {
                // Query for all blogs with names starting with B 
                //var tasks = from b in context.Tasks
                //            where b.id == id
                //            select b;
                //return tasks.ElementAt(0);

                Task1 t = entities.Task1.Where(x => ((x.task_id == id) && (x.relefortask == typearg.ToString()))).FirstOrDefault();
                return new Task
                {
                    id = t.task_id,
                    Answer = t.answer,
                    Description = t.descriptio,
                    DownCode = t.downcode,
                    RuleForTask = t.relefortask,
                    UpCode = t.upcode,
                    TaskCode = t.taskcode,
                    type = typearg,
                    LanguageId=t.language_id.GetValueOrDefault(),
                };
            }

        }
    }

}
