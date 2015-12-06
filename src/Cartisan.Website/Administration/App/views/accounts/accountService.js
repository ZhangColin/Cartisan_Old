(function(angular) {
    if (!angular) {
        return;
    }

    var cartisanApp = angular.module('cartisanApp');
    cartisanApp.factory('accountService', [
        '$http', function($http) {
            return new function() {
                this.getAccounts = function(input, httpParams) {
                    return $http({
                        url: '/admin/account/GetAccounts',
                        method: 'POST',
                        data: JSON.stringify(input)
                    }, httpParams);
                }

            }
        }
    ]);
})(angular);