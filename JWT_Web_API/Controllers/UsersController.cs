using JWT_Web_API.DBContextFile;
using JWT_Web_API.Models;
using JWT_Web_API.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly DBContextConn _context;
        private readonly IConfiguration _config;

        public UsersController(DBContextConn context,IConfiguration config) 
        {
            _context = context;
            this._config = config;
        }
        [HttpGet]
        [Route("[Action]")]
        //[Authorize]
        public async Task<IActionResult> UsersList()
        {
            if(_context.Users==null)
            {
                return NotFound("Database is Empty");
            }
            return Ok(await _context.Users.ToListAsync());
        }
        [HttpPost]
        [Route("[Action]")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(ViewUser user)
        {
            var Existuser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (Existuser==null)
            {
                var adduser=new User()
                {
                    Id=Guid.NewGuid(),
                    Email = user.Email,
                    UserName=user.UserName,
                    Password=user.Password,
                    Role=user.Role, 
                };
                _context.Users.Add(adduser);
                await _context.SaveChangesAsync();
                return Ok("Succesfully");
            }
            return BadRequest("Email Already Exist");
        }
        [HttpGet]
        [Route("[Action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email,string password)
        {
            var existuser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password==password);
            if (existuser != null)
            {
                var securityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
                var credential = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim(ClaimTypes.Email,email),
                    new Claim(ClaimTypes.Role,existuser.Role)
                };
                var token = new JwtSecurityToken(
                    issuer: _config["JWT:Issuer"],
                    audience: _config["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(2),
                    signingCredentials: credential
                    );
                var jwt=new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(jwt);
            }
            return BadRequest("User Not Found");
        }
    }
}
