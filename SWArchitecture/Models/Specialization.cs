using System.Collections.Generic;
using System.Windows.Documents;

namespace SWArchitecture.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Institute { get; set; }

        public List<StudyGroup> Groups { get; set; }
    }
}
