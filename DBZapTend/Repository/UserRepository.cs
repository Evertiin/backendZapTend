using DBZapTend.Models;
using Microsoft.EntityFrameworkCore;

namespace DBZapTend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbzapContext _context;

        public UserRepository(DbzapContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);


            await _context.SaveChangesAsync();


            return user;
        }
        public async Task<User> UpdateUser(User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            _context.Users.Entry(user).State = EntityState.Modified;


            await _context.SaveChangesAsync();


            return user;
        }
        public async Task<User> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user is null)
                throw new ArgumentNullException(nameof(user));


            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return user;
        }

        
    }
}
