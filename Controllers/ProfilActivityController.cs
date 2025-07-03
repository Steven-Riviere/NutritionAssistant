using Microsoft.AspNetCore.Mvc;
using NutritionAssistant.Models.Entities;
using NutritionAssistant.Models.Services;
using NutritionAssistant.Models.ViewModels;

namespace NutritionAssistant.Controllers
{
    public class ProfilActivityController : Controller
    {
        private readonly IMifflinStJeorService _mifflinService;
        private readonly IProfilActivityService _profilActivityService;

        public ProfilActivityController(IMifflinStJeorService mifflinService, IProfilActivityService profilActivityService)
        {
            _mifflinService = mifflinService;
            _profilActivityService = profilActivityService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var profil = await _profilActivityService.GetAllAsync();
            return View(profil);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProfilActivityViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProfilActivityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!double.TryParse(model.ActivityFactor, out double activityFactor))
            {
                ModelState.AddModelError("", "Le coefficient doit être un nombre valide");
                return View(model);
            }
            ProfilActivity profilActivity = new ProfilActivity
            {
                Name = model.Name,
                ActivityFactor = activityFactor
            };
            await _profilActivityService.AddAsync(profilActivity);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var profil = await _profilActivityService.GetByIdAsync(id);
            if (profil == null)
            {
                return NotFound();
            }

            ProfilActivityViewModel model = new ProfilActivityViewModel
            {
                Name = profil.Name,
                ActivityFactor = profil.ActivityFactor.ToString()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProfilActivityViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!double.TryParse(model.ActivityFactor, out double activityFactor))
            {
                ModelState.AddModelError("", "Le coefficient doit être un nombre valide");
                return View(model);
            }

            var profil = await _profilActivityService.GetByIdAsync(id);
            if (profil == null)
            {
                return NotFound();
            }
            profil.Name = model.Name;
            profil.ActivityFactor = activityFactor;
            await _profilActivityService.UpdateAsync(profil);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var profil = await _profilActivityService.GetByIdAsync(id);
            if (profil == null)
            {
                return NotFound();
            }
            return View(profil);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profil = await _profilActivityService.GetByIdAsync(id);
            if (profil == null)
            {
                return NotFound();
            }
            await _profilActivityService.DeleteAsync(id);
            return RedirectToAction("Index");
        }


        // Calcul dynamique des calories selon paramètres reçus
        [HttpGet]
        public async Task<IActionResult> CalculateCalories(MifflinStJeorViewModel mifflinVm, int profilActivityId)
        {
            if (!ModelState.IsValid)
            {
                // Recharger la liste des profils pour la vue si erreur
                var profils = await _profilActivityService.GetAllAsync();
                ViewBag.Profils = profils;
                return View("Index");
            }

            // Création d'un objet MifflinStJeor temporaire depuis le ViewModel
            var mifflin = new NutritionAssistant.Models.Entities.MifflinStJeor
            {
                Gender = mifflinVm.Gender,
                Age = mifflinVm.Age,
                Weight = double.TryParse(mifflinVm.Weight, out double w) ? w : 0,
                Height = double.TryParse(mifflinVm.Height, out double h) ? h : 0
            };

            try
            {

                double bmr = _mifflinService.CalculateBMR(mifflin);

                var profilActivity = await _profilActivityService.GetByIdAsync(profilActivityId);
                if (profilActivity == null)
                {
                    ModelState.AddModelError("", "Profil d'activité invalide.");
                    var profils = await _profilActivityService.GetAllAsync();
                    ViewBag.Profils = profils;
                    return View("Index");
                }

                double dailyCalories = bmr * profilActivity.ActivityFactor;

                ViewBag.Result = dailyCalories;
                ViewBag.Profils = await _profilActivityService.GetAllAsync();

                return View("Index");
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("", "Erreur lors du calcul : " + ex.Message);
                var profils = await _profilActivityService.GetAllAsync();
                ViewBag.Profils = profils;
                return View("Index");
            }
        }
    }
}
