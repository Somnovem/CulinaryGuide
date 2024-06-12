using CulinaryGuide.Server.Models.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CulinaryGuide.Server.Controllers;

[ApiController]
[Route("likes/")]
public class LikeController : ControllerBase
{
    private readonly ApplicationContext _context;
        
    public LikeController(ApplicationContext db)
    {
        _context = db;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddLike(Guid recipeId, string username)
    {
        Guid userId = _context.Users.AsNoTracking().First(u => u.Username.Equals(username)).Id;
        
        if (_context.Recipes.AsNoTracking().First(r => r.Id == recipeId).UserId == userId)
        {
            return BadRequest("Can't like your own recipe.");
        }
        
        Like like = new Like()
        {
            Id = new Guid(),
            UserId = userId,
            RecipeId = recipeId
        };
        _context.Likes.Add(like);
        await _context.SaveChangesAsync();
        return Ok("Like posted successfully.");
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveLike(Guid recipeId, string username)
    {
        Guid userId = _context.Users.AsNoTracking().First(u => u.Username.Equals(username)).Id;
        
        var like = _context.Likes.AsNoTracking().FirstOrDefault(l => l.UserId == userId && l.RecipeId == recipeId);
        if (like == null)
        {
            return NotFound("Like not found.");
        }

        _context.Likes.Remove(like);
        await _context.SaveChangesAsync();
        return Ok("Like removed successfully.");
    }
}