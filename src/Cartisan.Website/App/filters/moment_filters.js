angular.module('cartisanApp')
    .filter('fromNow', function() {
        return function(input) {
            return moment(input).fromNow();
        };
    });