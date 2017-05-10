
(function () {
    angular.module('quanstore.product', ['quanstore.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('product', {
            url: '/product',
            parent: 'base',
            templateUrl: '/app/components/product/productListView.html',
            controller: 'productListController'
        }).state('product_add', {
            url: '/product_add',
            parent: 'base',
            templateUrl: '/app/components/product/productAddView.html',
            controller: 'productAddController'
        }).state('product_import', {
            url: "/product_import",
            parent: 'base',
            templateUrl: "/app/components/product/productImportView.html",
            controller: "productImportController"
        }).state('product_edit', {
            url: '/product_edit/:id',
            parent: 'base',
            templateUrl: '/app/components/product/productEditView.html',
            controller: 'productEditController'
        });
    }
})();