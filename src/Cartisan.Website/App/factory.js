(function(angular) {
    if (!angular) {
        return;
    }

    var cartisanApp = angular.module('cartisanApp');
    cartisanApp.factory('questions.questionService', [
        '$http', function($http) {
            return new function() {
                this.getQuestions = function(input, httpParams) {
                    return $http({
                        url: '/question/GetQuestions',
                        method: 'POST',
                        data: JSON.stringify(input)
                    }, httpParams);
                }

                this.createQuestion = function(input, httpParams) {
                    return $http({
                        url: '/question/CreateQuestion',
                        method: 'POST',
                        data: JSON.stringify(input)
                    }, httpParams);
                };

                this.getQuestion = function(input, httpParams) {
                    return $http({
                        url: '/question/GetQuestion',
                        method: 'POST',
                        data: JSON.stringify(input)
                    }, httpParams);
                };

                this.voteUp = function(input, httpParams) {
                    return $http({
                        url: '/question/VoteUp',
                        method: 'POST',
                        data: JSON.stringify(input)
                    }, httpParams);
                };

                this.voteDown = function(input, httpParams) {
                    return $http({
                        url: '/question/VoteDown',
                        method: 'POST',
                        data: JSON.stringify(input)
                    }, httpParams);
                };

                this.submitAnswer = function(input, httpParams) {
                    return $http({
                        url: '/question/submitAnswer',
                        method: 'POST',
                        data: JSON.stringify(input)
                    }, httpParams);
                };

                this.acceptAnswer = function(input, httpParams) {
                    return $http({
                        url: '/question/acceptAnswer',
                        method: 'POST',
                        data: JSON.stringify(input)
                    }, httpParams);
                };
            }
        }
    ]);
})(angular);