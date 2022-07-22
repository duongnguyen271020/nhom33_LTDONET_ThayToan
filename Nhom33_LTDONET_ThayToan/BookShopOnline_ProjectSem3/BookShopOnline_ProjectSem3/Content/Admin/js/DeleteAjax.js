var deleteLinkObj;
// delete Link
$('.delete-link').click(function () {
    deleteLinkObj = $(this);  //for future use
    $('#delete-dialog').dialog('open');
    return false; // prevents the default behaviour
});