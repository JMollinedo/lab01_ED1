using Laboratorio_No_1.DataBase;
using Laboratorio_No_1.Models;
using ListasArtesanales;
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
            Jugador JugadorAMostrar = Datos.Players.FirstOrDefault(x => x.Id == id);
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
                Datos.Players.AddFirst(NuevoJugador);
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

                Jugador JugadorBuscado = Datos.Players.FirstOrDefault(x => x.Id == id);
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
              
                Jugador JugadorBuscado = Datos.Players.FirstOrDefault(x => x.Id == int.Parse(Collection["Id"]));
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
            Jugador JugadorABorrar = Datos.Players.FirstOrDefault(x => x.Id == id);
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
                Jugador JugadorABorrar = Datos.Players.FirstOrDefault(x => x.Id == id);
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
                            Datos.Players.AddLast(NuevoJugador);

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
                           Jugador JugadorABorrar = Datos.Players.FirstOrDefault(x => x.Club == Atributos[0] && x.LastName == Atributos[1] &&
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
        //------------------------------------------------Aquí Empiezan las búsquedas----------------------------------------------


        //-----------------------------------------------------Nombre--------------------------------------------------------------
        public ActionResult SerchByName()
        {
            return View(Datos.Players);
        }

        [HttpPost]
        public ActionResult SerchByName(FormCollection Collection)
        {
            Datos.SearchedPlayers.Clear();
            foreach (var item in Datos.Players)
            {
                if (item.Name.ToUpper() == Collection["Name"].ToUpper())
                {
                    Datos.SearchedPlayers.AddLast(item);
                }
            }
                return View(Datos.SearchedPlayers);

        }
        //-----------------------------------------------------Apellido--------------------------------------------------------------
        public ActionResult SearchByLastName()
        {
            return View(Datos.Players);
        }

        [HttpPost]
        public ActionResult SearchByLastName(FormCollection Collection)
        {
            Datos.SearchedPlayers.Clear();
            foreach (var item in Datos.Players)
            {
                if (item.LastName.ToUpper() == Collection["LastName"].ToUpper())
                {
                    Datos.SearchedPlayers.AddLast(item);
                }
            }
            return View(Datos.SearchedPlayers);

        }
        //-----------------------------------------------------Club--------------------------------------------------------------
        public ActionResult SearchByClub()
        {
            return View(Datos.Players);
        }

        [HttpPost]
        public ActionResult SearchByClub(FormCollection Collection)
        {
            Datos.SearchedPlayers.Clear();
            foreach (var item in Datos.Players)
            {
                if (item.Club.ToUpper() == Collection["Club"].ToUpper())
                {
                    Datos.SearchedPlayers.AddLast(item);
                }
            }
            return View(Datos.SearchedPlayers);

        }
        //-----------------------------------------------------Posición--------------------------------------------------------------
        public ActionResult SearchByPosition()
        {
            return View(Datos.Players);
        }

        [HttpPost]
        public ActionResult SearchByPosition(FormCollection Collection)
        {
            Datos.SearchedPlayers.Clear();
            foreach (var item in Datos.Players)
            {
                if (item.position.ToUpper() == Collection["Position"].ToUpper())
                {
                    Datos.SearchedPlayers.AddLast(item);
                }
            }
            return View(Datos.SearchedPlayers);
        }
        //-----------------------------------------------------Salario--------------------------------------------------------------
        public ActionResult SearchBySalary(int Order)
        {
            return View(Datos.Players);
        }

        [HttpPost]
        public ActionResult SearchBySalary(int order, FormCollection Collection)
        {
            Datos.SearchedPlayers.Clear();
            if (order == 1)
            {
                foreach (var item in Datos.Players)
                {
                    try
                    {
                        if (item.Salary > int.Parse(Collection["Salary"]))
                        {
                            Datos.SearchedPlayers.AddLast(item);
                        }
                    }
                    catch (Exception)
                    {

                        HttpNotFound();
                    }
                    
                }
            }
            else if(order == 2)
            {
                foreach (var item in Datos.Players)
                {
                    try
                    {
                        if (item.Salary < int.Parse(Collection["Salary"]))
                        {
                            Datos.SearchedPlayers.AddLast(item);
                        }
                    }
                    catch (Exception)
                    {

                        HttpNotFound();
                    }

                }
            }
            else
            {
                foreach (var item in Datos.Players)
                {
                    try
                    {
                        if (item.Salary == int.Parse(Collection["Salary"]))
                        {
                            Datos.SearchedPlayers.AddLast(item);
                        }
                    }
                    catch (Exception)
                    {

                        HttpNotFound();
                    }

                }
            }
            
            
            return View(Datos.SearchedPlayers);

        }




        //-------------------------------------------------------Implementación Lista Artesanal------------------------------------

        public ActionResult IndexGeneric()
        {
            return View(Datos.PlayersG);

        }

        public ActionResult CreateGeneric()
        {
            return View();
        }

        // POST: Jugador/Create
        [HttpPost]
        public ActionResult CreateGeneric(Jugador NuevoJugador)
        {
            try
            {
                // TODO: Add insert logic here
                NuevoJugador.Id = ++Datos.ActualIDG;
                Datos.PlayersG.Add(NuevoJugador);
                return RedirectToAction("IndexGeneric");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UploadGeneric()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadGeneric(HttpPostedFileBase upload)
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
                            NuevoJugador.Id = ++Datos.ActualIDG;

                            //Agregar jugador a la lista.
                            Datos.PlayersG.Add(NuevoJugador);

                            //Leer siguiente linea
                            Linea = Lector.ReadLine();
                        }

                        return View("IndexGeneric", Datos.PlayersG);
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

        public ActionResult DetailsGeneric(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Jugador JugadorAMostrar = Datos.PlayersG.FirstOrDefault(x => x.Id == id);
            if (JugadorAMostrar == null)
            {
                return HttpNotFound();
            }

            return View(JugadorAMostrar);
        }


        public ActionResult EditGeneric(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Jugador JugadorBuscado = Datos.PlayersG.FirstOrDefault(x => x.Id == id);
            if (JugadorBuscado == null)
            {
                return HttpNotFound();
            }
            return View(JugadorBuscado);





        }

        // POST: Jugador/Edit/5
        [HttpPost]
        public ActionResult EditGeneric(FormCollection Collection)
        {
            try
            {

                Jugador JugadorBuscado = Datos.PlayersG.FirstOrDefault(x => x.Id == int.Parse(Collection["Id"]));
                if (JugadorBuscado == null)
                {
                    return HttpNotFound();
                }
                JugadorBuscado.Name = Collection["Name"];
                JugadorBuscado.LastName = Collection["LastName"];
                JugadorBuscado.position = Collection["Position"];
                JugadorBuscado.Salary = double.Parse(Collection["Salary"]);
                JugadorBuscado.Club = Collection["Club"];




                return RedirectToAction("IndexGeneric");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult DeleteGeneric(int? id)

        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Jugador JugadorABorrar = Datos.PlayersG.FirstOrDefault(x => x.Id == id);
            if (JugadorABorrar == null)
            {
                return HttpNotFound();
            }

            return View(JugadorABorrar);
        }

        // POST: Jugador/Delete/5
        [HttpPost]
        public ActionResult DeleteGeneric(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Jugador JugadorABorrar = Datos.PlayersG.FirstOrDefault(x => x.Id == id);
                if (JugadorABorrar == null)
                {
                    HttpNotFound();
                }
                Datos.PlayersG.Remove(JugadorABorrar);
                return RedirectToAction("IndexGeneric");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult DeletingGeneric()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletingGeneric(HttpPostedFileBase upload)
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
                            Jugador JugadorABorrar = Datos.PlayersG.FirstOrDefault(x => x.Club == Atributos[0] && x.LastName == Atributos[1] &&
                             x.Name == Atributos[2] && x.position == Atributos[3] && x.Salary == double.Parse(Atributos[4]));

                            if (JugadorABorrar != null)
                            {
                                Datos.PlayersG.Remove(JugadorABorrar);
                            }
                            //Leer siguiente linea
                            Linea = Lector.ReadLine();
                        }

                        return View("IndexGeneric", Datos.PlayersG);
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

        //-----------------------------------------------------Aquí empiezan las búsquedas Artesanales---------------------------------

        //-----------------------------------------------------Nombre------------------------------------------------------------------
        public ActionResult SerchByNameGeneric()
        {
            return View(Datos.PlayersG);
        }

        [HttpPost]
        public ActionResult SerchByNameGeneric(FormCollection Collection)
        {
            Datos.SearchedPlayersG.Clear();
            foreach (var item in Datos.PlayersG)
            {
                if (item.Name.ToUpper() == Collection["Name"].ToUpper())
                {
                    Datos.SearchedPlayersG.Add(item);
                }
            }
            return View(Datos.SearchedPlayersG);

        }

        //-----------------------------------------------------Apellido--------------------------------------------------------------
        public ActionResult SearchByLastNameGeneric()
        {
            return View(Datos.PlayersG);
        }

        [HttpPost]
        public ActionResult SearchByLastNameGeneric(FormCollection Collection)
        {
            Datos.SearchedPlayersG.Clear();
            foreach (var item in Datos.PlayersG)
            {
                if (item.LastName.ToUpper() == Collection["LastName"].ToUpper())
                {
                    Datos.SearchedPlayersG.Add(item);
                }
            }
            return View(Datos.SearchedPlayersG);

        }

        //-----------------------------------------------------Club--------------------------------------------------------------
        public ActionResult SearchByClubGeneric()
        {
            return View(Datos.PlayersG);
        }

        [HttpPost]
        public ActionResult SearchByClubGeneric(FormCollection Collection)
        {
            Datos.SearchedPlayersG.Clear();
            foreach (var item in Datos.PlayersG)
            {
                if (item.Club.ToUpper() == Collection["Club"].ToUpper())
                {
                    Datos.SearchedPlayersG.Add(item);
                }
            }
            return View(Datos.SearchedPlayersG);

        }
        //-----------------------------------------------------Posición--------------------------------------------------------------
        public ActionResult SearchByPositionGeneric()
        {
            return View(Datos.PlayersG);
        }

        [HttpPost]
        public ActionResult SearchByPositionGeneric(FormCollection Collection)
        {
            Datos.SearchedPlayersG.Clear();
            foreach (var item in Datos.PlayersG)
            {
                if (item.position.ToUpper() == Collection["Position"].ToUpper())
                {
                    Datos.SearchedPlayersG.Add(item);
                }
            }
            return View(Datos.SearchedPlayersG);
        }
        //-----------------------------------------------------Salario--------------------------------------------------------------
        public ActionResult SearchBySalaryGeneric(int Order)
        {
            return View(Datos.PlayersG);
        }

        [HttpPost]
        public ActionResult SearchBySalaryGeneric(int order, FormCollection Collection)
        {
            Datos.SearchedPlayersG.Clear();
            if (order == 1)
            {
                foreach (var item in Datos.PlayersG)
                {
                    try
                    {
                        if (item.Salary > int.Parse(Collection["Salary"]))
                        {
                            Datos.SearchedPlayersG.Add(item);
                        }
                    }
                    catch (Exception)
                    {

                        HttpNotFound();
                    }

                }
            }
            else if (order == 2)
            {
                foreach (var item in Datos.PlayersG)
                {
                    try
                    {
                        if (item.Salary < int.Parse(Collection["Salary"]))
                        {
                            Datos.SearchedPlayersG.Add(item);
                        }
                    }
                    catch (Exception)
                    {

                        HttpNotFound();
                    }

                }
            }
            else
            {
                foreach (var item in Datos.PlayersG)
                {
                    try
                    {
                        if (item.Salary == int.Parse(Collection["Salary"]))
                        {
                            Datos.SearchedPlayersG.Add(item);
                        }
                    }
                    catch (Exception)
                    {

                        HttpNotFound();
                    }

                }
            }


            return View(Datos.SearchedPlayersG);

        }
    }


}
