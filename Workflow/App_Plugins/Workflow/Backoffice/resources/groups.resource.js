﻿(() => {
    'use strict';

    function workflowGroupsResource($http, $q, umbRequestHelper) {

        const urlBase = Umbraco.Sys.ServerVariables.workflow.apiBasePath + '/groups';

        const request = (method, url, data) =>
            umbRequestHelper.resourcePromise(
                method === 'DELETE' ? $http.delete(url)
                    : method === 'POST' ? $http.post(url, data)
                        : method === 'PUT' ? $http.put(url, data)
                            : $http.get(url),
                'Something broke'
            );

        const service = {

            /**
             * @returns {array} user groups
             * @description Get single group by id, or all groups if no id parameter provided
             */
            get: id => request('GET', urlBase + (id ? `/get/${id}` : '/get')),

            /**
             * @description Add a new group, where the param is the new group name
             * @param {string} name - the name of the new group
             * @returns the new user group
             */
            add: name => request('POST', urlBase + '/add', { data: name }),

            /**
             * @param {object} group
             * @returns {string}
             * @description save updates to an existing group object
             */
            save: group => request('PUT', urlBase + '/save', group),

            /**
             * @param {int} id
             * @returns {string}
             * @description delete group by id
             */
            'delete': id => request('DELETE', urlBase + '/delete/' + id)
        };

        return service;
    }

    angular.module('plumber.services').factory('plmbrGroupsResource', ['$http', '$q', 'umbRequestHelper', workflowGroupsResource]);

})();