using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GestionDeportiva.Models;

namespace GestionDeportiva.Controllers
{
    public class EquiposController : Controller
    {
        private BD_GestionDeportivaEntities db = new BD_GestionDeportivaEntities();

        // GET: Equipos
        public ActionResult Index()
        {
            if (isUserAuthenticated())
            {
                try
                {
                    var equipos = db.Equipos.Include(e => e.Deporte);
                    return View(equipos.ToList());
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

        // GET: Equipos/Details/5
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
                    Equipos equipos = db.Equipos.Find(id);
                    if (equipos == null)
                    {
                        return HttpNotFound();
                    }
                    return View(equipos);
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

        // GET: Equipos/Create
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

        // POST: Equipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipoId,EquipoNombre,DeporteId")] Equipos equipos)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Equipos.Add(equipos);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    ViewBag.DeporteId = new SelectList(db.Deportes, "DeporteID", "DeporteNombre", equipos.DeporteId);
                    return View(equipos);
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

        // GET: Equipos/Edit/5
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
                    Equipos equipos = db.Equipos.Find(id);
                    if (equipos == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.DeporteId = new SelectList(db.Deportes, "DeporteID", "DeporteNombre", equipos.DeporteId);
                    return View(equipos);
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

        // POST: Equipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipoId,EquipoNombre,DeporteId")] Equipos equipos)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(equipos).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.DeporteId = new SelectList(db.Deportes, "DeporteID", "DeporteNombre", equipos.DeporteId);
                    return View(equipos);
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

        // GET: Equipos/Delete/5
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
                    Equipos equipos = db.Equipos.Find(id);
                    if (equipos == null)
                    {
                        return HttpNotFound();
                    }
                    return View(equipos);
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

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    Equipos equipos = db.Equipos.Find(id);
                    db.Equipos.Remove(equipos);
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
