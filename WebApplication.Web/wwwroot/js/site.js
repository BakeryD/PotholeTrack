// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Login Modal
$(document).ready(function () {
    $("#login").click(function () {
        $("#myModal").modal();
    });
});

function saveData() {
    var reportNumber = getRndInteger();
    var base = window.location.protocol + "//" + window.location.host;
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
                Lattitude: lat,
                Longitude: lng,
                Severity: severity,
                Status: 1,
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


var button = $('#logout');
button.on('click', () => {
    document.querySelector('form[name="logout"]').submit();
});


function incrementReportCount() {
    var recordId = $('form[name="updateRecord"]').children('#p-id').val();
    var base = window.location.protocol + "//" + window.location.host;
    var url = `${base}/api/record/AddReport`;
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

function updateReport() {
    var base = window.location.protocol + "//" + window.location.host;
    var id = $('#report-id', $('#employee-modal')).val();
    var url = `${base}/api/record/update`;
    var dateInspected = $('#dateinspected', $('#employee-modal')).val();
    var dateRepaired = $('#daterepaired', $('#employee-modal')).val();
    var description = $('#description', $('#employee-modal')).val();
    var status = $('#status', $('#employee-modal')).val();
    var severity = $('#severity', $('#employee-modal')).val();


    if (dateInspected === "") {
        dateInspected = null;
    }
    if (dateRepaired === "") {
        dateRepaired = null;
    }
    if (description === "") {
        description = null;
    }

    var settings = {
        method: 'POST',
        credentials: 'include',
        body: JSON.stringify({
            //Only passing in values that are being changed and the id so that we
            // can get the existing report when updating
            // Also passing in anything marked as required in the model
            // Otherwise we get a 400 error
            Id : id,
            Severity: severity,
            Status: status,
            Description: description,
            DateInspected: dateInspected,
            DateRepaired: dateRepaired,
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