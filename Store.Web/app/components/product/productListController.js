
(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter', '$stateParams'];

    function productListController($scope, apiService, notificationService, $ngBootbox, $filter, $stateParams) {
        $scope.products = [];
        $scope.page = 0;
        $scope.totalPage = 0;
        $scope.getProducts = getProducts;
        $scope.keyword = '';
        $scope.deleteProduct = deleteProduct;
        $scope.selectAll = selectAll;
        $scope.deleteMultiple = deleteMultiple;
        $scope.loading = true;
        $scope.exportExcel = exportExcel;
        $scope.exportPdf = exportPdf;

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("products", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteProduct(id) {
            $ngBootbox.confirm('Bạn có muốn xóa không').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/product/delete', config, function (response) {
                    notificationService.displaySuccess('Sản phẩm ' + response.data.Name + 'đã được xóa thành công.');
                    getProducts();
                }, function (response) {
                    notificationService.displayError('Xóa không thành công.')
                });
            });
        }

        function deleteMultiple() {
            $ngBootbox.confirm('Bạn có muốn xóa không').then(function () {
                var listId = [];
                $.each($scope.selected, function (i, item) {
                    listId.push(item.Id);
                })
                var config = {
                    params: {
                        checkedListProducts: JSON.stringify(listId)
                    }
                }
                apiService.del('/api/product/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                    getProducts();
                }, function (error) {
                    notificationService.displayError('Xóa không thành công');

                });
            });
        }

        function getProducts(page) {
            page = page || 0;

            $scope.loading = true;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10
                }
            }

            apiService.get("/api/product/getall", config, dataLoadCompleted, dataLoadFailed);

            function dataLoadCompleted(response) {
                if (response.data.TotalCount == 0) {
                    notificationService.displayWarning("Không có bản ghi nào được tìm thấy.");
                }
                $scope.products = response.data.Items;
                $scope.page = response.data.Page;
                $scope.totalPage = response.data.TotalPage;
                $scope.totalCount = response.data.TotalCount;
                $scope.loading = false;
            }
            function dataLoadFailed() {
                notificationService.displayError("Không load được danh sách sản phẩm.");
            }
        }

        function exportExcel() {
            var config = {
                params: {
                    filter: $scope.keyword
                }
            }
            apiService.get('/api/product/ExportXls', config, function (response) {
                if (response.status = 200) {
                    window.location.href = response.data.Message;
                }
            }, function (error) {
                notificationService.displayError(error);

            });
        }

        function exportPdf(productId) {
            var config = {
                params: {
                    id: productId
                }
            }
            apiService.get('/api/product/ExportPdf', config, function (response) {
                if (response.status = 200) {
                    window.location.href = response.data.Message;
                }
            }, function (error) {
                notificationService.displayError(error);

            });
        }

        $scope.getProducts();
    }
})(angular.module('quanstore.product'))