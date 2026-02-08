function validarFormularioManual(form) {
    let esValido = true;
    const inputs = form.querySelectorAll('input[data-val="true"], select[data-val="true"], textarea[data-val="true"]');

    inputs.forEach(input => {
        const name = input.getAttribute('name');
        const spanError = form.querySelector(`span[data-valmsg-for="${name}"]`);

        input.classList.remove('input-validation-error');
        if (spanError) spanError.innerHTML = "";

        if (!input.value || input.value.trim() === "") {
            esValido = false;
            input.classList.add('input-validation-error');

            if (spanError) {
                const mensaje = input.getAttribute('data-val-required') || "Este campo es obligatorio";
                spanError.innerHTML = `<i class="bi bi-exclamation-circle"></i> ${mensaje}`;
                spanError.classList.add("field-validation-error");
            }
        }
    });

    return esValido;
}

function jQueryAjaxPost(form) {
    try {
        const errorDiv = document.getElementById('error-server-msg');
        if (errorDiv) errorDiv.style.display = 'none';

        if (!validarFormularioManual(form)) {
            return false;
        }

        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.success) {
                    const modalEl = document.getElementById('modalGeneral');
                    const modal = bootstrap.Modal.getInstance(modalEl);
                    modal.hide();
                    actualizarTabla();
                } else {
                    if (errorDiv) {
                        errorDiv.innerText = res.message;
                        errorDiv.style.display = 'block';
                    } else {
                        alert(res.message);
                    }
                }
            },
            error: function (err) {
                console.log(err);
                if (errorDiv) {
                    errorDiv.innerText = "Error de conexión con el servidor";
                    errorDiv.style.display = 'block';
                }
            }
        });
    } catch (ex) {
        console.log(ex);
    }
    return false;
}

let timeout = null;
const txtBusqueda = document.getElementById('txtBusqueda');

if (txtBusqueda) {
    txtBusqueda.addEventListener('keyup', function () {
        clearTimeout(timeout);
        timeout = setTimeout(() => {
            actualizarTabla();
        }, 300);
    });
}

function actualizarTabla() {
    const busqueda = document.getElementById('txtBusqueda').value;
    const radioSexo = document.querySelector('input[name="filtroSexo"]:checked');
    const sexo = radioSexo ? radioSexo.value : "";

    fetch(`/Trabajador/Index?busqueda=${busqueda}&sexo=${sexo}`, {
        headers: { 'X-Requested-With': 'XMLHttpRequest' }
    })
        .then(response => response.text())
        .then(html => {
            document.getElementById('tablaContainer').innerHTML = `<div class="card-body p-0">${html}</div>`;
        });
}

function mostrarModal(url, titulo) {
    const modalElement = document.getElementById('modalGeneral');
    const modalContent = document.getElementById('modalContent');
    const modal = new bootstrap.Modal(modalElement);

    modal.show();

    fetch(url)
        .then(res => res.text())
        .then(html => {
            modalContent.innerHTML = html;
        });
}

function eliminarTrabajador(id) {
    if (confirm('¿Estás seguro de eliminar este registro?')) {
        $.post('/Trabajador/Eliminar', { id: id }, function (res) {
            if (res.success) {
                actualizarTabla();
            } else {
                alert("Error al eliminar");
            }
        });
    }
}