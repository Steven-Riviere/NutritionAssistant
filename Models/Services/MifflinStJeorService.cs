using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NutritionAssistant.Models.Entities;
using NutritionAssistant.Models.Repositories;

namespace NutritionAssistant.Models.Services
{
    public class MifflinStJeorService : IMifflinStJeorService
    {
        private readonly IMifflinStJeorRepository _mifflinRepository;
        private readonly IProfilActivityRepository _profilActivityRepository;

        public MifflinStJeorService(
            IMifflinStJeorRepository mifflinRepository,
            IProfilActivityRepository profilActivityRepository)
        {
            _mifflinRepository = mifflinRepository;
            _profilActivityRepository = profilActivityRepository;
        }

        // Récupérer tous les enregistrements
        public async Task<IEnumerable<MifflinStJeor>> GetAllMifflinStJeorsAsync()
        {
            return await _mifflinRepository.GetAllMifflinStJeorsAsync();
        }

        // Récupérer par ID
        public async Task<MifflinStJeor?> GetMifflinStJeorByIdAsync(int id)
        {
            return await _mifflinRepository.GetMifflinStJeorByIdAsync(id);
        }

        // Ajouter un enregistrement
        public async Task AddMifflinStJeorAsync(MifflinStJeor entity)
        {
            await _mifflinRepository.AddMifflinStJeorAsync(entity);
        }

        // Mettre à jour un enregistrement
        public async Task UpdateMifflinStJeorAsync(MifflinStJeor entity)
        {
            await _mifflinRepository.UpdateMifflinStJeorAsync(entity);
        }

        // Supprimer un enregistrement
        public async Task DeleteMifflinStJeorAsync(int id)
        {
            await _mifflinRepository.DeleteMifflinStJeorAsync(id);
        }

        // Calcul BMR (Mifflin-St Jeor)
        public double CalculateBMR(MifflinStJeor data)
        {
            double baseCalc = 10 * data.Weight + 6.25 * data.Height - 5 * data.Age;

            if (data.Gender.ToLower() == "homme" || data.Gender.ToLower() == "male")
                baseCalc += 5;
            else if (data.Gender.ToLower() == "femme" || data.Gender.ToLower() == "female")
                baseCalc -= 161;
            else
                throw new ArgumentException("Genre invalide");

            return baseCalc;
        }

        // Calcul des besoins caloriques journaliers ajustés selon le profil d'activité
        public async Task<double> CalculateDailyCaloriesAsync(int mifflinId, int profilActivityId)
        {
            var mifflin = await _mifflinRepository.GetMifflinStJeorByIdAsync(mifflinId);
            var profilActivity = await _profilActivityRepository.GetProfilActivityByIdAsync(profilActivityId);

            if (mifflin == null)
                throw new ArgumentException("MifflinStJeor introuvable");
            if (profilActivity == null)
                throw new ArgumentException("ProfilActivity introuvable");

            var bmr = CalculateBMR(mifflin);

            // besoins caloriques totaux = BMR x facteur d'activité
            double dailyCalories = bmr * profilActivity.ActivityFactor;

            return dailyCalories;
        }
    }
}
