using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace SWArchitecture.Models
{
    public class SystemUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserTypes UserType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }

        public StudyGroup Group { get; set; }
        public List<Statistic> Statistics { get; set; }

    }

}
