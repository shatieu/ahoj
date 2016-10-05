using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasicForm.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index(bool firstTry = true)
        {
            if (firstTry == false)
            {
                ViewBag.Message = "Prihlaseni se nepovedlo, zkuste to znovu prosim.";
            }

            return View();
        }

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

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {
                    var v = dc.Users.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.UserID.ToString();
                        Session["LogedUserFullname"] = v.FullName.ToString();
                        return RedirectToAction("AfterLogin");
                    }
                }
            }
            return View(u);
        }
        */
    }
}