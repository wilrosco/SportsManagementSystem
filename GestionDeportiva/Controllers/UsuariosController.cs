using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GestionDeportiva.Models;

namespace GestionDeportiva.Controllers
{
    public class UsuariosController : Controller
    {
        private BD_GestionDeportivaEntities db = new BD_GestionDeportivaEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            if (isUserAuthenticated())
            {
                try
                {
                    return View(db.Usuarios.ToList());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return HttpNotFound();

                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }


        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Usuario usuario = db.Usuarios.Find(id);
                    if (usuario == null)
                    {
                        return HttpNotFound();
                    }
                    return View(usuario);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return HttpNotFound();

                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }


        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            if (isUserAuthenticated())
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
            else
            {
                return RedirectToAction("Login", "Home");
            }


        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsuarioId,Username,Password,RoleId")] Usuario usuario)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Usuarios.Add(usuario);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(usuario);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return HttpNotFound();

                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }


        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Usuario usuario = db.Usuarios.Find(id);
                    if (usuario == null)
                    {
                        return HttpNotFound();
                    }
                    return View(usuario);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return HttpNotFound();

                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }


        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioId,Username,Password,RoleId")] Usuario usuario)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(usuario).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(usuario);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return HttpNotFound();

                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }


        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Usuario usuario = db.Usuarios.Find(id);
                    if (usuario == null)
                    {
                        return HttpNotFound();
                    }
                    return View(usuario);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return HttpNotFound();

                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }


        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    Usuario usuario = db.Usuarios.Find(id);
                    db.Usuarios.Remove(usuario);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return HttpNotFound();

                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }


        }

        private bool isUserAuthenticated()
        {
            if (Session["UserID"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
