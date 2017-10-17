function toggleUIBlocker() {
    if ($('#uiBlocker').hasClass('elementHide')) {
        $('#uiBlocker').removeClass('elementHide');
        $('#uiBlocker').addClass('elementShow');
        $('#ajxLoad').addClass('elementShow');
        $('#ajxLoad').removeClass('elementHide');
    }
    else {
        $('#uiBlocker').removeClass('elementShow');
        $('#uiBlocker').addClass('elementHide');
        $('#ajxLoad').removeClass('elementShow');
        $('#ajxLoad').addClass('elementHide');
    }
    setTimeout(function () { document.getElementById("msgr").firstElementChild.innerHTML = ""; }, 100);
    if ($('#msgr').hasClass('elementShow')) {
        $('#msgr').removeClass('elementShow');
        $('#msgr').addClass('elementHide');
    }
}
function promptMsg(str_msg, bool_backtologin) {
    showPrompt()
    document.getElementById("msgr").firstElementChild.innerHTML = str_msg;
    $('#btnMsgr').unbind("click");
    if (typeof bool_backtologin == 'boolean' && bool_backtologin)
        $('#btnMsgr').click(function () { window.location.href = "../"; })
    else
        $('#btnMsgr').click(function () { toggleUIBlocker(); })
}

function showPrompt() {
    if ($('#uiBlocker').hasClass('elementHide')) {
        $('#uiBlocker').removeClass('elementHide');
        $('#uiBlocker').addClass('elementShow');
    }
    $('#msgr').removeClass('elementHide');
    $('#msgr').addClass('elementShow');
}