using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Univer.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Year { get; set; }

        [ForeignKey("Curator")]
        public int? CuratorId { get; set; }
        public Instructor Curator { get; set; }

        public int? CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        public int? SpecialId { get; set; }
        [ForeignKey("SpecialId")]
        public Special Special { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
