using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Data;
using MinimalApi.Models;

namespace MinimalApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly ApiContext _context;

        public UsersController(ApiContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<User> Get(int id) {
            try {
                var user = await _context.Users
                    .Where(u => u.id == id)
                    .FirstAsync();

                return user;
            } catch(Exception) {
                return new User();
            }
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll() {
            try {
                return await _context.Users
                    .ToListAsync();
            } catch(Exception) {
                return new List<User>();
            }
        }

        [HttpPost]
        public async Task<User> Add(User user) {
            var userAdded = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return userAdded.Entity;
        }

        [HttpPut]
        public async Task<bool> Update(User user) {
            var searched = await _context.Users
                .FindAsync(user.id);

            if (searched is null) {
                return false;
            }

            var modified = await _context.Users
                .Where(u => u.id == user.id)
                .FirstAsync();

            if (modified is null) {
                return false;
            }

            modified.firstname = user.firstname;
            modified.lastname = user.lastname;
            modified.phone = user.phone;
            modified.email = user.email;

            _context.Entry(modified).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        [HttpDelete]
        public async Task<bool> Delete(int id) {
            var searched = await _context.Users
                .Where(u => u.id == id)
                .FirstAsync();

            if (searched is null) {
                return false;
            }

            _context.Remove(searched);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}