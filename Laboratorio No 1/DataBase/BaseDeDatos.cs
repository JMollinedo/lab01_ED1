using Laboratorio_No_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListasArtesanales;

namespace Laboratorio_No_1.DataBase
{
    public class BaseDeDatos
    {

        private static volatile BaseDeDatos Instance;
        private static object syncRoot = new Object();

        public LinkList <Jugador> Players = new LinkList<Jugador>();
        public int ActualID { get; set; }

        private BaseDeDatos()
        {
            ActualID = 0;
        }

        public static BaseDeDatos getInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Instance == null)
                        {
                            Instance = new BaseDeDatos();
                        }
                    }
                }
                return Instance;
            }
        }



    }
}