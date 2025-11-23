using Microsoft.EntityFrameworkCore;
using mindtrack.Connection;
using mindtrack.Model;

namespace mindtrack.Repository
{

    public class RecomendacaoIARepository : IRecomendacaoIARepository
    {
        private readonly AppDbContext _context;

        public RecomendacaoIARepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RecomendacaoIA> AddAsync(RecomendacaoIA entity)
        {
            await _context.Recomendacoes.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RecomendacaoIA> UpdateAsync(RecomendacaoIA entity)
        {
            _context.Recomendacoes.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(RecomendacaoIA entity)
        {
            _context.Recomendacoes.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<RecomendacaoIA?> GetByIdAsync(int id)
        {
            return await _context.Recomendacoes.FirstOrDefaultAsync(x => x.IdRecomendacao == id);
        }
    }
}
