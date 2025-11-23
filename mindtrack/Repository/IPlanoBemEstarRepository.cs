using mindtrack.Model;

namespace mindtrack.Repository
{
    public interface IPlanoBemEstarRepository
    {
        Task<PlanoBemEstar> AddAsync(PlanoBemEstar planoBemEstar);
        Task<PlanoBemEstar> UpdateAsync(PlanoBemEstar planoBemEstar);
        Task<PlanoBemEstar> GetByIdAsync(int id);
        Task DeleteAsync(PlanoBemEstar planoBemEstar);
        Task<IEnumerable<PlanoBemEstar>> GetAllAsync(int page, int size);
    }
}
