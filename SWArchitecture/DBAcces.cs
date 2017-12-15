using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace SWArchitecture
{
    public enum TaskTypes { Refactoring, Modify, Style };
    public class Task
    {
        public int id;
        public TaskTypes type;
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
        private Loader LoadInst;
        private Getter GetterInst;
        public Task getCodeModifyTask(int id)
        {
            return GetterInst.GetTaskById(TaskTypes.Modify, id);
        }

        public Task getCodeRefactorTask(int id)
        {
            return GetterInst.GetTaskById(TaskTypes.Refactoring, id);
        }

        public Task getCodeStyleTask(int id)
        {
            return GetterInst.GetTaskById(TaskTypes.Style, id);
        }

        public void setCodeModifyTask(Task task)
        {
            LoadInst.LoadTaskByType(TaskTypes.Modify, task);
        }

        public void setCodeRefactorTask(Task task)
        {
            LoadInst.LoadTaskByType(TaskTypes.Refactoring, task);
        }

        public void setCodeStyleTask(Task task)
        {
            LoadInst.LoadTaskByType(TaskTypes.Style, task);
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
        public void LoadTaskByType(TaskTypes typearg, Task t)
        {
            using (var ctx = new TaskDbContext())
            {
                Task tmp = t;
                tmp.type = typearg;
                ctx.Tasks.Add(tmp);
                ctx.SaveChanges();
            }
        }
    }
    public class Getter
    {
        public Task GetTaskById(TaskTypes typearg, int id)
        {
            using (var context = new TaskDbContext())
            {
                // Query for all blogs with names starting with B 
                var tasks = from b in context.Tasks
                            where b.id == id
                            select b;
                return tasks.ElementAt(0);
            }

        }
    }

    }
