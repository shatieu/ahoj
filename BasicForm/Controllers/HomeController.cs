using System.Web.Mvc;

namespace BasicForm.Controllers
{
    
    public class HomeController : Controller
    {

        /// <summary>
        /// Page with basic information about project
        /// </summary>
        /// <returns>Page with redirect menu</returns>
        public ActionResult Index()
        {

            return View();
        }

    }
}
