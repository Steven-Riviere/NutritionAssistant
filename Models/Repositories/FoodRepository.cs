using Microsoft.EntityFrameworkCore;
using NutritionAssistant.Data;
using NutritionAssistant.Models.Entities;

namespace NutritionAssistant.Models.Repositories
{
    public class FoodRepository : IFoodRepository
    {

        public readonly ApplicationDbContext _context;

        public FoodRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Food>> GetAllFoodsAsync()
        {
            return await _context.Foods.ToListAsync();
        }

        public async Task<Food?> GetFoodByIdAsync(int id)
        {
            return await _context.Foods.FindAsync(id);
        }
        public async Task AddFoodAsync(Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateFoodAsync(Food food)
        {
            _context.Foods.Update(food);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteFoodAsync(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food is not null)
            {
                _context.Foods.Remove(food);
                await _context.SaveChangesAsync();
            }
        }
    }
}
