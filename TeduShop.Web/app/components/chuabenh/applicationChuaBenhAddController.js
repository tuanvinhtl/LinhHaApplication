(function (app) {
    app.controller('applicationChuaBenhAddController', applicationChuaBenhAddController);
    applicationChuaBenhAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']

    function applicationChuaBenhAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.applicationChuaBenhs = {

        }
        $scope.addApplicationChuaBenhs = addApplicationChuaBenhs;

        function addApplicationChuaBenhs() {
            apiService.post('api/chuaBenh/Create', $scope.applicationChuaBenhs, function (result) {
                notificationService.displaySuccess(result.data.FullName + ' đã được thêm mới.');
                $state.go('applicationChuaBenh_list');

            }, function (result) {
                notificationService.displayError('Thêm mới không thành công');
            })
        }

    }
})(angular.module('tedushop.applicationChuaBenhs'))