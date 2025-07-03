using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;
using NutritionAssistant.Models.Entities;
using NutritionAssistant.Models.Services;
using NutritionAssistant.Models.ViewModels;

namespace NutritionAssistant.Controllers
{
    public class MealFollowController : Controller
    {
        private readonly IMealFollowService _mealFollowService;

        public MealFollowController(IMealFollowService mealFollowService)
        {
            _mealFollowService = mealFollowService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var mealFollows = await _mealFollowService.GetAllMealFollowsAsync();

            var viewModels = mealFollows.Select(mf => _mealFollowService.ToViewModel(mf)).ToList();

            return View(viewModels);
        }


        [HttpPost]
        public async Task<IActionResult> Summary(string profil, double weightKg)
        {
            if (string.IsNullOrWhiteSpace(profil) || weightKg <= 0)
            {
                ModelState.AddModelError("", "Le profil et le poids doivent être valides.");
                return View("Index");
            }

            var summary = await _mealFollowService.GetDailySummaryAsync(profil, weightKg);

            if (summary == null)
            {
                return NotFound();
            }

            return View("Summary", summary); // La vue Razor à créer
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(new MealFollowViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MealFollowViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            if (!double.TryParse(viewModel.Quantity, out double quantity) || quantity <= 0)
            {
                ModelState.AddModelError(nameof(viewModel.Quantity), "Quantité invalide.");
                return View(viewModel);
            }

            if (viewModel.FoodId <= 0)
            {
                ModelState.AddModelError(nameof(viewModel.FoodId), "Aliment non sélectionné.");
                return View(viewModel);
            }

            var mealFollow = new MealFollow
            {
                FoodId = viewModel.FoodId,
                Quantity = quantity
            };

            await _mealFollowService.AddMealFollowAsync(mealFollow);

            return RedirectToAction(nameof(Index));
        }

        // ✅ DETAILS
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var mealFollow = await _mealFollowService.GetMealFollowByIdAsync(id);
            if (mealFollow == null)
            {
                return NotFound();
            }

            var viewModel = _mealFollowService.ToViewModel(mealFollow);
            return View(viewModel);
        }

        // ✅ EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var mealFollow = await _mealFollowService.GetMealFollowByIdAsync(id);
            if (mealFollow == null)
            {
                return NotFound();
            }

            // Utilisation correcte de ToViewModel pour avoir TOUS les champs calculés
            var viewModel = _mealFollowService.ToViewModel(mealFollow);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MealFollowViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            if (!double.TryParse(viewModel.Quantity, out double quantity) || quantity <= 0)
            {
                ModelState.AddModelError(nameof(viewModel.Quantity), "Quantité invalide.");
                return View(viewModel);
            }

            var mealFollow = await _mealFollowService.GetMealFollowByIdAsync(id);
            if (mealFollow == null)
            {
                return NotFound();
            }
            mealFollow.FoodId = viewModel.FoodId;
            mealFollow.Quantity = quantity;
            await _mealFollowService.UpdateMealFollowAsync(mealFollow);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var meal = await _mealFollowService.GetMealFollowByIdAsync(id);
            if (meal == null)
            {
                return NotFound();
            }
            return View(meal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meal = await _mealFollowService.GetMealFollowByIdAsync(id);
            if (meal == null)
            {
                return NotFound();
            }
            await _mealFollowService.DeleteMealFollowAsync(id);
            return RedirectToAction("Index");
        }
    }

}
