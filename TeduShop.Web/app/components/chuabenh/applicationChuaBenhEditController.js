(function (app) {
    app.controller('applicationChuaBenhEditController', applicationChuaBenhEditController);
    applicationChuaBenhEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']

    function applicationChuaBenhEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.applicationChuaBenhs = {
            
        }
        $scope.UpdateApplicationChuaBenhs = UpdateApplicationChuaBenhs;

        function UpdateApplicationChuaBenhs() {
            apiService.put('api/chuaBenh/Update', $scope.applicationChuaBenhs, function (result) {
                notificationService.displaySuccess(result.data.FullName + ' đã được chỉnh sửa.');
                $state.go('applicationChuaBenh_list');
            }, function (result) {
                notificationService.displayError('Chỉnh sửa không thành công');
            })
        }

        function loadApplicationChuaBenh() {
            apiService.get('api/chuaBenh/getbyid/' + $stateParams.id, null, function (result) {
                $scope.applicationChuaBenhs = result.data;
            }, function (error) {
                notificationService.displayWarning(error.data);
            })
        }

        loadApplicationChuaBenh();
    }
})(angular.module('tedushop.applicationChuaBenhs'))