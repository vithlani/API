using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseCRUD.Models
{
    public class Courselistwithcount
    {
        public int TotalCount { get; set; }

        public List<CourseDetail> CourseDetails { get; set; }

    }
}
