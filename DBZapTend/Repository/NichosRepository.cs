using DBZapTend.Models;
using Microsoft.EntityFrameworkCore;

namespace DBZapTend.Repository
{
    public class NichosRepository : INichosRepository
    {
        private readonly DbzapContext _context;

        public NichosRepository(DbzapContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Nicho>> GetNichos()
        {
            return await _context.Nichos
                .Include(u => u.Prompts)
                .Include(u => u.UserNichos)
                .ToListAsync();
        }

        public async Task<Nicho> GetNicho(int id)
        {
            return await _context.Nichos
                .Include(u => u.Prompts)
                .Include(u => u.UserNichos)
                .FirstOrDefaultAsync(u => u.IdNichos == id);
        }
        public async Task<Nicho> CreateNicho(Nicho nicho)
        {
            _context.Nichos.Add(nicho);


            await _context.SaveChangesAsync();


            return nicho;
        }

        public async Task<Nicho> UpdateNicho(Nicho nicho)
        {
            if (nicho is null)
                throw new ArgumentNullException(nameof(nicho));

            _context.Nichos.Entry(nicho).State = EntityState.Modified;


            await _context.SaveChangesAsync();


            return nicho;
        }

        public async Task<Nicho> DeleteNicho(int id)
        {
            var nicho = await _context.Nichos.FindAsync(id);

            if (nicho is null)
                throw new ArgumentNullException(nameof(nicho));


            _context.Nichos.Remove(nicho);

            await _context.SaveChangesAsync();

            return nicho;
        }
    }
}
