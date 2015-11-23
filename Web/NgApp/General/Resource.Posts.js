(function (angular) {

	"use strict";

	angular
		.main
		.factory("postsResource", PostsResource);

	PostsResource.$inject = ["$resource"];

	function PostsResource($resource) {
		return $resource("/api/posts/:id", { id: "@id" });
	}

})(angular);