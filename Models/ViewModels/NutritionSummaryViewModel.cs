public class NutritionSummaryViewModel
{
    public string Profil { get; set; } = string.Empty;
    public string WeightKg { get; set; } = string.Empty;


    public double CaloriesConsumed { get; set; }
    public double CaloriesTarget { get; set; }

    public double ProteinsConsumed { get; set; }
    public double ProteinsTarget { get; set; }

    public double CarbsConsumed { get; set; }
    public double CarbsTarget { get; set; }

    public double LipidsConsumed { get; set; }
    public double LipidsTarget { get; set; }
}
