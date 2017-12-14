// Write your JavaScript code.
$(document).ready(function () {
    // Wire up the Add button to send the new item to the server
    $('#add-agent-button').on('click', addAgent);
    //$('#delete-agent-button').on('click', deleteAgent);
    $('#add-make-button').on('click', addMake);
});

function addMake() {
    $('#add-make-error').hide();
    var newMakeName = $('#add-make-name').val();
    $.post('/Make/AddMake', { makeName: newMakeName }, function () {
        window.location = '/Make';
    })
        .fail(function (data) {
            if (data && data.responseJSON) {
                var firstError = data.responseJSON[Object.keys(data.responseJSON)[0]];
                $('#add-make-error').text(firstError);
                $('#add-make-error').show();
            }
        });
}

function addAgent() {
    $('#add-agent-error').hide();
    var email = $('#Email').val();
    var password = $('#Password').val();
    var confirmPassword = $('#ConfirmPassword').val();

    $.post('/ManageUsers/Register', { Email: email, Password: password, ConfirmPassword: confirmPassword}, function () {
        window.location = '/ManageUsers';
    })
        .fail(function (data) {
            if (data && data.responseJSON) {
                var firstError = data.responseJSON[Object.keys(data.responseJSON)[0]];
                $('#Password').val("");
                $('#ConfirmPassword').val("");
                $('#add-agent-error').text(firstError);
                $('#add-agent-error').show();
            }
        });
}

//function deleteAgent() {
//    $('#add-agent-error').hide();
//    var deleteEmail = $('#Email').val();
//    if (deleteEmail !== "") {
//        var url = '/ManageUsers/Delete?email=' + deleteEmail;
//        $.post(url, function () {
//            window.location = '/ManageUsers';
//        })
//            .fail(function (data) {
//                if (data && data.responseJSON) {
//                    var firstError = data.responseJSON[Object.keys(data.responseJSON)[0]];
//                    $('#Email').val("");
//                    $('#add-agent-error').show();
//                }
//            });
//    }
//    else {
//        $('#add-agent-error').text("Enter Agent email to be deleted.");
//        $('#add-agent-error').show();
//    }
//}