window.theme = window.theme || {};

theme.SidebarSection = (function () {

    function Sidebar() {
        theme.initCache();
        theme.initNav();

        theme.cache.$mobileMenuTrigger.on('click', function (evt) {
            evt.preventDefault();
            $(this).toggleClass('open');
            theme.cache.$siteNav.slideToggle();
        });
    }

    return Sidebar;

})();
theme.initCache = function () {
    theme.cache = {
        $html: $('html'),

        // Header and nav
        $cartCount: $('#CartCount'),
        $expandedSubmenus: $('.site-nav__submenu--expanded'),
        $submenuTrigger: $('.site-nav__collapse, .site-nav__expand'),
        $mobileMenuTrigger: $('#ToggleMobileMenu'),
        $siteNav: $('#SiteNav'),


    };
};
theme.init = function () {
    theme.initCache();
    theme.initNav();
};
theme.initNav = function () {
    theme.cache.$expandedSubmenus.each(function () {
        var $menu = $(this);
        if ($menu.has('.site-nav--active').length === 0) {
            $menu
              .removeClass('site-nav__submenu--expanded')
              .addClass('site-nav__submenu--collapsed')
              .attr('aria-hidden', true)
              .hide()
              .closest('.site-nav--has-submenu')
              .find('.site-nav__collapse').addClass('hidden').end()
              .find('.site-nav__expand').removeClass('hidden');
        }
    });

    theme.cache.$submenuTrigger.on('click', function () {
        var $trigger = $(this);
        // If this is an + button
        if ($trigger.hasClass('site-nav__expand')) {
            // Remove active class from all submenus
            $('.site-nav__submenu--active').removeClass('site-nav__submenu--active');
            $trigger
              .addClass('hidden') // Hide + trigger
              .closest('.site-nav--has-submenu')
              .find('.site-nav__collapse').removeClass('hidden').end() // Reveal - trigger
              .find('.site-nav__submenu').addClass('site-nav__submenu--expanded').addClass('site-nav__submenu--active').removeClass('site-nav__submenu--collapsed').slideDown().attr('aria-hidden', false); // Reveal menu
            // Hide all other menus.
            $('.site-nav__submenu--expanded').not('.site-nav__submenu--active').closest('.site-nav--has-submenu').find('.site-nav__collapse').click();
        } else {
            // If this is an - button
            $trigger
              .addClass('hidden') // Hide - trigger
              .closest('.site-nav--has-submenu')
              .find('.site-nav__expand').removeClass('hidden').end() // Reveal + trigger
              .find('.site-nav__submenu').addClass('site-nav__submenu--collapsed').removeClass('site-nav__submenu--expanded').slideUp().attr('aria-hidden', true); // Hide menu
        }
    });
};
$(theme.init);


// common-stuff
$(document).ready(function () {

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
                    }, 1000);
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
                dataType: 'json'
            }).done(function (res) {
                if (res.status == true) {
                    toastr.success("Sản phẩm của bạn đã được thêm vào " + " <a href='/gio-hang'><span class='cartNotification'>giỏ hàng</span></a> thành công.");
                }
                else
                    toastr.warning("Sản phẩm này hiện đang hết hàng.");
            }).fail(function (res) {
                toastr.danger("Có lỗi xảy ra vui lòng kiểm tra lại.");
            });
        }
    }
    common.init();

    $("#owl-slide").owlCarousel({
        slideSpeed: 300,
        paginationSpeed: 400,
        pagination: true,
        itemsCustom: [
            [0, 1],
            [450, 1],
            [600, 1],
            [700, 1],
            [1000, 1],
            [1200, 1],
            [1400, 1],
            [1600, 1]
        ],
        autoPlay: true
    });
})








