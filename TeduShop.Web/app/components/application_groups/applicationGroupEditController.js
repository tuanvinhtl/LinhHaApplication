(function (app) {
    app.controller('applicationGroupEditController', applicationGroupEditController);
    applicationGroupEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']

    function applicationGroupEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.applicationGroups = {
            Roles:[]
        }
        $scope.UpdateApplicationGroups = UpdateApplicationGroups;
        $scope.applicationRoles = [];
        $scope.getApplicationRoles = getApplicationRoles;


        function UpdateApplicationGroups() {
            apiService.put('api/applicationGroup/update', $scope.applicationGroups, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được chỉnh sửa.');
                $state.go('applicationGroup_list');
            }, function (result) {
                notificationService.displayError('Chỉnh sửa không thành công');
            })
        }

        function loadApplicationGroup() {
            apiService.get('api/applicationGroup/getbyid/' + $stateParams.id, null, function (result) {
                $scope.applicationGroups = result.data;
            }, function (error) {
                notificationService.displayWarning(error.data);
            })
        }

        function getApplicationRoles() {
            apiService.get('api/applicationRole/getall', null, function (response) {
                $scope.applicationRoles = response.data;
            }, function () {
                console.log("cant load data.")
            })
        }
        loadApplicationGroup();
        getApplicationRoles();
    }
})(angular.module('tedushop.applicationGroups'))