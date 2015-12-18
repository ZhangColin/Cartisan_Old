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
                usingRandomPassword: true,
                nextLoginNeedModifyPassword: false,
                sendActivationEmail: false
            };

            $scope.save = function () {
                accountService.createAccount($scope.account)
                    .success(function (result) {
                        console.log(result);
                        $uibModalInstance.close();
                    });
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            }
        }
    );
})();