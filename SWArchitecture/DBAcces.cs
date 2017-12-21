using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using SWArchitecture.Models;

namespace SWArchitecture
{

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
            return GetterInst.GetTaskById(TaskType.WriteCode, id);
        }

        public Task getCodeRefactorTask(int id)
        {
            return GetterInst.GetTaskById(TaskType.Refactor, id);
        }

        public Task getCodeStyleTask(int id)
        {
            return GetterInst.GetTaskById(TaskType.CodeStyle, id);
        }

        public void setCodeModifyTask(Task task)
        {
            LoadInst.LoadTaskByType(TaskType.WriteCode, task);
        }

        public void setCodeRefactorTask(Task task)
        {
            LoadInst.LoadTaskByType(TaskType.Refactor, task);
        }

        public void setCodeStyleTask(Task task)
        {
            LoadInst.LoadTaskByType(TaskType.CodeStyle, task);
        }

    }
    public class Loader
    {
        public Loader()
        {

        }
        ApplicationDbContext entities = new ApplicationDbContext();
        public void LoadTaskByType(TaskType typearg, Task t)
        {
            entities.Task.Add(new Task
            {
                Answer = t.Answer,
                Description = t.Description,
                DownCode = t.DownCode,
                RuleForTask = typearg.ToString(),
                TaskCode = t.TaskCode,
                UpCode = t.UpCode,
                Language = t.Language
            });
        }
    }
    public class Getter
    {
        ApplicationDbContext entities = new ApplicationDbContext();
        public Getter()
        {

        }
        public Task GetTaskById(TaskType typearg, int id)
        {
                Task t = entities.Task.Where(x => ((x.Id == id) && (x.RuleForTask == typearg.ToString()))).FirstOrDefault();
                return new Task
                {
                    Id = t.Id,
                    Answer = t.Answer,
                    Description = t.Description,
                    DownCode = t.DownCode,
                    RuleForTask = t.RuleForTask,
                    UpCode = t.UpCode,
                    TaskCode = t.TaskCode,
                    Language = t.Language,
                };
            }

        }
    }

