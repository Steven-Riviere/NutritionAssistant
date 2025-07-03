using NutritionAssistant.Models.Entities;
using NutritionAssistant.Models.ViewModels;

namespace NutritionAssistant.Models.Services
{
    public interface IMealFollowService
    {
        Task<IEnumerable<MealFollow>> GetAllMealFollowsAsync();
        Task<MealFollow?> GetMealFollowByIdAsync(int id);
        Task AddMealFollowAsync(MealFollow mealFollow);
        Task UpdateMealFollowAsync(MealFollow mealFollow);
        Task DeleteMealFollowAsync(int id);
        MealFollowViewModel ToViewModel(MealFollow mealFollow);
        Task<NutritionSummaryViewModel> GetDailySummaryAsync(string profil, double weightKg);

    }
}
