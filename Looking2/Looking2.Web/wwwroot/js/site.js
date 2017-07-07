// Write your Javascript code.
function search() {
    //$.get("/Events/Test", function (response) {
    //    $('#test-container').html(response);
    //});
    $.ajax({
        url: "/Events/Test",
        data: {
            textQuery: $('#text-query').val(),
            locationQuery: $('#location-query').val()
        },
        type: "GET",
        dataType: "json"
    }).done(function (jsonResponse) {
        $('#test-container').html(jsonResponse);
    });
}
