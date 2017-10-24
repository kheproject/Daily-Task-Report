﻿$(function () {
    assignLandingPage(window.location.protocol + '//' + window.location.host + $('#landingPageLink').attr('href'));
});

var landing_page;
var developer_console_log_on = true;

function console_log(msg) {
    if (developer_console_log_on)
        console.log(msg);
}

function assignLandingPage(landingPage) {
    landing_page = landingPage;
    console.log(landing_page);
}

function toggleUIBlocker(bool_ShowHide) {
    if ($('#uiBlocker').hasClass('elementShow') || (typeof bool_ShowHide == 'boolean' && !bool_ShowHide)) {
        $('#uiBlocker').removeClass('elementShow');
        $('#uiBlocker').addClass('elementHide');
        $('#ajxLoad').removeClass('elementShow');
        $('#ajxLoad').addClass('elementHide');
    }
    else {
        $('#uiBlocker').removeClass('elementHide');
        $('#uiBlocker').addClass('elementShow');
        $('#ajxLoad').addClass('elementShow');
        $('#ajxLoad').removeClass('elementHide');
    }
}
function promptMsg(str_msg, bool_backtologin) {
    showPrompt()
    document.getElementById("msgr").firstElementChild.innerHTML = str_msg;
    $('#btnMsgr').unbind("click");
    if (typeof bool_backtologin == 'boolean' && bool_backtologin)
        $('#btnMsgr').click(function () {
            console.log('redirecting to : ' + landing_page);
            window.location.href = landing_page;
        })
    else
        $('#btnMsgr').click(function () {
            toggleUIBlocker();
            setTimeout(function () { document.getElementById("msgr").firstElementChild.innerHTML = ""; }, 100);
            if ($('#msgr').hasClass('elementShow')) {
                $('#msgr').removeClass('elementShow');
                $('#msgr').addClass('elementHide');
            }
        })
    $('#btnMsgr').focus();
}

function showPrompt() {
    if ($('#uiBlocker').hasClass('elementHide')) {
        $('#uiBlocker').removeClass('elementHide');
        $('#uiBlocker').addClass('elementShow');
    }
    $('#msgr').removeClass('elementHide');
    $('#msgr').addClass('elementShow');
}