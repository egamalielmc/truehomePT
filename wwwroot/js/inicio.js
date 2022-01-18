$(document).ready(function () {
    lista_actividades();

    var span = document.getElementsByClassName("close")[0];
    var modal = document.getElementById("detalles");

    span.onclick = function () {
        modal.style.display = "none";
    }
});


function lista_actividades() {

    $.ajax({
        url: '/Actividades',
        type: 'GET',
        dataType: 'json',
        success: function (respuesta) {

            console.log(respuesta);
            $('#tabla_lista').empty();
            var tabla = '';

            for (var i = 0; i < respuesta.length; i++) {
                tabla += '<tr>' +
                    '<td>' + respuesta[i].id_actividad + '</td>' +
                    '<td>' + respuesta[i].fecha_agenda + '</td>' +
                    '<td>' + respuesta[i].titulo + '</td>' +
                    '<td>' + respuesta[i].fecha_creacion + '</td>' +
                    '<td>' + respuesta[i].estatus + '</td>' +
                    '<td>' + respuesta[i].condicion + '</td>' +
                    '<td>' + respuesta[i].titulo_propiedad + '</td>' +
                    '<td>' + respuesta[i].direccion + '</td>' +
                    '<td><button class="btn btn-warning" id="' + respuesta[i].id_actividad + '" onclick="detalles(this.id)">detalles</button></td>' +
                    '</tr>';
            }
            $('#tabla_lista').append(tabla);
        },
        error: function (request, message, error) {
            alert("Ocurrió un error.");
        }
    });
}


function detalles(id) {
    console.log(id);

    $.ajax({
        url: '/RegresaActividad/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (respuesta) {

            console.log(respuesta);
            $('#mfecha_agendada').val(respuesta[0].fecha_agenda);
            $('#mtitulo').val(respuesta[0].titulo);
            $('#mtitulo_propiedad').val(respuesta[0].titulo_propiedad);
            $('#mdireccion').val(respuesta[0].direccion);
            $('#mestatus').val(respuesta[0].estatus);

            var boton = '<input type="button" class="btn btn-primary" id="' + respuesta[0].id_actividad + '" onclick = "guardar_cambio(this.id)" value = "Guardar cambios" /> ';
            $('#boton').empty();
            $('#boton').append(boton);
        },
        error: function (request, message, error) {
            alert("Ocurrió un error.");
        }
    });

    $('#detalles').show("slow");
}


function guardar_cambio(id) {
    
    var estatus = $('#mestatus').val();

    $.ajax({
        url: '/CancelaActividad/' + id + '|' + estatus,
        type: 'GET',
        dataType: 'json',
        success: function (respuesta) {

            console.log(respuesta);
            $('#detalles').hide("slow");
            
        },
        error: function (request, message, error) {
            alert("Ocurrió un error.");
        }
    });
}