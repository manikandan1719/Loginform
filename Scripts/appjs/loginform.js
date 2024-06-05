$(document).ready(function () {
    $(document).on('click', '#Btn', function () {
        Login();
    });

    function Login() {
        var username = $('#firstName').val();
        var password = $('#password').val();
        var email = $('#email').val();

        var LoginDetails = {
            Username: username,
            UserPassword: password,
            useremail: email
        };

        console.log("LoginDetails:", LoginDetails);

        var validationErrors = validateLoginDetails(LoginDetails);

        if (validationErrors.length > 0) {
            displayValidationErrors(validationErrors);
            return;
        }

        var data = JSON.stringify({ LoginDetails: LoginDetails });
        var url = "/Login/checkLoginInfo";

        AjaxPost(url, data)
            .done(function (result) {
                if (result.success) {
                    if (result.message == "WELCOME USER!!") {
                        alert(result.message);
                        window.location.href = "https://github.com/manikandan1719"; 
                    } 
                    
                } else if (result.message == "Account does not exist. Please register."){
                    alert(result.message);
                    window.location.href = "/CreateAcc/index";
                    
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                alert("An error occurred: " + errorThrown);
            });
    }

    function validateLoginDetails(details) {
        var errors = [];

        if (!details.Username) {
            errors.push({ field: 'usernameError', message: "Username is required." });
        }
        if (!details.UserPassword) {
            errors.push({ field: 'passwordError', message: "Password is required." });
        } else if (details.UserPassword.length < 8) {
            errors.push({ field: 'passwordError', message: "Password must be at least 8 characters long." });
        }
        if (!details.useremail) {
            errors.push({ field: 'emailError', message: "Email is required." });
        } else if (!isValidEmail(details.useremail)) {
            errors.push({ field: 'emailError', message: "Email is not valid." });
        }

        return errors;
    }

    function isValidEmail(email) {
        var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
        return emailPattern.test(email);
    }

    function displayValidationErrors(errors) {
        $('.error').text('');
        errors.forEach(function (error) {
            $('#' + error.field).text(error.message);
        });
    }

    function AjaxPost(url, data) {
        return $.ajax({
            type: "POST",
            url: url,
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    }



    $(document).on('click', '#one', function () {
        create();
    });

    function create() {
        window.location.href = origin + "/CreateAcc/index";
    }
});
