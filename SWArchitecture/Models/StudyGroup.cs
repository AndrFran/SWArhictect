
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace SWArchitecture.Models
{
    public class StudyGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Course { get; set; }
        public Specialization Specialization { get; set; }

        public List<SystemUser> Users { get; set; }
    }
}
