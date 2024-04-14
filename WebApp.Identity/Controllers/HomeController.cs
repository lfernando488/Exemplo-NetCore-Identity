using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Identity.Models;

namespace WebApp.Identity.Controllers
{
    //[ApiController] //Ja faz a validacao de objetos com data annotation
    public class HomeController : Controller
    {

        private readonly UserManager<MyUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<MyUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        /*
        public HomeController(UserManager<MyUser> userManager)
        {
            _userManager = userManager;
        }
        */
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    user = new MyUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.UserName,
                    };
                }

                var result = await _userManager.CreateAsync(user, model.Password);

                return View("Success");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
