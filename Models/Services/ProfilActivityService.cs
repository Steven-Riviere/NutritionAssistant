using System.Collections.Generic;
using System.Threading.Tasks;
using NutritionAssistant.Models.Entities;
using NutritionAssistant.Models.Repositories;

namespace NutritionAssistant.Models.Services
{
    public class ProfilActivityService : IProfilActivityService
    {
        private readonly IProfilActivityRepository _profilActivityRepository;

        public ProfilActivityService(IProfilActivityRepository profilActivityRepository)
        {
            _profilActivityRepository = profilActivityRepository;
        }

        public async Task<IEnumerable<ProfilActivity>> GetAllAsync()
        {
            return await _profilActivityRepository.GetAllProfilActivitiesAsync();
        }

        public async Task<ProfilActivity?> GetByIdAsync(int id)
        {
            return await _profilActivityRepository.GetProfilActivityByIdAsync(id);
        }

        public async Task AddAsync(ProfilActivity profilActivity)
        {
            await _profilActivityRepository.AddProfilActivityAsync(profilActivity);
        }

        public async Task UpdateAsync(ProfilActivity profilActivity)
        {
            await _profilActivityRepository.UpdateProfilActivityAsync(profilActivity);
        }

        public async Task DeleteAsync(int id)
        {
            await _profilActivityRepository.DeleteProfilActivityAsync(id);
        }
    }
}
