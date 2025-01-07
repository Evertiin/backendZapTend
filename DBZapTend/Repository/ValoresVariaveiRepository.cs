using DBZapTend.Models;
using Microsoft.EntityFrameworkCore;

namespace DBZapTend.Repository
{
    public class ValoresVariaveiRepository : IValoresVariaveiRepository
    {
        private readonly DbzapContext _context;

        public ValoresVariaveiRepository(DbzapContext context)
        {
            _context = context;
        }

        public async Task<ValoresVariavei> CreateValoresVariaveis(ValoresVariavei valores)
        {
            if (valores is null)
                throw new ArgumentNullException(nameof(valores));


            _context.ValoresVariaveis.Add(valores);


            await _context.SaveChangesAsync();


            return valores;
        }

        public async Task<ValoresVariavei> DeleteValoresVariaveis(int id)
        {
            var valores = await _context.ValoresVariaveis.FindAsync(id);

            if (valores is null)
                throw new ArgumentNullException(nameof(valores));


            _context.ValoresVariaveis.Remove(valores);

            await _context.SaveChangesAsync();

            return valores;
        }
        public async Task<IEnumerable<ValoresVariavei>> GetValoresVariaveis()
        {
            return await _context.ValoresVariaveis
                .ToListAsync();
        }

        public async Task<ValoresVariavei> GetValoresVariavei(int id)
        {
            return await _context.ValoresVariaveis
                .FirstOrDefaultAsync(p => p.IdValoresVariaveis == id);
        }
        public async Task<ValoresVariavei> UpdateValoresVariaveis(ValoresVariavei valores)
        {
            if (valores is null)
                throw new ArgumentNullException(nameof(valores));

            _context.ValoresVariaveis.Entry(valores).State = EntityState.Modified;


            await _context.SaveChangesAsync();


            return valores;
        }
    }
}
