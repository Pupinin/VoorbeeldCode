$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/api/download/1",
        contentType: "application/json; charset=utf-8",
        dataType: "string",
        error: function (data) {
            $('#skeletnaam').text(data.responseText);
        }

    })
});