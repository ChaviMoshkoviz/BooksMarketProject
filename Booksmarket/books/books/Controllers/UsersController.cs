using Microsoft.Extensions.Configuration;
using AutoMapper;
using Books.core.DTO;
using Books.core.Entities;
using Books.core.Service;
using Books.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UsersController(IUserService service, IMapper mapper,IConfiguration configuration)
        {
            _service = service;
            _mapper = mapper;
            _configuration = configuration;
        }
        [Authorize]
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            // שימוש בפונקציה החדשה שיצרנו בשכבת ה-Service
            var user = await _service.GetByEmailAndPassword(email, password);

            if (user != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, "Registered") // התפקיד שקבענו
        };

                // שימי לב לשימוש ב-Configuration כפי שהמורה הראתה
                var key = _configuration["JWT:Key"] ?? "YourFallbackVerySecretKey123456";
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }
            return Unauthorized("פרטי התחברות שגויים");
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task< IActionResult> UpdateUser(int id, [FromBody] PutUsersDTO newUser)
        {
            // בדיקה אופציונלית: האם המשתמש המחובר מעדכן את עצמו?
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != id.ToString())
            {
                return Forbid("אינך מורשה לעדכן פרטים של משתמש אחר");
            }
            var userEntity = _mapper.Map<Users>(newUser);
            var user = await _service.UpdateUser(id, userEntity);
            if (user == null)
                return NotFound("user not found");
            var userDto = _mapper.Map<UsersDTO>(user);
            return Ok(userDto);
        }
        [Authorize]
        [HttpPut("deactivate/{id}")]
        public async Task< IActionResult> ChangeUserStatus(int id)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != id.ToString())
            {
                return Forbid();
            }
            var user = await _service.ChangeUserStatus(id);

            if (user == null)
                return NotFound("user not found");
            var userDto = _mapper.Map<DeactivateUsersDTO>(user);

            return Ok(userDto);
        }
    
    }
}
