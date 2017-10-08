(function (app) {
    app.controller('applicationUserEditController', applicationUserEditController);
    applicationUserEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService']

    function applicationUserEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.applicationUsers = {
            Groups: []
        }
        $scope.UpdateApplicationUsers = UpdateApplicationUsers;
        $scope.getApplicationGroups = getApplicationGroups;

        function UpdateApplicationUsers() {
            apiService.put('api/applicationUser/update', $scope.applicationUsers, function (result) {
                notificationService.displaySuccess(result.data.FullName + ' đã được chỉnh sửa.');
                $state.go('applicationUser_list');
            }, function (result) {
                notificationService.displayError('Chỉnh sửa không thành công');
            })
        }

        function loadApplicationUser() {
            apiService.get('api/applicationUser/getbyid/' + $stateParams.id, null, function (result) {
                $scope.applicationUsers = result.data;
            }, function (error) {
                notificationService.displayWarning(error.data);
            })
        }
        function getApplicationGroups() {
            apiService.get('api/applicationGroup/getall', null, function (response) {
                $scope.applicationGroups = response.data;
            }, function () {
                console.log("cant load data.")
            })
        }

        $scope.getApplicationGroups();

        loadApplicationUser();
    }
})(angular.module('tedushop.applicationUsers'))