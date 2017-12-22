using System.Collections.Generic;
using System.Windows.Documents;
using System;
namespace SWArchitecture.Models
{
    [Serializable]
    public class Task
    {
        public int Id { get; set; }
        public TaskType Type { get; set; }
        public string Name { get; set; }
        public string UpCode { get; set; }
        public string TaskCode { get; set; }
        public string DownCode { get; set; }
        public string Answer { get; set; }
        public string Description { get; set; }
        public string RuleForTask { get; set; }

        public TaskLanguage Language { get; set; }
        public List<Statistic> Statistics { get; set; }
    }
}
