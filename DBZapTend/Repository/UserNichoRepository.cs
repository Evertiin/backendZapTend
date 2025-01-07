using DBZapTend.Models;
using Microsoft.EntityFrameworkCore;

namespace DBZapTend.Repository
{
    public class UserNichoRepository : IUserNichoRepository
    {
        private readonly DbzapContext _context;

        public UserNichoRepository(DbzapContext context)
        {
            _context = context;
        }
        public async Task<UserNicho> CreateUserNicho(UserNicho userNicho)
        {
            if (userNicho is null)
                throw new ArgumentNullException(nameof(userNicho));


            _context.UserNichos.Add(userNicho);


            await _context.SaveChangesAsync();


            return userNicho;
        }

        public async Task<UserNicho> DeleteUserNicho(int id)
        {
            var userNicho = await _context.UserNichos.FindAsync(id);

            if (userNicho is null)
                throw new ArgumentNullException(nameof(userNicho));


            _context.UserNichos.Remove(userNicho);

            await _context.SaveChangesAsync();

            return userNicho;
        }

        public async Task<UserNicho> GetUserNicho(int id)
        {
            return await _context.UserNichos
                .FirstOrDefaultAsync(p => p.IdUserNichos == id);
        }

        public async Task<IEnumerable<UserNicho>> GetUserNichos()
        {
            return await _context.UserNichos
                .ToListAsync();
        }

        public async Task<UserNicho> UpdateUserNicho(UserNicho userNicho)
        {
            if (userNicho is null)
                throw new ArgumentNullException(nameof(userNicho));

            _context.UserNichos.Entry(userNicho).State = EntityState.Modified;


            await _context.SaveChangesAsync();


            return userNicho;
        }
    }
}
