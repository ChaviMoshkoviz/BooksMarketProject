using AutoMapper;
using Books.core.DTO;
using Books.core.Entities;
using Books.core.Service;
using Books.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        public UsersController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetActivUsers()
        {
            var user = _service.GetActivUsers();
            return Ok(_mapper.Map<List<UsersDTO>>(user));
        }

        [HttpGet("{UserId}")]
        public IActionResult GetUserById(int UserId)
        {
            var user = _service.GetUserById(UserId);
            if (user == null)
                return NotFound("user not found or inactiv");
            var userDto = _mapper.Map<IEnumerable<UsersDTO>>(user);
            return Ok(userDto);
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] Users newUser)
        {
         
            return Ok(_service.RegisterUser(newUser));
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] PutUsersDTO newUser)
        {
            var listingEntity = _mapper.Map<Users>(newUser);
            var user = _service.UpdateUser(id, listingEntity);
            if (user == null)
                return NotFound("user not found");
            var userDto = _mapper.Map<UsersDTO>(user);
            return Ok(userDto);
        }
     
        [HttpPut("deactivate/{id}")]
        public IActionResult DeactivateUser(int id)
        {
            var user = _service.DeactivateUser(id);

            if (user == null)
                return NotFound("user not found");
            var userDto = _mapper.Map<DeactivateUsersDTO>(user);

            return Ok(userDto);
        }
    
    }
}
