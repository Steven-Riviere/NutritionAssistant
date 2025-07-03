using Microsoft.AspNetCore.Mvc;
using NutritionAssistant.Models.Entities;
using NutritionAssistant.Models.Repositories;
using NutritionAssistant.Models.ViewModels;

namespace NutritionAssistant.Models.Services
{
    public class MealFollowService : IMealFollowService
    {
        private readonly IMealFollowRepository _mealFollowRepository;
        private readonly IBaseValueService _baseValueService;

        public MealFollowService(IMealFollowRepository mealFollowRepository, IBaseValueService baseValueService)
        {
            _mealFollowRepository = mealFollowRepository;
            _baseValueService = baseValueService;
        }


        public async Task<IEnumerable<MealFollow>> GetAllMealFollowsAsync()
        {
            return await _mealFollowRepository.GetAllMealFollowsAsync();
        }

        public async Task<MealFollow?> GetMealFollowByIdAsync(int id)
        {
            return await _mealFollowRepository.GetMealFollowByIdAsync(id);
        }

        public async Task AddMealFollowAsync(MealFollow mealFollow)
        {
            await _mealFollowRepository.AddMealFollowAsync(mealFollow);
        }

        public async Task UpdateMealFollowAsync(MealFollow mealFollow)
        {
            await _mealFollowRepository.UpdateMealFollowAsync(mealFollow);
        }

        public async Task DeleteMealFollowAsync(int id)
        {
            await _mealFollowRepository.DeleteMealFollowAsync(id);
        }

        public MealFollowViewModel ToViewModel(MealFollow mealFollow)
        {
            var food = mealFollow.Food;
            double qty = mealFollow.Quantity;

            // Calcul des valeurs nutritionnelles selon la quantité
            double calories = (food.CalorieFor100g * qty) / 100.0;
            double proteins = (food.ProteinFor100g * qty) / 100;
            double carbohydrates = (food.CarbohydrateFor100g * qty) / 100;
            double lipids = (food.LipidFor100g * qty) / 100;

            return new MealFollowViewModel
            {
                FoodName = food.Name,
                Quantity = qty.ToString("F2"),       // format avec 2 décimales
                Calories = calories.ToString("F2"),
                Proteins = proteins.ToString("F2"),
                Carbohydrates = carbohydrates.ToString("F2"),
                Lipids = lipids.ToString("F2")
            };
        }

        public async Task<NutritionSummaryViewModel> GetDailySummaryAsync(string profil, double weightKg)
        {
            var allMealFollows = await _mealFollowRepository.GetAllMealFollowsAsync();

            double totalCalories = 0;
            double totalProteins = 0;
            double totalCarbohydrates = 0;
            double totalLipids = 0;

            foreach (var mf in allMealFollows)
            {
                var food = mf.Food;
                double qty = mf.Quantity;
                // Calcul des valeurs nutritionnelles selon la quantité
                totalCalories += (food.CalorieFor100g * qty) / 100;
                totalProteins += (food.ProteinFor100g * qty) / 100;
                totalCarbohydrates += (food.CarbohydrateFor100g * qty) / 100;
                totalLipids += (food.LipidFor100g * qty) / 100;
            }

            var needs = await _baseValueService.CalculateNeedsAsync(profil, weightKg);

            if (needs == null)
            {
                // Gérer le cas où aucune base n’est trouvée
                return new NutritionSummaryViewModel();
            }

            return new NutritionSummaryViewModel
            {
                CaloriesConsumed = totalCalories,
                CaloriesTarget = needs.Value.calories,

                ProteinsConsumed = totalProteins,
                ProteinsTarget = needs.Value.proteins,

                CarbsConsumed = totalCarbohydrates,
                CarbsTarget = needs.Value.glucides,

                LipidsConsumed = totalLipids,
                LipidsTarget = needs.Value.lipids
            };
        }

    }
}
