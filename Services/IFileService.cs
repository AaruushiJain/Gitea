namespace Gitea_1.Services
{
    public interface IFileService
    {
        string? GetFileByHash(string hash);
    }
}
