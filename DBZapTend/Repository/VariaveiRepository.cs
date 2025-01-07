using DBZapTend.Models;
using Microsoft.EntityFrameworkCore;

namespace DBZapTend.Repository
{
    public class VariaveiRepository : IVariaveiRepository
    {
        private readonly DbzapContext _context;

        public VariaveiRepository(DbzapContext context)
        {
            _context = context;
        }
        public async Task<Variavei> CreateVariavei(Variavei variavei)
        {
            if (variavei is null)
                throw new ArgumentNullException(nameof(variavei));


            _context.Variaveis.Add(variavei);


            await _context.SaveChangesAsync();


            return variavei;
        }

        public async Task<Variavei> DeleteVariavei(int id)
        {
            var variavei = await _context.Variaveis.FindAsync(id);

            if (variavei is null)
                throw new ArgumentNullException(nameof(variavei));


            _context.Variaveis.Remove(variavei);

            await _context.SaveChangesAsync();

            return variavei;
        }

        public async Task<Variavei> GetVariavei(int id)
        {
            return await _context.Variaveis
               .Include(u => u.ValoresVariaveis)
               .FirstOrDefaultAsync(p => p.IdVariaveis == id);
        }

        public async Task<IEnumerable<Variavei>> GetVariaveis()
        {
            return await _context.Variaveis
               .Include(u => u.ValoresVariaveis)
               .ToListAsync();
        }
        public async Task<Variavei> UpdateVariavei(Variavei variavei)
        {
            if (variavei is null)
                throw new ArgumentNullException(nameof(variavei));

            _context.Variaveis.Entry(variavei).State = EntityState.Modified;


            await _context.SaveChangesAsync();


            return variavei;
        }
    }
}
