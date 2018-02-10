using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab01.Models;
using Lab01.DBContext;

namespace Lab01.Controllers
{
    public class JugadorController : Controller

    {
        DefaultConnection db = DefaultConnection.getInstance;


        // GET: Jugador
        public ActionResult Index()
        {
            return View(db.Jugadores.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "PersonaID,Nombre,Apellido,Club")] Jugador Jugador)
        {
            try
            {
                // TODO: Add insert logic here
                Jugador.JugadorID = ++db.IDActual;
                db.Jugadores.Add(Jugador);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Import()
        {
            //return View();
            return HttpNotFound();
        }
    }
}