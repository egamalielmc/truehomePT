$(document).ready(function () {
    lista_propiedades();
});


function lista_propiedades() {
    $.ajax({
        url: '/Propiedades/',
        type: 'GET',
        dataType: 'json',
        success: function (propiedades) {
            console.log(propiedades);

            var selectList = document.getElementById("lista_propiedad");
            $('#lista_propiedad').empty();

            var option = document.createElement("option");
            option.value = "";
            option.text = "- Seleccione -";
            selectList.appendChild(option);

            for (var i = 0; i < propiedades.length; i++) {

                option = document.createElement("option");
                option.value = propiedades[i].id_propiedad;
                option.text = propiedades[i].titulo;
                selectList.appendChild(option);
            }
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}


function guarda_actividad() {

    var propiedad = $('#lista_propiedad').val();
    var fecha_agenda = $('#fecha').val();
    var titulo = $('#titulo_actividad').val();
    var estatus = $('#estatus_actividad').val();

    if (propiedad.length > 0 && fecha_agenda.length > 0 && titulo.length > 0 && estatus.length > 0) {

        var string_actividad = propiedad + "|" + titulo + "|" + fecha_agenda + "|" + estatus;

        $.ajax({
            url: '/GuardaActividad/' + string_actividad,
            type: 'GET',
            dataType: 'json',
            success: function (respuesta) {

                if (respuesta == 1) {
                    alert("No es posible tal acción, la propiedad ya está ocupda en esa fecha.");
                } else {
                    alert("Actividad guardada.");

                    $('#lista_propiedad').val('');
                    fecha_agenda = $('#fecha').val('');
                    titulo = $('#titulo_actividad').val('');
                    estatus = $('#estatus_actividad').val('');
                }
            },
            error: function (request, message, error) {
                alert("Ocurrió un error.");
            }
        });
    } else {
        alert("Los cambios no pueden ir vacíos.");
    }
}


function guarda_propiedad() {

    var titulo = $('#titulo_propiedad').val();
    var direccion = $('#direccion').val();
    var descripcion = $('#descripcion').val();
    var estatus = $('#estatus_propiedad').val();

    if (titulo.length > 0 && direccion.length > 0 && descripcion.length > 0 && estatus.length > 0) {

        var string_propiedad = titulo + "|" + direccion + "|" + descripcion + "|" + estatus;

        $.ajax({
            url: '/GuardaPropiedad/' + string_propiedad,
            type: 'GET',
            dataType: 'json',
            success: function () {
                alert("Propiedad guardada.");

                $('#titulo_propiedad').val('');
                $('#direccion').val('');
                $('#descripcion').val('');
                $('#estatus_propiedad').val('');

                lista_propiedades();
            },
            error: function (request, message, error) {
                alert("Ocurrió un error.");
            }
        });
    } else {
        alert("Los cambios no pueden ir vacíos.");
    }
}