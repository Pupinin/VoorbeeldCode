var tokenKey = 'accessToken';
$(document).ready(function () {
    
    var token = sessionStorage.getItem(tokenKey);

    if (token == null) {
        $("div#aanmelden").show();
        $("div#overzicht").hide();
        overview
    }
    else
    {
        $("div#aanmelden").hide();
        $("#logout").show();
    }

    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }

    $.ajax({
        type: "GET",
        url: "/api/Student",
        headers: headers,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(data, function (i, item) {
                var rows = "<tr>" +
                    "<td id='Name'>" + item.Name + "</td>" +
                    "<td id='FirstName'>" + item.FirstName + "</td>" +
                    "<td id='StudentNumber'>" + item.StudentNumber + "</td>" +
                    "<td id='ExamenHash'>" + item.ExamenHash + "</td>" +
                    "<td id='ExamenHash'>" + item.FileName + "</td>" +
                    "<td id='ExamenHash'>" + item.Datetime + "</td>" 
                    "</tr>";
                $('#inner').append(rows);
            });
        },
        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function

    });
});



$("#login").click(function () {
    console.log("hier");
    var loginData = {
        grant_type: 'password',
        username: $('#LoginEmail').val(),//"ahmet.koocyigit@gmail.com",
        password: $('#LoginPassword').val()//"Testen123456!$"
    };
    console.log(loginData.username);
    console.log(loginData.password);
    var self = null;
    $.ajax({
        type: 'POST',
        url: '/Token',
        data: loginData
    }).done(function (data) {
        
        self = (data.userName);
        // Cache the access token in session storage.
        sessionStorage.setItem(tokenKey, data.access_token);
        location.reload();
        }).fail(function(data) {
            alert(data.Message);
    });
});

$("#register").click(function () {
    console.log("register biatch");
    var data = {
        Email: $('#RegisterEmail').val(),
        Password: $('#RegisterPassword').val(),
        ConfirmPassword: $('#RegisterPassword2').val()
    };

    $.ajax({
        type: 'POST',
        url: '/api/Account/Register',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data)
    }).done(function (data) {
        alert("Done!");
    }).fail(function (data) {
        alert(data.responseText);
    });
});

$("#logout").click(function () {
    // Log out from the cookie based logon.
    var token = sessionStorage.getItem(tokenKey);
    var headers = {};
    if (token) {
        headers.Authorization = 'Bearer ' + token;
    }

    $.ajax({
        type: 'POST',
        url: '/api/Account/Logout',
        headers: headers
    }).done(function (data) {
        
        sessionStorage.removeItem(tokenKey);

        location.reload();
        }).fail(function (data) {
            alert(data.responseText);
        });
});