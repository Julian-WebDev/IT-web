$(document).ready(function () {
    $.ajax({
        url: responseURL,  // URL del servidor o endpoint
        type: 'GET',              // Método HTTP (GET, POST, etc.)
        dataType: 'json',         // Tipo de datos que esperas recibir (json, text, xml, etc.)
        success: function (response) {
            // Código que se ejecuta si la solicitud es exitosa
            console.log('Respuesta del servidor:', response);
        },
        error: function (xhr, status, error) {
            // Código que se ejecuta si ocurre un error
            console.error('Error en la solicitud:', status, error);
        }
    });

});