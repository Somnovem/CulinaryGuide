namespace CulinaryGuide.Server.Models.DTOs;

public class RecipeBody
{
    public string Name { get; set; } = null!;
    public double Calories { get; set; }
    public string Ingredients { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Instructions { get; set; } = null!;
    public Guid TypeId { get; set; }
    public Guid CuisineId { get; set; }
    public string MealTime { get; set; } = null!;
    public string Username { get; set; } = null!;
    public IFormFile Thumbnail { get; set; } = null!;
}