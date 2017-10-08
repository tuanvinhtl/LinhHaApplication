(function () {
    angular.module("tedushop.applicationRoles", ['tedushop.common']).config(config);
    config.$inject = ["$stateProvider", "$urlRouterProvider"];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('applicationRole_list', {
                url: '/applicationRole_list',
                parent: 'base',
                templateUrl: '/app/components/application_roles/applicationRoleListView.html',
                controller: 'applicationRoleListController'
            })
            .state('applicationRole_add', {
                url: '/applicationRole_add',
                parent: 'base',
                templateUrl: '/app/components/application_roles/applicationRoleAddView.html',
                controller: 'applicationRoleAddController'
            })
            .state('applicationRole_edit', {
                url: '/applicationRole_edit/:id',
                parent: 'base',
                templateUrl: '/app/components/application_roles/applicationRoleEditView.html',
                controller: 'applicationRoleEditController'
            });
    }
})();