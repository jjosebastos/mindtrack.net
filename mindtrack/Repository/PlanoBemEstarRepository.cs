using Microsoft.EntityFrameworkCore;
using mindtrack.Connection;
using mindtrack.Model;

namespace mindtrack.Repository
{
    public class PlanoBemEstarRepository : IPlanoBemEstarRepository
    {
  
        private readonly AppDbContext _context;

        public PlanoBemEstarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PlanoBemEstar> AddAsync(PlanoBemEstar planoBemEstar)
        {
            await _context.Planos.AddAsync(planoBemEstar);
            _context.SaveChanges();
            return planoBemEstar;
        }

        public async Task DeleteAsync(PlanoBemEstar planoBemEstar)
        {
            _context.Planos.Remove(planoBemEstar);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<PlanoBemEstar>> GetAllAsync(int page, int size)
        {
            throw new NotImplementedException();
        }

        public async Task<PlanoBemEstar?> GetByIdAsync(int id)
        {
            return await _context.Planos.FirstOrDefaultAsync(p => p.IdPlano == id);
        }

        public async Task<PlanoBemEstar> UpdateAsync(PlanoBemEstar planoBemEstar)
        {
            _context.Planos.Update(planoBemEstar);
            await _context.SaveChangesAsync();
            return planoBemEstar;
        }
    }
}
