using DBZapTend.Models;

namespace DBZapTend.Repository
{
    public interface IVariaveiRepository
    {
        Task<IEnumerable<Variavei>> GetVariaveis();

        Task<Variavei> GetVariavei(int id);

        Task<Variavei> CreateVariavei(Variavei variavei);

        Task<Variavei> UpdateVariavei(Variavei variavei);

        Task<Variavei> DeleteVariavei(int id);
    }
}
