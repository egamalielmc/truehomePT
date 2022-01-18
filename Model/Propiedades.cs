using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TrueHomePT.Model
{
    public class propiedad
    {
        [Key]
        public int id_propiedad { get; set; }
        public string titulo { get; set; }
        public string direccion { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public DateTime fecha_deshabilitado { get; set; }
        public string estatus { get; set; }
    }

    class postgres : DbContext
    {
        public postgres(DbContextOptions<postgres> options) : base(options) { }
        public DbSet<propiedad> propiedad => Set<propiedad>();
    }
}
