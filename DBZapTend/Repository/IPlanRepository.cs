using DBZapTend.Models;

namespace DBZapTend.Repository
{
    public interface IPlanRepository
    {
        Task<IEnumerable<Plan>> GetPlans();

        Task<Plan> GetPlan(int id);

        Task<Plan> CreatePlan(Plan plan);

        Task<Plan> UpdatePlan(Plan plan);

        Task<Plan> DeletePlan(int id);
    }
}
