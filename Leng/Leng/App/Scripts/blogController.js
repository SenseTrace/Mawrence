
(function () {
    'use strict';

    angular
        .module('blog')
        .controller('blogController', blogController);

    blogController.inject = ['$scope', 'blogService'];
    function blogController($scope, blogService) {
        var vm = this;
        blogService
            .getAll()
            .then(function (response) {
                $scope.blogs = response.data;
            });
    }
})();
