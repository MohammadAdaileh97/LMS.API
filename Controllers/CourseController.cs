using LMS.Core.Data;
using LMS.Core.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }
       
        [HttpGet]
        public IActionResult GETALLCOURSES()
        {
            var courses = courseService.GETALLCOURSES();
            return Ok(courses);
        }
        [HttpPost]
        public IActionResult CreateCourse([FromBody] Course course)
        {
            courseService.CreateCourse(course);
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateCourse([FromBody] Course course)
        {
            courseService.UpdateCourse(course);
            return NoContent();
        }
        [HttpDelete]
        public IActionResult DeleteCourse(int id)
        {
            courseService.DeleteCourse(id);
            return Ok();
        }
        [HttpGet]
        [Route("GetById{id}")]
        public IActionResult GetCourseById(int id)
        {
            var course = courseService.GetCourseById(id);
            return Ok(course);
        }
        
        [HttpPost] // upload image
        [Route("Upload-Image")]
        public string UploadImage()
        {
            var image = Request.Form.Files[0];
            
            var ImageName = Guid.NewGuid().ToString() + "_" + image.FileName; // save the image in the folder of images
            var fullpath = Path.Combine("Images", ImageName);
            using (var stream = new FileStream(fullpath, FileMode.Create))
            {
                image.CopyTo(stream);
            }
            return ImageName; 
        }
        //DTOs are used to encapsulate the data that is sent between the layers of an application
        [HttpGet]
        [Route("Search")]
        public IActionResult SearchCourseByname(string courseName)
        {
           var coursesDto = courseService.SearchCourseByname(courseName);
            return Ok(coursesDto);
        }

    }
}
