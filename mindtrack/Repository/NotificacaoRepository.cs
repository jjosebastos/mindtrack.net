using Microsoft.EntityFrameworkCore;
using mindtrack.Connection;
using mindtrack.Model;

namespace mindtrack.Repository
{
    public class NotificacaoRepository : INotificacaoRepository
    {
        private readonly AppDbContext _context;

        public NotificacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Notificacao> AddAsync(Notificacao notificacao)
        {
            
            await _context.Notifications.AddAsync(notificacao);
            await _context.SaveChangesAsync();
            return notificacao;
        }

        public async Task DeleteAsync(Notificacao notificacao)
        {
            _context.Notifications.Remove(notificacao);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notificacao>> GetAllAsync(int page, int size)
        {
            return await _context.Notifications
                .AsNoTracking()
                .OrderBy(n => n.IdNotificacao)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<Notificacao?> GetByIdAsync(int id)
        {
            return await _context.Notifications.FirstOrDefaultAsync(n => n.IdNotificacao == id);
        }

        public async Task<Notificacao> UpdateAsync(Notificacao notificacao)
        {
            _context.Notifications.Update(notificacao);
            await _context.SaveChangesAsync();   
            return notificacao;
        }
    }
}
