using DBZapTend.Models;
using Microsoft.EntityFrameworkCore;

namespace DBZapTend.Repository
{
    public class PlanRepository : IPlanRepository
    {
        private readonly DbzapContext _context;

        public PlanRepository(DbzapContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Plan>> GetPlans()
        {
            return await _context.Plans
                 .ToListAsync();
        }
        public async Task<Plan> GetPlan(int id)
        {
            return await _context.Plans
                 .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Plan> CreatePlan(Plan plan)
        {
            _context.Plans.Add(plan);


            await _context.SaveChangesAsync();


            return plan;
        }
        public async Task<Plan> UpdatePlan(Plan plan)
        {
            if (plan is null)
                throw new ArgumentNullException(nameof(plan));

            _context.Plans.Entry(plan).State = EntityState.Modified;


            await _context.SaveChangesAsync();


            return plan;
        }
        public async Task<Plan> DeletePlan(int id)
        {
            var plan = await _context.Plans.FindAsync(id);

            if (plan is null)
                throw new ArgumentNullException(nameof(plan));


            _context.Plans.Remove(plan);

            await _context.SaveChangesAsync();

            return plan;
        }
    }
}
