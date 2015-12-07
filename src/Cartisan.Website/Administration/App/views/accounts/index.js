﻿(function () {
    var controllerId = 'accounts.index';
    var cartisanApp = angular.module('cartisanApp');
    cartisanApp.controller(controllerId, function ($rootScope, $scope, accountService, $uibModal, $http, $timeout) {
        $scope.$on('$viewContentLoaded', function () {
            // initialize core components
            App.initAjax();
        });

        // set sidebar closed and body solid layout mode
        $rootScope.settings.layout.pageContentWhite = true;
        $rootScope.settings.layout.pageBodySolid = false;
        $rootScope.settings.layout.pageSidebarClosed = false;

        $scope.pageIndex = 1;
        $scope.pageSize = 10;
        $scope.total = 0;


        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.pageChanged = function () {
            //$log.log('Page changed to: ' + $scope.currentPage);
            loadAccounts();
        };


        $scope.accounts = [];

        var loadAccounts = function() {
            accountService.getAccounts().success(function (data) {
                $scope.accounts = data.datas;
                $scope.pageIndex = data.pageIndex;
                $scope.total = data.total;
            });
        }

        loadAccounts();
    });
})();