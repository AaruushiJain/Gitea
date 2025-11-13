using Microsoft.AspNetCore.Mvc;
using Gitea_1.Services;
using Microsoft.AspNetCore.Http;

namespace Gitea_1.Controllers
{
    public class ObjectController : Controller
    {
        private readonly IObjectService _objectService;

        public ObjectController(IObjectService objectService)
        {
            _objectService = objectService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HashObject(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "No file selected!";
                return View("Index");
            }

            var hash = _objectService.HashAndStoreObject(file);

            ViewBag.Message = $"File hashed and stored successfully! SHA1: {hash}";
            return View("Index");
        }
    }
}
