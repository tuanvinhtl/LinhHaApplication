(function () {
    angular.module("tedushop.khachhangByIds", ['tedushop.common']).config(config);
    config.$inject = ["$stateProvider", "$urlRouterProvider"];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('khachhangById_list', {
                url: '/khachhangById_list/:id',
                parent: 'base',
                templateUrl: '/app/components/khachhangbyid/khachhangByIdListView.html',
                controller: 'khachhangByIdListController'
            }) 
    }
})();