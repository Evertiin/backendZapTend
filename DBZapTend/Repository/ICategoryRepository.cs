using DBZapTend.Models;

namespace DBZapTend.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategorys();

        Category GetCategory (int id);

        Category CreateCategory (Category category);

        Category UpdateCategory(Category category);

        Category DeleteCategory(int id);

    }
}
