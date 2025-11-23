using mindtrack.Model;

namespace mindtrack.Repository
{
    public interface INotificacaoRepository
    {
        Task<Notificacao> AddAsync(Notificacao notificacao);
        Task<Notificacao> UpdateAsync(Notificacao notificacao);
        Task<Notificacao> GetByIdAsync(int id);
        Task DeleteAsync(Notificacao notificacao);
        Task<IEnumerable<Notificacao>> GetAllAsync(int page, int size);
    }
}
