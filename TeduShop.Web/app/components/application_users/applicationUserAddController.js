(function (app) {
    app.controller('applicationUserAddController', applicationUserAddController);
    applicationUserAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']

    function applicationUserAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.applicationUsers = {
            Groups:[]
        }
        $scope.applicationGroups = [];
        $scope.getApplicationGroups = getApplicationGroups;
        $scope.addApplicationUsers = addApplicationUsers;


        function addApplicationUsers() {
            apiService.post('api/applicationUser/create', $scope.applicationUsers, function (result) {
                if (result.data[0]) {
                    notificationService.displayError('Thêm mới không thành công'+ result.data[0]);
                }
                else {
                    notificationService.displaySuccess(result.data.FullName + ' đã được thêm mới.');
                    $state.go('applicationUser_list');
                }
            }, function (result) {
                notificationService.displayError('Thêm mới không thành công');
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
    }
})(angular.module('tedushop.applicationUsers'))