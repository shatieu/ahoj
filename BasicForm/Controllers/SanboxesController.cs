using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
            string pes =
                "{\"employees\":["+
    "{ \"firstName\":\"fuck\", \"lastName\":\"Doe\"},"+
    "{ \"firstName\":\"Anna\", \"lastName\":\"Smith\"},"+
    "{ \"firstName\":\"Peter\",\"lastName\":\"Jones\"}]}";
            
            ViewBag.Data = pes;
            return View();
        }

        public JsonResult getJson(int a, int b)
        {

            string jsonTimes;

            try
            {
                List<string> times = new List<string>();
                times.Add("1");
                times.Add("2");
                times.Add("3");
                var jsonSerialiser = new JavaScriptSerializer();
                jsonTimes = jsonSerialiser.Serialize(times);
            }
            catch (System.ArgumentException e)
            {
                jsonTimes = "[{\"erorr\":\"" + e.ToString() + "\"}]";
            }

            return Json(jsonTimes, JsonRequestBehavior.AllowGet);

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