using System.Linq;
using SWArchitecture.Models;

namespace SWArchitecture
{
    public struct Reason
    {
        public bool Result { get; set; }
        string _description; //optional field if we want to retrun description what is wrong
    }
    public class TaskChecker
    {
        public Reason CheckTask(Task task)
        {
            var userAnswer = task.TaskCode.Split(' ', '\n', '\t').Aggregate((a, b) => a + b);
            var rightAnswer = task.TaskCode.Split(' ', '\n', '\t').Aggregate((a, b) => a + b);

            if (userAnswer.Equals(rightAnswer))
                return new Reason{Result=true};
            return new Reason { Result = false };
        }
    }
}
