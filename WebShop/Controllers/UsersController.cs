#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using WebShop;
using WebShop.Data;

namespace WebShop.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        public UsersController(DataContext context, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.Include(c=>c.orders).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.Include(c => c.orders).FirstOrDefaultAsync(i=>i.id==id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/username/5
        [HttpGet("username/{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            var user = await _context.Users.Include(c => c.orders).FirstOrDefaultAsync(i => i.username == username);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        [AllowAnonymous]
        [HttpGet("updateadmin")]
        public ActionResult updateAdmin()
        {
            var user =  _context.Users.Include(c => c.orders).FirstOrDefault(i => i.username == "admin");

            return Ok(user);
        }

        //POST: api/Users/authenticate
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            var user = _context.Users.FirstOrDefault(i => i.username == userCred.Username);
            if(user== null)
            {
                return NotFound();
            }
            if (!BCrypt.Net.BCrypt.Verify(userCred.Password,user.password))
            {
                return Unauthorized ();
            }

            var token = jwtAuthenticationManager.Authenticate(userCred.Username, userCred.Password);
            
            return Ok(token);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var u = await _context.Users.SingleOrDefaultAsync(t=>t.username==user.username);
            if (u == null)
            {
                user.password = BCrypt.Net.BCrypt.HashPassword(user.password);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = user.id }, user);
            }
            else
            {
                return NotFound();
            }
        }

        private readonly RandomNumberGenerator _rng;

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.id == id);
        }
    }
}
