
using System;

namespace SWArchitecture.Models
{
    public class Statistic
    {
        public int ID { get; set; }
        public string Mark { get; set; }
        public DateTime Date { get; set; }

        public SystemUser User { get; set; }
        public Task Task { get; set; }
    }
}
