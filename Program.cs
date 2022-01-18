using Npgsql;
using System.Data;
using TrueHomePT.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Conexión
var cs = "Host=localhost;Username=postgres;Password=a#23ZM).8;Database=postgres";
var con = new NpgsqlConnection(cs);

var app = builder.Build();

app.MapGet("/Hola", () => 
    new propiedad().titulo = "A"
);

// Trae todas las actividades
app.MapGet("/Actividades", () =>
{
    if (con.State != ConnectionState.Open)
    {
        con.Open();
    }

    DateTime fecha_mas_dos = DateTime.Now.AddDays(2);
    DateTime fecha_menos_tres = DateTime.Now.AddDays(-3);
    
    string sql = "select 		A.id_actividad, " +
                               "A.fecha_agenda, " +
                               "A.titulo, " +
                               "A.fecha_creacion, " +
                               "A.estatus, " +
                               "B.id_propiedad, " +
                               "B.titulo, " +
                               "B.direccion " +
                 "from        truehomept.actividad A " +
                 "inner join  truehomept.propiedad B on B.id_propiedad = A.id_propiedad";
    using var cmd = new NpgsqlCommand(sql, con);

    using NpgsqlDataReader rdr = cmd.ExecuteReader();

    List<Actividades> lista_actividades = new List<Actividades>();
    Actividades aa = new Actividades();

    while (rdr.Read())
    {
        if(rdr.GetDateTime(1) >= fecha_menos_tres && rdr.GetDateTime(1) <= fecha_mas_dos)
        {
            aa.id_actividad = rdr.GetInt32(0);
            aa.fecha_agenda = rdr.GetDateTime(1);
            aa.titulo = rdr.GetString(2);
            aa.fecha_creacion = rdr.GetDateTime(3);
            aa.estatus = rdr.GetString(4);
            aa.id_propiedad = rdr.GetInt32(5);
            aa.titulo_propiedad = rdr.GetString(6);
            aa.direccion = rdr.GetString(7);

            if(rdr.GetString(4).Equals("Activo") && rdr.GetDateTime(1) >= DateTime.Now)
            {
                aa.condicion = "Pendiente a realizar";
            }

            if (rdr.GetString(4).Equals("Activo") && rdr.GetDateTime(1) < DateTime.Now)
            {
                aa.condicion = "Atrasada";
            }

            if (rdr.GetString(4).Equals("Done"))
            {
                aa.condicion = "Finalizada";
            }

            lista_actividades.Add(aa);
            aa = new Actividades();
        }
    }
    con.Close();
    return lista_actividades;
});


// Regresa actividad
app.MapGet("/RegresaActividad/{id}", (int id) =>
{
    if (con.State != ConnectionState.Open)
    {
        con.Open();
    }

    string sql = "select 		A.id_actividad, " +
                               "A.fecha_agenda, " +
                               "A.titulo, " +
                               "A.fecha_creacion, " +
                               "A.estatus, " +
                               "B.id_propiedad, " +
                               "B.titulo, " +
                               "B.direccion " +
                 "from        truehomept.actividad A " +
                 "inner join  truehomept.propiedad B on B.id_propiedad = A.id_propiedad " +
                 "where         A.id_actividad = " + id;

    using var cmd = new NpgsqlCommand(sql, con);

    using NpgsqlDataReader rdr = cmd.ExecuteReader();

    List<Actividades> lista_actividades = new List<Actividades>();
    Actividades aa = new Actividades();

    while (rdr.Read())
    {
        aa.id_actividad = rdr.GetInt32(0);
        aa.fecha_agenda = rdr.GetDateTime(1);
        aa.titulo = rdr.GetString(2);
        aa.fecha_creacion = rdr.GetDateTime(3);
        aa.estatus = rdr.GetString(4);
        aa.id_propiedad = rdr.GetInt32(5);
        aa.titulo_propiedad = rdr.GetString(6);
        aa.direccion = rdr.GetString(7);

        if (rdr.GetString(4).Equals("Activo") && rdr.GetDateTime(1) >= DateTime.Now)
        {
            aa.condicion = "Pendiente a realizar";
        }

        if (rdr.GetString(4).Equals("Activo") && rdr.GetDateTime(1) < DateTime.Now)
        {
            aa.condicion = "Atrasada";
        }

        if (rdr.GetString(4).Equals("Done"))
        {
            aa.condicion = "Finalizada";
        }

        lista_actividades.Add(aa);
        aa = new Actividades();
    }
    con.Close();
    return lista_actividades;
});


// Cancela Actividad
app.MapGet("/CancelaActividad/{datos}", (string datos) =>
{
    if(con.State != ConnectionState.Open)
    {
        con.Open();
    }

    string sql = "update truehomept.actividad set estatus = " + datos.Split('|')[1] +
                " where id_actividad = " + Int32.Parse(datos.Split('|')[0]);
    using var cmd = new NpgsqlCommand(sql, con);

    cmd.ExecuteNonQuery();
    con.Close();
    return 1;
});


// Trae todas las propiedades
app.MapGet("/Propiedades", () => {

    if (con.State != ConnectionState.Open)
    {
        con.Open();
    }

    string sql = "SELECT * FROM truehomept.propiedad where estatus = 'Activo'";
    using var cmd = new NpgsqlCommand(sql, con);

    using NpgsqlDataReader rdr = cmd.ExecuteReader();

    propiedad pp = new propiedad();
    List<propiedad> Lista_Propiedades = new List<propiedad>();

    while (rdr.Read())
    {
        pp.id_propiedad = rdr.GetInt32(0);
        pp.titulo = rdr.GetString(1);
        pp.direccion = rdr.GetString(2);
        pp.descripcion = rdr.GetString(3);
        pp.fecha_creacion = rdr.GetDateTime(4);
        pp.fecha_actualizacion = rdr.GetDateTime(5);
        pp.fecha_deshabilitado = rdr.GetDateTime(6);
        pp.estatus = rdr.GetString(7);

        Lista_Propiedades.Add(pp);
        pp = new propiedad();
    }
    con.CloseAsync();
    return Lista_Propiedades;
});



// Guarda propiedades
app.MapGet("/GuardaPropiedad/{propiedad}", (string propiedad) =>
{
    if (con.State != ConnectionState.Open)
    {
        con.Open();
    }

    propiedad pp = new propiedad();

    pp.titulo = propiedad.Split('|')[0];
    pp.direccion = propiedad.Split('|')[1];
    pp.descripcion = propiedad.Split('|')[2];
    pp.estatus = propiedad.Split('|')[3];

    string sql = "insert into truehomept.propiedad(" +
                "titulo, direccion, descripcion, fecha_creacion, fecha_actualizacion, fecha_deshabilitado, estatus)" +
                "values('" + pp.titulo + "','" +
                        pp.direccion + "','" +
                        pp.descripcion + "'," +
                        "current_timestamp, current_timestamp, current_timestamp,'" +
                        pp.estatus + "')";
    using var cmd = new NpgsqlCommand(sql, con);

    cmd.ExecuteNonQuery();
    con.CloseAsync();
    return 1;
});


// Guarda actividades
app.MapGet("/GuardaActividad/{actividad}", (string actividad) =>
{
    if (con.State != ConnectionState.Open)
    {
        con.Open();
    }

    int respuesta = 0;
    Actividades aa = new Actividades();

    aa.id_propiedad = Int32.Parse(actividad.Split('|')[0]);
    aa.titulo = actividad.Split('|')[1];
    aa.fecha_agenda = DateTime.Parse(actividad.Split('|')[2]);
    aa.estatus = actividad.Split('|')[3];

    string sql = "select coalesce(count(*), 0) from truehomept.actividad where id_propiedad = " + aa.id_propiedad +
                 " and fecha_agenda = '" + aa.fecha_agenda + "'";

    using var cmd = new NpgsqlCommand(sql, con);

    using NpgsqlDataReader rdr = cmd.ExecuteReader();

    while (rdr.Read())
    {
        respuesta = rdr.GetInt32(0);
    }

    con.Close();

    if (respuesta == 0)
    {
        if (con.State != ConnectionState.Open)
        {
            con.Open();
        }

        var sql2 = "insert into truehomept.actividad(" +
             "id_propiedad, titulo, fecha_agenda, fecha_creacion, fecha_actualizacion, estatus)" +
             "values('" +
                aa.id_propiedad + "','" +
                aa.titulo + "','" +
                aa.fecha_agenda + "'," +
                "current_timestamp, current_timestamp, '" +
                aa.estatus + "')";
        using var cmd2 = new NpgsqlCommand(sql2, con);


        cmd2.ExecuteNonQuery();

        respuesta = 0;
    }
    else
    {
        respuesta = 1;
    };
    con.Close();
    return respuesta;
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();