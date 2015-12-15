(function () {
    var controllerId = 'accounts.createDialog';
    var cartisanApp = angular.module('cartisanApp');
    cartisanApp.controller(controllerId, function ($scope, accountService, $uibModalInstance) {
            $scope.account = {
                userName: '',
                nickName: '',
                trueName: '',
                password: '',
                confirmPassword: '',
                email: '',
                mobile: '',
                isActive: false,
                usingRandomPassword: false,
                nextLoginNeedModifyPassword: false,
                sendActivationEmail: false
            };

            $scope.save = function () {
//                questionService.createQuestion($scope.question)
//                    .success(function () {
//                        $uibModalInstance.close();
//                    });
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            }
        }
    );
})();