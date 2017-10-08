(function (app) {
    app.controller('applicationRoleAddController', applicationRoleAddController);
    applicationRoleAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']

    function applicationRoleAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.applicationRoles = {

        }


        $scope.addApplicationRoles = addApplicationRoles;
        function addApplicationRoles() {
            apiService.post('api/applicationRole/create', $scope.applicationRoles, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                $state.go('applicationRole_list');
            }, function (result) {
                notificationService.displayError('Thêm mới không thành công');
            })
        }

    }
})(angular.module('tedushop.applicationRoles'))