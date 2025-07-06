using LearnHubBackendDotNet.Data;
using LearnHubBackendDotNet.DTO;
using LearnHubBackendDotNet.Exceptions;
using LearnHubBackendDotNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnHubBackendDotNet.Controllers
{
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    //[ApiVersion("1.0")]
    //[ApiVersion("2.0")]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(AppDbContext context, ILogger<CoursesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //GET: api/Courses
       [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDetailsDto>>> GetCourses()
        {

            //_logger.LogInformation("Fetching all courses from db");
            if (_context.Courses == null)
            {
                return NotFound();

            }
            return await _context.Courses.Select(C => new CourseDetailsDto
            {
                id = C.Id,
                AuthorEmail = C.Email,
                author = C.author,
                category = C.category,
                image = C.image,
                courseName = C.name,
                price = C.price,
                rating = C.rating,
                students = C.students
            }).ToListAsync();
        }
        #region API Versioning

        //[HttpGet]
        //[MapToApiVersion("1.0")]
        //public async Task<ActionResult<IEnumerable<CourseDetailsDto>>> GetCoursesV1()
        //{

        //  _logger.LogInformation("Fetching all courses from db");
        //  if (_context.Courses == null)
        //  {
        //      return NotFound();

        //  }
        //    return await _context.Courses.Select(C=>new CourseDetailsDto
        //    {
        //        id = C.Id,
        //        AuthorEmail = C.Email,
        //        author = C.author,
        //        category = C.category,
        //        image = C.image,
        //        courseName = C.name,
        //        price = C.price,
        //        rating = C.rating,
        //        students = C.students
        //    }).ToListAsync();
        //}

        //[HttpGet]
        //[MapToApiVersion("2.0")]
        //public async Task<ActionResult<IEnumerable<CourseDetailsDto>>> GetCoursesV2()
        //{

        //    _logger.LogInformation("Fetching all courses from db");
        //    if (_context.Courses == null)
        //    {
        //        return NotFound();

        //    }
        //    return await _context.Courses.Select(C => new CourseDetailsDto
        //    {
        //        id = C.Id,
        //        AuthorEmail = C.Email,
        //        author = C.author,
        //        category = "API Versioning",
        //    }).ToListAsync();
        //} 
        #endregion

        // GET: api/Courses/Category
        [HttpGet("Category")]
        public async Task<ActionResult<IEnumerable<CourseDetailsDto>>> GetCoursesByCategory([FromHeader(Name ="Category")] string Category)
        {   
            if (string.IsNullOrEmpty(Category))
            {
                return BadRequest("Category parameter is required.");
            }

            return await _context.Courses.Where(C=>C.category==Category).
            Select(C => new CourseDetailsDto
            {
                id = C.Id,
                AuthorEmail = C.Email,
                author = C.author,
                category = C.category,
                image = C.image,
                courseName = C.name,
                price = C.price,
                rating = C.rating,
                students = C.students
            }).ToListAsync();
        }

        // GET: api/Courses/Author
        [HttpGet("Author")]
        public async Task<ActionResult<IEnumerable<CourseDetailsDto>>> GetCoursesByAuthor([FromHeader(Name = "Author")] string Author)
        {
            if (string.IsNullOrEmpty(Author))
            {
                return BadRequest("Category parameter is required.");
            }

            return await _context.Courses.Where(C => C.author == Author).
            Select(C => new CourseDetailsDto
            {
                id = C.Id,
                AuthorEmail = C.Email,
                author = C.author,
                category = C.category,
                image = C.image,
                courseName = C.name,
                price = C.price,
                rating = C.rating,
                students = C.students
            }).ToListAsync();
        }


        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
          if (_context.Courses == null)
          {
              return NotFound();
          }
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
          if (_context.Courses == null)
          {
              return Problem("Entity set 'AppDbContext.Courses'  is null.");
          }
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();


            // return CreatedAtAction("GetCourse", new { id = course.Id }, course);
            return Ok("Success");
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
