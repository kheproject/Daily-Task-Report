﻿//--for Keyup/down focusin/out--------------------\/
function isDash(e) {
    //return e.keyCode === 45 ? true : false;
    return e.keyCode === 189 ? true : e.keyCode === 109 ? true : false;
}
function isArrowKeys(e) {
    return e.keyCode >= 33 && e.keyCode <= 40 ? true : false;
}
function isSpace(e) {
    return e.keyCode === 32 ? true : false;
}
function isBackSpace(e) {
    return e.keyCode === 8 ? true : false;
}
function isEnter(e) {
    return e.keyCode === 13 ? true : false;
}
function isEscape(e) {
    return e.keyCode === 27 ? true : false;
}
function isTab(e) {
    return e.keyCode === 9 ? true : false;
}
function isDelete(e) {
    return e.keyCode === 46 ? true : false;
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
    return e.ctrlKey && e.keyCode === 65 ? true : false;
}
function isCopy(e) {
    return e.ctrlKey && e.keyCode === 67 ? true : false;
}
function isPaste(e) {
    return e.ctrlKey && e.keyCode === 86 ? true : false;
}
function isCut(e) {
    return e.ctrlKey && e.keyCode === 88 ? true : false;
}
//------------------------------------------------/\
//--for keypress----------------------------------\/
function isLetterPress(e) { //for lowercase letters, isLetter is uppercase
    return e.keyCode >= 97 && e.keyCode <= 122 ? true : false;
}

function numberOnly() {
    $('.numberOnly').keydown(function (e) {
        if (!isNumber(e) && !isBackSpace(e) && !isEnter(e) && !isTab(e) && !isArrowKeys(e) && !isDelete(e))
            e.preventDefault();
    });
}
function namesOnly() {
    $('.namesOnly').keydown(function (e) {
        if (!isLetter(e) && !isBackSpace(e) && !isEnter(e) && !isTab(e) && !isArrowKeys(e) && !isSpace(e) && !isDelete(e)
            //prevents multiple 'spaces' after each word
            || ($(this).val().split('').reverse().join('').indexOf(' ') === 0 && isSpace(e))
            //prevents from starting 'space' upon input
            || ($(this).val().length === 0 && isSpace(e))) 
            e.preventDefault();
    });
}
function userOnly() {
    $('.userOnly').keydown(function (e) {
        if (!isLetter(e) && !isNumber(e) && !isBackSpace(e) && !isEnter(e) && !isTab(e) && !isArrowKeys(e) && !isDelete(e))
            e.preventDefault();
    });
}
function workOrderFormatOnly() {
    $('.workOrderFormatOnly').keydown(function (e) {
        if ((!isLetter(e) && !isBackSpace(e) && !isEnter(e) && !isTab(e) && !isArrowKeys(e) && !isDelete(e) && !isDash(e) && !isNumber(e)) || isSpace(e))
            e.preventDefault();
    });
}
function promptSessionExpired() {
    console.log("Session expired.");
    promptMsg("Current session has expired. <br /> Redirect to login page", true);
}

function addTaskForm() {
    toggleUIBlocker();
    console_log('called addTaskForm');
    $.ajax({
        async: true,
        type: 'GET',
        url: landing_page + 'Task/addTask',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        dataType: 'html',
        cache: false,
        success: function (result) {
            setTimeout(function () {
                if (result !== "") {
                    $('#body-content').html(result);
                }
                else {
                    promptSessionExpired();
                }
            }, 300);
        },
        error: function (xhr, status, errorThrown) {
            ajaxErrorHandler('addTaskForm', status, errorThrown);
        }
    });
}

function getTaskView() {
    toggleUIBlocker(true);
    console_log('called getTaskView');
    $.ajax({
        async: true,
        type: 'GET',
        url: landing_page + 'Task/getTask',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        dataType: 'html',
        cache: false,
        success: function (result) {
            setTimeout(function () {
                if (result !== "")
                    $('#body-content').html(result);
                else
                    promptSessionExpired();
            }, 300);
        },
        error: function (xhr, status, errorThrown) {
            ajaxErrorHandler('getTaskView', status, errorThrown);
        }
    });
}

function _getTaskView(_user) {
    toggleUIBlocker(true);
    console_log('called getTaskView');
    $.ajax({
        async: true,
        type: 'GET',
        url: landing_page + 'Task/getTask',
        data: '_user=' + _user,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        dataType: 'html',
        cache: false,
        success: function (result) {
            setTimeout(function () {
                if (result !== "")
                    $('#body-content').html(result);
                else
                    promptSessionExpired();
            }, 300);
        },
        error: function (xhr, status, errorThrown) {
            ajaxErrorHandler('getTaskView', status, errorThrown);
        }
    });
}

function ajaxErrorHandler(functionAction, status, errorThrown) {
    console_log(functionAction + ' error ─ status : ' + status + ', errorThrown : ' + errorThrown);
    if (errorThrown === "SyntaxError: Unexpected end of JSON input")
        promptSessionExpired();
    else {
        var msg;
        if (status === "error" && errorThrown === "")
            msg = 'Unable to connect to server...';
        else
            msg = 'An error has occured upon ' + functionAction;
        console_log('Diagnosis : ' + msg);
        promptMsg(msg);
    }
}