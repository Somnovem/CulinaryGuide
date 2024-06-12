namespace CulinaryGuide.Server.Models.DTOs;

public class FullRecipe
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public double Calories { get; set; }
    public string Ingredients { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Instructions { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Cuisine { get; set; } = null!;
    public string Author { get; set; } = null!;
    public string MealTime { get; set; } = null!;
    public int Likes { get; set; }
    public string Thumbnail { get; set; } = null!;
}