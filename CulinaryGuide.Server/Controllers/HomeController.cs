using CulinaryGuide.Server.Models;
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
        
        var randomBreakfast = _context.Recipes.AsNoTracking().Where(recipe => recipe.MealTime == "Breakfast").ToArray();
        var randomLunch = _context.Recipes.AsNoTracking().Where(recipe => recipe.MealTime == "Lunch").ToArray();
        var randomDinner = _context.Recipes.AsNoTracking().Where(recipe => recipe.MealTime == "Dinner").ToArray();
        
        Random random = new Random();
        int randomBreakfastIndex = random.Next(0, randomLunch.Length);
        int randomLunchIndex = random.Next(0, randomLunch.Length);
        int randomDinnerIndex = random.Next(0, randomLunch.Length);
        
        recipesOfTheDay.Add(randomBreakfast[randomBreakfastIndex]);
        recipesOfTheDay.Add(randomLunch[randomLunchIndex]);
        recipesOfTheDay.Add(randomDinner[randomDinnerIndex]);
        
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
            });
        }
        return displayedRecipes.AsEnumerable();
    }
}