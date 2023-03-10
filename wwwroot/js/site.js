// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function verificarClaves() {
    var correo = document.getElementById("correo").value;
    var clave1 = document.getElementById("clave1").value;
    var clave2 = document.getElementById("clave2").value;
    var miBoton = document.getElementById("btncreate");

    var miArray = correo.split("@");
    
    if (clave1 === clave2 && miArray[1] == "utelvt.edu.ec") {
        // Mostrar el botón
        miBoton.style.display = "block";
       
    } else {
        //Ocultar 
        miBoton.style.display = "none";
    }
}
