using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Univer.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
