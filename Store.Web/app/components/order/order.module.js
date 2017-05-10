(function () {
    angular.module('quanstore.order', ['quanstore.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('order', {
            url: '/order',
            parent: 'base',
            templateUrl: '/app/components/order/orderListView.html',
            controller: 'orderListController'
        })
        .state('order_detail', {
            url: '/order_detail',
            parent: 'base',
            templateUrl: '/app/components/order/orderDetailViewModal.html',
            controller: 'orderDetailController'
        })
        //}).state('order_add', {
        //    url: '/order_add',
        //    templateUrl: '/app/components/order/orderAddView.html',
        //    controller: 'orderAddController'
        //}).state('order_edit', {
        //    url: '/order_edit/:id',
        //    templateUrl: '/app/components/order/orderEditView.html',
        //    controller: 'orderEditController'
        //});
    }
})();