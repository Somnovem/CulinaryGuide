using CulinaryGuide.Server.HelperClasses;
using CulinaryGuide.Server.Models.Tables;
using CulinaryGuide.Server.Models.DTOs;
using CulinaryGuide.Server.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Type = CulinaryGuide.Server.Models.Tables.Type;

namespace CulinaryGuide.Server.Controllers
{
    [ApiController]
    [Route("recipes/")]
    public class RecipeController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly List<String> _mealTimes = new() {"Breakfast", "Lunch", "Dinner"};
        
        public RecipeController(ApplicationContext db)
        {
            _context = db;
        }
        
        /// <summary>
        /// Get array of all cuisines
        /// </summary>
        /// <returns>Array of all plausible cuisines</returns>
        [HttpGet("getCuisines")]
        public IEnumerable<Cuisine> GetCuisines()
        {
            return _context.Cuisines.AsNoTracking().ToList();
        }
        
        /// <summary>
        /// Get array of all types
        /// </summary>
        /// <returns>Array of all plausible types</returns>
        [HttpGet("getTypes")]
        public IEnumerable<Type> GetTypes()
        {
            return _context.Types.AsNoTracking().ToList();
        }
        
        /// <summary>
        /// Get array of all mealtimes
        /// </summary>
        /// <returns>Array of all plausible mealtimes</returns>
        [HttpGet("getMealtimes")]
        public IEnumerable<String> GetMealTimes()
        {
            return _mealTimes;
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
                    Thumbnail = LinkBuilder.BuildThumbnailLink(recipe.Id)
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
        public FullRecipe GetFull(Guid recipeId)
        {
            Recipe recipe = _context.Recipes.AsNoTracking().First(recipe => recipe.Id == recipeId);
            return new FullRecipe()
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Calories = recipe.Calories,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                Ingredients = recipe.Ingredients,
                Type = _context.Types.AsNoTracking().First(type => type.Id == recipe.TypeId).Name,
                Cuisine = _context.Cuisines.AsNoTracking().First(cuisine => cuisine.Id == recipe.CuisineId).Name,
                Author = _context.Users.AsNoTracking().First(user => user.Id == recipe.UserId).Username,
                MealTime = recipe.MealTime,
                Likes = recipe.Likes,
                Thumbnail = LinkBuilder.BuildThumbnailLink(recipe.Id)
            };
        }

        /// <summary>
        /// Submit a recipe
        /// </summary>
        /// <param name="name">Name of the dish</param>
        /// <param name="calories">Calories</param>
        /// <param name="ingridients">Ingridients of the dish</param>
        /// <param name="description">Short description of the dish shown on its card</param>
        /// <param name="instructions">Guide how to cook the dish</param>
        /// <param name="type">Type of the dish(choosing from dropbox)</param>
        /// <param name="cuisine">Cuisine of the dish(choosing from dropbox)</param>
        /// <param name="mealtime">Breakfast/Lunch/Dinner(choosing from dropbox)</param>
        /// <param name="username">User's username</param>
        /// <param name="file">Thumbnail of the recipe uploaded</param>
        /// <returns></returns>
        [HttpPost("upload")]
        public async Task<IActionResult> AddRecipe([FromForm] RecipeBody recipeBody)
        {
            if (recipeBody.Thumbnail == null || recipeBody.Thumbnail.Length == 0)
            {
                return BadRequest("File is not selected or is empty.");
            }
            
            Recipe newRecipe = new Recipe()
            {
                Id = Guid.NewGuid(),
                Name = recipeBody.Name.CapitalizeFirstLetter(),
                Calories = recipeBody.Calories,
                Description = recipeBody.Description.CapitalizeFirstLetter(),
                Instructions = recipeBody.Instructions,
                Ingredients = recipeBody.Ingredients.CapitalizeWordsAndJoin(),
                TypeId = recipeBody.TypeId,
                CuisineId = recipeBody.CuisineId,
                UserId = _context.Users.AsNoTracking().First(user => user.Username == recipeBody.Username).Id,
                MealTime = recipeBody.MealTime
            };
            
            string directoryPath = Path.Combine("wwwroot", "Images", "Thumbnails", newRecipe.Id.ToString());
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = Path.Combine(directoryPath, "original.jpeg");
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await recipeBody.Thumbnail.CopyToAsync(stream);
            }
            
            _context.Recipes.Add(newRecipe);
            await _context.SaveChangesAsync();
            
            return Ok("Recipe uploaded successfully.");
        }
        
        /// <summary>
        /// Edit a recipe
        /// </summary>
        /// <param name="id">Id of the dish to edit</param>
        /// <param name="name">Name of the dish</param>
        /// <param name="calories">Calories</param>
        /// <param name="ingridients">Ingridients of the dish</param>
        /// <param name="description">Short description of the dish shown on its card</param>
        /// <param name="instructions">Guide how to cook the dish</param>
        /// <param name="type">Type of the dish(choosing from dropbox)</param>
        /// <param name="cuisine">Cuisine of the dish(choosing from dropbox)</param>
        /// <param name="mealtime">Breakfast/Lunch/Dinner(choosing from dropbox)</param>
        /// <param name="username">User's username</param>
        /// <param name="file">New thumbnail of the recipe</param>
        /// <returns></returns>
        [HttpPut("edit")]
        public async Task<IActionResult> EditRecipe([FromForm] UpdateRecipeRequest request)
        {
            Console.WriteLine("Recieved Id: " + request.Id);
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == request.Id);
    
            // If no matching recipe is found, return a 404 Not Found response
            if (recipe == null)
            {
                return NotFound("Recipe not found.");
            }
            
            if (request.Thumbnail == null || request.Thumbnail.Length == 0)
            {
                return BadRequest("File is not selected or is empty.");
            }
            
            // Update the recipe properties
            recipe.Name = request.Name.CapitalizeFirstLetter();
            recipe.Calories = request.Calories;
            recipe.Description = request.Description.CapitalizeFirstLetter();
            recipe.Instructions = request.Instructions;
            recipe.Ingredients = request.Ingredients.CapitalizeWordsAndJoin();
            recipe.TypeId = request.TypeId;
            recipe.CuisineId = request.CuisineId;
            recipe.MealTime = request.MealTime;
            
            string directoryPath = Path.Combine("wwwroot", "Images", "Thumbnails", request.Id.ToString());
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string filePath = Path.Combine(directoryPath, "original.jpeg");
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.Thumbnail.CopyToAsync(stream);
            }
            
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
            
            return Ok("Recipe edited successfully.");
        }

        /// <summary>
        /// Delete a recipe
        /// </summary>
        /// <param name="id">Id of the recipe to delete</param>
        /// <param name="username">Username to check if it's my recipe</param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRecipe(Guid id, string username)
        {
            Guid userId = _context.Users.AsNoTracking().First(user => user.Username.Equals(username)).Id;
            var recipe = await _context.Recipes.FirstAsync(recipe => recipe.Id == id && recipe.UserId == userId);
            if (recipe == null)
            {
                return NotFound("Recipe not found.");
            }
            
            string directoryPath = Path.Combine("wwwroot", "Images", "Thumbnails", id.ToString());
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
            
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return Ok("Recipe deleted successfully.");
        }
        
        /// <summary>
        /// Get all recipes of user
        /// </summary>
        /// <param name="Username">User's username</param>
        /// <returns></returns>
        [HttpGet("recipesOfUser")]
        public IEnumerable<ShortRecipe> RecipesOfUser(string username)
        {
            Guid id = _context.Users.AsNoTracking().First(u => u.Username.Equals(username)).Id;
            return _context.Recipes.AsNoTracking().Where(r => r.UserId == id).Select(recipe => new ShortRecipe ()
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
            }).AsEnumerable();
        }
        
        /// <summary>
        /// Search recipes by provided parameters
        /// </summary>
        /// <param name="query">Parameters of the search</param>
        /// <returns></returns>
        [HttpGet("search")]
        public async Task<IEnumerable<ShortRecipe>> Search([FromQuery] RecipeQuery query)
        {
            IEnumerable<Recipe> recipes = await _context.Recipes.AsNoTracking().ToListAsync();
            if (query.TypeId != null)
                recipes = recipes.Where(r => r.TypeId == query.TypeId);
            if (query.CuisineId != null)
                recipes = recipes.Where(r => r.CuisineId == query.CuisineId);
            if (!query.MealTime.IsNullOrEmpty())
                recipes = recipes.Where(r => r.MealTime == query.MealTime);
            if (query.MinCalories != null)
                recipes = recipes.Where(r => r.Calories >= query.MinCalories);
            if (query.MaxCalories != null)
                recipes = recipes.Where(r => r.Calories <= query.MaxCalories);
            if (!query.Name.IsNullOrEmpty())
                recipes = recipes.Where(r => r.Name.Contains(query.Name!));
            if (!query.Ingredients.IsNullOrEmpty())
            {
                foreach (string ingredient in query.Ingredients!.Split(", "))
                {
                    recipes = recipes.Where(r => r.Ingredients.Contains(ingredient));
                }
            }

            return recipes.Select(recipe => new ShortRecipe ()
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
            }).AsEnumerable();
        }
    }
}
