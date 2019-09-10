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

    $('#SimulationType').change(function () {
        var x = document.getElementById("nextIteration");
        if (x.style.display === "none") {
            x.style.display = "block";
        } else {
            x.style.display = "none";
        }
    })
});