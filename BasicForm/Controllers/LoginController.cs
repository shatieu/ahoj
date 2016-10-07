using System;
using System.Linq;
using System.Web.Mvc;

namespace BasicForm.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// Checks if first try with login and additively write warning when not
        /// </summary>
        /// <param name="firstTry">Param that say if warning should be shown</param>
        /// <returns>Page with login</returns>
        [HttpGet]
        public ActionResult Index(bool firstTry = true)
        {
            if (firstTry == false)
            {
                ViewBag.Message = "Prihlaseni se nepovedlo, zkuste to znovu prosim.";
            }

            return View();
        }

        /// <summary>
        /// Checks if data for loggin are correct.
        /// </summary>
        /// <param name="provider">Data with credentials</param>
        /// <returns> If so, create session, if not, redirect back to login form</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(BasicForm.Models.DBRepresentations.Provider provider)
        {
            if (ModelState.IsValid)
            {
                using(CalendarEntities entities = new CalendarEntities())
                {
                    Provider prov = entities.Providers.Where(x => x.Email.Equals(provider.Email) && x.PassHashed.Equals(provider.PassHashed)).SingleOrDefault();
                    if(prov != null)
                    {
                        Session["LogedUserID"] = prov.ID.ToString();
                        return RedirectToAction("Logged");
                    }
                }

                
            }

            return RedirectToAction("Index", new { firstTry = false });
            
        }

        /// <summary>
        /// Page with info that you are logged
        /// </summary>
        /// <returns>Page with success text</returns>
        public ActionResult Logged()
        {
            if (Session["LogedUserID"] != null)
            {
                using (CalendarEntities entities = new CalendarEntities())
                {
                    int sessionID;
                    bool succes = Int32.TryParse( Session["LogedUserID"].ToString(),out sessionID);
                    if (succes)
                    {
                        Provider prov = entities.Providers.Where(x => x.ID.Equals(sessionID)).SingleOrDefault();
                        return View(prov);
                    }
                    
                }
               
            }
           
            return RedirectToAction("Index");
            
        }

       
    }
}