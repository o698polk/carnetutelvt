// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function verificarClaves() {
    var correo = document.getElementById("correo").value;
    var clave1 = document.getElementById("clave1").value;
    var clave2 = document.getElementById("clave2").value;
    var miBoton = document.getElementById("btncreate");

    var miArray = correo.split("@");
   // && miArray[1] == "utelvt.edu.ec"
    if (clave1 === clave2 ) {
        // Mostrar el botón
        miBoton.style.display = "block";
       
    } else {
        //Ocultar 
        miBoton.style.display = "none";
    }
}

//-------------------------------------///
$(function () {
    var slides = $('.slides'),
        images = slides.find('img');

    images.each(function (i) {
        $(this).attr('data-id', i + 1);
    })

    var typed = new Typed('.typed-words', {
        strings: ["¿Qué esperas? ", " Actualízate", " Vamos", " UTELVT"],
        typeSpeed: 80,
        backSpeed: 80,
        backDelay: 4000,
        startDelay: 1000,
        loop: true,
        showCursor: true,
        preStringTyped: (arrayPos, self) => {
            arrayPos++;
            console.log(arrayPos);
            $('.slides img').removeClass('active');
            $('.slides img[data-id="' + arrayPos + '"]').addClass('active');
        }

    });
})


//-------------------------------------------------//

const $boton = document.querySelector("#btnCapturar"),
    $objetivo = document.querySelector("#carnetid"),
    $contenedorCanvas = document.querySelector("#contenedorCanvas");


$boton.addEventListener("click", () => {
    html2canvas($objetivo)
        .then(canvas => {

            $contenedorCanvas.appendChild(canvas);
        });
    setTimeout(function () {
        $objetivo.style.display = "none";
        window.print();

   

    }, 2000);
    setTimeout(function () {

        $objetivo.style.display = "block";

    }, 10000);
});
window.addEventListener("afterprint", function () {
    // Realizar alguna acción después de que se complete la impresión
    window.location.reload();
});



//----------------------------------------------------//

// Obtener el div donde se mostrará la hora
var divHora = document.getElementById("div-hora");

// Función para actualizar la hora
function actualizarHora() {
    // Obtener la hora actual
    var fecha = new Date();
    var horas = fecha.getHours();
    var minutos = fecha.getMinutes();
    var segundos = fecha.getSeconds();
    var año = fecha.getFullYear();
    var mes = fecha.getMonth() + 1; // Se suma 1 porque los meses empiezan en 0
    var dia = fecha.getDate();
    // Formatear la hora en el formato deseado
    var horaFormateada = dia + "/" + mes + "/" + año +"/"+ horas + ":" + minutos + ":" + segundos;

    // Mostrar la hora en el div correspondiente
    divHora.innerHTML = horaFormateada;
}

// Actualizar la hora cada segundo
setInterval(actualizarHora, 1000);



