(function (app) {
    app.controller('khachhangByIdListController', khachhangByIdListController);
    khachhangByIdListController.$inject = ['$scope', 'apiService', 'notificationService', '$stateParams', '$ngBootbox', '$filter', 'commonService'];

    function khachhangByIdListController($scope, apiService, notificationService, $state, $stateParams, commonService) {

        function loadKhachHangct() {
            apiService.get('api/chitietkhachHang/getlistbyid/' + $stateParams.id, null, function (result) {
  
            }, function (error) {
                notificationService.displayWarning(error.data);
            })
        }

        loadKhachHangct();
    }
})(angular.module('tedushop.khachhangByIds'));