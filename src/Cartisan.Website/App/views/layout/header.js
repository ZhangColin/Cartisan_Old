(function() {
    var controllerId = 'app.views.layout.header';
    angular.module('cartisanApp').controller(controllerId, [
        '$scope', function($scope) {
            var vm = this;

            $scope.$on('$includeContentLoaded', function() {
                Layout.initHeader(); // init header
            });

            //            vm.languages = abp.localization.languages;
            //            vm.currentLanguage = abp.localization.currentLanguage;
            //
            //            vm.menu = abp.nav.menus.MainMenu;
            //            vm.currentMenuName = $state.current.menu;
            //
            //            $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
            //                vm.currentMenuName = toState.menu;
            //            });
        }
    ]);
})();