
(function (app) {
    app.controller('slideEditController', slideEditController);

    slideEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService'];

    function slideEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.EditSlide = EditSlide;

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

        function loadSlideDetails() {
            apiService.get('/api/slide/getbyid/' + $stateParams.id, null, function (result) {
                $scope.slide = result.data;
                $scope.singleImage = $scope.slide.Image;
            }, function () {
                notificationService.displayError('Can not get specific Slide .');
            });
        }

        function EditSlide() {
            $scope.slide.Image = $scope.singleImage;
            apiService.put('/api/slide/update', $scope.slide, dataEditCompleted, dataEditFailed);

            function dataEditCompleted(result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới thành công.');
                $state.go('slide');
            }
            function dataEditFailed(result) {
                notificationService.displayError(result.data);
            }
        }

        loadSlideDetails();
    }
})(angular.module('quanstore.slide'));