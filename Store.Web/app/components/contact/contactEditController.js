
(function (app) {
    app.controller('contactEditController', contactEditController);

    contactEditController.$inject = ['$scope', '$uibModalInstance', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService'];

    function contactEditController($scope, $uibModalInstance, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.CancelEdit = CancelEdit;
        $scope.UpdateContact = UpdateContact;
      

        function CancelEdit() {
            $scope.isEnabled = false;
            $uibModalInstance.dismiss();
        }


        function UpdateContact() {
            apiService.put('/api/contact/update', $scope.contact, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                $uibModalInstance.dismiss();
            }, function error(result) {
                notificationService.displayError(result.data);
            });
        }


    }

})(angular.module('quanstore.contact'));