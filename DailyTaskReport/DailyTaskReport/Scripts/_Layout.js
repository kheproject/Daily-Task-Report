function toggleUIBlocker() {
    if ($('#uiBlocker').hasClass('elementHide')) {
        $('#uiBlocker').removeClass('elementHide');
        $('#uiBlocker').addClass('elementShow');
    }
    else {
        $('#uiBlocker').removeClass('elementShow');
        $('#uiBlocker').addClass('elementHide');
    }
}