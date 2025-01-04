using DBZapTend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBZapTend.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbzapContext _context;

        public CategoryRepository(DbzapContext context)
        {
              _context = context;
        }

        public async Task <IEnumerable<Category>> GetCategorys()
        {
            return await _context.Categories
                .Include(u => u.Nichos)
                .ToListAsync();
        }

        public  async Task <Category> GetCategory(int id) 
        {
           return await _context.Categories
                .Include(u => u.Nichos)
                .FirstOrDefaultAsync(p => p.IdCategory == id);

           
        }

        public async Task<Category> CreateCategory(Category category)
        {
            if (category is null) 
               throw new ArgumentNullException(nameof(category));
            
            
            _context.Categories.Add(category);

            
            await _context.SaveChangesAsync();

            
            return category;
        }

        public async Task <Category> UpdateCategory(Category category)
        {
            if (category is null)
                throw new ArgumentNullException(nameof(category));

            _context.Categories.Entry(category).State = EntityState.Modified;


            await _context.SaveChangesAsync();


            return category;
        }

        public async Task <Category> DeleteCategory(int id)
        {
            var category =  await _context.Categories.FindAsync(id);

            if (category is null)
                throw new ArgumentNullException(nameof(category));


            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return category;
        }

    }
}
