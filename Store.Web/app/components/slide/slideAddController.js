(function (app) {
    app.controller('slideAddController', slideAddController);

    slideAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];

    function slideAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.AddSlide = AddSlide;

        $scope.slide = {
            Status: true
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.singleImage = fileUrl;
                });
            }
            finder.popup();
        }

        function AddSlide() {
            $scope.slide.Image = $scope.singleImage;
            apiService.post('/api/slide/create', $scope.slide, dataAddSucceeded, dataAddFailed);

            function dataAddSucceeded(result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới thành công.');
                $state.go('slide');
            }
            function dataAddFailed(result) {
                notificationService.displayError(result.data);
            }
        }
    }
})(angular.module('quanstore.slide'));