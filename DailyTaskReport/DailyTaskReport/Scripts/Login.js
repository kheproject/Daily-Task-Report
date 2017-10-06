$(function () {
    $('#frmLogin').on("submit", function (e) {
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
            error: function () {
                alert('An error has occured during login');
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