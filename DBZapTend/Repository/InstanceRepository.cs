using DBZapTend.Models;
using Microsoft.EntityFrameworkCore;

namespace DBZapTend.Repository
{
    public class InstanceRepository : IInstanceRepository
    {
        private readonly DbzapContext _context;

        public InstanceRepository(DbzapContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Instance>> GetInstances()
        {
            return await _context.Instances.ToListAsync();
        }

        public async Task<Instance> GetInstance(int id)
        {
            return await _context.Instances.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Instance> CreateInstance(Instance instance)
        {
            if (instance is null)
                throw new ArgumentNullException(nameof(instance));


            _context.Instances.Add(instance);


            await _context.SaveChangesAsync();


            return instance;
        }

        public async Task<Instance> UpdateInstance(Instance instance)
        {
            if (instance is null)
                throw new ArgumentNullException(nameof(instance));

            _context.Instances.Entry(instance).State = EntityState.Modified;


            await _context.SaveChangesAsync();


            return instance;
        }

        public async Task<Instance> DeleteInstance(int id)
        {
            var instance = await _context.Instances.FindAsync(id);

            if (instance is null)
                throw new ArgumentNullException(nameof(instance));


            _context.Instances.Remove(instance);

            await _context.SaveChangesAsync();

            return instance;
        }
    }
}
