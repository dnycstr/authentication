
var dataurl = $("#dataurl").val();

var table = $('#clients-dataTable').DataTable({
    "processing": true,
    "serverSide": true,
    "ajax":
    {
        "url": dataurl,
        "type": "POST",
        "dataType": "JSON"
    },
    rowId: "Id",
    "columns": [
        { "data": "ClientId" },
        { "data": "ClientName" },
        { "data": "Enabled" }
    ]
});

table
    .buttons()
    .container()
    .appendTo('#table-control-panel');


$('#clients-dataTable tbody').on('click', 'tr', function () {
    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        table.$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});