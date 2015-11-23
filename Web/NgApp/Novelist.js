(function (angular) {

	"use strict";

	angular.main = angular.main || angular.module("Novelist", ["ngRoute", "ngResource", "ngSanitize"]);

	angular
		.main
		.config(Routing);

	Routing.$inject = ["$routeProvider", "$locationProvider"];

	function Routing($routeProvider, $locationProvider) {
		$routeProvider
			// content
			.when("/posts", {
				templateUrl: "/NgApp/Posts/All.html",
				controller: "PostsController",
				controllerAs: "context"
			})
			.when("/posts/new", {
				templateUrl: "/NgApp/Posts/New.html",
				controller: "NewPostController",
				controllerAs: "context"
			})
			// security
			.when("/login", {
				templateUrl: "/NgApp/Security/Login.html",
				controller: "LoginController",
				controllerAs: "context"
			})
			.when("/signup", {
				templateUrl: "/NgApp/Security/Signup.html",
				controller: "SignupController",
				controllerAs: "context"
			})
			.otherwise({
				redirectTo: "/posts"
			});

		$locationProvider.html5Mode(true);
	}
		
})(angular);