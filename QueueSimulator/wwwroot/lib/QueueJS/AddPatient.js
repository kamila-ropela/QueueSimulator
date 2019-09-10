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
        if (patientCount.length == 0) {
            Swal.fire({
                type: 'warning',
                title: 'Oops...',
                text: 'Zapomniałeś wpisać liczby pacjentów!'
            })
            return;
        }
        $.ajax({
            url: "/Simulation/AddRandomPatients",
            type: "GET",
            data: {
                patientCount: patientCount,
            }
        }).done(function (patientList) {
            Swal.fire({
                type: 'success',
                title: 'Sukces...',
                text: 'Pacjenci zostali dodani!'
            })
        });
    });
});

//Add all random patients from db
$(document).ready(function () {
    $("#AddDb").on("click", function () {
        var patientCount = $("#CountPatient").val();
        if (patientCount.length == 0) {
            Swal.fire({
                type: 'warning',
                title: 'Oops...',
                text: 'Zapomniałeś wpisać liczby pacjentów!'
            })
            return;
        }
        $.ajax({
            url: "/Simulation/AddRandomPatientsFromDB",
            type: "GET",
            data: {
                patientCount: patientCount,
            }
        }).done(function (patientList) {
            Swal.fire({
                type: 'success',
                title: 'Sukces...',
                text: 'Pacjenci zostali dodani!'
            })
        });
    });
});

//Add one random patients
$(document).ready(function () {
    $("#AddOneRandom").on("click", function () {
        $.ajax({
            url: "/Simulation/AddRandomPatients",
            method: "GET",
            data: {
                patientCount: 1,
            }
        }).done(function (patientList) {
            Swal.fire({
                type: 'success',
                title: 'Sukces...',
                text: 'Pacjenci zostali dodani!'
            })
        });
    });
});

//Add one random patients from db
$(document).ready(function () {
    $("#AddOneDb").on("click", function () {
        $.ajax({
            url: "/Simulation/AddRandomPatientsFromDB",
            method: "GET",
            data: {
                patientCount: 1,
            }
        }).done(function (patientList) {
            Swal.fire({
                type: 'success',
                title: 'Sukces...',
                text: 'Pacjenci zostali dodani!'
            })
        });
    });
});