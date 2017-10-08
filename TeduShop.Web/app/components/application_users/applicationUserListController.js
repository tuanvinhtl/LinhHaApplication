(function (app) {
    app.controller('applicationUserListController', applicationUserListController);
    applicationUserListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function applicationUserListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.applicationUsers = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getApplicationUsers = getApplicationUsers;
        $scope.keyWord = '';
        $scope.search = search;
        $scope.selected;
        $scope.deleteApplicationUser = deleteApplicationUser;
        $scope.deleteMutile = deleteMutile;
        $scope.selectAll = selectAll;
        $scope.isAll = false;

        function search() {
            getApplicationUsers();
        }

        function deleteMutile() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            })
            var config = {
                params: {
                    listProductCategoryId: JSON.stringify(listId)
                }
            }
            apiService.del('/api/productcategory/deletemutile', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi');
                search()
            }, function (error) {
                notificationService.displayWarning('Xóa không thành công')
            });
        };

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.applicationUsers, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.applicationUsers, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("applicationUsers", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteApplicationUser(id) {
            var config = {
                params: {
                    id: id
                }
            }
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                apiService.del('/api/applicationUser/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data);
                    search();
                }, function () {
                    notificationService.displayWarning('Xóa không thành công');
                })
            })
        }

        function getApplicationUsers(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 5
                }
            }
            apiService.get('api/applicationUser/getlistpaging', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy')
                }
                else {
                    $scope.applicationUsers = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pageCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount
                }

            }, function () {
                console.log('Load failed.');

            });
        }

        $scope.getApplicationUsers();

    }
})(angular.module('tedushop.applicationUsers'));