namespace NutritionAssistant.Models.ViewModels
{
    public class BaseValueViewModel
    {
        public string Profil { get; set; } = string.Empty;
        public string WeightKg {  get; set; } = string.Empty;
        public string Calories { get; set; } = string.Empty;
        public string CalorieForKg { get; set; } = string.Empty;
        public string ProteinForKg { get; set; } = string.Empty;
        public string CarbohydrateForKg { get; set; } = string.Empty;
        public string LipidForKg { get; set; } = string.Empty;
    }
}
