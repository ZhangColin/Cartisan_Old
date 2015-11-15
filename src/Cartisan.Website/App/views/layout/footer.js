(function() {
    var controllerId = 'app.views.layout.footer';
    angular.module('cartisanApp').controller(controllerId, [
        '$scope', function($scope) {
            $scope.$on('$includeContentLoaded', function() {
                Layout.initFooter(); // init footer
            });
        }
    ]);
})();