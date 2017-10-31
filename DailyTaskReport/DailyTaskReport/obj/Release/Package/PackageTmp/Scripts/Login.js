$(function () {
    $('#frmLogin').on("submit", function (e) {
        console_log("logging in...");
        e.preventDefault();
        toggleForm();
        $('#user').attr('readonly', 'true');

        $.ajax({
            async: true,
            type: 'POST',
            url: $(this).attr("action"),
            data: $('form :input').serializeArray(),
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            dataType: 'html',
            cache: false,
            success: function (result) {
                setTimeout(function () { $('form#frmLogin').html(result); toggleForm(); }, 300);
            },
            error: function (xhr, status, errorThrown) {
                ajaxErrorHandler('logging in', status, errorThrown);
                setTimeout(function () { $('#user').removeAttr('readonly'); toggleForm(); }, 300);
            }
        });
    });
});

function toggleForm() {
    if ($('#frmLogin').hasClass('frmShow')) {
        $('#frmLogin').removeClass('frmShow');
        $('#frmLogin').addClass('frmHide');
    }
    else {
        $('#frmLogin').removeClass('frmHide');
        $('#frmLogin').addClass('frmShow');
    }
}