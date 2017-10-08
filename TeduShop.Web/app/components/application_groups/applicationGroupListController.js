(function (app) {
    app.controller('applicationGroupListController', applicationGroupListController);
    applicationGroupListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function applicationGroupListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.applicationGroups = [];

        $scope.getApplicationGroups = getApplicationGroups;

        $scope.deleteApplicationGroup = deleteApplicationGroup;

        function deleteApplicationGroup(id) {
            var config = {
                params: {
                    id: id
                }
            }
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                apiService.del('/api/applicationGroup/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data.Name);
                    getApplicationGroups();
                }, function () {
                    notificationService.displayWarning('Xóa không thành công');
                })
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
})(angular.module('tedushop.applicationGroups'));