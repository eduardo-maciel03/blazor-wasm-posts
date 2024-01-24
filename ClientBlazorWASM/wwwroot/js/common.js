window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operacão Correta", { timeOut: 10000 });
    }
    if (type === "error") {
        toastr.error(message, "Operacão Falhou", { timeOut: 10000 });
    }
}

window.ShowSwal = (type, message) => {
    if (type === "success") {
        Swal.fire(
            'Success Notification',
            message,
            'success'
        );
    }
    if (type === "error") {
        Swal.fire(
            'Error Notification',
            message,
            'error'
        );
    }
}

function MostrarModalConfirmacaoApagado() {
    $('#modalConfirmacaoApagar').modal('show');
}

function OcultarModalConfirmacaoApagado() {
    $('#modalConfirmacaoApagar').modal('hide');
}