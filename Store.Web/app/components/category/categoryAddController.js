
(function (app) {
    app.controller('categoryAddController', categoryAddController);

    categoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];

    function categoryAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.AddCategory = AddCategory;
        $scope.parentCategory = [];
        $scope.flatFolders = [];

        $scope.category = {
            CreatedDate: new Date(),
            Status: true
        }

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

        function AddCategory() {
            apiService.post('/api/category/create', $scope.category, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới thành công.');
                $state.go('category');
            }, function (result) {
                notificationService.displayError(result.data);
            });
        }


        loadParentCategory();


    }

})(angular.module('quanstore.category'));