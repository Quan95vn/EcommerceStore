
(function (app) {
    app.controller('slideListController', slideListController);

    slideListController.$inject = ['$scope', '$uibModal', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function slideListController($scope, $uibModal, apiService, notificationService, $ngBootbox, $filter) {
        $scope.slides = [];
        $scope.page = 0;
        $scope.totalPage = 0;
        $scope.getSlides = getSlides;
        $scope.keyword = '';
        $scope.deleteSlide = deleteSlide;
        $scope.selectAll = selectAll;
        $scope.deleteMultiple = deleteMultiple;
        $scope.loading = true;

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.slides, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.slides, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("slides", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteMultiple() {
            $ngBootbox.confirm('Bạn có muốn xóa không?').then(function () {
                var listId = [];
                $.each($scope.selected, function (i, item) {
                    listId.push(item.Id);
                })
                var config = {
                    params: {
                        checkedListSlides: JSON.stringify(listId)
                    }
                }
                apiService.del('/api/slide/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                    getSlides();
                }, function (error) {
                    notificationService.displayError('Xóa không thành công');

                });
            })
        }

        function deleteSlide(id) {
            $ngBootbox.confirm('Bạn có muốn xóa không').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/slide/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công.');
                    getSlides();
                }, function () {
                    notificationService.displayError('Xóa không thành công.')
                });
            })
        }

        function getSlides(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10
                }
            }
            apiService.get('/api/slide/getall', config, dataLoadCompleted, dataLoadFailed);

            function dataLoadCompleted(result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không tìm thấy bản ghi nào.')
                }
                $scope.slides = result.data.Items;
                $scope.page = result.data.Page;
                $scope.totalPage = result.data.TotalPage;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }
            function dataLoadFailed(result) {
                notificationService.displayError('Không load được danh sách slide')
            }
        }

        $scope.getSlides();

    }
})(angular.module("quanstore.slide"))