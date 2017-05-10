var common = {
    init: function () {
        common.registerEvents();
    },
    registerEvents: function () {
        $("#txtKeyword").autocomplete({
            minLength: 3,
            source: function (request, response) {
                setTimeout(function () {
                    $.ajax({
                        url: "/Product/GetListProductByName",
                        dataType: "json",
                        data: {
                            keyword: request.term
                        },
                        success: function (res) {
                            response($.map(res.data, function (value, key) {
                                return {
                                    label: value.Name,
                                    value: value.Name,
                                    Id: value.Id,
                                    Alias: value.Alias
                                }
                            }));

                        }
                    })
                }, 3000);
            },
            focus: function (event, ui) {

                $("#txtKeyword").val(ui.item.Name);
                return false;
            },
            select: function (event, ui) {
                window.location.href = "/sanpham/" + ui.item.Id + "/" + ui.item.Alias + ".html";
            }
        }).autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
              .append("<a>" + item.label + "</a>")
              .appendTo(ul);
        };
        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            common.addItem(productId)
        });

        $(function () {
            $('nav a[href^="/' + location.pathname.split("/")[1] + '"]').addClass('site--active');
        });


    },
    addItem: function (productId) {
        $.ajax({
            url: '/ShoppingCart/Add',
            data: {
                productId: productId
            },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status == true) {

                    toastr.success("Sản phẩm " + "<strong>" + res.data[0].Product.Name + "</strong>" +
                      " đã được thêm vào" + " <a href='/gio-hang'><span class='cartNotification'>giỏ hàng</span></a> thành công.");

                }
                else if (res.status == false) {
                    toastr.warning("Sản phẩm này hiện đang hết hàng.");
                }
                else {
                    toastr.error("Có lỗi xảy ra. Bạn vui lòng kiểm tra lại.");
                }

            }
        });
    }
}
common.init();