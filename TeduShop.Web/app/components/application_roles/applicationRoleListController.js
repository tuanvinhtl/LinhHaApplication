(function (app) {
    app.controller('applicationRoleListController', applicationRoleListController);
    applicationRoleListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function applicationRoleListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.applicationRoles = [];

        $scope.getApplicationRoles = getApplicationRoles;

        $scope.deleteApplicationRole = deleteApplicationRole;

        function deleteApplicationRole(id) {
            var config = {
                params: {
                    id: id
                }
            }
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                apiService.del('/api/applicationRole/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data.Name);
                    getApplicationRoles();
                }, function () {
                    notificationService.displayWarning('Xóa không thành công');
                })
            })
        }

        function getApplicationRoles() {
            apiService.get('api/applicationRole/getall', null, function (response) {
                $scope.applicationRoles = response.data;
            }, function () {
                console.log("cant load data.")
            })
        }

        $scope.getApplicationRoles();

    }
})(angular.module('tedushop.applicationRoles'));