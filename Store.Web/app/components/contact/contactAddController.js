
(function (app) {
    app.controller('contactAddController', contactAddController);

    contactAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];

    function contactAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.AddContact = AddContact;
      

        $scope.contact = {
            Status: false
        }

        function AddContact() {
            apiService.post('/api/contact/create', $scope.contact, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới thành công.');
                $state.go('contact');
            }, function (result) {
                notificationService.displayError(result.data);
            });
        }
    }

})(angular.module('quanstore.contact'));