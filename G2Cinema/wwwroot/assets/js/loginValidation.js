$("#submitForm").click(function (e) {
    if ($(".userNameError .error").length != 0) {
        $(".userNameError .error").remove();
    }

    if ($(".passwordError .error").length != 0) {
        $(".passwordError .error").remove();
    }

    var regUserName = /^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$/;

    var userNameVal = $("#userName").val();
    var passwordVal = $("#password").val();

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

    $("#userName").focus(function (e) {
        $(".userNameError .error").remove();
    });

    $("#password").focus(function (e) {
        $(".passwordError .error").remove();
    });
});
