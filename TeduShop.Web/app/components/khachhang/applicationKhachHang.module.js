(function () {
    angular.module("tedushop.applicationKhachHangs", ['tedushop.common']).config(config);
    config.$inject = ["$stateProvider", "$urlRouterProvider"];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('applicationKhachHang_list', {
                url: '/applicationKhachHang_list',
                parent: 'base',
                templateUrl: '/app/components/khachhang/applicationKhachHangListView.html',
                controller: 'applicationKhachHangListController'
            })
            .state('applicationKhachHang_add', {
                url: '/applicationKhachHang_add',
                parent: 'base',
                templateUrl: '/app/components/khachhang/applicationKhachHangAddView.html',
                controller: 'applicationKhachHangAddController'
            })
            .state('applicationKhachHang_edit', {
                url: '/applicationKhachHang_edit/:id',
                parent: 'base',
                templateUrl: '/app/components/khachhang/applicationKhachHangEditView.html',
                controller: 'applicationKhachHangEditController'
            });
    }
})();