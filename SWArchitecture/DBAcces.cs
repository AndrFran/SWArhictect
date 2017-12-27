using System;
using System.Data.Entity;
using System.Linq;
using SWArchitecture.Models;

namespace SWArchitecture
{
    interface IBdInterface
    {
        // use this interface to retrive or set task info 
        Task GetTask(int id);
        Task GetCodeModifyTask(int id);
        Task GetCodeRefactorTask(int id);
        void SetCodeStyleTask(Task task);
        void SetCodeModifyTask(Task task);
        void SetCodeRefactorTask(Task task);
    }
    
}
