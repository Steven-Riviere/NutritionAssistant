using NutritionAssistant.Models.Entities;

namespace NutritionAssistant.Models.Repositories
{
    public interface IProfilActivityRepository
    {
        Task<IEnumerable<ProfilActivity>> GetAllProfilActivitiesAsync();
        Task<ProfilActivity?> GetProfilActivityByIdAsync(int id);
        Task AddProfilActivityAsync(ProfilActivity profilActivity);
        Task UpdateProfilActivityAsync(ProfilActivity profilActivity);
        Task DeleteProfilActivityAsync(int id);
    }
}
