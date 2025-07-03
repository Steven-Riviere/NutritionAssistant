using Microsoft.AspNetCore.Mvc;
using NutritionAssistant.Models.Entities;
using NutritionAssistant.Models.Services;
using NutritionAssistant.Models.ViewModels;

namespace NutritionAssistant.Controllers
{
    public class BaseValueController : Controller
    {
        private readonly IBaseValueService _baseValueService;
        private readonly IMealFollowService _mealFollowService;

        public BaseValueController(IBaseValueService baseValueService, IMealFollowService mealFollowService)
        {
            _baseValueService = baseValueService;
            _mealFollowService = mealFollowService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var baseValue = await _baseValueService.GetAllBaseValuesAsync();
            return View(baseValue);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var baseValue = await _baseValueService.GetBaseValueByIdAsync(id);
            if (baseValue == null)
            {
                return NotFound();
            }
            return View(baseValue);
        }

        [HttpGet]
        public IActionResult Create()
        {
            BaseValueViewModel viewModel = new BaseValueViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BaseValueViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (!double.TryParse(viewModel.CalorieForKg, out double calorieForKg) ||
               !double.TryParse(viewModel.ProteinForKg, out double proteinForKg) ||
               !double.TryParse(viewModel.CarbohydrateForKg, out double carbohydrateForKg) ||
               !double.TryParse(viewModel.LipidForKg, out double lipidForKg))
            {
                ModelState.AddModelError("", "Les valeurs nutritionnelles doivent être des nombres valides.");
                return View(viewModel);
            }
            BaseValue baseValue = new BaseValue
            {
                Profil = viewModel.Profil,
                CalorieForKg = calorieForKg,
                ProteinForKg = proteinForKg,
                CarbohydrateForKg = carbohydrateForKg,
                LipidForKg = lipidForKg
            };
            await _baseValueService.AddBaseValueAsync(baseValue);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var baseValue = await _baseValueService.GetBaseValueByIdAsync(id);
            if (baseValue == null)
            {
                return NotFound();
            }
            BaseValueViewModel viewModel = new BaseValueViewModel
            {
                Profil = baseValue.Profil,
                CalorieForKg = baseValue.CalorieForKg.ToString(),
                ProteinForKg = baseValue.ProteinForKg.ToString(),
                CarbohydrateForKg = baseValue.CarbohydrateForKg.ToString(),
                LipidForKg = baseValue.LipidForKg.ToString()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BaseValueViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            if (!double.TryParse(viewModel.CalorieForKg, out double calorieForKg) ||
               !double.TryParse(viewModel.ProteinForKg, out double proteinForKg) ||
               !double.TryParse(viewModel.CarbohydrateForKg, out double carbohydrateForKg) ||
               !double.TryParse(viewModel.LipidForKg, out double lipidForKg))
            {
                ModelState.AddModelError("", "Les valeurs nutritionnelles doivent être des nombres valides.");
                return View(viewModel);
            }
            var baseValue = await _baseValueService.GetBaseValueByIdAsync(id);
            if (baseValue == null)
            {
                return NotFound();
            }
            baseValue.Profil = viewModel.Profil;
            baseValue.CalorieForKg = calorieForKg;
            baseValue.ProteinForKg = proteinForKg;
            baseValue.CarbohydrateForKg = carbohydrateForKg;
            baseValue.LipidForKg = lipidForKg;
            await _baseValueService.UpdateBaseValueAsync(baseValue);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var baseValue = await _baseValueService.GetBaseValueByIdAsync(id);
            if (baseValue == null)
            {
                return NotFound();
            }
            return View(baseValue);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baseValue = await _baseValueService.GetBaseValueByIdAsync(id);
            if (baseValue == null)
            {
                return NotFound();
            }
            await _baseValueService.DeleteBaseValueAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CalculateNeeds(BaseValueViewModel viewModel)
        {
            if (!double.TryParse(viewModel.WeightKg, out double weightKg))
            {
                ModelState.AddModelError(nameof(viewModel.WeightKg), "Le poids doit être un nombre valide.");
                return View(viewModel);
            }

            if (string.IsNullOrWhiteSpace(viewModel.Profil))
            {
                ModelState.AddModelError(nameof(viewModel.Profil), "Le profil doit être renseigné.");
                return View(viewModel);
            }

            var needs = await _baseValueService.CalculateNeedsAsync(viewModel.Profil, weightKg);

            if (needs == null)
            {
                ModelState.AddModelError("", "Profil non trouvé dans la base de données.");
                return View(viewModel);
            }

            var viewModelSummary = new NutritionSummaryViewModel
            {
                Profil = viewModel.Profil,
                WeightKg = weightKg.ToString("F2"),
                CaloriesTarget = needs.Value.calories,
                ProteinsTarget = needs.Value.proteins,
                CarbsTarget = needs.Value.glucides,
                LipidsTarget = needs.Value.lipids
            };

            return View("NeedsSummary", viewModelSummary);
        }

        [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> ShowNutritionSummary(string profil, string weightKg)
            {
                if (!double.TryParse(weightKg, out double weight))
                {
                    ModelState.AddModelError("WeightKg", "Le poids doit être un nombre valide");
                return View();
                }

            if (string.IsNullOrWhiteSpace(profil))
            {
                ModelState.AddModelError("Profil", "Le profil doit être renseigné.");
                return View();
            }

            var nutritionSummary = await _mealFollowService.GetDailySummaryAsync(profil, weight);

            return View("NeedsSummary", nutritionSummary);
        }
    }
}
