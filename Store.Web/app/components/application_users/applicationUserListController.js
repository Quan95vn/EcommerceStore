(function (app) {
    'use strict';

    app.controller('applicationUserListController', applicationUserListController);

    applicationUserListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox'];

    function applicationUserListController($scope, apiService, notificationService, $ngBootbox) {
        $scope.loading = true;
        $scope.data = [];
        $scope.page = 0;
        $scope.totalPage = 0;
        $scope.totalCount = 0;
        $scope.getUsers = getUsers;
       
        function getUsers(page) {
            page = page || 0;

            $scope.loading = true;
            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                }
            }

            apiService.get('/api/applicationUser/getlistpaging', config, dataLoadCompleted, dataLoadFailed);
        }

        function dataLoadCompleted(result) {
            $scope.data = result.data.Items;
            $scope.page = result.data.Page;
            $scope.totalCount = result.data.TotalCount;
            $scope.totalPage = result.data.TotalPage;
            $scope.loading = false;

        }
        function dataLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        $scope.getUsers();
    }
})(angular.module('quanstore.application_users'));