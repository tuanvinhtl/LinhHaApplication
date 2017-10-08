(function () {
    angular.module("tedushop.applicationUsers", ['tedushop.common']).config(config);
    config.$inject = ["$stateProvider", "$urlRouterProvider"];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('applicationUser_list', {
                url: '/applicationUser_list',
                parent: 'base',
                templateUrl: '/app/components/application_users/applicationUserListView.html',
                controller: 'applicationUserListController'
            })
            .state('applicationUser_add', {
                url: '/applicationUser_add',
                parent: 'base',
                templateUrl: '/app/components/application_users/applicationUserAddView.html',
                controller: 'applicationUserAddController'
            })
            .state('applicationUser_edit', {
                url: '/applicationUser_edit/:id',
                parent: 'base',
                templateUrl: '/app/components/application_users/applicationUserEditView.html',
                controller: 'applicationUserEditController'
            });
    }
})();