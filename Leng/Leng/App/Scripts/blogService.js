(function () {
    'use strict';

    angular
        .module('blog')
        .service('blogService', blogService);

    blogService.inject = ['$http', 'blogApiUrl'];
    function blogService($http, blogApiUrl) {

        var service = {
            getAll: getAll,
            getById: getById
        };

        return service;

        ////////////

        function getAll() {
            return $http.get(blogApiUrl);
        }

        function getById(id) {
            return $http.get(blogApiUrl + id);
            // implementation details go here
        }

    }
})();