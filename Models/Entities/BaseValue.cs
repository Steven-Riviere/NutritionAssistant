namespace NutritionAssistant.Models.Entities
{
    public class BaseValue
    {
        public int Id { get; set; }
        public string Profil { get; set; } = string.Empty;
        public double CalorieForKg { get; set; }
        public double ProteinForKg { get; set; }
        public double CarbohydrateForKg { get; set; }
        public double LipidForKg { get; set; }

    }
}
