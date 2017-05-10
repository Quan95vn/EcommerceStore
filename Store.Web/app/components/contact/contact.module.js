
(function () {
    angular.module('quanstore.contact', ['quanstore.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $stateProvider.state('contact', {
            url: '/contact',
            parent: 'base',
            templateUrl: '/app/components/contact/contactListView.html',
            controller: 'contactListController'
        }).state('contact_add', {
            url: '/contact_add',
            parent: 'base',
            templateUrl: '/app/components/contact/contactAddView.html',
            controller: 'contactAddController'
        }).state('contact_edit', {
            url: '/contact_edit/:id',
            parent: 'base',
            templateUrl: '/app/components/contact/contactEditView.html',
            controller: 'contactEditController'
        });
    }
})();