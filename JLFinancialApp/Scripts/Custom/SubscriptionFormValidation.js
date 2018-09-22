$(document).ready(function () {
    
    // Custom validation
    $.validator.addMethod("validname", function (value) {
        return value && value !== "";
    }, "Please select a value for Name.");

    $.validator.addMethod("validtype", function (value) {
        return value && value != 0;
    }, "Please select a type.");

    $.validator.addMethod("validamount", function (value) {
        return value && value > 0;
    }, "Please select an amount greater than 0.");


    // Setting up form variables
    var form = $("#recurringAmount");
    var method = form.data("method");
    var apiUrl = "/api/" + form.data("url");    
    var id = form.data("id");

    if (method == "put" || method == "delete") {
        apiUrl += "/" + id;
    }

    // Validate on form submit and post data if successful
    var validator = form.validate({
        submitHandler: function () {
            $.ajax({
                url: apiUrl,
                method: method,
                data: {
                    id: id,
                    name: $("#name").val(),
                    periodType: {
                        id: $("#type").val(),
                        name: $("#type option:selected").text(),
                    },
                    amount: $("#amount").val(),
                },
            })
                .done(function () {
                    // Reset form values
                    $("#name").val("");
                    $("#type").val("0");
                    $("#amount").val("");

                    // Reset form validation
                    validator.resetForm();

                    if (method !== "post") {
                        window.location.pathname = "/" + form.data("url");
                    }
                })
                .fail(function () {
                    $("#alert").removeAttr("hidden");
                });

            return false;
        },
        // Setting valid class to bootstrap styles
        validClass: "is-valid",
        // Setting invalid class to bootstrap styles
        errorClass: "is-invalid",
        errorPlacement: function (error, element) {
            // Adding invalid-feedback class for bootstrap styling
            error.addClass("invalid-feedback");

            // Either appending the error element to the parent form-group div of the input group if it is part of an input group
            // Or appending it to its parent form-group div if not
            if (element.hasClass("input-group-input")) {
                error.appendTo(element.parent(".input-group").parent(".form-group"));
            } else {
                error.appendTo(element.parent(".form-group"));
            } 
        }
    });
});
