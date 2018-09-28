﻿

var jl_alert = {
    _element: null,
    _remove: function() {
        this._element.removeAttr("hidden");
        this._element.removeClass();
    },
    _setText: function (titleText, infoText) {
        this._element.find("#alert-title").text(titleText);
        this._element.find("#alert-info").text(infoText);
    },
    _displayAlert: function (titleText, infoText, colourClass) {
        this._remove();
        this._element.addClass("alert alert-" + colourClass + " alert-dismissible");
        this._setText(titleText, infoText);  
    },
    success: function(titleText, infoText) {
        this._displayAlert(titleText, infoText, "success");    
    },
    danger: function(titleText, infoText) {
        this._displayAlert(titleText, infoText, "danger");     
    },
    dark: function (titleText, infoText) {
        this._displayAlert(titleText, infoText, "dark");    
    }
}

$(document).ready(function () {
    jl_alert._element = $("#alert");
});
