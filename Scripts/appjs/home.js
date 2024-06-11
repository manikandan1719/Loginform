$(document).ready(function () {
    var email = $("#userEmail").val();
    if (email) {
        fetchUserDetails(email);
    }

    function fetchUserDetails(email) {
        $.ajax({
            type: "GET",
            url: "/homepage/GetUserDetails",
            data: { email: email },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.success) {
                    var user = result.userDetails;
                    $('#registrationData').append(
                        `<tr>
                            <td>${user.Username}</td>
                            <td>${user.LastName}</td>
                            <td>${user.Gender}</td>
                            <td>${user.useremail}</td>
                            <td>${user.Age}</td>
                            <td>${user.PhoneNumber}</td>
                            <td>${user.UserPassword}</td>
                            <td><button style=" background-color: white;" class="edit" data-user='${JSON.stringify(user)}'>EDIT</button></td>
                        </tr>`
                    );
                } else {
                    alert(result.message);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("An error occurred: " + errorThrown);
            }
        });
    }

    $(document).on('click', '.edit', function () {
        var user = $(this).data('user');
        var url = `/update/update?Username=${encodeURIComponent(user.Username)}&LastName=${encodeURIComponent(user.LastName)}&Gender=${encodeURIComponent(user.Gender)}&useremail=${encodeURIComponent(user.useremail)}&Age=${encodeURIComponent(user.Age)}&PhoneNumber=${encodeURIComponent(user.PhoneNumber)}&UserPassword=${encodeURIComponent(user.UserPassword)}`;
        window.location.href = url;
    });
    $(document).ready(function () {
        function getUrlParameter(name) {
            name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
            var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
            var results = regex.exec(location.search);
            return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
        }

        $('#firstName').val(getUrlParameter('Username'));
        $('#lastName').val(getUrlParameter('LastName'));
        var gender = getUrlParameter('Gender');
        if (gender === 'male') {
            $('#male').prop('checked', true);
        } else if (gender === 'female') {
            $('#female').prop('checked', true);
        }
        $('#email').val(getUrlParameter('useremail'));
        $('#age').val(getUrlParameter('Age'));
        $('#phonenumber').val(getUrlParameter('PhoneNumber'));
        $('#regpass').val(getUrlParameter('UserPassword'));
    });
});
