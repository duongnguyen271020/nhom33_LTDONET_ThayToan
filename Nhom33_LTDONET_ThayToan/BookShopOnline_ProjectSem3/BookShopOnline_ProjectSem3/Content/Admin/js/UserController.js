// using ajax change status client
var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Users/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",               
                success: function (response) {
                    console.log(response);
                    if (response.Status == true) {
                        btn.text('Active');
                    }
                    else {
                        btn.text('Locked');
                    }
                }
            });
        });
    }
}
user.init();

// using ajax change status book
var book = {
    init: function () {
        book.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active1').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/BookMangement/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.Status == true) {
                        btn.text('Instock');
                    }
                    else {
                        btn.text('OutStock');
                    }
                }
            });
        });
    }
}
book.init();