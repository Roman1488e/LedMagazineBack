using LedMagazineBack.Services.Abstract;

namespace LedMagazineBack.Services;

public class FileService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    : IFileService
{
    public bool CheckIsImage(IFormFile file)
    {
        var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
        return allowedTypes.Contains(file.ContentType.ToLower());
    }

    public bool CheckIsVideo(IFormFile file)
    {
        var allowedTypes = new[] { "video/mp4", "video/webm", "video/ogg" };
        return allowedTypes.Contains(file.ContentType.ToLower());
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        
        var uploadsFolder = Path.Combine(env.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);
        
        var fullPath = Path.Combine(uploadsFolder, fileName);

        await using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        
        var request = httpContextAccessor.HttpContext!.Request;
        var baseUrl = $"{request.Scheme}://{request.Host}";

        return $"{baseUrl}/uploads/{fileName}";
    }
}
