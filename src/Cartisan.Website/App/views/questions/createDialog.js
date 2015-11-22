(function () {
    var controllerId = 'questions.createDialog';
    var cartisanApp = angular.module('cartisanApp');
    cartisanApp.controller(controllerId, [
        'questions.questionService', '$modalInstance', function ($state, questionService) {
            $scope.$on('$viewContentLoaded', function () {
                // initialize core components
                App.initAjax();
            });

            // set sidebar closed and body solid layout mode
            $rootScope.settings.layout.pageContentWhite = true;
            $rootScope.settings.layout.pageBodySolid = false;
            $rootScope.settings.layout.pageSidebarClosed = false;

            var vm = this;

            vm.question = {
                title: '',
                text: ''
            };

            vm.save = function() {
                questionService.canCreateQuestions(vm.question)
                    .success(function() {
                        $modalInstance.close();
                    });
            };

            vm.cancel = function() {
                $modalInstance.dismiss('cancel');
            }
        }
    ]);
})();