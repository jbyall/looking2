// Write your Javascript code.
function search(url, container) {
    $.ajax({
        url: url,
        type: "GET",
        data: {
            textQuery: $('#text-query').val(),
            locationQuery: $('#location-query').val()
        },
        dataType: "html"
    }).done(function (response) {
        $(container).html(response)
    }).fail(function (xhr, status, errorThrown) {
        console.log("Error: " + errorThrown);
        console.log("Status: " + status);
        console.dir(xhr);
    });
}
