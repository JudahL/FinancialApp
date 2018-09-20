$(document).ready(function () {
    //Custom validation
    $.validator.addMethod("validName", function (value) {
        return value && value !== "";
    }, "Please select a value for Name.");

    $.validator.addMethod("validType", function (value) {
        return value && value !== 0;
    }, "Please select a type.");

    $.validator.addMethod("validAmount", function (value) {
        return value && value >= 0;
    }, "Please select an amount greater than 0.");

    //Validate on form submit and post data if successful
    const validator = $("#newSubscription").validate({
        submitHandler: function () {
            $.ajax({
                url: "/api/subscriptions",
                method: "post",
                data: {
                    id: 0,
                    name: $("#name").val(),
                    type: $("#type").val(),
                    amount: $("#amount").val(),
                },
            })
            .done(function () {
                validator.resetForm();
            })
            .fail(function () {
                $("#alert").removeAttr("hidden");
            });

            return false;
        }
    });
});
