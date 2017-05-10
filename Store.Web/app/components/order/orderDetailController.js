
(function (app) {
    app.controller('orderDetailController', orderDetailController);

    orderDetailController.$inject = ['$scope', '$uibModalInstance', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService', '$location'];

    function orderDetailController($scope, $uibModalInstance, apiService, notificationService, $state, $stateParams, commonService, $location) {
        $scope.CancelEdit = CancelEdit;
        $scope.UpdateOrder = UpdateOrder;

        $scope.paymentOptions = ["Đang thanh toán", "Đã thanh toán", "Bị hủy"];
        //$scope.totalPrice = $scope.order.Price * $scope.order.Quantity;

        function CancelEdit() {
            $scope.isEnabled = false;
            $uibModalInstance.dismiss();
        }

        function UpdateOrder() {
            apiService.put('/api/order/update', $scope.order, function (result) {
                notificationService.displaySuccess('Đơn hàng đã được cập nhật thành công.');
               
            }, function (error) {
                notificationService.displayError('Cập nhật thất bại.');
            });
        }
    }

})(angular.module('quanstore.order'));