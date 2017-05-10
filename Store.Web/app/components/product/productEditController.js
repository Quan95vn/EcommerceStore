
(function (app) {
    app.controller('productEditController', productEditController);

    productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService'];

    function productEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.EditProduct = EditProduct;
        $scope.flatFolders = [];
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.parentCategory = [];

        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        }

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function loadParentCategory() {
            apiService.get('/api/category/getallparents', null, function (result) {
                $scope.parentCategory = commonService.getTree(result.data, 'Id', 'ParentId');
                $scope.parentCategory.forEach(function (item) {
                    recur(item, 0, $scope.flatFolders);
                });
            }, function () {
                console.log('Load Parent Category failed.');
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

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fireUrl) {
                $scope.$apply(function () {
                    $scope.singleImage = fireUrl;
                });
            }
            finder.popup();
        }

        $scope.moreImages = [];
        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                });
            }
            finder.popup();
        }

        $scope.removeData = function (selData) {
            $scope.moreImages.splice($scope.moreImages.indexOf(selData), 1);
        }

        function loadProductDetails() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                $scope.singleImage = $scope.product.ThumbImage;
                $scope.moreImages = JSON.parse($scope.product.MoreImages);
            }, function (result) {
                notificationService.displayError('Không lấy được chi tiết sản phẩm .');
            });
        }

        function EditProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            $scope.product.ThumbImage = $scope.singleImage;
            apiService.put('/api/product/update', $scope.product, dataEditCompleted, dataEditFailed);

            function dataEditCompleted(result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công.');
                $state.go('product');
            }
            function dataEditFailed(result) {
                notificationService.displayError(result.data);
            }
        }

        loadParentCategory();
        loadProductDetails();
    }

})(angular.module('quanstore.product'));