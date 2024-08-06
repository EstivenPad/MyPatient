function DeleteItem(btn) {
    var rows = document.getElementsByTagName("tr");
    var rowCounter = 0;

    for (var i = 0; i < rows.length; i++) {
        console.log(rows[i].innerHTML)
        if (rows[i].innerHTML.includes('False'))
            rowCounter++;
    }

    if (rowCounter == 1) {
        toastr.warning('¡No se puede eliminar la última fila!')
        return;
    }

    var btnId = btn.id.replaceAll('btnremove-', '');
    var idOfIsDeleted = btnId + '__IsDeleted';
    var hidIsDelId = document.querySelector("[id$='" + idOfIsDeleted + "']").id;

    document.getElementById(hidIsDelId).value = "true";

    $(btn).closest('tr').hide();
}

function AddItem(btn) {
    var table = document.getElementById('ExpTable');
    var rows = table.getElementsByTagName('tr');

    var rowOuterHtml = rows[rows.length - 1].outerHTML;

    var lastrowIdx = rows.length - 2;

    var nextrowIdx = eval(lastrowIdx) + 1;

    rowOuterHtml = rowOuterHtml.replaceAll('_' + lastrowIdx + '_', '_' + nextrowIdx + '_');
    rowOuterHtml = rowOuterHtml.replaceAll('[' + lastrowIdx + ']', '[' + nextrowIdx + ']');
    rowOuterHtml = rowOuterHtml.replaceAll('-' + lastrowIdx, '-' + nextrowIdx);

    var newRow = table.insertRow();
    newRow.innerHTML = rowOuterHtml;

    var inputs = document.getElementsByTagName("input");

    for (var x = 0; x < inputs.length; x++) {
        if (inputs[x].type == "text" && inputs[x].id.indexOf('_' + nextrowIdx + '_') > 0)
            inputs[x].value = "";

        if (inputs[x].type == "hidden" && inputs[x].id.indexOf('MedicalOrderDetailId') > 0 && inputs[x].id.indexOf('_' + nextrowIdx + '_') > 0)
            inputs[x].value = "0";

        if (inputs[x].type == "hidden" && inputs[x].id.indexOf('IsDeleted') > 0 && inputs[x].id.indexOf('_' + nextrowIdx + '_') > 0)
            inputs[x].value = "False";
    }

    rebindValidators();
}

function rebindValidators() {
    var form = $("#medicalOrderForm");

    $form.unbind();

    $form.data("validator", null);

    $.validator.unobtrusive.parse($form);

    $form.validate($form.data("unobtrusiveValidation").options);
}

function AlertDelete(title, text, deleteUrl, redirectUrl) {
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
                            timer: 1800
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
