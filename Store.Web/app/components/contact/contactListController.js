
(function (app) {
    app.controller('contactListController', contactListController);

    contactListController.$inject = ['$scope', '$uibModal', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function contactListController($scope, $uibModal, apiService, notificationService, $ngBootbox, $filter) {
        $scope.contacts = [];
        $scope.page = 0;
        $scope.totalPage = 0;
        $scope.getContacts = getContacts;
        $scope.keyword = '';
        $scope.deleteContact = deleteContact;
        $scope.search = search;
        $scope.selectAll = selectAll;
        $scope.deleteMultiple = deleteMultiple;
        $scope.openEditDialog = openEditDialog;

        function openEditDialog(contact) {
            $scope.contact = contact;
            $uibModal.open({
                templateUrl: '/app/components/contact/contactEditViewModal.html',
                controller: 'contactEditController',
                backdrop: 'static',
                scope: $scope
            }).result.then(function ($scope) {

            }, function () {
            });
        }


        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.contacts, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.contacts, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("contacts", function (n, o) {
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
                        checkedListContactDetails: JSON.stringify(listId)
                    }
                }
                apiService.del('/api/contact/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                    search();
                }, function (error) {
                    notificationService.displayError('Xóa không thành công');

                });
            })

        }

        function deleteContact(id) {
            $ngBootbox.confirm('Bạn có muốn xóa không').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/contact/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công.');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công.')
                });
            })
        }

        function search() {
            getContacts();
        }

        function getContacts(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 5
                }
            }
            apiService.get('/api/contact/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không tìm thấy bản ghi nào.')
                }
                $scope.contacts = result.data.Items;
                $scope.page = result.data.Page;
                $scope.totalPage = result.data.TotalPage;
                $scope.totalCount = result.data.TotalCount;

            }, function () {
                notificationService.displayError('Không load được thông tin liên hệ.')
            });
        }

        $scope.getContacts();

    }
})(angular.module("quanstore.contact"))