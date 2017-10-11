//--for Keyup/down focusin/out--------------------\/
function isDash(e) {
    return e.keyCode == 45 ? true : false;
}
function isArrowKeys(e) {
    return e.keyCode >= 33 && e.keyCode <= 40 ? true : false;
}
function isSpace(e) {
    return e.keyCode == 32 ? true : false;
}
function isBackSpace(e) {
    return e.keyCode == 8 ? true : false;
}
function isEnter(e) {
    return e.keyCode == 13 ? true : false;
}
function isEscape(e) {
    return e.keyCode == 27 ? true : false;
}
function isTab(e) {
    return e.keyCode == 9 ? true : false;
}
function isDelete(e) {
    return e.keyCode == 46 ? true : false;
}
function isNumber(e) {
    return !e.shiftKey &&
           ((e.keyCode >= 48 && e.keyCode <= 57) ||
           (e.keyCode >= 96 && e.keyCode <= 105))
           ? true : false;
}
function isLetter(e) {
    return e.keyCode >= 65 && e.keyCode <= 90 ? true : false;
}
function isSelectAll(e) {
    return e.ctrlKey && e.keyCode == 65 ? true : false;
}
function isCopy(e) {
    return e.ctrlKey && e.keyCode == 67 ? true : false;
}
function isPaste(e) {
    return e.ctrlKey && e.keyCode == 86 ? true : false;
}
function isCut(e) {
    return e.ctrlKey && e.keyCode == 88 ? true : false;
}
//------------------------------------------------/\


//--for keypress----------------------------------\/
function isLetterPress(e) { //for lowercase letters, isLetter is uppercase
    return e.keyCode >= 97 && e.keyCode <= 122 ? true : false;
}

function promptSessionExpired() {
    console.log("Session expired.");
    promptMsg("Current session has expired. <br /> Redirect to login page", true);
}

function addTaskForm() {
    //if ($('#body-content').html() == "") {
        toggleUIBlocker();
        $.ajax({
            async: true,
            type: 'GET',
            url: '/Task/addTask',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            dataType: 'html',
            cache: false,
            success: function (result) {
                setTimeout(function () {
                    if (result != "") {
                        $('#body-content').html(result);
                    }
                    else {
                        promptSessionExpired();
                    }
                }, 300);
            },
            error: function () {
                toggleUIBlocker();
                alert('an error has occured');
            }
        })
    //}
    //else {
    //    $('#body-content').html("");
    //}
}

function getTaskView() {
    if ($('#body-content').html() == "") {
        toggleUIBlocker();
        $.ajax({
            async: true,
            type: 'GET',
            url: '/Task/getTask',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            dataType: 'html',
            cache: false,
            success: function (result) {
                setTimeout(function () {
                    if (result != "") {
                        $('#body-content').html(result);
                    }
                    else {
                        promptSessionExpired();
                    }
                }, 300);
            },
            error: function () {
                toggleUIBlocker();
                alert('an error has occured');
            }
        })
    }
    else {
        $('#body-content').html("");
    }
}