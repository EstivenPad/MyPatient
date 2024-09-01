function addSolutionDetail() {
    var index = $('#solutionDetails tr').length;
    var row = '<tr>'
        + '<td><input class="form-control" name="MedicalOrder.Solutions[' + index + '].MedicalOrderDetailId" type="hidden" value="0" />'
        + '<input class="form-control" name="MedicalOrder.Solutions[' + index + '].SolutionName" type="text" /></td>'
        + '<td><input class="form-control" name="MedicalOrder.Solutions[' + index + '].Dose" type="text" /></td>'
        + '<td><input class="form-control" name="MedicalOrder.Solutions[' + index + '].Frecuency" type="text" /></td>'
        + '<td><input class="form-control" name="MedicalOrder.Solutions[' + index + '].Via" type="text" /></td>'
        + '<td style="position: relative"><button type="button" class="btn btn-danger visible" style="position: absolute; left: 0%" onclick="removeSolutionDetail(this)">Eliminar</button></td>'
        + '</tr>';
    $('#solutionDetails').append(row);

    // Re-apply validation to the newly added row
    var form = $("form");
    $.validator.unobtrusive.parse(form);
}

function removeSolutionDetail(button) {
    var rows = $('#solutionDetails tr');

    if (rows.length > 1) {
        $(button).closest('tr').remove();
    } else {
        toastr.warning('¡No se puede eliminar la última fila!');
    }
}

function alertDelete(title, text, deleteUrl, redirectUrl) {
    Swal.fire({
        title: title,
        text: "Los cambios son irrevertibles!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#999",
        confirmButtonText: "Si, Eliminar",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: deleteUrl,
                type: "POST",
                headers: {
                    'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {
                    if (response.success) {
                        Swal.fire({
                            title: "Eliminado!",
                            text: text,
                            icon: "success",
                            showConfirmButton: false,
                            timer: 1500
                        }).then(() => {
                            if (redirectUrl) {
                                window.location = redirectUrl;
                            }
                        });
                    } else {
                        if (response.redirectUrl) {
                            window.location = response.redirectUrl;
                        }
                    }
                }
            })
        }
    });
}