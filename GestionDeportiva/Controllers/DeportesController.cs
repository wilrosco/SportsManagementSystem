using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GestionDeportiva.Models;

namespace GestionDeportiva.Controllers
{
    public class DeportesController : Controller
    {
        private BD_GestionDeportivaEntities db = new BD_GestionDeportivaEntities();

        // GET: Sports
        public ActionResult Index()
        {
            if (isUserAuthenticated())
            {
                try
                {
                    return View(db.Deportes.ToList());
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

        // GET: Sports/Details/5
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
                    Deporte deporte = db.Deportes.Find(id);
                    if (deporte == null)
                    {
                        return HttpNotFound();
                    }
                    return View(deporte);
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

        // GET: Deportes/Create
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

        // POST: Deportes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeporteID,DeporteNombre")] Deporte deporte)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Deportes.Add(deporte);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(deporte);
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

        // GET: Deportes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    Deporte deporte = db.Deportes.Find(id);
                    if (deporte == null)
                    {
                        return HttpNotFound();
                    }
                    return View(deporte);
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

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

        }

        // POST: Deportes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeporteID,DeporteNombre")] Deporte deporte)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(deporte).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(deporte);
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

        // GET: Deportes/Delete/5
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
                    Deporte deporte = db.Deportes.Find(id);
                    if (deporte == null)
                    {
                        return HttpNotFound();
                    }
                    return View(deporte);
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

        // POST: Deportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    Deporte deporte = db.Deportes.Find(id);
                    db.Deportes.Remove(deporte);
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
