using NutritionAssistant.Models.Entities;

namespace NutritionAssistant.Models.Repositories
{
    public interface IMealFollowRepository
    {
        Task<IEnumerable<MealFollow>> GetAllMealFollowsAsync();
        Task<MealFollow?> GetMealFollowByIdAsync(int id);
        Task AddMealFollowAsync(MealFollow mealFollow);
        Task UpdateMealFollowAsync(MealFollow mealFollow);
        Task DeleteMealFollowAsync(int id);
        Task<BaseValue?> GetBaseValueAsync();
    }
}
