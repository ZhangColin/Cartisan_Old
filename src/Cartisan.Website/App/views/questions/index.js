(function () {
    var controllerId = 'questions.index';
    var cartisanApp = angular.module('cartisanApp');
    cartisanApp.controller(controllerId, [
        '$rootScope', '$scope', 'questions.questionService', '$uibModal', function ($rootScope, $scope, questionService, $modal) {
            $scope.$on('$viewContentLoaded', function () {
                // initialize core components
                App.initAjax();
            });

            // set sidebar closed and body solid layout mode
            $rootScope.settings.layout.pageContentWhite = true;
            $rootScope.settings.layout.pageBodySolid = false;
            $rootScope.settings.layout.pageSidebarClosed = false;

            $scope.permissions = {
                canCreateQuestions: true
            };

            $scope.sortingDirections = [
                '创建时间 倒序',
                '投票数 倒序',
                '浏览数 倒序',
                '回答数 倒序'
            ];

            $scope.questions = [];
            $scope.totalQuestionCount = 0;
            $scope.sorting = '创建时间 倒序';

            $scope.loadQuestions = function(append) {
                var skipCount = append ? $scope.questions.length : 0;

                // Todo: set busy
                questionService.getQuestions({
                    skipCount: skipCount,
                    sorting: $scope.sorting
                }).success(function (data) {
                    if (append) {
                        for (var i = 0; i < data.datas.length; i++) {
                            $scope.questions.push(data.datas[i]);
                        }
                    } else {
                        $scope.questions = data.datas;
                    }

                    $scope.totalQuestionCount = data.total;
                });

                
            };

            $scope.showNewQuestionDialog = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/CartisanApp/Load?viewUrl=/App/views/questions/createDialog.cshtml',
                    controller: 'questions.createDialog',
                    backdrop: 'static',
                    size: 'md'
                });

                modalInstance.result.then(function() {
                    $scope.loadQuestions();
                });
            };

            $scope.sort = function(sortingDirection) {
                $scope.sorting = sortingDirection;
                $scope.loadQuestions();
            }

            $scope.showMore = function() {
                $scope.loadQuestions(true);
            };

            $scope.loadQuestions();
        }
    ]);
})();