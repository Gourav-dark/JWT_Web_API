using JWT_Web_API.DBContextFile;
using JWT_Web_API.Models;
using JWT_Web_API.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly DBContextConn _context;
        public UsersController(DBContextConn context) 
        {
            _context = context;
        }
        [HttpGet]
        [Route("[Action]")]
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
                };
                _context.Users.Add(adduser);
                await _context.SaveChangesAsync();
            }
            return BadRequest("Email Already Exist");
        }
        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> Login(string email,string password)
        {
            var existuser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password==password);
            if (existuser != null)
            {
                return Ok(existuser.Id);
            }
            return BadRequest("User Not Found");
        }
    }
}
