(function (app) {
    app.controller('detailKhachHangListController', detailKhachHangListController);
    detailKhachHangListController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService', '$ngBootbox']

    function detailKhachHangListController($scope, apiService, notificationService, $state, $stateParams, commonService, $ngBootbox) {
        $scope.detailkhachhangs = {

        }
        $scope.KhachHang = {

        };
        $scope.arrayTongSoNo = [];
        $scope.totalAmount = 0;
        $scope.deleteDetailKhachHang = deleteDetailKhachHang;
        $scope.deleteKhachHang = deleteKhachHang;

        function deleteKhachHang(id) {
            var config = {
                params: {
                    id: $stateParams.id
                }
            }
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                apiService.del('/api/khachHang/Delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data);
                    $state.go('chitietKhachHang_list');
                }, function () {
                    notificationService.displayWarning('Xóa không thành công');
                })
            })
            
        }

        function loadDetailKhachHang() {
            apiService.get('api/khachHang/getbyid/' + $stateParams.id, null, function (result) {
                $scope.KhachHang = result.data;
            }, function (error) {
                notificationService.displayWarning(error.data);
            })
        }

        function loadChiTietKhachHang() {
            apiService.get('api/chitietkhachHang/getlistbyid/' + $stateParams.id, null, function (result) {
                $scope.detailkhachhangs = result.data;
                $scope.arrayTongSoNo = result.data;
                getTotalAmount();
            }, function (error) {
                notificationService.displayWarning(error.data);
            })
        }

        function getTotalAmount() {
            var sum = 0;
            angular.forEach($scope.arrayTongSoNo, function (item) {
                sum += item.CTNoLai
            });
            $scope.totalAmount = sum;
        };

        function deleteDetailKhachHang(id) {
            var config = {
                params: {
                    id: id
                }
            }
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                apiService.del('/api/chitietKhachHang/Delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data);
                    loadChiTietKhachHang();
                }, function () {
                    notificationService.displayWarning('Xóa không thành công');
                })
            })
            
        }

        loadDetailKhachHang();
        loadChiTietKhachHang();
    }
})(angular.module('tedushop.chitietKhachHangs'))