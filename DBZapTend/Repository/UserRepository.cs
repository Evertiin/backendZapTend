using DBZapTend.DTO;
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
                .Include(u => u.Plan)
                .Include(u => u.UserNichos)
                .Include(u => u.ValoresVariaveis)
                .ToListAsync();
        }
        public async Task<User> GetUser(string id)
        {

            var userWithInstances = await _context.Users
             .Include(u => u.Instances)
             .Include(u => u.Payments) 
             .Include(u => u.Plan)    
             .Include(u => u.UserNichos) 
             .Include(u => u.ValoresVariaveis)
             .FirstOrDefaultAsync(u => u.IdAutentication == id);

            return userWithInstances;
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);


            await _context.SaveChangesAsync();


            return user;
        }
        public async Task<User> UpdateUsers(User user)

        {

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
           
        }
        public async Task<User> DeleteUser(string id)
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
