(function () {
    var controllerId = 'questions.createDialog';
    var cartisanApp = angular.module('cartisanApp');
    cartisanApp.controller(controllerId, [
        '$scope', 'questions.questionService', '$uibModalInstance', function ($scope, questionService, $uibModalInstance) {
            //            $scope.$on('$viewContentLoaded', function () {
            //                // initialize core components
            //                App.initAjax();
            //            });
            //
            //            // set sidebar closed and body solid layout mode
            //            $rootScope.settings.layout.pageContentWhite = true;
            //            $rootScope.settings.layout.pageBodySolid = false;
            //            $rootScope.settings.layout.pageSidebarClosed = false;

//            console.log($uibModalInstance);
//            $uibModalInstance.close();

            $scope.question = {
                title: '',
                content: ''
            };

            $scope.save = function () {
                questionService.createQuestion($scope.question)
                    .success(function () {
                        $uibModalInstance.close();
                    });
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            }
        }
    ]);
})();