
$(document).ready(function () {
    //Khi bàn phím được nhấn và thả ra thì sẽ chạy phương thức này
    $("#registerform").validate({
        rules: {
            FirstName: "required",
            LastName: "required",
            Mail: {
                required: true,
                email: true
            },
            PhoneNumber: "required",
            Address: "required",
            UserName: "required",
            Password: "required",
            ConfirmPassword: {
                required: true,
                equalTo: "#Password"
            },
            rememberme: "required"
        },
        messages: {
            FirstName: "Please input your First Name",
            LastName: "Please input your Last Name",
            Mail: {
                required: "Please input your Email Address",
                email: "Email Address Error",
                
            },
            PhoneNumber: "Please input your phone number",
            Address: "Please input your Address",
            UserName: "Please input your UserName",
            Password: "Please input your Password",
            ConfirmPassword: {
                required: "Please input ConfirmPassword",
                equalTo: "Confirm Password Wrong"
            },
            
        }
    });
});