using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IDataContext _context;
        public UsersController(IDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetActivUsers()
        {
            var activUser = _context.users.Where(u => u.status).ToList();
            return Ok(activUser);
        }

        [HttpGet("{UserId}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.users.FirstOrDefault(u => u.UserId == id && u.status);
            if (user == null)
                return NotFound("user not found or inactiv");
            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] Users newUser)
        {
            newUser.UserId = _context.users.Any() ? _context.users.Max(u => u.UserId) + 1 : 1;
            newUser.status = true;
            _context.users.Add(newUser);
            return Ok(newUser);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] Users newUser)
        {
            var user = _context.users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
                return NotFound("user not found");
            user.FullName = newUser.FullName;
            user.Email=newUser.Email;
            user.Phone = newUser.Phone;
            user.City=newUser.City;
            return Ok(user);
        }
        [HttpPost("deactivate/{id}")]
        public IActionResult deactivateUser(int id) 
        {
        var user = _context.users.FirstOrDefault(u=>u.UserId == id);
            if (user == null)
                return NotFound("user not found");
            user.status = false;
            return Ok("user {user.FullName} hes been marked as inactiv");
        }
    }
}
