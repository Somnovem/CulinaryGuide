using CulinaryGuide.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CulinaryGuide.Server.Controllers
{
    [ApiController]
    [Route("recipes/")]
    public class RecipeController : ControllerBase
    {
        private readonly ApplicationContext _context;
        
        public RecipeController(ApplicationContext db)
        {
            _context = db;
        }
        
        /// <summary>
        /// Get paginated short recipes
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("getPage/{page:int=1}")]
        public IEnumerable<ShortRecipe> GetPage(int page = 1, [FromQuery] int pageSize = 12)
        {
            if (page < 1) page = 1;
            int itemsToSkip = (page - 1) * pageSize;
            List<Recipe> recipes = _context.Recipes.AsNoTracking().Skip(itemsToSkip).Take(pageSize).ToList();
            List<ShortRecipe> displayedRecipes = new List<ShortRecipe>();
            foreach (Recipe recipe in recipes)
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

        /// <summary>
        /// Get full description of recipe by ID
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        [HttpGet("getFull/{recipeId:guid}")]
        public Recipe GetFull(Guid recipeId)
        {
            return _context.Recipes.AsNoTracking().First(recipe => recipe.Id == recipeId);
        }

        /// <summary>
        /// Submit a recipe
        /// </summary>
        /// <param name="Id">Id of the recipe uploaded</param>
        /// <param name="thumbnail">Thumbnail of the recipe uploaded</param>
        /// <returns></returns>
        [HttpPost("upload")]
        public async Task<IActionResult> AddRecipe(Guid Id,IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is not selected or is empty.");
            }
            
            string directoryPath = Path.Combine("Images", "Thumbnails", Id.ToString());
            if(!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            string filePath = Path.Combine(directoryPath, "original.jpeg");
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            
            return Ok("File uploaded successfully.");
        }
    }
}
