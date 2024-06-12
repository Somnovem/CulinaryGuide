using CulinaryGuide.Server.Models.Tables;

public class Like
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
}