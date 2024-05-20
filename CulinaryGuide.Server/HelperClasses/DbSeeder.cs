using CulinaryGuide.Server.Models;
using Microsoft.EntityFrameworkCore;
using Type = CulinaryGuide.Server.Models.Type;
namespace CulinaryGuide.Server.HelperClasses;

public static class DbSeeder
{
    private static ApplicationContext _context;
    
    public static void SeedData(ApplicationContext context)
    {
        if (context.Users.Any()) return;
        _context = context;
        SeedUsersDb();
        SeedCuisinesDb();
        SeedTypesDb();
        SeedRecipesDb();
        SeedLikesDb();
    }

    private static void SeedUsersDb()
    {
        User jeremiah = new User()
        {
            Id = new Guid(),
            Username = "Jeremiah",
            Email = "jeremiah1982@gmail.com",
            Password = Encrypter.CalculateSha256("tomTheBestDog")
        };
        User mary = new User()
        {
            Id = new Guid(),
            Username = "Mary",
            Email = "marydoe@gmail.com",
            Password = Encrypter.CalculateSha256("MaryTary")
        };
        User john = new User()
        {
            Id = new Guid(),
            Username = "John",
            Email = "johndoe@gmail.com",
            Password = Encrypter.CalculateSha256("AbCdEfGh")
        };
        User marsh = new User()
        {
            Id = new Guid(),
            Username = "Marsh",
            Email = "ironeyes@gmail.com",
            Password = Encrypter.CalculateSha256("getMeAtium")
        };
        User kelsier = new User()
        {
            Id = new Guid(),
            Username = "Kelsier",
            Email = "survivor@gmail.com",
            Password = Encrypter.CalculateSha256("forIAmHope")
        };
        User sazed = new User()
        {
            Id = new Guid(),
            Username = "Sazed",
            Email = "lastferuchemist@gmail.com",
            Password = Encrypter.CalculateSha256("DoYouKnowGod")
        };
        _context.Users.AddRange(jeremiah,mary,john,marsh,kelsier,sazed);
        _context.SaveChanges();
    }

    private static void SeedCuisinesDb()
    {
        _context.Cuisines.AddRange(
            new Cuisine(){ Id = new Guid(), Name = "Ukrainian"},
                new Cuisine() { Id = new Guid(), Name = "Italian"},
                new Cuisine() { Id = new Guid(), Name = "Mexican"},
                new Cuisine() { Id = new Guid(), Name = "Chinese"},
                new Cuisine() { Id = new Guid(), Name = "Japanese"},
                new Cuisine() { Id = new Guid(), Name = "Thai"},
                new Cuisine() { Id = new Guid(), Name = "English"},
                new Cuisine() { Id = new Guid(), Name = "American"},
                new Cuisine() { Id = new Guid(), Name = "French"},
                new Cuisine() { Id = new Guid(), Name = "Spanish"},
                new Cuisine() { Id = new Guid(), Name = "Norwegian"},
                new Cuisine() { Id = new Guid(), Name = "Danish" }
            );
        _context.SaveChanges();
    }

    private static void SeedTypesDb()
    {
        _context.Types.AddRange(
            new Type(){ Id = new Guid(), Name = "Soup"},
            new Type() { Id = new Guid(), Name = "Garnish"},
            new Type() { Id = new Guid(), Name = "Salad"},
            new Type() { Id = new Guid(), Name = "Desert"},
            new Type() { Id = new Guid(), Name = "Drink"},
            new Type() { Id = new Guid(), Name = "Cocktail"},
            new Type() { Id = new Guid(), Name = "Pizza"}
        );
        _context.SaveChanges();
    }

    private static void SeedRecipesDb()
    {
        Recipe borscht = new Recipe()
        {
            Id = new Guid(),
            Name = "Traditional Ukrainian Borscht",
            Calories = 1250d,
            Ingredients = "List of ingredients for borscht",
            Description = "The best recipe for a traditional ukrainian borscht that even Klopotenko would apprecieate",
            Instructions = "Instructions for making borscht",
            MealTime = "Lunch",
            TypeId = _context.Types.AsNoTracking().First(type => type.Name == "Soup").Id,
            CuisineId = _context.Cuisines.AsNoTracking().First(type => type.Name == "Ukrainian").Id,
            UserId = _context.Users.AsNoTracking().First(user => user.Email == "jeremiah1982@gmail.com").Id,
            Likes = 0
        };
        _context.Recipes.AddRange(borscht);
        _context.SaveChanges();
    }

    private static void SeedLikesDb()
    {
        
    }
}