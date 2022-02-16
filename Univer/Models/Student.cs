using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Univer.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [MinLength(9), StringLength(9), RegularExpression(@"[0][7][7]\d*")]
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }

        public int? CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        public int? GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
    }
}
