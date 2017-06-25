using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        GamerDb gamerDb = new GamerDb();
        public ActionResult GetGamer()
        {
            gamerDb = new GamerDb();

            List<Gamer> listGamers = new List<Gamer>();
            listGamers = gamerDb.GetGamers();
            return View(listGamers);
        }




        [HttpPost]
        public ActionResult Register(Gamer gamer)
        {
            gamerDb = new GamerDb();
            List<Gamer> listGamers = new List<Gamer>();

            listGamers = gamerDb.GetGamers();


            //  var game = listGamers.Exists(x => x.UserName.Equals(gamer) && listGamers.Exists(y => y.Email.Equals(gamer.Email)));

            if (ModelState.IsValid)
            {
                try
                {
                    gamerDb.InsertGamer(gamer);

                    ViewBag.Message = "New Gamer is added to database";
                    return RedirectToAction("GetGamer");
                }
                catch (System.Exception)
                {

                    ModelState.AddModelError("", $"Trenutni Gamer postoji vec u bazi");
                }
            }

            ModelState.AddModelError("", "Gamer is not add to database");
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Gamer gamer)
        {
            gamerDb = new GamerDb();

            List<Gamer> listGamerLogin = new List<Gamer>();
            listGamerLogin = gamerDb.GetGamersLogin();


            var gamerExists = listGamerLogin.Exists(x => x.UserName.Equals(gamer.UserName) && listGamerLogin.Exists(y => y.Password.Equals(gamer.Password)));




            if (gamerExists)
            {
                Session["gamer"] = gamer.UserName;

                return View("LoginSuccess");
            }

            return View();




        }
        public ActionResult LoginSuccess()
        {
            return View();
        }








        public ActionResult Delete(int id)
        {
            gamerDb = new GamerDb();
            if (gamerDb.DeleteGamer(id))
            {
                return View();
            }
            else
            {
                ViewBag.Error = "Gamer is not deleted from database";
            }
            return View();
        }
    }
}
