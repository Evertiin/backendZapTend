using DBZapTend.Models;

namespace DBZapTend.Repository
{
    public interface IUserNichoRepository
    {
        Task<IEnumerable<UserNicho>> GetUserNichos();

        Task<UserNicho> GetUserNicho(int id);

        Task<UserNicho> CreateUserNicho(UserNicho userNicho);

        Task<UserNicho> UpdateUserNicho(UserNicho userNicho);

        Task<UserNicho> DeleteUserNicho(int id);
    }
}
