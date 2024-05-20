using CulinaryGuide.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CulinaryGuide.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public HomeController(ApplicationContext db)
        {
            _context = db;
        }

        [HttpGet]
        public IEnumerable<Recipe> Get()
        {
            List<Recipe> recipesOfTheDay = new List<Recipe>();
            //Recipe randomBreakfast = _context.Recipes.AsNoTracking().Where(recipe => recipe.MealTime == "Breakfast").MaxBy(recipe => recipe.Likes)!;
            var randomLunch = _context.Recipes.AsNoTracking().Where(recipe => recipe.MealTime == "Lunch").ToArray();
            Random random = new Random();
            int randomIndex = random.Next(0, randomLunch.Length);
            //Recipe randomDinner = _context.Recipes.AsNoTracking().Where(recipe => recipe.MealTime == "Dinner").MaxBy(recipe => recipe.Likes)!;
            //recipesOfTheDay.Add(randomBreakfast);
            recipesOfTheDay.Add(randomLunch[randomIndex]);
            //recipesOfTheDay.Add(randomDinner);
            return recipesOfTheDay.AsEnumerable();
        }
    }
}
