using GestionDeportiva.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace GestionDeportiva.Controllers
{
    public class HomeController : Controller
    {
        BD_GestionDeportivaEntities db = new BD_GestionDeportivaEntities();

        public ActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return HttpNotFound();

            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario User)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BD_GestionDeportivaEntities db = new BD_GestionDeportivaEntities())
                    {
                        var obj = db.Usuarios.Where(a => a.Username.Equals(User.Username) && a.Password.Equals(User.Password)).FirstOrDefault();
                        if (obj != null)
                        {
                            Session["UserID"] = obj.UsuarioId.ToString();
                            Session["Username"] = obj.Username.ToString();
                            return RedirectToAction("Index", "Eventos");
                        }
                    }
                }
                return View(User);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return HttpNotFound();

            }

        }


        public ActionResult Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.RemoveAll();
                Session["UserInfo"] = null;
                Session.Abandon();
                string[] myCookies = Request.Cookies.AllKeys;
                foreach (string cookie in myCookies)
                {
                    Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return HttpNotFound();

            }

        }


        public ActionResult Index()
        {
            try
            {
                return View(db.Eventos.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return HttpNotFound();

            }
            

        }
    }
}