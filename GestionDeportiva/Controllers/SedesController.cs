using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GestionDeportiva.Models;

namespace GestionDeportiva.Controllers
{
    public class SedesController : Controller
    {
        private BD_GestionDeportivaEntities db = new BD_GestionDeportivaEntities();

        // GET: Sedes
        public ActionResult Index()
        {
            if (isUserAuthenticated())
            {
                try
                {

                    if (Session["UserID"] != null)
                    {
                        var sedes = db.Sedes.Include(s => s.Deporte);
                        return View(sedes.ToList());
                    }
                    else
                    {
                        return RedirectToAction("Login", "Home");
                    }
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

        // GET: Sedes/Details/5
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
                    Sede sede = db.Sedes.Find(id);
                    if (sede == null)
                    {
                        return HttpNotFound();
                    }
                    return View(sede);
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

        // GET: Sedes/Create
        public ActionResult Create()
        {
            if (isUserAuthenticated())
            {
                try
                {
                    ViewBag.DeporteId = new SelectList(db.Deportes, "DeporteID", "DeporteNombre");
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

        // POST: Sedes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SedeId,SedeNombre,DeporteId")] Sede sede)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Sedes.Add(sede);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    ViewBag.DeporteId = new SelectList(db.Deportes, "DeporteID", "DeporteNombre", sede.DeporteId);
                    return View(sede);
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

        // GET: Sedes/Edit/5
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
                    Sede sede = db.Sedes.Find(id);
                    if (sede == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.DeporteId = new SelectList(db.Deportes, "DeporteID", "DeporteNombre", sede.DeporteId);
                    return View(sede);
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

        // POST: Sedes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SedeId,SedeNombre,DeporteId")] Sede sede)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(sede).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.DeporteId = new SelectList(db.Deportes, "DeporteID", "DeporteNombre", sede.DeporteId);
                    return View(sede);
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

        // GET: Sedes/Delete/5
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
                    Sede sede = db.Sedes.Find(id);
                    if (sede == null)
                    {
                        return HttpNotFound();
                    }
                    return View(sede);
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

        // POST: Sedes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    Sede sede = db.Sedes.Find(id);
                    db.Sedes.Remove(sede);
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
