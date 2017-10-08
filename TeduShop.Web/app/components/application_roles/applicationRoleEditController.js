(function (app) {
    app.controller('applicationRoleEditController', applicationRoleEditController);
    applicationRoleEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']

    function applicationRoleEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.applicationRoles = {

        }
        $scope.UpdateApplicationRoles = UpdateApplicationRoles;


        function UpdateApplicationRoles() {
            apiService.put('api/applicationRole/update', $scope.applicationRoles, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được chỉnh sửa.');
                $state.go('applicationRole_list');
            }, function (result) {
                notificationService.displayError('Chỉnh sửa không thành công');
            })
        }

        function loadApplicationRole() {
            apiService.get('api/applicationRole/getbyid/' + $stateParams.id, null, function (result) {
                $scope.applicationRoles = result.data;
            }, function (error) {
                notificationService.displayWarning(error.data);
            })
        }

        loadApplicationRole();
    }
})(angular.module('tedushop.applicationRoles'))