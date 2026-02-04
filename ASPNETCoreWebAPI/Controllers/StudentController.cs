using ASPNETCoreWebAPI.Data;
using ASPNETCoreWebAPI.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // dbcontext added
        private readonly CollegeDBContext _dbContext;

        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger, CollegeDBContext collegeDBContext)
        {
            _logger = logger;
            _dbContext = collegeDBContext;
        }

        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            _logger.LogInformation("Get all the student.");
            //using LINQ query
            var students = _dbContext.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Email = s.Email,
                Address = s.Address,
                DOB = s.DOB
            });

            return Ok(students);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("give a valid Id!");
                return BadRequest();
            }

            var student = _dbContext.Students.Where(s => s.Id == id).FirstOrDefault();
            if (student == null)
            {
                _logger.LogError("given id student not found!");
                return NotFound($"The student with id {id} not found!.");
            }

            //create a studentDTO here.
            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address,
                DOB = student.DOB
            };

            return Ok(studentDTO);
        }

        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = _dbContext.Students.Where(s => s.StudentName == name).FirstOrDefault();
            if (student == null)
                return NotFound($"The student with name {name} not found!.");

            //create a studentDTO here.
            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address,
                DOB = student.DOB
            };

            return Ok(studentDTO);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        {
            if (model == null)
                return BadRequest();

            Student student = new Student
            {
                StudentName = model.StudentName,
                Email = model.Email,
                Address = model.Address,
                DOB = model.DOB
            };

            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();

            model.Id = student.Id;

            return CreatedAtRoute("GetStudentById", new { id = model.Id }, model);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null || model.Id <= 0)
                return BadRequest();

            var existingStudent = _dbContext.Students.AsNoTracking().Where(s => s.Id == model.Id).FirstOrDefault();

            if (existingStudent == null)
                return NotFound();

            var newRecord = new Student()
            {
                Id = existingStudent.Id,
                StudentName = model.StudentName,
                Email = model.Email,
                Address = model.Address,
                DOB = model.DOB
            };

            _dbContext.Students.Update(newRecord);

            //existingStudent.StudentName = model.StudentName;
            //existingStudent.Email = model.Email;
            //existingStudent.Address = model.Address;
            //existingStudent.DOB = model.DOB;

            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                return BadRequest();

            var existingStudent = _dbContext.Students.Where(s => s.Id == id).FirstOrDefault();

            if (existingStudent == null)
                return NotFound();

            var studentDTO = new StudentDTO
            {
                Id = existingStudent.Id,
                StudentName = existingStudent.StudentName,
                Email = existingStudent.Email,
                Address = existingStudent.Address,
                DOB = existingStudent.DOB
            };

            patchDocument.ApplyTo(studentDTO, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            existingStudent.StudentName = studentDTO.StudentName;
            existingStudent.Email = studentDTO.Email;
            existingStudent.Address = studentDTO.Address;
            existingStudent.DOB = studentDTO.DOB;

            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest();

            var student = _dbContext.Students.Where(s => s.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound($"The student with id {id} not found!.");

            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();

            return Ok(true);
        }

    }
}
