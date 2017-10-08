(function (app) {
    app.controller('applicationKhachHangAddController', applicationKhachHangAddController);
    applicationKhachHangAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']

    function applicationKhachHangAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.applicationKhachHangs = {

        }
        $scope.addApplicationKhachHangs = addApplicationKhachHangs;

        function addApplicationKhachHangs() {
            apiService.post('api/khachHang/create', $scope.applicationKhachHangs, function (result) {
                notificationService.displaySuccess(result.data.FullName + ' đã được thêm mới.');
                $state.go('applicationKhachHang_list');

            }, function (result) {
                notificationService.displayError('Thêm mới không thành công');
            })
        }

    }
})(angular.module('tedushop.applicationKhachHangs'))