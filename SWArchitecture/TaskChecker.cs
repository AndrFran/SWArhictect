using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWArchitecture
{
    public struct Reason
    {
        public bool result { get; set; }
        string description; //optional field if we want to retrun description what is wrong
    }
    public class TaskChecker
    {
        public Reason checkTask(Task task)
        {
            var userAnswer = task.TaskCode.Split(' ', '\n', '\t').Aggregate((a, b) => a + b);
            var rightAnswer = task.TaskCode.Split(' ', '\n', '\t').Aggregate((a, b) => a + b);

            if (userAnswer.Equals(rightAnswer))
                return new Reason{result=true};
            else        
                return new Reason { result = false };
            // logic to check task here
            // may be multiple functions for every task type or just one if check is common 

        }
    }
}
