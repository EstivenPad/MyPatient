function addSolutionDetail() {
    var index = $('#solutionDetails tr').length;

    var row = `
        <tr>
            <td>
                <input class="form-control" name="MedicalOrder.Solutions[${index}].MedicalOrderDetailId" type="hidden" value="0" />
                <input class="form-control" name="MedicalOrder.Solutions[${index}].SolutionName" type="text" />
            </td>
            <td>
                <input class="form-control" name="MedicalOrder.Solutions[${index}].Dose" type="text" />
            </td>
            <td>
                <input class="form-control" name="MedicalOrder.Solutions[${index}].Frecuency" type="text" />
            </td>
            <td>
                <input class="form-control" name="MedicalOrder.Solutions[${index}].Via" type="text" />
            </td>
            <td style="position: relative">
                <button type="button" class="btn btn-danger visible" style="position: absolute; left: 0%" onclick="removeSolutionDetail(this)">
                    <i class="bi bi-trash-fill"></i>
                </button>
            </td>
        </tr>`;

    $('#solutionDetails').append(row);

    // Re-apply validation to the newly added row
    var form = $("form");
    $.validator.unobtrusive.parse(form);
}

function removeSolutionDetail(button) {
    var rows = $('#solutionDetails tr');

    if (rows.length > 1) {
        $(button).closest('tr').remove();

        //Re-indexing the rest of elements after remove the specific one
        $('#solutionDetails tr').each(function (index, element) {
            $(this).find('input').each(function () {
                var name = $(this).attr('name');
                if (name) {
                    var newName = name.replace(/\[\d+\]/, '[' + index + ']');
                    $(this).attr('name', newName);
                }
            });
        });
    } else {
        toastr.warning('¡No se puede eliminar la última fila!');
    }
}

function addDiscoveryDetail() {
    var index = $('#discoveryDetails tr').length;

    var row = `
        <tr>
            <td class="w-75">
                <input type="hidden" class="form-control" name="SurgicalProcedure.Discoveries[${index}].Id" value="0" />
                <input type="text" name="SurgicalProcedure.Discoveries[${index}].Description" class= "form-control" data-val="true" data-val-required="La Descripción es requerida."/>
                <span class="text-danger" data-valmsg-for="SurgicalProcedure.Discoveries[${index}].Description" data-valmsg-replace="true"></span>
            </td>
            <td style="position: relative">
                <button type="button" class="btn btn-danger visible" style="position: absolute; left: 0%" onclick="removeDiscoveryDetail(this)">
                    <i class="bi bi-trash-fill"></i>
                </button>
            </td>
        </tr>`;

    $('#discoveryDetails').append(row);

    // Re-apply validation to the newly added row
    var form = $("form");
    $.validator.unobtrusive.parse(form);
}

function removeDiscoveryDetail(button) {
    var rows = $('#discoveryDetails tr');

    if (rows.length > 1) {
        $(button).closest('tr').remove();

        //Re-indexing the rest of elements after remove the specific one
        $('#discoveryDetails tr').each(function (index, element) {
            $(this).find('input').each(function () {
                var name = $(this).attr('name');
                if (name) {
                    var newName = name.replace(/\[\d+\]/, '[' + index + ']');
                    $(this).attr('name', newName);
                }
            });

            //Re-indexing also the span error messages
            $(this).find('span').each(function () {
                var name = $(this).attr('data-valmsg-for');
                if (name) {
                    var newName = name.replace(/\[\d+\]/, '[' + index + ']');
                    $(this).attr('data-valmsg-for', newName);
                }
            });
        });
    } else {
        toastr.warning('¡No se puede eliminar la última fila!');
    }
}

function addDoctorDetail() {
    var index = $('#doctorDetails tr').length;

    $.get('/SurgicalProcedure/PopulateDoctorDropList', function (doctorList) {
        var options = doctorList.map((doctor) => {
            return `<option value="${doctor.value}">${doctor.text}</option>`;
        }).join('');
        
        var row = `
            <tr>
                <td class="w-75">
                    <select id="doctorSelect[${index}]" class="form-select doctorSelect" name="SurgicalProcedure.DoctorSurgicalProcedures[${index}].DoctorId">
                        <option disabled selected>--Seleccione Residente--</option>
                        ${options}
                    </select>
                </td>
                <td style="position: relative">
                    <button 
                        type="button"
                        class="btn btn-danger visible"
                        style="position: absolute; left: 0%"
                        onclick="removeDoctorDetail(this)"
                    >
                        <i class="bi bi-trash-fill"></i>
                    </button>
                </td>
            </tr>`;

        $('#doctorDetails').append(row);

        $('#doctorDetails .doctorSelect').last().select2({ theme: 'bootstrap-5' });
    })

    // Re-apply validation to the newly added row
    var form = $("form");
    $.validator.unobtrusive.parse(form);
}

function removeDoctorDetail(button) {
    var rows = $('#doctorDetails tr');

    if (rows.length > 1) {
        $(button).closest('tr').remove();

        //Re-indexing the rest of elements after remove the specific one
        $('#doctorDetails tr').each(function (index, element) {
            $(this).find('select').each(function () {
                var name = $(this).attr('name');
                if (name) {
                    var newName = name.replace(/\[\d+\]/, '[' + index + ']');
                    $(this).attr('name', newName);
                }
            });
        });
    } else {
        toastr.warning('¡No se puede eliminar la última fila!');
    }
}

function alertDelete(title, text, deleteUrl, redirectUrl) {
    Swal.fire({
        title: title,
        text: "ESTOS CAMBIOS SERÁN IRREVERSIBLES!",
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