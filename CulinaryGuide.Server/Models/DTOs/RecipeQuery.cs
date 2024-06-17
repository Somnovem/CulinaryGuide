namespace CulinaryGuide.Server.Models.DTOs;

public class RecipeQuery
{
    public string? Name { get; set; }
    public string? Ingredients { get; set; }
    public double? MinCalories { get; set; }
    public double? MaxCalories { get; set; }
    public Guid? TypeId { get; set; }
    public Guid? CuisineId { get; set; }
    public string? MealTime { get; set; }
}