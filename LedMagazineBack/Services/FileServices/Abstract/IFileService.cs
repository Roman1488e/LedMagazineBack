namespace LedMagazineBack.Services.FileServices.Abstract;

public interface IFileService
{
    public bool CheckIsImage(IFormFile file);
    public bool CheckIsVideo(IFormFile file);
    public Task<string> UploadFile(IFormFile file);
    public Task UpdateFile(string path, IFormFile file);
}