using Books.core.Entities;
using Books.core.Service;
using Books.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetActivUsers()
        {
           
            return Ok(_service.GetActivUsers());
        }

        [HttpGet("{UserId}")]
        public IActionResult GetUserById(int UserId)
        {
            var user = _service.GetUserById(UserId);
            if (user == null)
                return NotFound("user not found or inactiv");
            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] Users newUser)
        {
         
            return Ok(_service.RegisterUser(newUser));
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] Users newUser)
        {
            var user = _service.UpdateUser(id, newUser);
            if (user == null)
                return NotFound("user not found");
            return Ok(user);
        }
        [HttpPost("deactivate/{id}")]
        public IActionResult DeactivateUser(int id) 
        {
        var user = _service.DeactivateUser(id);
            if (user == null)
                return NotFound("user not found");
          
            return Ok($"user {user.FullName} hes been marked as inactiv");
        }
    }
}
