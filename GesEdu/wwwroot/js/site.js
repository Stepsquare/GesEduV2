$(document).on('ajaxError', function (event, jqxhr, settings, thrownError) {
    ErrorHandling(jqxhr);
});

function ErrorHandling(error) {
    if (error.responseJSON != null) {
        ErrorToast(error.responseJSON.messages);
    } else if (error.status === 401) {
        location.reload();
    } else if (error.status === 403) {
        ErrorToast(['Utilizador não tem permissões.']);
    }
}

$(document).on('ajaxSuccess', function (event, jqxhr, settings, response) {
    if (response.isRedirect) {
        window.location.href = response.url;
    }
    if (response.isMessage) {
        SuccessToast(response.messages);
    }
});

$(document).on('ajaxStart', function () {
    ToggleFullPageLoader();
});

$(document).on('ajaxComplete', function () {
    ToggleFullPageLoader();
});

function ToggleFullPageLoader() {
    $('body').toggleClass('overflow-hidden');

    $('#loader').fadeToggle('fast');
}

function ShowDivLoader(containerDivId) {
    var divLoader = $('<div id=\"divloader\" style=\"display:none\" class=\"ig-load\"><span class=\"spinner-grow\" role=\"status\"></span> Aguarde por favor...</div>');

    if (typeof containerDivId !== 'undefined') {
        $(`#${containerDivId} .ig-loadable-div`).append(divLoader);
    } else {
        $('.ig-loadable-div').append(divLoader);
    }

    $('#divloader').fadeIn("fast");
}

function HideDivLoader() {
    $('#divloader').fadeOut('fast', function () {
        this.remove()
    });
}

function ShowErrorModal(messages, onclose) {

    var errorList = $('#error_modal .modal-body ul');
    errorList.empty();

    for (var i = 0; i < messages.length; i++) {

        var li = $('<li></li>');
        li.html(messages[i]);
        errorList.append(li);
    }

    $('#error_modal').off('hidden.bs.modal');

    if (onclose != null) {
        $('#error_modal').on('hidden.bs.modal', function () {
            onclose();
        });
    } else {
        $('#error_modal').on('hidden.bs.modal', false);
    }

    $('#error_modal').modal('show');
}

function ShowConfirmModal(obj) {
    const confirmModalEl = document.getElementById('confirm_modal');
    const confirmModal = bootstrap.Modal.getInstance(confirmModalEl) || new bootstrap.Modal(confirmModalEl);

    $('#confirm_modal_title').text(obj.title);
    $('#confirm_modal .modal-body p').text(obj.message);

    // Reset buttons
    $('#confirm_modal_deny_btn').off('click');
    $('#confirm_modal_approve_btn').off('click');

    // Lógica do botão de negação
    if (obj.previousModalId) {
        $('#confirm_modal_deny_btn').on('click', function () {
            confirmModal.hide();

            const prevModalEl = document.getElementById(obj.previousModalId);
            const prevModal = bootstrap.Modal.getInstance(prevModalEl) || new bootstrap.Modal(prevModalEl);

            // Esperar a confirmação fechar antes de abrir a anterior
            $(confirmModalEl).one('hidden.bs.modal', function () {
                prevModal.show();
            });
        });
    } else {
        $('#confirm_modal_deny_btn').on('click', function () {
            confirmModal.hide();
        });
    }

    // Botão de aprovação
    $('#confirm_modal_approve_btn').on('click', function () {
        obj.callbackfunction();
        confirmModal.hide();
    });

    // Se a modal anterior está visível, fecha ela primeiro
    if (obj.previousModalId) {
        const prevModalEl = document.getElementById(obj.previousModalId);
        const prevModal = bootstrap.Modal.getInstance(prevModalEl);

        if (prevModal) {
            $(prevModalEl).one('hidden.bs.modal', function () {
                confirmModal.show();
            });
            prevModal.hide();
        } else {
            confirmModal.show();
        }
    } else {
        confirmModal.show();
    }
}

function SuccessToast(messages) {
    var toastContainer = $('#toast_container');
    toastContainer.empty();

    for (var i = 0; i < messages.length; i++) {
        var toastDiv = $('<div class="ig-toast-success toast hide" role="alert" aria-live="assertive" aria-atomic="true"></div>');
        var toastHeader = $('<div class="toast-header"><strong class="me-auto"> <i class="fas fa-check"></i> Sucesso</strong><button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button></div>');
        var toastBody = $(`<div class="toast-body">${messages[i]}</div >`);

        toastDiv.append(toastHeader);
        toastDiv.append(toastBody);

        toastContainer.append(toastDiv);
    }

    $('#toast_container .toast').each(function () {
        $(this).toast('show');
    });
}

function ErrorToast(errors) {
    var toastContainer = $('#toast_container');
    toastContainer.empty();

    for (var i = 0; i < errors.length; i++) {
        var toastDiv = $('<div class="ig-toast-error toast hide" role="alert" aria-live="assertive" aria-atomic="true"></div>');
        var toastHeader = $('<div class="toast-header"><strong class="me-auto"> <i class="fas fa-exclamation-triangle"></i> Erro</strong><button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button></div>');
        var toastBody = $(`<div class="toast-body">${errors[i]}</div>`);

        toastDiv.append(toastHeader);
        toastDiv.append(toastBody);

        toastContainer.append(toastDiv);
    }

    $('#toast_container .toast').each(function () {
        $(this).toast('show');
    });
}