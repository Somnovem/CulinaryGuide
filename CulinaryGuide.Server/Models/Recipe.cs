namespace CulinaryGuide.Server.Models;

public class Recipe
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public double Calories { get; set; }
    public string Ingredients { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Instructions { get; set; } = null!;
    public Guid TypeId { get; set; }
    public Type? Type { get; set; }
    public Guid CuisineId { get; set; }
    public Cuisine? Cuisine { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public string MealTime { get; set; } = null!;
    public int Likes { get; set; }
}