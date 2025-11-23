using Microsoft.EntityFrameworkCore;
using mindtrack.Connection;
using mindtrack.Model;

namespace mindtrack.Repository
{
    public class CheckinHumorRepository : ICheckinHumorRepository
    {

        private readonly AppDbContext _context;

        public CheckinHumorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CheckinHumor> AddAsync(CheckinHumor checkinHumor)
        {
            await _context.Checkins.AddAsync(checkinHumor);
            await _context.SaveChangesAsync();
            return checkinHumor;
        }

        public async Task DeleteAsync(CheckinHumor checkinHumor)
        {
            _context.Checkins.Remove(checkinHumor);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CheckinHumor>> GetAllAsync(int page, int size)
        {
            return await _context.Checkins
                .AsNoTracking()
                .OrderBy(u => u.IdCheckin)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<CheckinHumor?> GetByIdAsync(int id)
        {
            return await _context.Checkins.AsNoTracking()
                                 .FirstOrDefaultAsync(c => c.IdCheckin == id);
        }

        public async Task<CheckinHumor> UpdateAsync(CheckinHumor checkinHumor)
        {
            _context.Checkins.Update(checkinHumor);
            await _context.SaveChangesAsync();
            return checkinHumor;
        }
    }
}
