
(function () {
    angular.module('quanstore.slide', ['quanstore.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('slide', {
            url: '/slide',
            parent: 'base',
            templateUrl: '/app/components/slide/slideListView.html',
            controller: 'slideListController'
        }).state('slide_add', {
            url: '/slide_add',
            parent: 'base',
            templateUrl: '/app/components/slide/slideAddView.html',
            controller: 'slideAddController'
        }).state('slide_edit', {
            url: '/slide_edit/:id',
            parent: 'base',
            templateUrl: '/app/components/slide/slideEditView.html',
            controller: 'slideEditController'
        });
    }
})();