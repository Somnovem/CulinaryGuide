using CulinaryGuide.Server.Models.DTOs;

namespace CulinaryGuide.Server.Models.Requests;

public class UpdateRecipeRequest : RecipeBody
{
    public Guid Id { get; set; }
}