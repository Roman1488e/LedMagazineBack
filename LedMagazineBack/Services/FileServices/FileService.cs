using LedMagazineBack.Services.FileServices.Abstract;

namespace LedMagazineBack.Services.FileServices;

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

    public async Task UpdateFile(string absoluteUrl, IFormFile newFile)
    {
        var request = httpContextAccessor.HttpContext!.Request;
        var baseUrl = $"{request.Scheme}://{request.Host}"; 
        if (!absoluteUrl.StartsWith(baseUrl))
            throw new ArgumentException("Invalid file URL", nameof(absoluteUrl));
        var relativePath = absoluteUrl.Substring(baseUrl.Length); 
        var cleanRelativePath = relativePath.TrimStart('/');
        var fullPath = Path.Combine(env.WebRootPath, cleanRelativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));
        if (!System.IO.File.Exists(fullPath))
            throw new FileNotFoundException("Старый файл не найден", fullPath);
        System.IO.File.Delete(fullPath);
        await using var stream = new FileStream(fullPath, FileMode.Create);
        await newFile.CopyToAsync(stream);
    }
}
