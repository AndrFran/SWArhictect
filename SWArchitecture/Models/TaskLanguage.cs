using System.Collections.Generic;
using System.Windows;

namespace SWArchitecture.Models
{
    public  class TaskLanguage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
