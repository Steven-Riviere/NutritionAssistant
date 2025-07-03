namespace NutritionAssistant.Models.ViewModels
{
    public class MealFollowViewModel
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; } = string.Empty;
        public string Quantity { get; set; } = string.Empty;
        public string Calories { get; set; } = string.Empty;
        public string Proteins { get; set; } = string.Empty;
        public string Carbohydrates { get; set; } = string.Empty;
        public string Lipids { get; set; } = string.Empty;

    }
}
