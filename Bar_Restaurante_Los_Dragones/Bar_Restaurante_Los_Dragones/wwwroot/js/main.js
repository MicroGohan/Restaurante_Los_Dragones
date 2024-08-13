$(document).ready(function(){
    $('.tooltips-general').tooltip('hide');
    $('.mobile-menu-button').on('click', function () {
        var mobileMenu = $('.navbar-lateral');
        if (mobileMenu.is(':visible')) {
            mobileMenu.fadeOut(300);
        } else {
            mobileMenu.fadeIn(300);
        }
    });

    $(document).ready(function () {
        $('.desktop-menu-button').on('click', function (e) {
            e.preventDefault();
            var NavLateral = $('.navbar-lateral');
            var ContentPage = $('.content-page-container');

            if (NavLateral.is(':visible')) {
                // Oculta el menú lateral y ajusta el padding
                NavLateral.hide();
                ContentPage.css('padding-left', '0');
            } else {
                // Muestra el menú lateral y ajusta el padding
                NavLateral.show();
                ContentPage.css('padding-left', '300px');
            }
        });
    });

    $(window).on('resize', function () {
        var mobileMenu = $('.navbar-lateral');
        var ContentPage = $('.content-page-container');

        if ($(window).width() <= 923) {
            mobileMenu.hide();
            ContentPage.css('padding-left', '0'); // Asegúrate de ajustar el padding en dispositivos móviles
        } else {
            mobileMenu.show();
            ContentPage.css('padding-left', '300px'); // Ajusta el padding cuando el menú lateral está visible
        }
    });

    $('.dropdown-menu-button').on('click', function(e){
        e.preventDefault();
        var icon=$(this).children('.icon-sub-menu');
        if(icon.hasClass('zmdi-chevron-down')){
            icon.removeClass('zmdi-chevron-down').addClass('zmdi-chevron-up');
            $(this).addClass('dropdown-menu-button-active');
        }else{
            icon.removeClass('zmdi-chevron-up').addClass('zmdi-chevron-down');
            $(this).removeClass('dropdown-menu-button-active');
        }
        
        var dropMenu=$(this).next('ul');
        dropMenu.slideToggle('slow');
    });
    // Asegúrate de que jQuery y SweetAlert2 se carguen en tu página
    $('#logout-button').on('click', function (e) {
        e.preventDefault(); // Prevenir la acción por defecto del enlace

        Swal.fire({
            title: '¿Estás seguro?',
            text: "Quieres salir del sistema y cerrar la sesión actual",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#5cb85c',
            confirmButtonText: 'Sí, salir',
            cancelButtonText: 'No, cancelar',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: logoutUrl, // Usa la variable que contiene la URL
                    success: function (response) {
                        if (response.success) {
                            window.location.href = indexUrl; // Redirige al Index del controlador Login
                        }
                    },
                    error: function () {
                        Swal.fire('Error', 'No se pudo cerrar la sesión. Inténtalo de nuevo.', 'error');
                    }
                });
            }
        });
    });





    $('.search-book-button').click(function(e){
        e.preventDefault();
        var LinkSearchBook=$(this).attr("data-href");
        swal({
           title: "¿Qué libro estás buscando?",
           text: "Por favor escribe el nombre del libro",
           type: "input",   
           showCancelButton: true,
           closeOnConfirm: false,
           animation: "slide-from-top",
           cancelButtonText: "Cancelar",
           confirmButtonText: "Buscar",
           confirmButtonColor: "#3598D9",
           inputPlaceholder: "Escribe aquí el nombre de libro" }, 
      function(inputValue){
           if (inputValue === false) return false;  

           if (inputValue === "") {
               swal.showInputError("Debes escribir el nombre del libro");     
               return false;   
           } 
            window.location=LinkSearchBook+"?bookName="+inputValue;
       });
    });
    $('.btn-help').on('click', function(){
        $('#ModalHelp').modal({
            show: true,
            backdrop: "static"
        });
    });
});
(function($){
    $(window).load(function(){
        $(".nav-lateral-scroll").mCustomScrollbar({
            theme:"light-thin",
            scrollbarPosition: "inside",
            autoHideScrollbar: true,
            scrollButtons:{ enable: true }
        });
        $(".custom-scroll-containers").mCustomScrollbar({
            theme:"dark-thin",
            scrollbarPosition: "inside",
            autoHideScrollbar: true,
            scrollButtons:{ enable: true }
        });
    });
})(jQuery);