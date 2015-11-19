var cartisanApp = angular.module("cartisanApp", [
    "ui.router",
    "ui.bootstrap",
    "oc.lazyLoad",
    "ngSanitize"
]);

/* Configure ocLazyLoader(refer: https://github.com/ocombe/ocLazyLoad) */
cartisanApp.config(['$ocLazyLoadProvider', function ($ocLazyLoadProvider) {
    $ocLazyLoadProvider.config({
        cssFilesInsertBefore: 'ng_load_plugins_before' // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
    });
}]);

cartisanApp.config(['$controllerProvider', function ($controllerProvider) {
    // this option might be handy for migrating old apps, but please don't use it
    // in new ones!
    $controllerProvider.allowGlobals();
}]);

/* Setup global settings */
cartisanApp.factory('settings', ['$rootScope', function ($rootScope) {
    // supported languages
    var settings = {
        layout: {
            pageAutoScrollOnLoad: 1000 // auto scroll to top on page load
        },
        layoutImgPath: Metronic.getAssetsPath() + 'admin/layout/img/',
        layoutCssPath: Metronic.getAssetsPath() + 'admin/layout/css/'
    };

    $rootScope.settings = settings;

    return settings;
}]);

/* Setup global settings */
cartisanApp.factory('settings', ['$rootScope', function ($rootScope) {
    // supported languages
    var settings = {
        layout: {
            pageSidebarClosed: false, // sidebar menu state
            pageBodySolid: false, // solid body color state
            pageAutoScrollOnLoad: 1000 // auto scroll to top on page load
        },
        layoutImgPath: Metronic.getAssetsPath() + 'admin/layout/img/',
        layoutCssPath: Metronic.getAssetsPath() + 'admin/layout/css/'
    };

    $rootScope.settings = settings;

    return settings;
}]);

/* Setup App Main Controller */
cartisanApp.controller('AppController', ['$scope', '$rootScope', function ($scope, $rootScope) {
    $scope.$on('$viewContentLoaded', function () {
        Metronic.initComponents(); // init core components
        //Layout.init(); //  Init entire layout(header, footer, sidebar, etc) on page load if the partials included in server side instead of loading with ng-include directive 
    });
}]);

/***
Layout Partials.
By default the partials are loaded through AngularJS ng-include directive. In case they loaded in server side(e.g: PHP include function) then below partial 
initialization can be disabled and Layout.init() should be called on page load complete as explained above.
***/

/* Setup Layout Part - Header */
cartisanApp.controller('HeaderController', ['$scope', function ($scope) {
    $scope.$on('$includeContentLoaded', function () {
        Layout.initHeader(); // init header
    });
}]);

/* Setup Layout Part - Sidebar */
cartisanApp.controller('PageHeadController', ['$scope', function ($scope) {
    $scope.$on('$includeContentLoaded', function () {
        Demo.init(); // init theme panel
    });
}]);

/* Setup Layout Part - Footer */
cartisanApp.controller('FooterController', ['$scope', function ($scope) {
    $scope.$on('$includeContentLoaded', function () {
        Layout.initFooter(); // init footer
    });
}]);


/* Setup Rounting For All Pages */
cartisanApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    // Redirect any unmatched url
    $urlRouterProvider.otherwise("/questions");

    $stateProvider

        // Dashboard
//        .state('dashboard', {
//            url: "/dashboard.html",
//            templateUrl: "views/dashboard.html",
//            data: { pageTitle: 'Dashboard', pageSubTitle: 'statistics & reports' },
//            controller: "DashboardController",
//            resolve: {
//                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
//                    return $ocLazyLoad.load({
//                        name: 'cartisanApp',
//                        insertBefore: '#ng_load_plugins_before', // load the above css files before '#ng_load_plugins_before'
//                        files: [
//                            '../../../assets/global/plugins/morris/morris.css',
//                            '../../../assets/admin/pages/css/tasks.css',
//
//                            '../../../assets/global/plugins/morris/morris.min.js',
//                            '../../../assets/global/plugins/morris/raphael-min.js',
//                            '../../../assets/global/plugins/jquery.sparkline.min.js',
//
//                            '../../../assets/admin/pages/scripts/index3.js',
//                            '../../../assets/admin/pages/scripts/tasks.js',
//
//                             'js/controllers/DashboardController.js'
//                        ]
//                    });
//                }]
//            }
//        })


        // Questions
        .state("questions", {
            url: "/questions",
            templateUrl: "/CartisanApp/Load?viewUrl=/App/views/questions/index.cshtml",
            data: { pageTitle: '问答', pageSubTitle: '交流与分享' },
            resolve: {
                deps: ['$ocLazyLoad', function($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: 'cartisanApp',
                            insertBefore: '#ng_load_plugins_before',
                            files: [
                                '../App/views/questions/questions.css'
                            ]
                        });
                    }
                ]
            },
            menu: '提问'
        })

        // User Profile Account
        .state("questionDetail", {
            url: "/questions/:id",
            templateUrl: "/CartisanApp/Load?viewUrl=views/questions/detail.cshtml",
            data: { pageTitle: '问答', pageSubTitle: '交流与分享' },
            menu: '提问'
        });

}]);

/* Init global settings and run the app */
cartisanApp.run(["$rootScope", "settings", "$state", function ($rootScope, settings, $state) {
    $rootScope.$state = $state; // state to be accessed from view
}]);