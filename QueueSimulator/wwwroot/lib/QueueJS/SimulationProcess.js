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

        $.ajax({
            async: false,
            url: "/Simulation/GetPatientCountInDB",
            type: "GET",
            data: { }
        }).done(function (patientCount) {
            if (patientCount == 0) {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Zapomniałeś dodać pacjentów!'
                })
                return;
            }        

            if (CountIteration.length == 0) {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Zapomniałeś dodać iteracje!'
                })
                return;
            }

            if (DoctorCount.length == 0) {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Zapomniałeś dodać lekarzy!'
                })
                return;
            }

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
                if (iteration == CountIteration) {
                    console.log("stop")
                    window.clearInterval(intervalID);
                } else {
                    ++iteration;

                    $.ajax({
                        async: false,
                        url: "/Simulation/ActivePatients",
                        type: "GET",
                        data: { iteration: iteration }
                    }).done(function (patientList) {
                        $("#main").html(patientList);
                    });
                    console.log("Itemation number: " + iteration)                
                }
            }, 6000);

        });
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