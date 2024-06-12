namespace CulinaryGuide.Server.HelperClasses;

public static class LinkBuilder
{
    private static string baseLink = "http://localhost:5000";
    
    public static string BuildThumbnailLink(Guid recipeId)
    {
        return $"{baseLink}/Images/Thumbnails/{recipeId}/original.jpeg";
    }
}