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
    
    var base = 'https://localhost:44302';
    var url = `${base}/api/record`;
    var lat = marker.getPosition().lat();
    var lng = marker.getPosition().lng();
    var settings = {
        method: 'POST',
        credentials: 'include',
        body: JSON.stringify({
            DateCreated: new Date(),
            Lattitude: lat,  
            Longitude: lng, 
            Status: 1,
            ReportCount: 1
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

// COOKIE MANIPULATION




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