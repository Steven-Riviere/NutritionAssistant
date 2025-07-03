using NutritionAssistant.Models.Entities;

namespace NutritionAssistant.Models.Repositories
{
    public interface IFoodRepository
    {
    Task<IEnumerable<Food>> GetAllFoodsAsync();
    Task<Food?> GetFoodByIdAsync(int id);
    Task AddFoodAsync(Food food);
    Task UpdateFoodAsync(Food food);
    Task DeleteFoodAsync(int id);
    }
}
