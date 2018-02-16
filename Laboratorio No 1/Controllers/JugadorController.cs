using Laboratorio_No_1.DataBase;
using Laboratorio_No_1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Laboratorio_No_1.Controllers
{

    public class JugadorController : Controller
    {
        BaseDeDatos Datos = BaseDeDatos.getInstance;
        // GET: Jugador
        public ActionResult Index()
        {
            return View(Datos.Players);
        }

        // GET: Jugador/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Jugador JugadorAMostrar = Datos.Players.Find(x => x.Id == id);
            if (JugadorAMostrar== null)
            {
                return HttpNotFound();
            }
            
            return View(JugadorAMostrar);
        }

        // GET: Jugador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jugador/Create
        [HttpPost]
        public ActionResult Create(Jugador NuevoJugador)
        {
            try
            {
                // TODO: Add insert logic here
                NuevoJugador.Id = ++Datos.ActualID;
                Datos.Players.Add(NuevoJugador);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Jugador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return HttpNotFound();
            }

                Jugador JugadorBuscado = Datos.Players.Find(x => x.Id == id);
            if (JugadorBuscado == null)
            {
                return HttpNotFound();
            }
                return View(JugadorBuscado);
               
            


            
        }

        // POST: Jugador/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection Collection)
        {
            try
            {
              
                Jugador JugadorBuscado = Datos.Players.Find(x => x.Id == int.Parse(Collection["Id"]));
                if (JugadorBuscado == null)
                {
                    return HttpNotFound();
                }
                JugadorBuscado.Name = Collection["Name"];
                JugadorBuscado.LastName = Collection["LastName"];
                JugadorBuscado.position = Collection["Position"];
                JugadorBuscado.Salary = double.Parse(Collection["Salary"]);
                JugadorBuscado.Club = Collection["Club"];




                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Jugador/Delete/5
        public ActionResult Delete(int? id)

        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Jugador JugadorABorrar = Datos.Players.Find(x => x.Id == id);
            if (JugadorABorrar == null)
            {
                return HttpNotFound();
            }

            return View(JugadorABorrar);
        }

        // POST: Jugador/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Jugador JugadorABorrar = Datos.Players.Find(x => x.Id == id);
                if (JugadorABorrar == null)
                {
                    HttpNotFound();
                }
                Datos.Players.Remove(JugadorABorrar);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //IMPORTAR ARCHIVO CSV

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {

                    if (upload.FileName.EndsWith(".csv"))
                    {
                        Stream stream = upload.InputStream;
                        StreamReader Lector = new StreamReader(stream);
                        string Linea;
                        //Lee la primera linea, se salta a la segunda porque la primera solo es formato.
                        Linea = Lector.ReadLine();
                        Linea = Lector.ReadLine();
                        while (Linea != null)
                        {
                            //split de la linea
                            string[] Atributos = Linea.Split(',');
                            //Instacia del jugador
                            Jugador NuevoJugador = new Jugador();

                            //Asignación de atributos al objeto
                            NuevoJugador.Club = Atributos[0];
                            NuevoJugador.LastName = Atributos[1];
                            NuevoJugador.Name = Atributos[2];
                            NuevoJugador.position = Atributos[3];
                            NuevoJugador.Salary = double.Parse(Atributos[4]);
                            NuevoJugador.Id = ++Datos.ActualID;

                            //Agregar jugador a la lista.
                            Datos.Players.Add(NuevoJugador);

                            //Leer siguiente linea
                            Linea  = Lector.ReadLine();
                        }
                        
                        return View("Index", Datos.Players);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }

        public ActionResult Deleting()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deleting(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {

                    if (upload.FileName.EndsWith(".csv"))
                    {
                        Stream stream = upload.InputStream;
                        StreamReader Lector = new StreamReader(stream);
                        string Linea;
                        //Lee la primera linea, se salta a la segunda porque la primera solo es formato.
                        Linea = Lector.ReadLine();
                        Linea = Lector.ReadLine();
                        while (Linea != null)
                        {
                            //split de la linea
                            string[] Atributos = Linea.Split(',');
                           Jugador JugadorABorrar = Datos.Players.Find(x => x.Club == Atributos[0] && x.LastName == Atributos[1] &&
                            x.Name == Atributos[2] && x.position == Atributos[3] && x.Salary == double.Parse(Atributos[4]));

                            if (JugadorABorrar != null)
                            {
                                Datos.Players.Remove(JugadorABorrar);
                            }
                            //Leer siguiente linea
                            Linea = Lector.ReadLine();
                        }

                        return View("Index", Datos.Players);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }
    }
}
