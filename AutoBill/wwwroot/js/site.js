// Write your JavaScript code.
$(document).ready(function () {
    // Wire up the Add button to send the new item to the server
    $('#add-agent-button').on('click', addAgent);
});

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