(function (app) {
    app.controller('applicationKhachHangListController', applicationKhachHangListController);
    applicationKhachHangListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function applicationKhachHangListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.applicationKhachHangs = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getApplicationKhachHangs = getApplicationKhachHangs;
        $scope.keyWord = '';
        $scope.search = search;
        $scope.selected;
        $scope.deleteApplicationKhachHang = deleteApplicationKhachHang;
        $scope.deleteMutile = deleteMutile;
        $scope.selectAll = selectAll;
        $scope.isAll = false;

        function search() {
            getApplicationKhachHangs();
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
                angular.forEach($scope.applicationKhachHangs, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.applicationKhachHangs, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("applicationKhachHangs", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteApplicationKhachHang(id) {
            var config = {
                params: {
                    id: id
                }
            }
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                apiService.del('/api/khachHang/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data);
                    search();
                }, function () {
                    notificationService.displayWarning('Xóa không thành công');
                })
            })
        }

        function getApplicationKhachHangs(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 5
                }
            }
            apiService.get('api/khachHang/getlistpaging', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy')
                }
                else {
                    $scope.applicationKhachHangs = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pageCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount
                }

            }, function () {
                console.log('Load failed.');

            });
        }

        $scope.getApplicationKhachHangs();

    }
})(angular.module('tedushop.applicationKhachHangs'));