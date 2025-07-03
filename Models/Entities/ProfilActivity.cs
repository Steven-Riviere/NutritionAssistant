namespace NutritionAssistant.Models.Entities
{
    public class ProfilActivity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double ActivityFactor { get; set; } // Activity factor for the profile
    }
}
