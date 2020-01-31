using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseCRUD.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseCRUD.Controllers
{
    //[EnableCors("_Allowed")]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly CourseContext _context;
       

        public CourseController(CourseContext context)
        {
            _context = context;
        }
         [HttpGet("pagination")]
         // GET:api/Course
         public IActionResult pagination([FromQuery]Parameter parameter)
         
        {
              try
              {
                   var courses = from c in _context.courseDetails
                                 select c;
                var course = _context.courseDetails.Include(s => s.subjects).Skip((parameter.PageNumber - 1) * parameter.PageSize)
                   .Take(parameter.PageSize).ToList();
                Courselistwithcount courselistwithcount= new Courselistwithcount();
                courselistwithcount.CourseDetails = course;
                courselistwithcount.TotalCount = courses.Count();
                        return Ok(courselistwithcount);
              }
              catch
               {
                   return BadRequest();
               }
          }
        [HttpGet("search/{searchString}")]
        public IActionResult GetCourses(string searchstring)
        {
            try
            {

                var courses = from c in _context.courseDetails
                              select c;
                var course = _context.courseDetails.Include(s => s.subjects).Where(c => c.Name.Contains(searchstring)
                                           || c.Description.Contains(searchstring)).ToList();
                return Ok(course);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        //GET:api/Course
        public IActionResult GetCourses()
          {
                  
                  var course = _context.courseDetails.Include(s => s.subjects).OrderBy(name=>name.Name).ToList();
                  return Ok(course);
              
          }
        [HttpGet("{id}")]
        // GET:api/Course/{1}
        public IActionResult GetCourse(int id)
        {
            try
            {
                var course = _context.courseDetails.Include(s => s.subjects).FirstOrDefault(i => i.CourseDetailId == id); 
                return Ok(course);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("sorting")]

        [HttpPost]
        public IActionResult AddCourse([FromBody]CourseDetail course)
        {
            try
            {
                _context.courseDetails.Add(course);
                _context.SaveChanges();
                return CreatedAtAction("GetCourses", course);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("id")]

        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id,CourseDetail course)
        {
          
            if(id!=course.CourseDetailId)
            {
                return BadRequest();
            }
            _context.Entry(course).State = EntityState.Modified;
            _context.subjects.UpdateRange(course.subjects);
            try
            {
                _context.SaveChanges();
                var courses = _context.courseDetails.Include(s => s.subjects).FirstOrDefault(i => i.CourseDetailId == id);
                return Ok(course);
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!CourseDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }
        private bool CourseDetailExists(int id)
        {
            return _context.courseDetails.Any(e => e.CourseDetailId == id);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var results = _context.courseDetails.Find(id);
            if (results == null)
            {
                return NotFound();
            }
            _context.courseDetails.Remove(results);
            try
            {
                _context.SaveChanges();
                return Ok(results);
            }
            catch
            {
                return BadRequest();
            }

        }
    }

}
  