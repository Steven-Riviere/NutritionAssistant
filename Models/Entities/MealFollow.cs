namespace NutritionAssistant.Models.Entities
{
    public class MealFollow
    {
        public int Id { get; set; }

        public int FoodId { get; set; }
        public double Quantity { get; set; }

        public Food Food { get; set; } = null!;
    }
}
