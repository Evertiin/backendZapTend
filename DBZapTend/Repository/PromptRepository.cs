using DBZapTend.Models;
using Microsoft.EntityFrameworkCore;

namespace DBZapTend.Repository
{
    public class PromptRepository : IPromptRepository
    {
        private readonly DbzapContext _context;

        public PromptRepository(DbzapContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prompt>> GetPrompts()
        {
            return await _context.Prompts
               .Include(u => u.ValoresVariaveis)
               .Include(u => u.Variaveis)
               .ToListAsync();
        }
        public async Task<Prompt> GetPrompt(int id)
        {
            return await _context.Prompts
              .Include(u => u.ValoresVariaveis)
              .Include(u => u.Variaveis)
              .FirstOrDefaultAsync(u => u.IdPrompts == id);
        }
        public async Task<Prompt> CreatePrompt(Prompt prompt)
        {
            _context.Prompts.Add(prompt);


            await _context.SaveChangesAsync();


            return prompt;
        }
        public async Task<Prompt> UpdatePrompt(Prompt prompt)
        {
            if (prompt is null)
                throw new ArgumentNullException(nameof(prompt));

            _context.Prompts.Entry(prompt).State = EntityState.Modified;


            await _context.SaveChangesAsync();


            return prompt;
        }

        public async Task<Prompt> DeletePrompt(int id)
        {
            var prompt = await _context.Prompts.FindAsync(id);

            if (prompt is null)
                throw new ArgumentNullException(nameof(prompt));


            _context.Prompts.Remove(prompt);

            await _context.SaveChangesAsync();

            return prompt;
        }

    }
}
