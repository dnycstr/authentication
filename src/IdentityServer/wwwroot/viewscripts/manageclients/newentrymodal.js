
var newentryurl = $("#newentryurl").val();

// Call new entry modal form
$('#btn-new-entry-modal').click(function () {

    $("#my-modal-lg-form-container").html('<span class="fa fa-refresh fa-animate-spin"></span>&nbsp;Loading...');

    $.ajax({
        cache: false,
        type: "GET",
        url: newentryurl,
        success: function (data) {
            $("#my-modal-lg-form-container").html(data);
            $("#my-modal-lg-title").html("Add Client");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("#my-modal-lg-form-container")
                .html('<p class="text-danger"><span class="fa fa-exclamation-triangle"></span>&nbsp;Failed to load data.</p>');
        }
    });

    $('#my-modal-lg').modal('show');
});
//End - Call new entry modal form