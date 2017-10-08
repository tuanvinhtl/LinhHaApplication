(function () {
    angular.module("tedushop.applicationChuaBenhs", ['tedushop.common']).config(config);
    config.$inject = ["$stateProvider", "$urlRouterProvider"];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('applicationChuaBenh_list', {
                url: '/applicationChuaBenh_list',
                parent: 'base',
                templateUrl: '/app/components/chuabenh/applicationChuaBenhListView.html',
                controller: 'applicationChuaBenhListController'
            })
            .state('applicationChuaBenh_add', {
                url: '/applicationChuaBenh_add',
                parent: 'base',
                templateUrl: '/app/components/chuabenh/applicationChuaBenhAddView.html',
                controller: 'applicationChuaBenhAddController'
            })
            .state('applicationChuaBenh_edit', {
                url: '/applicationChuaBenh_edit/:id',
                parent: 'base',
                templateUrl: '/app/components/chuabenh/applicationChuaBenhEditView.html',
                controller: 'applicationChuaBenhEditController'
            });
    }
})();