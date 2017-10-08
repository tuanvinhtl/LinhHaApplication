(function (app) {
    app.controller('applicationGroupAddController', applicationGroupAddController);
    applicationGroupAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']

    function applicationGroupAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.applicationGroups = {

        }
        $scope.applicationRoles = [];
        $scope.getApplicationRoles = getApplicationRoles;

        $scope.addApplicationGroups = addApplicationGroups;
        function addApplicationGroups() {
            apiService.post('api/applicationGroup/create', $scope.applicationGroups, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                $state.go('applicationGroup_list');
            }, function (result) {
                notificationService.displayError('Thêm mới không thành công');
            })
        }
        function getApplicationRoles() {
            apiService.get('api/applicationRole/getall', null, function (response) {
                $scope.applicationRoles = response.data;
            }, function () {
                console.log("cant load data.")
            })
        }
        getApplicationRoles();
    }
})(angular.module('tedushop.applicationGroups'))