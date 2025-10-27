using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public static List<Users> users = new List<Users>
        {
            new Users{ UserId=45,FullName="ronit coen",Email="ronit.coen@gmail.com"
                ,Phone="0578965420",City="jerusalem",status=true},
            new Users{ UserId=46,FullName="david levi",Email="0578965423l@gmail.com"
                ,Phone="0578965423",City="afula",status=false},
            new Users{ UserId=47,FullName="shlomi bar",Email="a.bar@bizmail.co.il"
                ,Phone="0502487256",City="tel aviv",status=true}

        };

        [HttpGet]
        public IActionResult GetActivUsers()
        {
            var activUser=users.Where(u => u.status).ToList();
            return Ok(activUser);
        }

        [HttpGet("{UserId}")]
        public IActionResult GetUserById(int id)
        {
            var user = users.FirstOrDefault(u => u.UserId == id && u.status);
            if (user == null)
                return NotFound("user not found or inactiv");
            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] Users newUser)
        {
            newUser.UserId = users.Any() ? users.Max(u => u.UserId) + 1 : 1;
            newUser.status = true;
            users.Add(newUser);
            return Ok(newUser);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] Users newUser)
        {
            var user = users.FirstOrDefault(u => u.UserId == id);
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
        var user =users.FirstOrDefault(u=>u.UserId == id);
            if (user == null)
                return NotFound("user not found");
            user.status = false;
            return Ok("user {user.FullName} hes been marked as inactiv");
        }
    }
}
