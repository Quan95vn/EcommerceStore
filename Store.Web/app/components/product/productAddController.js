
(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];

    function productAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.moreImages = [];
        $scope.parentCategory = [];
        $scope.AddProduct = AddProduct;
        $scope.flatFolders = [];
        $scope.GetSeoTitle = GetSeoTitle;

        $scope.product = {
            CreatedDate: new Date(),
            Status: true
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

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
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
   
        function AddProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            $scope.product.ThumbImage = $scope.singleImage;

            apiService.post('/api/product/create', $scope.product, dataAddCompleted, dataAddFailed);

            function dataAddCompleted(result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới thành công.');
                $state.go('product');
            }
            function dataAddFailed(result) {
                notificationService.displayError(result.data);
            }
        }

        loadParentCategory();


    }

})(angular.module('quanstore.product'));