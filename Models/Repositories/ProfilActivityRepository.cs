using Microsoft.EntityFrameworkCore;
using NutritionAssistant.Data;
using NutritionAssistant.Models.Entities;

namespace NutritionAssistant.Models.Repositories
{
    public class ProfilActivityRepository : IProfilActivityRepository
    {

        public readonly ApplicationDbContext _context;

        public ProfilActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProfilActivity>> GetAllProfilActivitiesAsync()
        {
            return await _context.ProfilActivities.ToListAsync();
        }

        public async Task<ProfilActivity?> GetProfilActivityByIdAsync(int id)
        {
            return await _context.ProfilActivities.FindAsync(id);
        }

        public async Task AddProfilActivityAsync(ProfilActivity profilActivity)
        {
            _context.ProfilActivities.Add(profilActivity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProfilActivityAsync(ProfilActivity profilActivity)
        {
            _context.ProfilActivities.Update(profilActivity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProfilActivityAsync(int id)
        {
            var profilActivity = await _context.ProfilActivities.FindAsync(id);
            if (profilActivity is not null)
            {
                _context.ProfilActivities.Remove(profilActivity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
