
(function (app) {
    app.controller('categoryEditController', categoryEditController);

    categoryEditController.$inject = ['$scope','$uibModalInstance', 'apiService', 'notificationService', '$state', '$stateParams','commonService'];

    function categoryEditController($scope, $uibModalInstance, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.CancelEdit = CancelEdit;
        $scope.UpdateCategory = UpdateCategory;
        $scope.parentCategory = [];
        $scope.flatFolders = [];

        function loadParentCategory() {
            apiService.get('/api/category/getallparents', null, function (result) {
                $scope.parentCategory = commonService.getTree(result.data, 'Id', 'ParentId');
                $scope.parentCategory.forEach(function (item) {
                    recur(item, 0, $scope.flatFolders);
                });
            }, function () {
                console.log('Load parent category failed.');
            });
        }
        function times(n, str) {
            var result = '';
            for (var i = 0; i < n; i++) {
                result += str;
            }
            return result;
        };
        function recur(item, level, arr) {
            arr.push({
                Name: times(level, '–') + ' ' + item.Name,
                Id: item.Id,
                Level: level,
                Indent: times(level, '–')
            });

            if (item.children) {
                item.children.forEach(function (item) {
                    recur(item, level + 1, arr);
                });
            }
        };

        function CancelEdit() {
            $scope.isEnabled = false;
            $uibModalInstance.dismiss();
        }

        function UpdateCategory() {
            apiService.put('/api/category/update', $scope.category, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                
                $uibModalInstance.dismiss();
            }, function error(result) {
                notificationService.displayError(result.data);
            });
        }


        loadParentCategory();

    }

})(angular.module('quanstore.category'));