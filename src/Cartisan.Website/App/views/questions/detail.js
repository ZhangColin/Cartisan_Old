(function () {
    var controllerId = 'questions.detail';
    var cartisanApp = angular.module('cartisanApp');
    cartisanApp.controller(controllerId, [
        '$state', 'questions.questionService', function ($state, questionService) {
            $scope.$on('$viewContentLoaded', function () {
                // initialize core components
                App.initAjax();
            });

            // set sidebar closed and body solid layout mode
            $rootScope.settings.layout.pageContentWhite = true;
            $rootScope.settings.layout.pageBodySolid = false;
            $rootScope.settings.layout.pageSidebarClosed = false;

            var vm = this;

            vm.questions = null;
            vm.answerText = '';
            vm.ownQuestion = false;

            vm.voteUp = function() {
                questionService.voteUp({
                    id: vm.question.id
                }).success(function(data) {
                    vm.question.voteCount = data.voteCount;

                    // Todo: notify
                });
            };

            vm.voteDown = function() {
                questionService.voteDown({
                    id: vm.question.id
                }).success(function(data) {
                    vm.question.voteCount = data.voteCount;

                    // Todo: notify
                });
            };

            vm.submitAnswer = function() {
                questionService.submitAnswer({
                    questionId: vm.question.id,
                    content: vm.answerText
                }).success(function(data) {
                    vm.question.answers.push(data.answer);
                    vm.answerText = '';
                });
            };

            vm.acceptAnswer = function(answer) {
                questionService, acceptAnswer({
                    id: answer.id
                }).success(function() {
                    // Todo: notify

                    loadQuestions();
                });
            };

            var loadQuestion = function() {
                // Todo: set busy
                questionService.getQuestion({
                    id: $state.params.id,
                    incrementViewCount: true
                }).success(function(data) {
                    vm.question = data.question;
                    vm.ownQuestion = vm.question.creatorUserId == ''; // Todo: current user id

                    var acceptedAnswerIndex = -1;
                    for (var i = 0; i < vm.question.answers.length; i++) {
                        if (vm.question.answers[i].isAccepted) {
                            acceptedAnswerIndex = i;
                            break;
                        }
                    }

                    if (acceptedAnswerIndex > 0) {
                        var acceptedAnswer = vm.question.answers[acceptedAnswerIndex];
                        vm.question.answers.splice(acceptedAnswerIndex, 1);
                        vm.question.answers.unshift(acceptedAnswer);
                    }
                });
            };

            loadQuestions();
        }
    ]);
})();