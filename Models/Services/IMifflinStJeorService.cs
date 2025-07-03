using NutritionAssistant.Models.Entities;

namespace NutritionAssistant.Models.Services
{
    public interface IMifflinStJeorService
    {
        Task<IEnumerable<MifflinStJeor>> GetAllMifflinStJeorsAsync();
        Task<MifflinStJeor?> GetMifflinStJeorByIdAsync(int id);
        Task AddMifflinStJeorAsync(MifflinStJeor entity);
        Task UpdateMifflinStJeorAsync(MifflinStJeor entity);
        Task DeleteMifflinStJeorAsync(int id);
        double CalculateBMR(MifflinStJeor data);
        Task<double> CalculateDailyCaloriesAsync(int mifflinId, int profilActivityId);
    }
}
