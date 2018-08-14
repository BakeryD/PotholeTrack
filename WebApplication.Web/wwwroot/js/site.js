// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Login Modal
$(document).ready(function () {
    $("#login").click(function () {
        $("#myModal").modal();
    });
});

// Pothole datatable on employee page
$(document).ready(function () {
    $('#pothole-table').DataTable({
        "scrollY": "20vh",
        "scrollCollapse": true,
    });
    $('.dataTables_length').addClass('bs-select');
});


function saveData() {
    var submitter = placeholder('submitter');
    var datecreated = placeholder('datecreated');
    var dateinspected = placeholder('dateinspected');
    var severity = placeholder('severity');
    var repairdate = placeholder('repairdate');
    var status = placeholder('status');
    var reportcount = placeholder('reportcount');
    var description = placeholder('description');
    var latlng = marker.getPosition();
    var url = 'phpsqlinfo_addrow.php?submitter=' + submitter + '&datecreated=' + datecreated + '&dateinspected=' + dateinspected + '&severity=' + severity + '&repairdate=' + repairdate + '&status=' + status + '&reportcount=' + reportcount + '&description=' + description + '&lattitude=' + latlng.lat() + '&longitude=' + latlng.lng();

    downloadUrl(url, function (data, responseCode) {

        if (responseCode == 200 && data.length <= 1) {
            infowindow.close();
            messagewindow.open(map, marker);
        }
    });
}

function downloadUrl(url, callback) {
    var request = window.ActiveXObject ?
        new ActiveXObject('Microsoft.XMLHTTP') :
        new XMLHttpRequest;

    request.onreadystatechange = function () {
        if (request.readyState == 4) {
            request.onreadystatechange = doNothing;
            callback(request.responseText, request.status);
        }
    };

    request.open('GET', url, true);
    request.send(null);
}

function doNothing() {
};


// COOKIE MANIPULATION

if (!checkCookie('loggedInUsr')) {
    $('#employee-btn').attr('href', '#');
    $('#employee-btn').remove('#employee-btn');
   // $('#login').css("display", "none");
} else {
    $('.navbar-right').remove();
}



function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return null;
}

function checkCookie(cname) {
    var user = getCookie(cname);
    if (user != null) {
        return true;
    } else {
        return false;
    }
}