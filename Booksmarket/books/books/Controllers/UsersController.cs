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
        public async Task< IActionResult> GetActivUsers()
        {
            var user =await _service.GetActivUsers();
            return Ok(_mapper.Map<IEnumerable<UsersDTO>>(user));
        }

        [HttpGet("{UserId}")]
        public async Task< IActionResult> GetUserById(int UserId)
        {
            var user =await _service.GetUserById(UserId);
            if (user == null)
                return NotFound("user not found or inactiv");
            var userDto = _mapper.Map<UsersDTO>(user);
         
            return Ok(userDto);
        }

        [HttpPost("register")]
        public async Task< IActionResult> RegisterUser([FromBody] RegisterUserDTO newUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // יחזיר פירוט על השדות החסרים
            }
            // אבטחה: מיפוי מה-DTO לישות כדי למנוע הזרקת שדות רגישים
            var userEntity = _mapper.Map<Users>(newUserDto);

            var createdUser = await _service.RegisterUser(userEntity);

            // החזרת התוצאה כ-DTO (מומלץ להשתמש ב-CreatedAtAction)
            var resultDto = _mapper.Map<UsersDTO>(createdUser);
            return CreatedAtAction(nameof(GetUserById), new { userId = resultDto.UserId }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task< IActionResult> UpdateUser(int id, [FromBody] PutUsersDTO newUser)
        {
            var userEntity = _mapper.Map<Users>(newUser);
            var user = await _service.UpdateUser(id, userEntity);
            if (user == null)
                return NotFound("user not found");
            var userDto = _mapper.Map<UsersDTO>(user);
            return Ok(userDto);
        }
     
        [HttpPut("deactivate/{id}")]
        public async Task< IActionResult> ChangeUserStatus(int id)
        {
            var user = await _service.ChangeUserStatus(id);

            if (user == null)
                return NotFound("user not found");
            var userDto = _mapper.Map<DeactivateUsersDTO>(user);

            return Ok(userDto);
        }
    
    }
}
