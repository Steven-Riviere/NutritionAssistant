using NutritionAssistant.Models.Entities;
using NutritionAssistant.Models.Repositories;

namespace NutritionAssistant.Models.Services
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;
        public FoodService(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }
        public async Task<IEnumerable<Food>> GetAllFoodsAsync()
        {
            return await _foodRepository.GetAllFoodsAsync();
        }
        public async Task<Food?> GetFoodByIdAsync(int id)
        {
            return await _foodRepository.GetFoodByIdAsync(id);
        }
        public async Task AddFoodAsync(Food food)
        {
            await _foodRepository.AddFoodAsync(food);
        }
        public async Task UpdateFoodAsync(Food food)
        {
            await _foodRepository.UpdateFoodAsync(food);
        }
        public async Task DeleteFoodAsync(int id)
        {
            await _foodRepository.DeleteFoodAsync(id);
        }
    }
}
