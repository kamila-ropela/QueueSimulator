//checkbox
$(document).ready(function () {
    $("#ReturnToQuery").click(function () {
        var $input = $(this);

        if ($input.is(":checked")) {
            $('input[id="ReturnToQuery"]').val("1");
        }
        else {
            $('input[id="ReturnToQuery"]').val("0");
        }
    });

    $("#AddToQuery").click(function () {
        var $input = $(this);

        if ($input.is(":checked")) {
            $('input[id="AddToQuery"]').val("1");
        }
        else {
            $('input[id="AddToQuery"]').val("0");
        }
    });

    $("#TwoQuery").click(function () {
        var $input = $(this);

        if ($input.is(":checked")) {
            $('input[id="TwoQuery"]').val("1");
        }
        else {
            $('input[id="TwoQuery"]').val("0");
        }
    });
});

//check number patients
//$(document).ready(function () {
//    $("#Add").submit(function () {
//        $.ajax({
//            url: "/Simulation/Check",
//            type: "GET",
//            data: {}
//        }).done(function (patientForm) {
//            $("#main").html(patientForm);
//        });
//    });
//    $("#h").on("click", function () {
//        $.ajax({
//            url: "/Simulation/Check",
//            type: "GET",
//            data: {
               
//            }
//        }).done(function (patientForm) {
//            $("#main").html(patientForm);
//        });
//    });
//    $("#v").on("click", function () {
//        var patientCount = $("#CountPatient").val();
//        $.ajax({
//            url: "/Simulation/AddPatient",
//            type: "GET",
//            data: {
//                patientCount: patientCount,
//            }
//        }).done(function (patientForm) {
//            $("#main").html(patientForm);
//        });
//    });
//});



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

//start simulation
    $(document).ready(function () {
        $("#start").on("click", function () {
            var CountPatient = $("#CountPatient").val();
            var CountIteration = $("#CountIteration").val();
            var Algorytm = $("#Algorytm").val();
            var ReturnToQuery = $("#ReturnToQuery").val();
            var AddToQuery = $("#AddToQuery").val();
            var TwoQuery = $("#TwoQuery").val();
            $.ajax({
                url: "/Simulation/StartSimulation",
                type: "GET",
                data: {
                    CountPatient: CountPatient,
                    CountIteration: CountIteration,
                    Algorytm: Algorytm,
                    ReturnToQuery: ReturnToQuery,
                    AddToQuery: AddToQuery,
                    TwoQuery: TwoQuery
                }
            }).done(function (patientList) {
                $("#main").html(patientList);
                });
            
            var iteration = 0;
                var intervalID = window.setInterval(function () {

                    $.ajax({
                        url: "/Simulation/ActivePatients",
                        type: "GET",
                        data: { iteration: iteration}
                    }).done(function (patientList) {
                        $("#main").html(patientList);
                        });
                    console.log("Itemation number: " + iteration)
                    ++iteration;
                    if (iteration == CountIteration) {
                        window.clearInterval(intervalID);
                    }
                }, 4000);
        });
    });