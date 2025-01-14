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
                .Include(u => u.Plans)
                .Include(u => u.UserNichos)
                .Include(u => u.ValoresVariaveis)
                .ToListAsync();
        }
        public async Task<User> GetUser(string id)
        {

            var userWithInstances = await _context.Users
             .Include(u => u.Instances)
             .Include(u => u.Payments) 
             .Include(u => u.Plans)    
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
        public async Task<User> UpdateUsers(string id,UpdateUserDto user)

        {
            var findUser = await _context.Users.FindAsync(id);

            if (findUser is null)
                throw new ArgumentNullException(nameof(findUser));

            
            if (user == null)
            {
                throw new ArgumentException("Usuário não encontrado.");
            }


            if (!string.IsNullOrWhiteSpace(user.Name))
            {
                findUser.Name = user.Name;
            }

            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                findUser.Email = user.Email;
            }

            if (user.CpfCnpj.HasValue) 
            {
                findUser.CpfCnpj = user.CpfCnpj.Value;
            }

            if (user.Telephone.HasValue)
            {
                findUser.Telephone = user.Telephone.Value;
            }

            if (!string.IsNullOrWhiteSpace(user.Adress))
            {
                findUser.Adress = user.Adress;
            }
            
            await _context.SaveChangesAsync();

            
            return findUser;
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

        public Task<User> UpdateUser(string id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
