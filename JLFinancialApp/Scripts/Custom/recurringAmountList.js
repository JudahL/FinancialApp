﻿$(document).ready(function () {
    var controller = $("#amount-list").data("controller");

    $("#editModal").on("show.bs.modal", function (event) {
        var button = $(event.relatedTarget);
        var modal = $(this);

        var modalTitle = modal.find("#editModal-title");
        var modalEditLink = modal.find("#editModal-edit");
        var modalDeleteButton = modal.find("#editModal-delete");

        modalTitle.text(button.text());
        modalEditLink.attr("href", "/" + controller + "/edit/" + button.data("id"));
        modalDeleteButton.attr("data-id", button.data("id"));
    });

    $("#editModal-delete").on("click", function () {
        var id = $(this).data("id");

        $.ajax({
            url: "/api/" + controller + "/" + id,
            method: "delete",
            success: function () {
                $("#row-" + id).remove();
            }
        });
    });
});