using Microsoft.AspNetCore.Http;

namespace Gitea_1.Services
{
    public interface IObjectService
    {
        string HashAndStoreObject(IFormFile file);
    }
}
