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

            var vm = this;
            vm.dosPermissions = {
                canCreateQuestions: ''
            };

            vm.sortingDirections = [
                '创建时间 倒序',
                '投票数 倒序',
                '浏览数 倒序',
                '回答数 倒序'
            ];

            vm.questions = [];
            vm.totalQuestionCount = 0;
            vm.sorting = '创建时间 倒序';

            vm.loadQuestions = function(append) {
                var skipCount = append ? vm.questions.length : 0;

                // Todo: set busy
                questionService.getQuestions({
                    skipCount: skipCount,
                    sorting: vm.sorting
                }).success(function (data) {
                    if (append) {
                        for (var i = 0; i < data.datas.length; i++) {
                            vm.questions.push(data.datas[i]);
                        }
                    } else {
                        vm.questions = data.datas;
                    }

                    vm.totalQuestionCount = data.totalCount;
                });

                
            };

            vm.showNewQuestionDialog = function() {
                var modalInstance = $modal.open({
                    templateUrl: '/CartisanApp/Load?viewUrl=/App/views/questions/createDialog.cshtml',
                    controller: 'questions.createDialog',
                    size: 'md'
                });

                modalInstance.result.then(function() {
                    vm.loadQuestions();
                });
            };

            vm.sort = function(sortingDirection) {
                vm.sorting = sortingDirection;
                vm.loadQuestions();
            }

            vm.showMore = function() {
                vm.loadQuestions(true);
            };

            vm.loadQuestions();
        }
    ]);
})();