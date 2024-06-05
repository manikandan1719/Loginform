$(document).ready(function () {
    // Show the modal
    //var modal = document.getElementById("otpModal");

    //var span = document.getElementsByClassName("close")[0];

  
    //span.onclick = function () {
    //    modal.style.display = "none";
    //}

   
    //window.onclick = function (event) {
    //    if (event.target == modal) {
    //        modal.style.display = "none";
    //    }
    //}

    $('#phonenumber').on('input', function () {
        $(this).val($(this).val().replace(/\D/, ''));
    });

    $(document).on('click', '#Btn', function () {
        register();
    });

    //$(document).on('click', '#otpVerifyButton', function () {
    //    var otp = $('#otpInput').val();
    //    if (otp === '1712') {
    //        alert('OTP verified!');
    //        window.location.href = "/Login/index";
    //    } else {
    //        alert('OTP invalid!');
         
    //    }
    //});

    $(document).on('click', '#two', function () {
        window.location.href = window.location.origin + "/Login/index";
    });

    function register() {
        var LoginDetails = {
            FirstName: $('#firstName').val(),
            LastName: $('#lastName').val(),
            Email: $('#email').val(),
            Gender: $('input[name="gender"]:checked').val(),
            Age: $('#age').val(),
            Phonenumber: $('#phonenumber').val(),
            RegPassword: $('#regpass').val()
        };

        var validationErrors = validateLoginDetails(LoginDetails);

        if (validationErrors.length > 0) {
            displayValidationErrors(validationErrors);
            return;
        }

        var data = JSON.stringify(LoginDetails);
        console.log(data);
        var url = "/CreateAcc/signup";
        AjaxPost(url, data);
        alert('Verification  Sent to Your Email!!');
        


    }

    function validateLoginDetails(details) {
        var errors = [];

        if (!details.FirstName) {
            errors.push({ field: 'fnameError', message: "Firstname is required." });
        }
        if (!details.LastName) {
            errors.push({ field: 'LnameError', message: "Lastname is required." });
        }
        if (!details.Gender) {
            errors.push({ field: 'genderError', message: "Gender is required." });
        }
        if (!details.Email) {
            errors.push({ field: 'emailError', message: "Email is required." });
        } else if (!isValidEmail(details.Email)) {
            errors.push({ field: 'emailError', message: "Email is not valid." });
        }
        if (!details.Age) {
            errors.push({ field: 'ageError', message: "Age is required." });
        }
        if (!details.Phonenumber) {
            errors.push({ field: 'phError', message: "Number is required." });
        }
        if (!details.RegPassword) {
            errors.push({ field: 'passwordError', message: "Password is required." });
        } else if (details.RegPassword.length < 8) {
            errors.push({ field: 'passwordError', message: "Password must be at least 8 characters." });
        }

        return errors;
    }

    function isValidEmail(Email) {
        var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
        return emailPattern.test(Email);
    }


    function displayValidationErrors(errors) {
        $('.error').text('');

        errors.forEach(function (error) {
            $('#' + error.field).text(error.message);
        });
    }

    

    function AjaxPost(url, data) {
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json",
            data: data,
            success: function (response) {
                console.log("Data sent successfully");
                // Show the OTP modal upon successful data submission
                //var modal = document.getElementById("otpModal");
                //modal.style.display = "flex";
            },
            error: function (error) {
                console.error("Error in data submission", error);
                // Handle the error accordingly
            }
        });
    }

});