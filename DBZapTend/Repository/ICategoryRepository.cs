using DBZapTend.Models;

namespace DBZapTend.Repository
{
    public interface ICategoryRepository
    {
        Task <IEnumerable<Category>> GetCategorys();

        Category GetCategory (int id);

        Task <Category> CreateCategory (Category category);

        Category UpdateCategory(Category category);

        Category DeleteCategory(int id);

    }
}
