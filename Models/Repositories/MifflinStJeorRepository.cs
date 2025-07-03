using Microsoft.EntityFrameworkCore;
using NutritionAssistant.Data;
using NutritionAssistant.Models.Entities;

namespace NutritionAssistant.Models.Repositories
{
    public class MifflinStJeorRepository : IMifflinStJeorRepository
    {

        public readonly ApplicationDbContext _context;

        public MifflinStJeorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MifflinStJeor>> GetAllMifflinStJeorsAsync()
        {
            return await _context.MifflinStJeors.ToListAsync();
        }

        public async Task<MifflinStJeor?> GetMifflinStJeorByIdAsync(int id)
        {
            return await _context.MifflinStJeors.FindAsync(id);
        }

        public async Task AddMifflinStJeorAsync(MifflinStJeor mifflinStJeor)
        {
            _context.MifflinStJeors.Add(mifflinStJeor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMifflinStJeorAsync(MifflinStJeor mifflinStJeor)
        {
            _context.MifflinStJeors.Update(mifflinStJeor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMifflinStJeorAsync(int id)
        {
            var mifflinStJeor = await _context.MifflinStJeors.FindAsync(id);
            if (mifflinStJeor is not null)
            {
                _context.MifflinStJeors.Remove(mifflinStJeor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
