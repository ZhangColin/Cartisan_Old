(function() {
    var controllerId = 'app.views.layout.page_head';
    angular.module('cartisanApp').controller(controllerId, [
        '$scope', function($scope) {
            $scope.$on('$includeContentLoaded', function() {
                Demo.init(); // init theme panel
            });
        }
    ]);
})();