using NutritionAssistant.Models.Entities;

namespace NutritionAssistant.Models.Services
{
    public interface IBaseValueService
    {
        Task<BaseValue?> GetBaseValueByIdAsync(int id);
        Task<IEnumerable<BaseValue>> GetAllBaseValuesAsync();
        Task AddBaseValueAsync(BaseValue baseValue);
        Task UpdateBaseValueAsync(BaseValue baseValue);
        Task DeleteBaseValueAsync(int id);
        Task<BaseValue?> GetBaseValueByProfilAsync(string profil);
        Task<(double calories, double proteins, double glucides, double lipids)?> CalculateNeedsAsync(string profil, double weightKg);

    }
}
