using ASPNETCoreWebAPI.Data;
using ASPNETCoreWebAPI.Model;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ASPNETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        //automapper
        private readonly IMapper _mapper;

        // dbcontext added
        private readonly CollegeDBContext _dbContext;

        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger, CollegeDBContext collegeDBContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = collegeDBContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsAsync()
        {
            _logger.LogInformation("Get all the student.");
            //using LINQ query
            var students = await _dbContext.Students.ToListAsync();

            //automatic copy data one class to another class
            var studentDTOData = _mapper.Map<List<StudentDTO>>(students);

            return Ok(studentDTOData);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> GetStudentByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("give a valid Id!");
                return BadRequest();
            }

            var student = await _dbContext.Students.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (student == null)
            {
                _logger.LogError("given id student not found!");
                return NotFound($"The student with id {id} not found!."); 
            }

            //create a studentDTO here.
            var studentDTO = _mapper.Map<StudentDTO>(student);

            return Ok(studentDTO);
        }

        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> GetStudentByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = await _dbContext.Students.Where(s => s.StudentName == name).FirstOrDefaultAsync();
            if (student == null)
                return NotFound($"The student with name {name} not found!.");

            //create a studentDTO here.
            var studentDTO = _mapper.Map<StudentDTO>(student);

            return Ok(studentDTO);
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> CreateStudentAsync([FromBody] StudentDTO dto)
        {
            if (dto == null)
                return BadRequest();

            Student student = _mapper.Map<Student>(dto);

            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();

            dto.Id = student.Id;

            return CreatedAtRoute("GetStudentById", new { id = dto.Id }, dto);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateStudentAsync([FromBody] StudentDTO dto)
        {
            if (dto == null || dto.Id <= 0)
                return BadRequest();

            var existingStudent = await _dbContext.Students.AsNoTracking().Where(s => s.Id == dto.Id).FirstOrDefaultAsync();

            if (existingStudent == null)
                return NotFound();

            var newRecord = _mapper.Map<Student>(dto);

            _dbContext.Students.Update(newRecord);

            //existingStudent.StudentName = model.StudentName;
            //existingStudent.Email = model.Email;
            //existingStudent.Address = model.Address;
            //existingStudent.DOB = model.DOB;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateStudentPartialAsync(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
                return BadRequest();

            var existingStudent = await _dbContext.Students.AsNoTracking().Where(s => s.Id == id).FirstOrDefaultAsync();

            if (existingStudent == null)
                return NotFound();

            var studentDTO = _mapper.Map<StudentDTO>(existingStudent);

            patchDocument.ApplyTo(studentDTO, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            existingStudent = _mapper.Map<Student>(studentDTO);

            _dbContext.Students.Update(existingStudent);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteStudentAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            var student = await _dbContext.Students.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (student == null)
                return NotFound($"The student with id {id} not found!.");

            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();

            return Ok(true);
        }

    }
}
