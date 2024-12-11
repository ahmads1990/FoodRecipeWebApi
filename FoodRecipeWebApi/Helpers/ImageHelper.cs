namespace FoodRecipeWebApi.Helpers
{
    public class ImageHelper(IWebHostEnvironment env)
    {
    private readonly IWebHostEnvironment _env = env;

        public async Task<string> SaveImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("Invalid file. File is either null or empty.");
        }

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(fileExtension))
        {
            throw new ArgumentException("Invalid file type. Only JPG, JPEG, PNG, and GIF are allowed.");
        }
        var fileName = Guid.NewGuid() + fileExtension;
        var directoryPath = Path.Combine(_env.WebRootPath, "images", "recipes");
        Directory.CreateDirectory(directoryPath); 
        var filePath = Path.Combine(directoryPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return $"/images/recipes/{fileName}";
    }
}

}
