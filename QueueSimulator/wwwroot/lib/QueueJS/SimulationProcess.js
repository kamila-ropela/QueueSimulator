//start simulation
$(document).ready(function () {
    $("#start").on("click", function () {
        var CountPatient = $("#CountPatient").val();
        var CountIteration = $("#CountIteration").val();
        var Algorytm = $("#Algorytm").val();
        var ReturnToQuery = $("#ReturnToQuery").val();
        var AddToQuery = $("#AddToQuery").val();
        var TwoQuery = $("#TwoQuery").val();
        var DoctorCount = $("#DoctorCount").val();

        if (CountIteration.length == 0)
            CountIteration = 10;
        if (DoctorCount.length == 0)
            DoctorCount = 4;
        //if (CountPatient.length == 0) {
        //    Swal.fire({
        //        type: 'error',
        //        title: 'Oops...',
        //        text: 'Zapomniałeś dodać pacjentów!'
        //    })
        //    return;
        //}

        $.ajax({
            async: false,
            url: "/Simulation/StartSimulation",
            type: "GET",
            data: {
                CountPatient: CountPatient,
                CountIteration: CountIteration,
                Algorytm: Algorytm,
                ReturnToQuery: ReturnToQuery,
                AddToQuery: AddToQuery,
                TwoQuery: TwoQuery,
                DoctorCount: DoctorCount
            }
        }).done(function (patientList) {
            $("#main").html(patientList);
        });

        var iteration = 0;
        var intervalID = window.setInterval(function () {

            $.ajax({
                async: false,
                url: "/Simulation/ActivePatients",
                type: "GET",
                data: { iteration: iteration }
            }).done(function (patientList) {
                $("#main").html(patientList);
            });
            console.log("Itemation number: " + iteration)
            ++iteration;
            if (iteration == CountIteration) {
                console.log("stop")
                window.clearInterval(intervalID);
            }
        }, 6000);

    });
});

function showPopup(patientId) {
    $.ajax({
        url: "/Simulation/SetPatientDataToModalPopup",
        type: "GET",
        data: {
            patientId: patientId,
        }
    }).done(function (response) {
        console.log(response);

        Swal.fire({
            type: 'info',
            html: response
        })
    });
}