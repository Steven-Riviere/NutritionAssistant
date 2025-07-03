using NutritionAssistant.Models.Entities;

namespace NutritionAssistant.Models.Services
{
    public interface IProfilActivityService
    {
        Task<IEnumerable<ProfilActivity>> GetAllAsync();
        Task<ProfilActivity?> GetByIdAsync(int id);
        Task AddAsync(ProfilActivity profilActivity);
        Task UpdateAsync(ProfilActivity profilActivity);
        Task DeleteAsync(int id);
    }
}
