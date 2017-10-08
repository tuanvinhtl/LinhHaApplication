(function (app) {
    app.controller('loginController', ['$scope', 'loginService', '$injector', 'notificationService', 'authData',
        function ($scope, loginService, $injector, notificationService, authData) {

            $scope.loginData = {
                userName: "",
                password: ""
            };

            $scope.loginSubmit = function () {
                loginService.login($scope.loginData.userName, $scope.loginData.password).then(function (response) {
                    if (response != null && response.data.error != undefined) {
                        notificationService.displayError("Không thể đăng nhập: " + $scope.loginData.userName);
                    }
                    else { 
                        var stateService = $injector.get('$state');
                        stateService.go('chitietKhachHang_list');
                        notificationService.displaySuccess("Đăng nhập thành công " + $scope.loginData.userName);
                    }
                });
            }
        }]);
})(angular.module('tedushop'));