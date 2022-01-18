using Microsoft.AspNetCore.Mvc;
using Npgsql;
using TrueHomePT.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrueHomePT.Servicios
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropiedadesService : ControllerBase
    {
        // GET: api/<PropiedadesService>
        [HttpGet(Name = "GetAllPropiedades")]
        //[Route("GetAllPropiedades")]
        public List<propiedad> Get()
        {
            var cs = "Host=localhost;Username=postgres;Password=a#23ZM).8;Database=postgres";
            //return new string[] { "value1", "value2" };
            var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "SELECT * FROM truehomept.propiedad";
            using var cmd = new NpgsqlCommand(sql, con);

            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            propiedad pp = new propiedad();
            List<propiedad> Lista_Propiedades = new List<propiedad>();

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

                Lista_Propiedades.Add(pp);
            }

            return Lista_Propiedades;
        }

        // GET api/<PropiedadesService>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PropiedadesService>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PropiedadesService>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PropiedadesService>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
