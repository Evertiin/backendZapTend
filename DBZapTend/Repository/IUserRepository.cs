using DBZapTend.DTO;
using DBZapTend.Models;

namespace DBZapTend.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();

        Task<User> GetUser(string id);

        Task<User> CreateUser(User user);

        Task<User> UpdateUsers(string id,UpdateUserDto user);

        Task<User> DeleteUser(string id);
    }
}
