using System;
using System.Web.Mvc;

namespace BasicForm.Controllers
{
    public class ErrorController : Controller
    {
        
        /// <summary>
        /// GET - returns error page with description that its Invalid model state
        /// </summary>
        /// <param name="details">To describe error with some more details. Added to general description</param>
        /// <returns>Error view</returns>
        public ActionResult InvalidState(String details = "No description")
        {
            ViewBag.description = "Error: Invalid model state <br/>"+details;
            return View("Error");
        }
    }
}