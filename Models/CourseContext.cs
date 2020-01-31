using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseCRUD.Models
{
    public class CourseContext:DbContext
    {
        public CourseContext(DbContextOptions<CourseContext> options):base(options)
        {
                
        }

        public DbSet<CourseDetail> courseDetails { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public int totalCount;
    }

}
