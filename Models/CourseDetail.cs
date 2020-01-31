using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseCRUD.Models
{
    public class CourseDetail
    {
        public int CourseDetailId { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Fees { get; set; } 
        public string Description { get; set; }
        public  IList<Subject> subjects { get; set; }
    }
}
