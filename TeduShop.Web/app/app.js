﻿/// <reference path="/Assets/admin/libs/angular/angular.js" />
/// <reference path="/admin/libs/angular-ui-router/release/angular-ui-router.js" />

(function () {
    angular.module("tedushop", [
        'tedushop.common',
        'tedushop.applicationGroups',
        'tedushop.applicationUsers',
        'tedushop.applicationRoles',
        'tedushop.applicationKhachHangs',
        'tedushop.applicationChuaBenhs',
        'tedushop.chitietKhachHangs',
        'tedushop.khachhangByIds'

    ])
        .config(config)
        .config(configAuthentication);

    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('base', {
                url: '',
                templateUrl: '/app/shared/view/baseView.html',
                abstract: true
            })
            .state('login', {
                url: '/login',
                templateUrl: '/app/components/login/loginView.html',
                controller: 'loginController'
            })
            .state('home', {
                url: '/',
                parent: 'base',
                templateUrl: '/app/components/home/homeView.html',
                controller: 'homeController'
            });
        $urlRouterProvider.otherwise('/login');
    }
    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {

                    return config;
                },
                requestError: function (rejection) {

                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {

                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }

})();