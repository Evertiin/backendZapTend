using DBZapTend.Models;

namespace DBZapTend.Repository
{
    public interface IValoresVariaveiRepository
    {
        Task<IEnumerable<ValoresVariavei>> GetValoresVariaveis();

        Task<ValoresVariavei> GetValoresVariavei(int id);

        Task<ValoresVariavei> CreateValoresVariaveis(ValoresVariavei valores);

        Task<ValoresVariavei> UpdateValoresVariaveis(ValoresVariavei valores);

        Task<ValoresVariavei> DeleteValoresVariaveis(int id);
    }
}
