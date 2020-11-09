var webMotorsApp = angular.module('webMotorsApp', ['toastr']);

// Toastr config
webMotorsApp.config(function (toastrConfig) {
    angular.extend(toastrConfig, {
        autoDismiss: false,
        allowHtml: true,
        extendedTimeOut: 0,
        positionClass: "toast-top-right",
        timeOut: 0,
        closeButton: true,
        tapToDismiss: true,
        progressBar: false,
        closeHtml: "<button>&times;</button>",
        newestOnTop: true,
        maxOpened: 0,
        preventDuplicates: false,
        preventOpenDuplicates: true
    });
});

function ValidarRetorno(response, toastr) {
    if (response.status == 200) {
        return true;
    }
    else if (response.status == 504) {
        // Trata se ocorreu timeout
        toastr.error('A requisição alcançou o tempo limite máximo, favor tentar novamente.');
    }
    else if (response.status == 400 || response.status == 500) {
        if (response.data.erros && response.data.erros.length > 0) {
            for (var i = 0; i < response.data.erros.length; i++) {
                toastr.error(response.data.erros[i]);
            }
        }
        else {
            toastr.error('Falha na requisição');
        }
    }
    else if (typeof response.status == 'number') {
        toastr.error('Falha na requisição, código: ' + response.status + '.');
    }
    else {
        toastr.error('Resposta inesperada recebida, favor tentar novamente.');
    }

    return false;
}