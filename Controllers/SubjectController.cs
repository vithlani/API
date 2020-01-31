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
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : Controller
    {
        private readonly CourseContext _context;
        Merger merger;
        public SubjectController(CourseContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET:api/subject
        public IActionResult GetSubjects()
        {
            try
            {

                /*// var co = merger.courseDetail.CourseDetailId;
                 var subjects = _context.subjects.ToList();
                 var subjectList = new List<Merger>();
                 foreach (var sub in subjects)
                 {
                     var subject = new Merger();
                     subject.CourseDetailId = sub.CourseDetailId;

                     var course =  _context.courseDetails.Find(sub.CourseDetailId);
                     subject.CourseName = course.Name;
                     subjectList.Add(subject);
                 }
                 return Ok(subjectList);*/
                var subject = _context.subjects.ToList();
                return Ok(subject);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult AddSubject([FromBody]Subject subject)
        {

            try
            {
                _context.subjects.Add(subject);
                _context.SaveChanges();
                return CreatedAtAction("GetSubjects", subject);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
      public IActionResult GetSubject(int id)
        {
            try
            {
                var subject = _context.subjects.Find(id);
                return Ok(subject);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}/subbyid")]
        public IActionResult GetSubjectsByCId(int id)
        {
            var courses = from c in _context.courseDetails
                          select c;
            try
            {
  
                var subject = _context.subjects.Where(i => i.CourseDetailId == id).ToList();
                return Ok(subject);

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("id")]

        [HttpPut("{id}")]
        public IActionResult UpdateSubject(int id, Subject subject)
        {
            if (id != subject.SubjectId)
            {
                return BadRequest();
            }
            _context.Entry(subject).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
                return Ok(subject);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectDetailExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }
     
        private bool SubjectDetailExist(int id)
        {
            throw new NotImplementedException();
        }
      //  [HttpDelete("{id}")]
       /* public IActionResult DeleteSubject(int id)
        {
            var result = _context.subjects.Find(id);
            if (result == null)
            {
                return NotFound();
            }

            _context.subjects.Remove(result);
          
            try 
            {
                _context.SaveChanges();
                return Ok(result);
            }
            catch
            { 
                return BadRequest();
            }
        }*/

        [HttpDelete]
        public IActionResult DeleteSubject([FromQuery] int[] subids)
        {
            for (int i = 0; i < subids.Length; i++)
            {
                var result = _context.subjects.Find(subids[i]);
                if (result == null) 
                { 
                    return NotFound();
                }
                _context.subjects.Remove(result);
            }
            _context.SaveChanges();
            return Ok();
        }

    }
}