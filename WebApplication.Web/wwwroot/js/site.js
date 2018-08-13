// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#login").click(function () {
        $("#myModal").modal();
    });
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

