using Microsoft.AspNetCore.Mvc;
using Gitea_1.Services;

namespace Gitea_1.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CatFile(string hash)
        {
            if (string.IsNullOrEmpty(hash))
            {
                ViewBag.Message = "Please enter a valid hash!";
                return View("Index");
            }

            var content = _fileService.GetFileByHash(hash);

            if (content == null)
                ViewBag.Message = "File not found!";
            else
                ViewBag.Message = $"File content:\n{content}";

            return View("Index");
        }
    }
}
