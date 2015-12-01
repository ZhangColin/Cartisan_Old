(function () {
    var controllerId = 'questions.detail';
    var cartisanApp = angular.module('cartisanApp');
    cartisanApp.controller(controllerId, [
        '$rootScope', '$scope', '$state', 'questions.questionService', function ($rootScope, $scope, $state, questionService) {
            $scope.$on('$viewContentLoaded', function () {
                // initialize core components
                App.initAjax();
            });

            // set sidebar closed and body solid layout mode
            $rootScope.settings.layout.pageContentWhite = true;
            $rootScope.settings.layout.pageBodySolid = false;
            $rootScope.settings.layout.pageSidebarClosed = false;

            $scope.question = null;
            $scope.answerContent = '';
            $scope.ownQuestion = true;

            var loadQuestion = function (incrementViewCount) {
                // Todo: set busy
                questionService.getQuestion({
                    questionId: $state.params.id,
                    incrementViewCount: incrementViewCount
                }).success(function (data) {
                    $scope.question = data;
                    //$scope.ownQuestion = $scope.question.creatorUserId == '1'; // Todo: current user id

                    var acceptedAnswerIndex = -1;
                    for (var i = 0; i < $scope.question.answers.length; i++) {
                        if ($scope.question.answers[i].isAccepted) {
                            acceptedAnswerIndex = i;
                            break;
                        }
                    }

                    if (acceptedAnswerIndex > 0) {
                        var acceptedAnswer = $scope.question.answers[acceptedAnswerIndex];
                        $scope.question.answers.splice(acceptedAnswerIndex, 1);
                        $scope.question.answers.unshift(acceptedAnswer);
                    }
                });
            };

            $scope.voteUp = function() {
                questionService.voteUp({
                    questionId: $scope.question.id
                }).success(function(data) {
                    $scope.question.voteCount = data.voteCount;

                    // Todo: notify
                });
            };

            $scope.voteDown = function() {
                questionService.voteDown({
                    questionId: $scope.question.id
                }).success(function(data) {
                    $scope.question.voteCount = data.voteCount;

                    // Todo: notify
                });
            };

            $scope.submitAnswer = function() {
                questionService.submitAnswer({
                    questionId: $scope.question.id,
                    content: $scope.answerContent
                }).success(function(data) {
                    $scope.question.answers.push(data);
                    $scope.answerContent = '';
                });
            };

            $scope.acceptAnswer = function(answer) {
                questionService.acceptAnswer({
                    answerId: answer.id
                }).success(function() {
                    // Todo: notify

                    loadQuestion(false);
                });
            };

            loadQuestion(true);
        }
    ]);
})();