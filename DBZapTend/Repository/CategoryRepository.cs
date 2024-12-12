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
            return await _context.Categories.ToListAsync();
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> CreateCategory(Category category)
        {
            
            _context.Categories.Add(category);

            
            await _context.SaveChangesAsync();

            
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Category DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

    }
}
