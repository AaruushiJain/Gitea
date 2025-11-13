using Microsoft.AspNetCore.Mvc;
using Gitea_1.Services;

namespace Gitea_1.Controllers
{
    public class InitController : Controller
    {
        private readonly IInitService _initService;

        public InitController(IInitService initService)
        {
            _initService = initService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Initialize()
        {
            bool success = _initService.InitializeRepository();

            if (success)
                ViewBag.Message = "Initialized empty Gitea repository successfully!";
            else
                ViewBag.Message = "Repository already exists or failed to initialize.";

            return View("Index");
        }
    }
}
