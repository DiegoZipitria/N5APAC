using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PAC.BusinessLogic;
using PAC.Domain;
using PAC.IBusinessLogic;
using PAC.WebAPI.Filters;
using StudentDomain = PAC.Domain.Student;
namespace PAC.WebAPI
{
    [ApiController]
    [Route("Students")]
    public class StudentController : ControllerBase
    {
        public readonly IStudentLogic StudentLogic;
        public StudentController(IStudentLogic studentLogic) 
        {
            StudentLogic = studentLogic;
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]

        [HttpPost]
        public IActionResult PostStudentOk([FromBody] Student studentRequest)
        {
            StudentLogic.InsertStudents(studentRequest);
            return Created($"api/users/{studentRequest.Id}", studentRequest);
        }

        [HttpGet("{Id}")]
        [ServiceFilter(typeof(AuthorizationFilter))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult GetStudentyId([FromRoute] int Id)
        {
            Student student = StudentLogic.GetStudentById(Id);
            return Ok(student);
        }

        [HttpGet]
        [ServiceFilter(typeof(AuthorizationFilter))]
        [FilterNew(StudentDomain.Age)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult GetAllUsers()
        {
            IEnumerable<Student> users = StudentLogic.GetStudents();
            return Ok(users);
        }

    }
}
