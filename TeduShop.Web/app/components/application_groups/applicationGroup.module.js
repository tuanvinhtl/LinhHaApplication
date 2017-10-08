(function () {
    angular.module("tedushop.applicationGroups", ['tedushop.common']).config(config);
    config.$inject = ["$stateProvider", "$urlRouterProvider"];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('applicationGroup_list', {
                url: '/applicationGroup_list',
                parent: 'base',
                templateUrl: '/app/components/application_groups/applicationGroupListView.html',
                controller: 'applicationGroupListController'
            })
            .state('applicationGroup_add', {
                url: '/applicationGroup_add',
                parent: 'base',
                templateUrl: '/app/components/application_groups/applicationGroupAddView.html',
                controller: 'applicationGroupAddController'
            })
            .state('applicationGroup_edit', {
                url: '/applicationGroup_edit/:id',
                parent: 'base',
                templateUrl: '/app/components/application_groups/applicationGroupEditView.html',
                controller: 'applicationGroupEditController'
            });
    }
})();