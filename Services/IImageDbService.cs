namespace MauiTemplateEcreo.Services
{
    public interface IImageDbService
    {
        Task<byte[]> GetImage(string filename);
        Task UploadImageAsync(Stream image, string fileName);
    }
}