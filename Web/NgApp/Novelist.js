(function (angular) {

	"use strict";

	angular.main = angular.main || angular.module("Novelist", ["ngRoute", "ngResource", "ngSanitize"]);

	angular
		.main
		.config(Routing);

	Routing.$inject = ["$routeProvider", "$locationProvider"];

	function Routing($routeProvider, $locationProvider) {
		$routeProvider
			.when("/", {
				templateUrl: "/NgApp/Posts/All.html",
				controller: "PostsController",
				controllerAs: "context"
			})
			.otherwise({
				redirectTo: "/"
			});

		$locationProvider.html5Mode(true);
	}
		
})(angular);