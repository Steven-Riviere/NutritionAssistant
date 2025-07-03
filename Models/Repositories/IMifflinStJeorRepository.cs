using NutritionAssistant.Models.Entities;

namespace NutritionAssistant.Models.Repositories
{
    public interface IMifflinStJeorRepository
    {
        Task<IEnumerable<MifflinStJeor>> GetAllMifflinStJeorsAsync();
        Task<MifflinStJeor?> GetMifflinStJeorByIdAsync(int id);
        Task AddMifflinStJeorAsync(MifflinStJeor mifflinStJeor);
        Task UpdateMifflinStJeorAsync(MifflinStJeor mifflinStJeor);
        Task DeleteMifflinStJeorAsync(int id);
    }
}
