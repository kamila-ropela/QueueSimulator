//Add new patients
$(document).ready(function () {
    $("#AddNew").on("click", function () {
        var patientCount = $("#CountPatient").val();
        $.ajax({
            url: "/Simulation/AddPatient",
            type: "GET",
            data: {
                patientCount: patientCount,
            }
        }).done(function (patientForm) {
            $("#main").html(patientForm);
        });
    });
});

//Add all random patients
$(document).ready(function () {
    $("#AddRandom").on("click", function () {
        var patientCount = $("#CountPatient").val();
        $.ajax({
            url: "/Simulation/AddRandomPatients",
            type: "GET",
            data: {
                patientCount: patientCount,
            }
        }).done(function (patientList) {
            $("#main").html(patientList);
        });
    });
});

//Add all random patients from db
$(document).ready(function () {
    $("#AddDb").on("click", function () {
        var patientCount = $("#CountPatient").val();
        $.ajax({
            url: "/Simulation/AddRandomPatientsFromDB",
            type: "GET",
            data: {
                patientCount: patientCount,
            }
        }).done(function (patientList) {
            $("#main").html(patientList);
        });
    });
});