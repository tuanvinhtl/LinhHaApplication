(function (app) {
    app.controller('chitietKhachHangEditController', chitietKhachHangEditController);
    chitietKhachHangEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']

    function chitietKhachHangEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.chitietkhachhangs = {
            
        }
        $scope.editChiTietKhachHangs = editChiTietKhachHangs;

        function editChiTietKhachHangs() {
            apiService.put('api/chitietkhachHang/update', $scope.chitietkhachhangs, function (result) {
                notificationService.displaySuccess(result.data.FullName + ' đã được chỉnh sửa.');
                $state.go('chitietKhachHang_list');
            }, function (result) {
                notificationService.displayError('Chỉnh sửa không thành công');
            })
        }

        function loadChiTietKhachHang() {
            apiService.get('api/chitietkhachHang/getbyid/' + $stateParams.id, null, function (result) {
                $scope.chitietkhachhangs = result.data;
            }, function (error) {
                notificationService.displayWarning(error.data);
            })
        }

        loadChiTietKhachHang();
    }
})(angular.module('tedushop.chitietKhachHangs'))