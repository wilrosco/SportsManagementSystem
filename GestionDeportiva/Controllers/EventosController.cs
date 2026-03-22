using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GestionDeportiva.Models;

namespace GestionDeportiva.Controllers
{
    public class EventosController : Controller
    {
        private BD_GestionDeportivaEntities db = new BD_GestionDeportivaEntities();

        // GET: Eventoes
        public ActionResult Index()
        {
            if (isUserAuthenticated())
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
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        // GET: Eventoes/Details/5
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
                    Evento evento = db.Eventos.Find(id);
                    if (evento == null)
                    {
                        return HttpNotFound();
                    }
                    return View(evento);
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

        // GET: Eventoes/Create
        public ActionResult Create()
        {
            if (isUserAuthenticated())
            {
                try
                {
                    var deportes = db.Deportes.ToList().Select(deporte => new SelectListItem
                    {
                        Value = deporte.DeporteNombre.ToString(),
                        Text = deporte.DeporteNombre.ToString()
                    });

                    var sedes = db.Sedes.ToList();

                    //Se puede extraer esta logica a un metodo

                    var sedesFutbol = sedes.Where(x => x.Deporte.DeporteNombre.Equals("Futbol")).Select(sede => new SelectListItem
                    {
                        Value = sede.SedeNombre.ToString(),
                        Text = sede.SedeNombre.ToString()
                    });

                    var sedesBaseball = sedes.Where(x => x.Deporte.DeporteNombre.Equals("Baseball")).Select(sede => new SelectListItem
                    {
                        Value = sede.SedeNombre.ToString(),
                        Text = sede.SedeNombre.ToString()
                    });

                    var sedesBasketball = sedes.Where(x => x.Deporte.DeporteNombre.Equals("Basketball")).Select(sede => new SelectListItem
                    {
                        Value = sede.SedeNombre.ToString(),
                        Text = sede.SedeNombre.ToString()
                    });

                    var sedesFutbolAmericano = sedes.Where(x => x.Deporte.DeporteNombre.Equals("Futbol Americano")).Select(sede => new SelectListItem
                    {
                        Value = sede.SedeNombre.ToString(),
                        Text = sede.SedeNombre.ToString()
                    });

                    var equipos = db.Equipos.ToList();
                    var equiposFutbol = equipos.Where(x => x.Deporte.DeporteNombre.Equals("Futbol")).Select(equipo => new SelectListItem
                    {
                        Value = equipo.EquipoNombre.ToString(),
                        Text = equipo.EquipoNombre.ToString()
                    });
                    var equiposBaseball = equipos.Where(x => x.Deporte.DeporteNombre.Equals("Baseball")).Select(equipo => new SelectListItem
                    {
                        Value = equipo.EquipoNombre.ToString(),
                        Text = equipo.EquipoNombre.ToString()
                    });
                    var equiposBasketball = equipos.Where(x => x.Deporte.DeporteNombre.Equals("Basketball")).Select(equipo => new SelectListItem
                    {
                        Value = equipo.EquipoNombre.ToString(),
                        Text = equipo.EquipoNombre.ToString()
                    });
                    var equiposFutbolAmericano = equipos.Where(x => x.Deporte.DeporteNombre.Equals("Futbol Americano")).Select(equipo => new SelectListItem
                    {
                        Value = equipo.EquipoNombre.ToString(),
                        Text = equipo.EquipoNombre.ToString()
                    });

                    ViewBag.deportes = deportes;
                    ViewBag.sedesFutbol = sedesFutbol;
                    ViewBag.sedesBaseball = sedesBaseball;
                    ViewBag.sedesBasketball = sedesBasketball;
                    ViewBag.sedesFutbolAmericano = sedesFutbolAmericano;
                    ViewBag.equiposFutbol = equiposFutbol;
                    ViewBag.equiposBaseball = equiposBaseball;
                    ViewBag.equiposBasketball = equiposBasketball;
                    ViewBag.equiposFutbolAmericano = equiposFutbolAmericano;
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

        // POST: Eventoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventoId, Equipo1, Equipo2, Tipo, Fecha, Hora, Lugar")] Evento evento)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        evento.Nombre = evento.Equipo1 + " vs " + evento.Equipo2;
                        db.Eventos.Add(evento);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(evento);
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

        // GET: Eventoes/Edit/5
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
                    Evento evento = db.Eventos.Find(id);
                    if (evento == null)
                    {
                        return HttpNotFound();
                    }
                    return View(evento);
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

        // POST: Eventoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventoId,Nombre,Tipo,Fecha,Hora,Lugar")] Evento evento)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(evento).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(evento);
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

        // GET: Eventoes/Delete/5
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
                    Evento evento = db.Eventos.Find(id);
                    if (evento == null)
                    {
                        return HttpNotFound();
                    }
                    return View(evento);
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

        // POST: Eventoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (isUserAuthenticated())
            {
                try
                {
                    Evento evento = db.Eventos.Find(id);
                    db.Eventos.Remove(evento);
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
