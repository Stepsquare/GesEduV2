
$(document).on("ajaxError", function (event, jqxhr, settings, thrownError) {
    if (jqxhr.responseJSON != null) {
        ErrorToast(jqxhr.responseJSON.messages);
    } else if (jqxhr.status === 401) {
        location.reload();
    } else if (jqxhr.status === 403) {
        ErrorToast(['Utilizador não tem permissões.']);
    }
});

$(document).on("ajaxSuccess", function (event, jqxhr, settings, response) {
    if (response.isRedirect) {
        window.location.href = response.url;
    }
    if (response.isMessage) {
        SuccessToast(response.message);
    }
});

$(document).on("ajaxStart" ,function () {
    ToggleFullPageLoader();
});

$(document).on("ajaxComplete", function () {
    ToggleFullPageLoader();
});

function GoBack() {
    window.history.back();
}

function ToggleFullPageLoader() {
    $('body').toggleClass('overflow-hidden');

    $('#loader').fadeToggle("fast");
}

function ShowDivLoader(div) {
    var divLoader = $("<div id=\"divloader\" style=\"display:none\" class=\"ig-load\"><span class=\"spinner-grow\" role=\"status\"></span> Aguarde por favor...</div>");
    $(".ig-loadable-div").append(divLoader);
    $("#divloader").fadeIn("fast");
}

function HideDivLoader() {
    $("#divloader").fadeOut("fast", function () {
        this.remove()
    });
}

function ShowErrorModal(messages, onclose) {

    var errorList = $("#error_modal .modal-body ul");
    errorList.empty();

    for (var i = 0; i < messages.length; i++) {

        var li = $("<li></li>");
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

    $("#error_modal").modal("show");
}

function ShowConfirmModal(title, message, callbackfunction) {

    $("#confirm_modal_btn").prop("onclick", null).off("click");

    $("#confirm_modal_title").html(title);
    $("#confirm_modal .modal-body").html(message);

    $("#confirm_modal_btn").on('click', function () {
        callbackfunction();
    });

    $("#confirm_modal").modal("show");
}

function SuccessToast(message) {
    var toastContainer = $("#toast_container");
    toastContainer.empty();

    var toastDiv = $("<div class=\"ig-toast-success toast hide\" role=\alert\" aria-live=\"assertive\" aria-atomic=\"true\"></div>");
    var toastHeader = $("<div class=\"toast-header\"><strong class=\"me-auto\"> <i class=\"fas fa-check\"></i> Sucesso</strong><button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"toast\"aria-label=\"Close\"></button></div> ");
    var toastBody = $("<div class=\"toast-body\">" + message + "</div >");

    toastDiv.append(toastHeader);
    toastDiv.append(toastBody);

    toastContainer.append(toastDiv);

    $("#toast_container .toast").each(function () {
        $(this).toast('show');
    });
}

function ErrorToast(errors) {
    var toastContainer = $("#toast_container");
    toastContainer.empty();

    for (var i = 0; i < errors.length; i++) {
        var toastDiv = $("<div class=\"ig-toast-error toast hide\" role=\alert\" aria-live=\"assertive\" aria-atomic=\"true\"></div>");
        var toastHeader = $("<div class=\"toast-header\"><strong class=\"me-auto\"> <i class=\"fas fa-exclamation-triangle\"></i> Erro</strong><button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"toast\"aria-label=\"Close\"></button></div> ");
        var toastBody = $("<div class=\"toast-body\">" + errors[i] + "</div >");

        toastDiv.append(toastHeader);
        toastDiv.append(toastBody);

        toastContainer.append(toastDiv);
    }

    $("#toast_container .toast").each(function () {
        $(this).toast('show');
    });
}