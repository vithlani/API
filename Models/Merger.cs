using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseCRUD.Models
{
    public class Merger
    {
        public int SubjectId { get; set; }
        public string Sname { get; set; }
        public string SCredit { get; set; }
        public string Description { get; set; }
        public int CourseDetailId { get; set; }
        public string CourseName { get; set; }
    }
}
