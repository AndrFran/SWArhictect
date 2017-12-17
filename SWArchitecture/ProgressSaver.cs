using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SWArchitecture
{
    namespace VLPI
    {
        public class Caretaker
        {
            TaskMemento Info;

            public void BeginTest()
            {
                Info = new TaskMemento();
            }
            public void SaveProgress(TaskState Inf)
            {//save progress here
                Info.TaskMementoSaveState(Inf);
            }
            public TaskState LoadProgress()
            {
                //load here
                return Info.GetState();
            }
        }
        public class ProgressSaver//we don`t need this but this is in lab so it is dead code
        {
            // Стан містить здоров’я та к-ть вбитих монстрів
            private TaskState _state;
            public void Go()
            {
                //Тут процес повинен міститися тестування 
            }
            public TaskMemento TaskSave()
            {
                return new TaskMemento();
            }
            public void LoadTask(TaskMemento memento)
            {
                _state = memento.GetState();
            }
        }

        public class TaskMemento
        {
            private TaskState _state;
            private String SaveFile= "Memento.dat";
            private void Serialize(TaskState taskState)
            {
                //serialize here
                BinaryFormatter formatter = new BinaryFormatter();
                // получаем поток, куда будем записывать сериализованный объект
                using (FileStream fs = new FileStream(SaveFile, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, taskState);
                }
            }
            private TaskState Deserialize()
            {
                BinaryFormatter formatter = new BinaryFormatter();
                //deserialize here
                TaskState newPerson;
                using (FileStream fs = new FileStream(SaveFile, FileMode.OpenOrCreate))
                {
                     newPerson = (TaskState)formatter.Deserialize(fs);
                }
                return newPerson;
            }
            public TaskMemento()
            {
                _state = new TaskState();
            }
            public void TaskMementoSaveState(TaskState state)
            {
                _state = state;
                Serialize(state);
            }
            public TaskState GetState()
            {
                _state=Deserialize();
                return _state;
            }
        }
        [Serializable]
        public class TaskState
        {
            public TaskState()
            {

            }
            private List<Task> currentTestSet;
            private int CountOfPossitiveAnswers { get; }
            private int CountOfNegativeAnswers { get; }
            private int NumberOfCurrentTask { get; }
            private int CurrentTimeLeft { get; }
            private string CurrentAnswer { get; }
            public TaskState(int pos, int neg, int num, int tl, string ans, List<Task> tasks)
            {
                CountOfPossitiveAnswers = pos;
                CountOfNegativeAnswers = neg;
                NumberOfCurrentTask = num;
                CurrentTimeLeft = tl;
                CurrentAnswer = ans;
                currentTestSet = tasks;
            }
        }
    }


}
