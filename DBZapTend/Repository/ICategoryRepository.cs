using DBZapTend.Models;

namespace DBZapTend.Repository
{
    public interface ICategoryRepository
    {
        Task <IEnumerable<Category>> GetCategorys();

        Task <Category> GetCategory (int id);

        Task <Category> CreateCategory (Category category);

        Task <Category> UpdateCategory(Category category);

        Task <Category> DeleteCategory(int id);

    }
}
