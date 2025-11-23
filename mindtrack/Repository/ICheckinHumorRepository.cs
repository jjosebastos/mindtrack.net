using mindtrack.Model;

namespace mindtrack.Repository
{
    public interface ICheckinHumorRepository
    {

        Task<CheckinHumor> AddAsync(CheckinHumor checkinHumor);

        Task<CheckinHumor> UpdateAsync(CheckinHumor checkinHumor);

        Task<CheckinHumor> GetByIdAsync(int id);

        Task DeleteAsync(CheckinHumor checkinHumor);

        Task<IEnumerable<CheckinHumor>> GetAllAsync(int page, int size);
    }
}
