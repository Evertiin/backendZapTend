using DBZapTend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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
            return await _context.Users.Include(u => u.Instances)
                .Include(u => u.Payments)
                .Include(u => u.Plans)
                .Include(u => u.UserNichos)
                .Include(u => u.ValoresVariaveis)
                .ToListAsync();
        }
        public async Task<User> GetUser(int id)
        {

            var userWithInstances = await _context.Users
             .Include(u => u.Instances)
             .Include(u => u.Payments) 
             .Include(u => u.Plans)    
             .Include(u => u.UserNichos) 
             .Include(u => u.ValoresVariaveis)
             .FirstOrDefaultAsync(u => u.Id == id);

            return userWithInstances;
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
