using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWArchitecture
{
    public struct Reason
    {
        bool rezult;
        string description; //optional field if we want to retrun description what is wrong
    }
    public class TaskChecker
    {
        public Reason checkCodeStyleTask(Task task)
        {
            Reason rezult=new Reason();
            // logic to check task here
            // may be multiple functions for every task type or just one if check is common 
            return rezult;
        }
        public Reason checkRefactoringTask(Task task)
        {
            Reason rezult = new Reason();
            // logic to check task here
            // may be multiple functions for every task type or just one if check is common 
            return rezult;
        }
        public Reason checkWriteTask(Task task)
        {
            Reason rezult = new Reason();
            // logic to check task here
            // may be multiple functions for every task type or just one if check is common 
            return rezult;
        }
    }
}
