(function (app) {
    app.controller('detailKhachHangAddController', detailKhachHangAddController);
    detailKhachHangAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService', '$ngBootbox']

    function detailKhachHangAddController($scope, apiService, notificationService, $state, $stateParams, commonService, $ngBootbox) {

        var idKhachHang = $stateParams.id;

        $scope.detailkhachhangs = {
            IdKhachHang:idKhachHang
        }

        $scope.addDetailKhachHangs = addDetailKhachHangs;
        function addDetailKhachHangs() {
            apiService.post('api/chitietKhachHang/Create', $scope.detailkhachhangs, function (result) {
                notificationService.displaySuccess(result.data.IdKhachHang + ' đã được thêm mới.');
                $state.go('detailKhachHang_list', {id:idKhachHang});

            }, function (result) {
                notificationService.displayError('Thêm mới không thành công');
            })
        }
        

    }
})(angular.module('tedushop.chitietKhachHangs'))