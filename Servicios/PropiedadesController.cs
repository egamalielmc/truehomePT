using Microsoft.AspNetCore.Mvc;
using Npgsql;
using TrueHomePT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace TrueHomePT.Servicios
{
    public class PropiedadesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string Get()
        {
            return "Hello World";
        }

        public ActionResult<propiedad> RegresaPropiedades(){
            propiedad pp = new propiedad();
            var cs = "Host=localhost;Username=postgres;Password=a#23ZM).8;Database=postgres";

            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "SELECT * FROM truehomept.propiedad";
            using var cmd = new NpgsqlCommand(sql, con);

            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                //Console.WriteLine("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1),
                //        rdr.GetInt32(2));
                pp.id_propiedad = rdr.GetInt32(0);
                pp.titulo = rdr.GetString(1);
                pp.direccion = rdr.GetString(2);
                pp.descripcion = rdr.GetString(3);
                pp.fecha_creacion = rdr.GetDateTime(4);
                pp.fecha_actualizacion = rdr.GetDateTime(5);
                pp.fecha_deshabilitado = rdr.GetDateTime(6);
                pp.estatus = rdr.GetString(7);
            }

            return pp;
        }
    }
}
