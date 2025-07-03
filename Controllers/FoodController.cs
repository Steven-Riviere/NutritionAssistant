using Microsoft.AspNetCore.Mvc;
using NutritionAssistant.Models.Entities;
using NutritionAssistant.Models.Services;
using NutritionAssistant.Models.ViewModels;

namespace NutritionAssistant.Controllers
{
    public class FoodController : Controller
    {

        private readonly IFoodService _foodService;

        public FoodController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var food = await _foodService.GetAllFoodsAsync();

            return View(food);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new FoodViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (!double.TryParse(viewModel.CalorieFor100g, out double calorieFor100g) ||
               !double.TryParse(viewModel.ProteinFor100g, out double proteinFor100g) ||
               !double.TryParse(viewModel.CarbohydrateFor100g, out double carbohydrateFor100g) ||
               !double.TryParse(viewModel.LipidFor100g, out double lipidFor100g))
            {
                ModelState.AddModelError("", "Les valeurs nutritionnelles doivent être des nombres valides.");
                return View(viewModel);
            }
            Food food = new Food
            {
                Name = viewModel.Name,
                CalorieFor100g = calorieFor100g,
                ProteinFor100g = proteinFor100g,
                CarbohydrateFor100g = carbohydrateFor100g,
                LipidFor100g = lipidFor100g
            };
            await _foodService.AddFoodAsync(food);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var food = await _foodService.GetFoodByIdAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            FoodViewModel viewModel = new FoodViewModel
            {
                Name = food.Name,
                CalorieFor100g = food.CalorieFor100g.ToString(),
                ProteinFor100g = food.ProteinFor100g.ToString(),
                CarbohydrateFor100g = food.CarbohydrateFor100g.ToString(),
                LipidFor100g = food.LipidFor100g.ToString()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FoodViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            if (!double.TryParse(viewModel.CalorieFor100g, out double calorieFor100g) ||
                !double.TryParse(viewModel.ProteinFor100g, out double proteinFor100g) ||
                !double.TryParse(viewModel.CarbohydrateFor100g, out double carbohydrateFor100g) ||
                !double.TryParse(viewModel.LipidFor100g, out double lipidFor100g)) 
            {
                ModelState.AddModelError("", "Les valeurs nutritionnelles doivent être des nombres valides.");
                return View(viewModel);
            }

            var food = await _foodService.GetFoodByIdAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            food.Name = viewModel.Name;
            food.CalorieFor100g = calorieFor100g;
            food.ProteinFor100g = proteinFor100g;
            food.CarbohydrateFor100g = carbohydrateFor100g;
            food.LipidFor100g = lipidFor100g;
            await _foodService.UpdateFoodAsync(food);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var food = await _foodService.GetFoodByIdAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await _foodService.GetFoodByIdAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            await _foodService.DeleteFoodAsync(id);
            return RedirectToAction("Index");
        }

    }
}
