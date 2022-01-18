using System.ComponentModel.DataAnnotations;

namespace TrueHomePT.Model
{
    public class Actividades
    {
        [Key]
        public int id_actividad { get; set; }
        public int id_propiedad { get; set; }
        public string titulo { get; set; }
        public DateTime fecha_agenda { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public string estatus { get; set; }
        public string condicion { get; set; }
        public string titulo_propiedad { get; set; }
        public string direccion { get; set; }
    }
}
