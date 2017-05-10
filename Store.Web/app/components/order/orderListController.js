
(function (app) {
    app.controller('orderListController', orderListController);

    orderListController.$inject = ['$scope', '$uibModal', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function orderListController($scope, $uibModal, apiService, notificationService, $ngBootbox, $filter) {
        $scope.orders = [];
        $scope.page = 0;
        $scope.totalPage = 0;
        $scope.getOrders = getOrders;
        $scope.keyword = '';
        $scope.deleteOrder = deleteOrder;
        $scope.selectAll = selectAll;
        $scope.deleteMultiple = deleteMultiple;
        $scope.openEditDialog = openEditDialog;
        $scope.loading = true;

        function openEditDialog(order) {
            $scope.order = order;
            $uibModal.open({
                templateUrl: '/app/components/order/orderDetailViewModal.html',
                controller: 'orderDetailController',
                scope: $scope
            }).result.then(function ($scope) {

            }, function () {
            });
        }


        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.orders, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.orders, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("orders", function (n, o) {
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
                        checkedListOrder: JSON.stringify(listId)
                    }
                }
                apiService.del('/api/order/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                    getOrders();
                }, function (error) {
                    notificationService.displayError('Xóa không thành công');

                });
            })

        }

        function deleteOrder(id) {
            $ngBootbox.confirm('Bạn có muốn xóa không').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/order/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công.');
                    getOrders();
                }, function () {
                    notificationService.displayError('Xóa không thành công.')
                });
            })
        }

        function getOrders(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 5
                }
            }
            apiService.get('/api/order/getall', config, loadDataCompleted, loadDataFailed);

            function loadDataCompleted(result){
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không tìm thấy bản ghi nào.')
                }
                $scope.orders = result.data.Items;
                $scope.page = result.data.Page;
                $scope.totalPage = result.data.TotalPage;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }
            function loadDataFailed(result){
                notificationService.displayError('Không load được danh sách đơn hàng')
            }   

        }

        $scope.getOrders();


    }
})(angular.module("quanstore.order"))

