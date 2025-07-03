using Microsoft.EntityFrameworkCore;
using NutritionAssistant.Models.Entities;
using NutritionAssistant.Models.Repositories;
using NutritionAssistant.Models.Helpers;


namespace NutritionAssistant.Models.Services
{
    public class BaseValueService : IBaseValueService
    {

        private readonly IBaseValueRepository _baseValueRepository;

        public BaseValueService(IBaseValueRepository baseValueRepository)
        {
            _baseValueRepository = baseValueRepository;
        }

        public async Task<BaseValue?> GetBaseValueByIdAsync(int id)
        {
            return await _baseValueRepository.GetBaseValueByIdAsync(id);
        }

        public async Task<IEnumerable<BaseValue>> GetAllBaseValuesAsync()
        {
            return await _baseValueRepository.GetAllBaseValuesAsync();
        }

        public async Task AddBaseValueAsync(BaseValue baseValue)
        {
            await _baseValueRepository.AddBaseValueAsync(baseValue);
        }

        public async Task UpdateBaseValueAsync(BaseValue baseValue)
        {
            await _baseValueRepository.UpdateBaseValueAsync(baseValue);
        }

        public async Task DeleteBaseValueAsync(int id)
        {
            await _baseValueRepository.DeleteBaseValueAsync(id);
        }

        public async Task<BaseValue?> GetBaseValueByProfilAsync(string profil)
        {
            var baseValues = await _baseValueRepository.GetAllBaseValuesAsync();
            return baseValues.FirstOrDefault(bv => bv.Profil == profil);
        }

        //Calculer besoins nutritionnels en fonction du poids saisie
        public async Task<(double calories, double proteins, double glucides, double lipids)?> CalculateNeedsAsync(string profil, double weightKg)
        {
            var baseValue = await GetBaseValueByProfilAsync(profil);
            if (baseValue == null) return null;

            double calories = baseValue.CalorieForKg * weightKg;
            double proteins = baseValue.ProteinForKg * (double)weightKg;
            double glucides = baseValue.CarbohydrateForKg * (double)weightKg;
            double lipids = baseValue.LipidForKg * (double)weightKg;

            double caloriesProtein = (double)(proteins * NutritionConstants.CaloriesPerGramProtein);
            double caloriesCarb = (double)(glucides * NutritionConstants.CaloriesPerGramCarbohydrate);
            double caloriesLipid = (double)(lipids * NutritionConstants.CaloriesPerGramLipid);

            return (calories, proteins, glucides, lipids);
        }

    }
}
