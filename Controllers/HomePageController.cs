
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Library.Controllers
{
    
    public class HomePageController : Controller
    {
      
        public IActionResult HomePage()
        {
            return View();
        }
      

    }
}
