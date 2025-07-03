using NutritionAssistant.Models.Entities;

namespace NutritionAssistant.Models.Repositories
{
    public interface IBaseValueRepository
    {
        Task<IEnumerable<BaseValue>> GetAllBaseValuesAsync();
        Task<BaseValue?> GetBaseValueByIdAsync(int id);
        Task AddBaseValueAsync(BaseValue baseValue);
        Task UpdateBaseValueAsync(BaseValue baseValue);
        Task DeleteBaseValueAsync(int id);
    }
}
