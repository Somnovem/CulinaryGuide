using CulinaryGuide.Server.Models.Tables;
using Microsoft.EntityFrameworkCore;
using Type = CulinaryGuide.Server.Models.Tables.Type;
namespace CulinaryGuide.Server.HelperClasses;

public static class DbSeeder
{
    private static ApplicationContext _context;
    
    public static void SeedData(ApplicationContext context)
    {
        if (context.Users.Any()) return;
        _context = context;
        SeedUsersTable();
        SeedCuisinesTable();
        SeedTypesTable();
        SeedRecipesTable();
        SeedLikesTable();
    }

    private static void SeedUsersTable()
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

    private static void SeedCuisinesTable()
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

    private static void SeedTypesTable()
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

    private static void SeedRecipesTable()
{
    Recipe borscht = new Recipe()
    {
        Id = Guid.Parse("75730875-5cda-4454-72da-08dc86e0948a"),
        Name = "Traditional Ukrainian Borscht",
        Calories = 1250d,
        Ingredients = "List of ingredients for borscht",
        Description = "The best recipe for a traditional ukrainian borscht that even Klopotenko would appreciate",
        Instructions = "Instructions for making borscht",
        MealTime = "Lunch",
        TypeId = _context.Types.AsNoTracking().First(type => type.Name == "Soup").Id,
        CuisineId = _context.Cuisines.AsNoTracking().First(type => type.Name == "Ukrainian").Id,
        UserId = _context.Users.AsNoTracking().First(user => user.Email == "jeremiah1982@gmail.com").Id,
        Likes = 0
    };

    Recipe caesarSalad = new Recipe()
    {
        Id = Guid.Parse("4fd6d0f5-4c04-4d4f-72db-08dc86e0948a"),
        Name = "Caesar Salad",
        Calories = 350d,
        Ingredients = "List of ingredients for Caesar Salad",
        Description = "A classic Caesar Salad with crispy croutons and creamy dressing",
        Instructions = "Instructions for making Caesar Salad",
        MealTime = "Lunch",
        TypeId = _context.Types.AsNoTracking().First(type => type.Name == "Salad").Id,
        CuisineId = _context.Cuisines.AsNoTracking().First(type => type.Name == "Italian").Id,
        UserId = _context.Users.AsNoTracking().First(user => user.Email == "marydoe@gmail.com").Id,
        Likes = 0
    };

    Recipe chickenWrap = new Recipe()
    {
        Id = Guid.Parse("54ff5521-4fd4-4193-72dc-08dc86e0948a"),
        Name = "Chicken Wrap",
        Calories = 450d,
        Ingredients = "List of ingredients for Chicken Wrap",
        Description = "A healthy and delicious chicken wrap with fresh veggies",
        Instructions = "Instructions for making Chicken Wrap",
        MealTime = "Lunch",
        TypeId = _context.Types.AsNoTracking().First(type => type.Name == "Garnish").Id,
        CuisineId = _context.Cuisines.AsNoTracking().First(type => type.Name == "American").Id,
        UserId = _context.Users.AsNoTracking().First(user => user.Email == "johndoe@gmail.com").Id,
        Likes = 0
    };
    
    Recipe pancakes = new Recipe()
    {
        Id = Guid.Parse("a4984124-8c6d-4884-72dd-08dc86e0948a"),
        Name = "Fluffy Pancakes",
        Calories = 520d,
        Ingredients = "List of ingredients for Pancakes",
        Description = "Light and fluffy pancakes perfect for a family breakfast",
        Instructions = "Instructions for making Pancakes",
        MealTime = "Breakfast",
        TypeId = _context.Types.AsNoTracking().First(type => type.Name == "Garnish").Id,
        CuisineId = _context.Cuisines.AsNoTracking().First(type => type.Name == "American").Id,
        UserId = _context.Users.AsNoTracking().First(user => user.Email == "ironeyes@gmail.com").Id,
        Likes = 0
    };
    
    Recipe omelette = new Recipe()
    {
        Id = Guid.Parse("93ccbf7b-c1cb-496b-72de-08dc86e0948a"),
        Name = "Cheese Omelette",
        Calories = 300d,
        Ingredients = "List of ingredients for Cheese Omelette",
        Description = "A simple and delicious cheese omelette",
        Instructions = "Instructions for making Cheese Omelette",
        MealTime = "Breakfast",
        TypeId = _context.Types.AsNoTracking().First(type => type.Name == "Garnish").Id,
        CuisineId = _context.Cuisines.AsNoTracking().First(type => type.Name == "French").Id,
        UserId = _context.Users.AsNoTracking().First(user => user.Email == "lastferuchemist@gmail.com").Id,
        Likes = 0
    };
    
    Recipe smoothie = new Recipe()
    {
        Id = Guid.Parse("72a97fd9-3063-49a3-72df-08dc86e0948a"),
        Name = "Berry Smoothie",
        Calories = 180d,
        Ingredients = "List of ingredients for Berry Smoothie",
        Description = "A refreshing berry smoothie to start your day",
        Instructions = "Instructions for making Berry Smoothie",
        MealTime = "Breakfast",
        TypeId = _context.Types.AsNoTracking().First(type => type.Name == "Drink").Id,
        CuisineId = _context.Cuisines.AsNoTracking().First(type => type.Name == "American").Id,
        UserId = _context.Users.AsNoTracking().First(user => user.Email == "jeremiah1982@gmail.com").Id,
        Likes = 0
    };

    Recipe sushi = new Recipe()
    {
        Id = Guid.Parse("243d6f42-0a73-42d1-72e0-08dc86e0948a"),
        Name = "Sushi Platter",
        Calories = 600d,
        Ingredients = "List of ingredients for Sushi",
        Description = "An assortment of fresh sushi rolls",
        Instructions = "Instructions for making Sushi",
        MealTime = "Dinner",
        TypeId = _context.Types.AsNoTracking().First(type => type.Name == "Garnish").Id,
        CuisineId = _context.Cuisines.AsNoTracking().First(type => type.Name == "Japanese").Id,
        UserId = _context.Users.AsNoTracking().First(user => user.Email == "survivor@gmail.com").Id,
        Likes = 0
    };

    Recipe lasagna = new Recipe()
    {
        Id = Guid.Parse("65aa900a-8330-4de2-72e1-08dc86e0948a"),
        Name = "Classic Lasagna",
        Calories = 850d,
        Ingredients = "List of ingredients for Lasagna",
        Description = "A hearty and delicious classic Italian lasagna",
        Instructions = "Instructions for making Lasagna",
        MealTime = "Dinner",
        TypeId = _context.Types.AsNoTracking().First(type => type.Name == "Garnish").Id,
        CuisineId = _context.Cuisines.AsNoTracking().First(type => type.Name == "Italian").Id,
        UserId = _context.Users.AsNoTracking().First(user => user.Email == "marydoe@gmail.com").Id,
        Likes = 0
    };

    Recipe tacos = new Recipe()
    {
        Id = Guid.Parse("855c23d8-88b4-4f19-72e2-08dc86e0948a"),
        Name = "Fish Tacos",
        Calories = 500d,
        Ingredients = "List of ingredients for Fish Tacos",
        Description = "Fresh and zesty fish tacos with a tangy slaw",
        Instructions = "Instructions for making Fish Tacos",
        MealTime = "Dinner",
        TypeId = _context.Types.AsNoTracking().First(type => type.Name == "Garnish").Id,
        CuisineId = _context.Cuisines.AsNoTracking().First(type => type.Name == "Mexican").Id,
        UserId = _context.Users.AsNoTracking().First(user => user.Email == "johndoe@gmail.com").Id,
        Likes = 0
    };
    
    _context.Recipes.AddRange(borscht, caesarSalad, chickenWrap, pancakes, omelette, smoothie, sushi, lasagna, tacos);
    _context.SaveChanges();
}

    private static void SeedLikesTable()
    {
        
    }
}