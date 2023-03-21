$("#submitForm").click(function (e) {
    if ($(".firstNameError .error").length != 0) {
        $(".firstNameError .error").remove();
    }

    if ($(".lastNameError .error").length != 0) {
        $(".lastNameError .error").remove();
    }

    if ($(".userNameError .error").length != 0) {
        $(".userNameError .error").remove();
    }

    if ($(".passwordError .error").length != 0) {
        $(".passwordError .error").remove();
    }

    if ($(".confirmPasswordError .error").length != 0) {
        $(".confirmPasswordError .error").remove();
    }

    var regName = /^(?=.{1,50}$)[a-z]+(?:['_.\s][a-z]+)*$/i;
    var regUserName = /^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$/;

    var firstNameVal = $("#firstName").val();
    var lastNameVal = $("#lastName").val();
    var userNameVal = $("#userName").val();
    var passwordVal = $("#password").val();
    var confirmPasswordVal = $("#confirmPassword").val();

    // Validate First name
    if (firstNameVal.length == 0) {
        $(".firstNameError").append(
            '<p class="error"><span class="errorText"><b>Oops!</b>, you didn\'t enter the first name </span></p>'
        );
        e.preventDefault();
    } else if (regName.test(firstNameVal) == false) {
        $(".firstNameError").append(
            '<p class="error"><span class="errorText"><b>Oops!</b>, invalid first name</span></p>'
        );
        e.preventDefault();
    }

    // Validate last name
    if (lastNameVal.length == 0) {
        $(".lastNameError").append(
            '<p class="error"><span class="errorText"><b>Oops!</b>, you didn\'t enter the last name </span></p>'
        );
        e.preventDefault();
    } else if (regName.test(lastNameVal) == false) {
        $(".lastNameError").append(
            '<p class="error"><span class="errorText"><b>Oops!</b>, invalid last name</span></p>'
        );
        e.preventDefault();
    }

    // Validate user name
    if (userNameVal.length < 8 || userNameVal.length > 20) {
        $(".userNameError").append(
            '<p class="error"><span class="errorText"><b>Oops!</b>, user name must be <= 20 and >= 8 characters</span></p>'
        );
        e.preventDefault();
    } else if (regUserName.test(userNameVal) == false) {
        $(".userNameError").append(
            '<p class="error"><span class="errorText"><b>Oops!</b>, invalid userName</span></p>'
        );
        e.preventDefault();
    }

    // Validate password
    if (passwordVal.length == 0) {
        $(".passwordError").append(
            '<p class="error"><span class="errorText"><b>Oops!</b>, you didn\'t enter the password</span></p>'
        );
        e.preventDefault();
    }

    // Validate Confirm password
    if (confirmPasswordVal != passwordVal) {
        $(".confirmPasswordError").append(
            '<p class="error"><span class="errorText"><b>Oops!</b>, invalid confirm password</span></p>'
        );
        e.preventDefault();
    }

    $("#firstName").focus(function (e) {
        $(".firstNameError .error").remove();
    });

    $("#lastName").focus(function (e) {
        $(".lastNameError .error").remove();
    });

    $("#userName").focus(function (e) {
        $(".userNameError .error").remove();
    });

    $("#password").focus(function (e) {
        $(".passwordError .error").remove();
    });

    $("#confirmPassword").focus(function (e) {
        $(".confirmPasswordError .error").remove();
    });
});
