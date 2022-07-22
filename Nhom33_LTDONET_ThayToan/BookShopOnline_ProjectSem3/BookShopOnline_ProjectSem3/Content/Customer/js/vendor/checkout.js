$(document).ready(function () {
    //Khi bàn phím được nhấn và thả ra thì sẽ chạy phương thức này
    $("#form-checkout").validate({
        rules: {
            otherAddress: "required",
            otherEmail: "required",
            otherPhone: "required"
        },
        messages: {
            otherAddress: "Please Input Your Address",
            otherEmail: "Please Input Your Email",
            otherPhone: "Please Input Your Phone"
        }
    });
    $("#form-checkout").submit(function () {
        if ($('input#ship-box').is(':checked')) {
            if ($('#form-checkout').valid()) {
                return true;
            }
            return false;
        } else {
            return true;
        }
        
    });


});
