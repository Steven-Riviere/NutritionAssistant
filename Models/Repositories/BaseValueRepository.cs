using NutritionAssistant.Data;
using NutritionAssistant.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace NutritionAssistant.Models.Repositories
{
    public class BaseValueRepository : IBaseValueRepository
    {
        public readonly ApplicationDbContext _context;
        public BaseValueRepository(ApplicationDbContext  context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<BaseValue>> GetAllBaseValuesAsync()
        {
            return await _context.BaseValues.ToListAsync();
        }

        public async Task<BaseValue?> GetBaseValueByIdAsync(int id)
        {
            return await _context.BaseValues.FindAsync(id);
        }

        public async Task AddBaseValueAsync(BaseValue baseValue)
        {
            _context.BaseValues.Add(baseValue);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBaseValueAsync(BaseValue baseValue)
        {
            _context.BaseValues.Update(baseValue);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBaseValueAsync(int id)
        {
            var baseValue = await _context.BaseValues.FindAsync(id);
            if (baseValue is not null)
            {
                _context.BaseValues.Remove(baseValue);
                await _context.SaveChangesAsync();
            }
        }
    }
}
