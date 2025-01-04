using DBZapTend.Models;

namespace DBZapTend.Repository
{
    public interface IInstanceRepository
    {
        Task<IEnumerable<Instance>> GetInstances();

        Task<Instance> GetInstance(int id);

        Task<Instance> CreateInstance(Instance instance);

        Task<Instance> UpdateInstance(Instance instance);

        Task<Instance> DeleteInstance(int id);
    }
}
