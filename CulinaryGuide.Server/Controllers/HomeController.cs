using CulinaryGuide.Server.HelperClasses;
using CulinaryGuide.Server.Models.Tables;
using CulinaryGuide.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CulinaryGuide.Server.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    
    private readonly ApplicationContext _context;

    public HomeController(ApplicationContext db)
    {
        _context = db;
    }
    
    /// <summary>
    /// Get recipes of the day
    /// </summary>
    /// <returns></returns>
    [HttpGet("getrecipes")]
    public IEnumerable<ShortRecipe> GetRecipesOfTheDay()
    {
        List<Recipe> recipesOfTheDay = new List<Recipe>();
        var recipesByMealTime = _context.Recipes.AsNoTracking()
            .Where(recipe => recipe.MealTime == "Breakfast" || recipe.MealTime == "Lunch" || recipe.MealTime == "Dinner")
            .GroupBy(recipe => recipe.MealTime)
            .ToDictionary(group => group.Key, group => group.ToList());

        Random random = new Random();

        if (recipesByMealTime.ContainsKey("Breakfast") && recipesByMealTime["Breakfast"].Count > 0)
        {
            int randomBreakfastIndex = random.Next(recipesByMealTime["Breakfast"].Count);
            recipesOfTheDay.Add(recipesByMealTime["Breakfast"][randomBreakfastIndex]);
        }
        if (recipesByMealTime.ContainsKey("Lunch") && recipesByMealTime["Lunch"].Count > 0)
        {
            int randomLunchIndex = random.Next(recipesByMealTime["Lunch"].Count);
            recipesOfTheDay.Add(recipesByMealTime["Lunch"][randomLunchIndex]);
        }
        if (recipesByMealTime.ContainsKey("Dinner") && recipesByMealTime["Dinner"].Count > 0)
        {
            int randomDinnerIndex = random.Next(recipesByMealTime["Dinner"].Count);
            recipesOfTheDay.Add(recipesByMealTime["Dinner"][randomDinnerIndex]);
        }
        
        List<ShortRecipe> displayedRecipes = new List<ShortRecipe>();
        foreach (Recipe recipe in recipesOfTheDay)
        {
            displayedRecipes.Add(new ShortRecipe()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Calories = recipe.Calories,
                Description = recipe.Description,
                Type = _context.Types.AsNoTracking().First(type => type.Id == recipe.TypeId).Name,
                Cuisine = _context.Cuisines.AsNoTracking().First(cuisine => cuisine.Id == recipe.CuisineId).Name,
                Author = _context.Users.AsNoTracking().First(user => user.Id == recipe.UserId).Username,
                MealTime = recipe.MealTime,
                Likes = recipe.Likes,
                Thumbnail = LinkBuilder.BuildThumbnailLink(recipe.Id)
            });
        }
        return displayedRecipes.AsEnumerable();
    }
}