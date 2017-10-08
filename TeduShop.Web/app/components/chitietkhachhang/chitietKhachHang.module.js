(function () {
    angular.module("tedushop.chitietKhachHangs", ['tedushop.common']).config(config);
    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('chitietKhachHang_list', {
                url: '/chitietKhachHang_list',
                parent: 'base',
                templateUrl: '/app/components/chitietkhachhang/chitietKhachHangListView.html',
                controller: 'chitietKhachHangListController'
            })
            .state('chitietKhachHang_add', {
                url: '/chitietKhachHang_add',
                parent: 'base',
                templateUrl: '/app/components/chitietkhachhang/chitietKhachHangAddView.html',
                controller: 'chitietKhachHangAddController'
            })
            .state('chitietKhachHang_edit', {
                url: '/chitietKhachHang_edit/:id',
                parent: 'base',
                templateUrl: '/app/components/chitietkhachhang/chitietKhachHangEditView.html',
                controller: 'chitietKhachHangEditController'
            })

            .state('detailKhachHang_add', {
                url: '/detailKhachHang_add/:id',
                parent: 'base',
                templateUrl: '/app/components/chitietkhachhang/detailKhachHangAddView.html',
                controller: 'detailKhachHangAddController'
            })

            .state('detailKhachHang_list', {
                url: '/detailKhachHang_list/:id',
                parent: 'base',
                templateUrl: '/app/components/chitietkhachhang/detailKhachHangListView.html',
                controller: 'detailKhachHangListController'
            });


    }
})();