using Microsoft.EntityFrameworkCore;
using NutritionAssistant.Data;
using NutritionAssistant.Models.Entities;

namespace NutritionAssistant.Models.Repositories
{
    public class MealFollowRepository : IMealFollowRepository
    {
        public readonly ApplicationDbContext _context;

        public MealFollowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MealFollow>> GetAllMealFollowsAsync()
        {
            return await _context.MealFollows.Include(mf => mf.Food).ToListAsync();
        }

        public async Task<MealFollow?> GetMealFollowByIdAsync(int id)
        {
            return await _context.MealFollows.Include(mf => mf.Food).FirstOrDefaultAsync(mf => mf.Id == id);
        }

        public async Task AddMealFollowAsync(MealFollow mealFollow)
        {
            _context.MealFollows.Add(mealFollow);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMealFollowAsync(MealFollow mealFollow)
        {
            _context.MealFollows.Update(mealFollow);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMealFollowAsync(int id)
        {
            var mealFollow = await _context.MealFollows.FindAsync(id);
            if (mealFollow is not null)
            {
                _context.MealFollows.Remove(mealFollow);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<BaseValue?> GetBaseValueAsync()
        {
            return await _context.BaseValues.FirstOrDefaultAsync();
        }

    }
}
