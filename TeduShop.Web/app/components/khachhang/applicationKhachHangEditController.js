(function (app) {
    app.controller('applicationKhachHangEditController', applicationKhachHangEditController);
    applicationKhachHangEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']

    function applicationKhachHangEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.applicationKhachHangs = {
            
        }
        $scope.UpdateApplicationKhachHangs = UpdateApplicationKhachHangs;

        function UpdateApplicationKhachHangs() {
            apiService.put('api/khachHang/update', $scope.applicationKhachHangs, function (result) {
                notificationService.displaySuccess(result.data.FullName + ' đã được chỉnh sửa.');
                $state.go('applicationKhachHang_list');
            }, function (result) {
                notificationService.displayError('Chỉnh sửa không thành công');
            })
        }

        function loadApplicationKhachHang() {
            apiService.get('api/khachHang/getbyid/' + $stateParams.id, null, function (result) {
                $scope.applicationKhachHangs = result.data;
            }, function (error) {
                notificationService.displayWarning(error.data);
            })
        }

        loadApplicationKhachHang();
    }
})(angular.module('tedushop.applicationKhachHangs'))