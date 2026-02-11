using ASPNETCoreWebAPI.Data;
using ASPNETCoreWebAPI.Data.Repository;
using ASPNETCoreWebAPI.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ASPNETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICollegeRepository<Role> _roleRepository;
        private APIResponse _apiResponse;

        public RoleController(IMapper mapper, ICollegeRepository<Role> roleRepository)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _apiResponse = new();
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> CreateRoleAsync(RoleDTO dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest();

                Role role = _mapper.Map<Role>(dto);
                role.IsDeleted = false;
                role.CreatedDate = DateTime.Now;
                role.ModifiedDate = DateTime.Now;

                var result = await _roleRepository.CreateAsync(role);
                dto.Id = result.Id;
                _apiResponse.Data = dto;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                //return Ok(_apiResponse); // if GetRoleById endpoint is not created.
                return CreatedAtRoute("GetRoleById", new { id = dto.Id }, _apiResponse);

            }
            catch (Exception ex)
            {
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                _apiResponse.Errors.Add(ex.Message);
                return _apiResponse;
            }
        }

        [HttpGet]
        [Route("All", Name = "GetAllRoles")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> GetRolesAsync()
        {
            try
            {
                var roles = await _roleRepository.GetAllAsync();

                _apiResponse.Data = _mapper.Map<List<RoleDTO>>(roles);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                _apiResponse.Errors.Add(ex.Message);
                return _apiResponse;
            }
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetRoleById")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetRolesAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();

                var role = await _roleRepository.GetAsync(role => role.Id == id);
                if (role == null)
                    return NotFound($"The student with id {id} not found!.");

                _apiResponse.Data = _mapper.Map<RoleDTO>(role);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return _apiResponse;
            }
        }

        [HttpGet]
        [Route("{name:alpha}", Name = "GetRoleByName")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetRolesAsync(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    return BadRequest();

                var role = await _roleRepository.GetAsync(role => role.RoleName.ToLower() == name.ToLower());

                if (role == null)
                    return NotFound($"the role not found! with name: {name}");

                _apiResponse.Data = _mapper.Map<RoleDTO>(role);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                _apiResponse.Errors.Add(ex.Message);
                return _apiResponse;
            }
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> UpdateRoleAsync(RoleDTO dto)
        {
            try
            {
                if (dto == null || dto.Id <= 0)
                    return BadRequest();

                var existingRole = await _roleRepository.GetAsync(role => role.Id == dto.Id, true);

                if (existingRole == null)
                    return BadRequest($"Role not found with id: {dto.Id} to update");

                var newRole = _mapper.Map<Role>(dto);
                
                await _roleRepository.UpdateAsync(newRole);

                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.Data = newRole;

                return Ok(_apiResponse);

            }
            catch (Exception ex)
            {
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                _apiResponse.Errors.Add(ex.Message);
                return _apiResponse;
            }
        }

        [HttpDelete]
        [Route("Delete/{id:int}", Name = "DeleteRoleById")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteRoleAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();

                var role = await _roleRepository.GetAsync(role => role.Id == id);
               
                if (role == null)
                    return NotFound($"Role not found with id: {id} to delete");

                await _roleRepository.DeleteAsync(role);
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.Data = true;

                return Ok(_apiResponse);

            }
            catch (Exception ex)
            {
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                _apiResponse.Errors.Add(ex.Message);
                return _apiResponse;
            }
        }
    }
}
