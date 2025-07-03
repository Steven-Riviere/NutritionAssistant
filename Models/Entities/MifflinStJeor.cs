namespace NutritionAssistant.Models.Entities
{
    public class MifflinStJeor
    {
        public int Id { get; set; }
        public string Gender { get; set; } = string.Empty;
        public double Weight { get; set; } // in kg
        public double Height { get; set; } // in cm
        public int Age { get; set; } // in years


    }
}
