using mindtrack.Model;

namespace mindtrack.Repository
{
    public interface IRecomendacaoIARepository
    {
        Task<RecomendacaoIA> AddAsync(RecomendacaoIA entity);
        Task<RecomendacaoIA> UpdateAsync(RecomendacaoIA entity);
        Task DeleteAsync(RecomendacaoIA entity);
        Task<RecomendacaoIA?> GetByIdAsync(int id);
    }
}
