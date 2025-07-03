using Microsoft.AspNetCore.Mvc;
using NutritionAssistant.Models.Entities;
using NutritionAssistant.Models.Services;
using NutritionAssistant.Models.ViewModels;
using System.Threading.Tasks;

namespace NutritionAssistant.Controllers
{
    public class MifflinStJeorController : Controller
    {
        private readonly IMifflinStJeorService _mifflinService;

        public MifflinStJeorController(IMifflinStJeorService mifflinService)
        {
            _mifflinService = mifflinService;
        }

        // Liste tous les enregistrements
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _mifflinService.GetAllMifflinStJeorsAsync();
            return View(list);
        }

        // Voir les détails d'un enregistrement
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var entity = await _mifflinService.GetMifflinStJeorByIdAsync(id);
            if (entity == null) return NotFound();

            return View(entity);
        }

        // Affiche le formulaire de création
        [HttpGet]
        public IActionResult Create()
        {
            MifflinStJeorViewModel model = new MifflinStJeorViewModel();
            return View(model);
        }

        // Traite le formulaire de création
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MifflinStJeorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (!double.TryParse(vm.Weight, out double weight) || weight <= 0)
            {
                ModelState.AddModelError(nameof(vm.Weight), "Poids invalide");
            }

            if (!double.TryParse(vm.Height, out double height) || height <= 0)
            {
                ModelState.AddModelError(nameof(vm.Height), "Taille invalide");
            }

            if (vm.Age <= 0)
            {
                ModelState.AddModelError(nameof(vm.Age), "Âge invalide");
            }

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            MifflinStJeor mifflinStJeor = new MifflinStJeor
            {
                Gender = vm.Gender,
                Weight = weight,
                Height = height,
                Age = vm.Age
            };


            await _mifflinService.AddMifflinStJeorAsync(mifflinStJeor);

            return RedirectToAction("Index");
        }

        // Affiche le formulaire d'édition
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var mifflin = await _mifflinService.GetMifflinStJeorByIdAsync(id);
            if (mifflin == null)
            {
                return NotFound();
            }

            MifflinStJeorViewModel vm = new MifflinStJeorViewModel
            {
                Gender = mifflin.Gender,
                Weight = mifflin.Weight.ToString(),
                Height = mifflin.Height.ToString(),
                Age = mifflin.Age
            };

            return View(vm);
        }

        // Traite la mise à jour
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MifflinStJeorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (!double.TryParse(vm.Weight, out double weight) || weight <= 0)
            {
                ModelState.AddModelError(nameof(vm.Weight), "Poids invalide");
            }

            if (!double.TryParse(vm.Height, out double height) || height <= 0)
            {
                ModelState.AddModelError(nameof(vm.Height), "Taille invalide");
            }

            if (vm.Age <= 0)
            {
                ModelState.AddModelError(nameof(vm.Age), "Âge invalide");
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var entity = await _mifflinService.GetMifflinStJeorByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Gender = vm.Gender;
            entity.Weight = weight;
            entity.Height = height;
            entity.Age = vm.Age;

            await _mifflinService.UpdateMifflinStJeorAsync(entity);

            return RedirectToAction("Index");
        }

        // Confirmation de suppression
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _mifflinService.GetMifflinStJeorByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        // Suppression définitive
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mifflin = await _mifflinService.GetMifflinStJeorByIdAsync(id);
            if (mifflin == null)
            {
                return NotFound();
            }
            await _mifflinService.DeleteMifflinStJeorAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
