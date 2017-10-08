(function (app) {
    app.controller('chitietKhachHangAddController', chitietKhachHangAddController);
    chitietKhachHangAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']

    function chitietKhachHangAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.chitietkhachhangs = {

        }
        $scope.addChiTietKhachHangs = addChiTietKhachHangs;

        function addChiTietKhachHangs() {
            apiService.post('api/chitietKhachHang/CreateAny', $scope.chitietkhachhangs, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                $state.go('chitietKhachHang_list');

            }, function (result) {
                notificationService.displayError('Thêm mới không thành công');
            })
        }

    }
})(angular.module('tedushop.chitietKhachHangs'))