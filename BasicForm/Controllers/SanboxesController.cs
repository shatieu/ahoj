using System.Web.Mvc;

namespace BasicForm.Controllers
{
    /// <summary>
    /// Class that handles sandboxes
    /// For each member is one with his name on. 
    /// Sandbox is only for single member use to develop
    /// Sandbox will be changed by other member only if developer asks him
    /// </summary>
    public class SanboxesController : Controller
    {
        // GET: Sanboxes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ondra()
        {
            return View();
        }

        public ActionResult Jenda()
        {
            return View();
        }

        public ActionResult Jirka()
        {
            return View();
        }
    }
}