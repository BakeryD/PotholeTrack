// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Login Modal
$(document).ready(function () {
    $("#login").click(function () {
        $("#myModal").modal();
    });
});

// Pothole datatable on employee page
//$(document).ready(function () {
//    $('#pothole-table').DataTable({
//        "scrollY": "20vh",
//        "scrollCollapse": true,
//    });
//    $('.dataTables_length').addClass('bs-select');
//});


function saveData() {
    var reportNumber = getRndInteger();
    var base = window.location.href;
    var url = `${base}/api/record/create`;
    var lat = marker.getPosition().lat();
    var lng = marker.getPosition().lng();
    var severity = parseInt(document.querySelector('#marker-severity').selectedOptions[0].value);
    if (lng >= -81.820158 &&
        lng <= -81.535962 &&
        lat <= 41.579773 &&
        lat >= 41.427390) {

        var settings = {
            method: 'POST',
            credentials: 'include',
            body: JSON.stringify({
                DateCreated: new Date(),
                Lattitude: lat,
                Longitude: lng,
                Severity: severity,
                Status: 1,
                ReportCount: 1,
                ReportNumber: 'CLE' + reportNumber
            }),
            headers: {
                'Content-Type': 'application/json'
            }
        };

        fetch(url, settings)
            .then(function (response) {
                if (!response.ok) {
                    console.log(response.statusText);
                    //throw Error(response.statusText);
                }
                return response;
            }).then(function (response) {
                console.log("ok");
            }).catch(error => {
                console.error('Error:', error);
            });
    }

}

function getRndInteger() {
    return Math.floor(Math.random() * (80000 - 35000 + 1)) + 35000;
}

function addCount() {

    var base = window.location.href;
    var url = `${base}/api/record/update`;
    var reportid = 0;
    var settings = {
        method: 'POST',
        credentials: 'include',
        body: JSON.stringify({
            reportid: reportid
        }),
        headers: {
            'Content-Type': 'application/json'
        }
    };

    fetch(url, settings)
        .then(function (response) {
            if (!response.ok) {
                console.log(response.statusText);
                //throw Error(response.statusText);
            }
            return response;
        }).then(function (response) {
            console.log("ok");
        }).catch(error => {
            console.error('Error:', error);
        });

}

var button = $('#logout');
button.on('click', () => {
    document.querySelector('form[name="logout"]').submit();
});


function CountReports() {

}

function incrementReportCount() {
    var recordId = $('form[name="updateRecord"]').children('#p-id').val();
    var base = 'https://localhost:44302';
    var url = `${base}/api/record/update`;
    var settings = {
        method: 'POST',
        credentials: 'include',
        body: JSON.stringify({
            Id: recordId
        }),
        headers: {
            'Content-Type': 'application/json'
        }
    };

    fetch(url, settings)
        .then(function (response) {
            if (!response.ok) {
                console.log(response.statusText);
                //throw Error(response.statusText);
            }
            return response;
        }).then(function (response) {
            console.log("ok");
        }).catch(error => {
            console.error('Error:', error);
        });
}

// COOKIE MANIPULATION

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + exdays * 24 * 60 * 60 * 1000);
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return null;
}

function checkCookie(cname) {
    var user = getCookie(cname);
    if (user !== null) {
        return true;
    } else {
        return false;
    }
}

