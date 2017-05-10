
(function (app) {
    app.controller('categoryListController', categoryListController);

    categoryListController.$inject = ['$scope', '$uibModal', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function categoryListController($scope, $uibModal, apiService, notificationService, $ngBootbox, $filter) {
        $scope.categories = [];
        $scope.page = 0;
        $scope.totalPage = 0;
        $scope.getCategories = getCategories;
        $scope.keyword = '';
        $scope.deleteCategory = deleteCategory;
        $scope.search = search;
        $scope.selectAll = selectAll;
        $scope.deleteMultiple = deleteMultiple;
        $scope.openEditDialog = openEditDialog;
        $scope.loading = true;

        function openEditDialog(category) {
            $scope.category = category;
            $uibModal.open({
                templateUrl: '/app/components/category/categoryEditViewModal.html',
                controller: 'categoryEditController',
                backdrop: 'static',
                scope: $scope
            }).result.then(function ($scope) {
             
            }, function () {
            });
        }


        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.categories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.categories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("categories", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteMultiple() {
            $ngBootbox.confirm('Bạn có muốn xóa không').then(function () {
                var listId = [];
                $.each($scope.selected, function (i, item) {
                    listId.push(item.Id);
                })
                var config = {
                    params: {
                        checkedListCategories: JSON.stringify(listId)
                    }
                }
                apiService.del('/api/category/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                    search();
                }, function (error) {
                    notificationService.displayError('Xóa không thành công');

                });
            })

        }

        function deleteCategory(id) {
            $ngBootbox.confirm('Bạn có muốn xóa không').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/category/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công.');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công.')
                });
            })
        }

        //search
        function search() {
            getCategories();
        }

        //get
        function getCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 5
                }
            }
            apiService.get('/api/category/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không tìm thấy bản ghi nào.')
                }
                $scope.categories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.totalPage = result.data.TotalPage;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                notificationService.displayError('Không load được danh mục sản phẩm.')
            });
        }

        $scope.getCategories();

    }
})(angular.module("quanstore.category"))