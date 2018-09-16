$("#uploadButton").click(function (event) {
    var data = new FormData();
    var file = $('form input[type=file]')[0].files[0];
    var studentName = $('#naamStudent').val();
    var studentNumber = $('#studentNumber').val();
    data.append('file', file);
    data.append('naam', studentName);
    data.append('studentNumber', studentNumber);
    $.ajax({
        url: '/Api/Upload',
        processData: false,
        contentType: false,
        data: data,
        method: 'POST'
    }).done(function (result) {
        alert(result);
    }).fail(function (a, b, c) {
        console.log(a, b, c);
    });
});