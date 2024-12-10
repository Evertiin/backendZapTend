using DBZapTend.Models;

namespace DBZapTend.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbzapContext _context;

        public CategoryRepository(DbzapContext context)
        {
              _context = context;
        }

        public IEnumerable<Category> GetCategorys()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Category CreateCategory(Category category)
        {
            throw new NotImplementedException();
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
