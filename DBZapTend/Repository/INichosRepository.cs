using DBZapTend.Models;

namespace DBZapTend.Repository
{
    public interface INichosRepository
    {
        Task<IEnumerable<Nicho>> GetNichos();

        Task<Nicho> GetNicho(int id);

        Task<Nicho> CreateNicho(Nicho nicho);

        Task<Nicho> UpdateNicho(Nicho nicho);

        Task<Nicho> DeleteNicho(int id);
    }
}
