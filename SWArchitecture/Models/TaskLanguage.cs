using System.Collections.Generic;
using System.Windows;
using System;
namespace SWArchitecture.Models
{
    [Serializable]
    public  class TaskLanguage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
