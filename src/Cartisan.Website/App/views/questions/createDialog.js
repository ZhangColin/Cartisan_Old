(function () {
    var controllerId = 'questions.createDialog';
    angular.module('cartisanApp').controller(controllerId, [
        'questionService', '$modalInstance', function ($state, questionService) {
            var vm = this;

            vm.question = {
                title: '',
                text: ''
            };

            vm.save = function() {
                questionService.canCreateQuestions(vm.question)
                    .success(function() {
                        $modalInstance.close();
                    });
            };

            vm.cancel = function() {
                $modalInstance.dismiss('cancel');
            }
        }
    ]);
})();