namespace NutritionAssistant.Models.Entities
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double CalorieFor100g { get; set; }
        public double ProteinFor100g { get; set; }
        public double CarbohydrateFor100g { get; set; }
        public double LipidFor100g { get; set; }
    }
}
