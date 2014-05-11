/// <reference path="../jquery-1.7.1.js" />

function fillDepartmentsDdl() {
    var options = {};
    options.url = "/Devices/GetModels";
    options.type = "POST";
    options.data = JSON.stringify({ idMaker: $("#Makers").val() });
    options.dataType = "json";
    options.contentType = "application/json";
    options.success = function (states) {
        //alert(states);
        $("#IdModel").empty();
        for (var i = 0; i < states.length; i++) {
            $("#IdModel").append("<option value=" + states[i].IdDeviceModel + ">" + states[i].ModelName + "</option>");
        }
        $("#IdModel").prop("disabled", false);
    };
    options.error = function (data) { alert(data.toString()); };
    $.ajax(options);
}
