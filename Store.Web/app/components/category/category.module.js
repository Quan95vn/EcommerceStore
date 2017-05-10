
(function () {
    angular.module('quanstore.category', ['quanstore.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
       
	    $stateProvider.state('category', {
	        url: '/category',
	        parent: 'base',
	        templateUrl: '/app/components/category/categoryListView.html',
	        controller: 'categoryListController'
	    }).state('category_add', {
	        url: '/category_add',
	        parent: 'base',
	        templateUrl: '/app/components/category/categoryAddView.html',
	        controller: 'categoryAddController'
	    }).state('category_edit', {
	        url: '/category_edit/:id',
	        parent: 'base',
	        templateUrl: '/app/components/category/categoryEditView.html',
	        controller: 'categoryEditController'
	    });
	}
})();