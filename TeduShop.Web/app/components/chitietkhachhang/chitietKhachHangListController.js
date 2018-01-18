(function (app) {
    app.controller('chitietKhachHangListController', chitietKhachHangListController);
    chitietKhachHangListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function chitietKhachHangListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.chitietKhachHangs = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getChiTietKhachHangs = getChiTietKhachHangs;
        $scope.keyWordName = '';
        $scope.keyWordAddress = '';
        $scope.fromDate = '';
        $scope.toDate = '';
        $scope.exportExcel = exportExcel;
        $scope.totalAmount = 0;
        $scope.arrayTongSoNo = [];

        $scope.search = search;
        $scope.selected;
        $scope.deleteChiTietKhachHang = deleteChiTietKhachHang;
        $scope.deleteMutile = deleteMutile;
        $scope.selectAll = selectAll;
        $scope.isAll = false;

        function search() {
            getChiTietKhachHangs();
            loadTotalAmount();
        }

        function exportExcel() {
            apiService.get('api/chitietKhachHang/exportExcel', null, function (result) {
                window.open("http://localhost:50202/" + result.data.Message)
            }, function () {
                console.log('cant export file');

            });
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
                angular.forEach($scope.chitietKhachHangs, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.chitietKhachHangs, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("chitietKhachHangs", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteChiTietKhachHang(id) {
            var config = {
                params: {
                    id: id
                }
            }
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                apiService.del('/api/chitietKhachHang/Delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data);
                    search();
                }, function () {
                    notificationService.displayWarning('Xóa không thành công');
                })
            })
        }

        function getChiTietKhachHangs(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWordName: $scope.keyWordName,
                    keyWordAddress: $scope.keyWordAddress,
                    fromDate: $scope.fromDate,
                    toDate: $scope.toDate,
                    page: page,
                    pageSize: 5
                }
            }
            apiService.get('api/chitietKhachHang/getlistpaging', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy')
                }
                else {
                    $scope.chitietKhachHangs = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pageCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount
                }

            }, function () {
                console.log('Load failed.');

            });
        }

        function loadTotalAmount() {
            var config = {
                params: {
                    keyWordName: $scope.keyWordName,
                    keyWordAddress: $scope.keyWordAddress,
                    fromDate: $scope.fromDate,
                    toDate: $scope.toDate,
                }
            }
            apiService.get('api/chitietKhachHang/getTotalAmount', config, function (result) {
                if (result.data == null) {
                    $scope.totalAmount = 0;
                }
                else {
                    $scope.arrayTongSoNo = result.data;
                    getTotalAmount();
                }

            }, function () {
                console.log('Load failed.');

            });
        }

        function getTotalAmount() {
            var sum = 0;
            angular.forEach($scope.arrayTongSoNo, function (item) {
                sum += item.CTNoLai
            });
            $scope.totalAmount = sum;
        };

        loadTotalAmount();
        getChiTietKhachHangs();

    }
})(angular.module('tedushop.chitietKhachHangs'));