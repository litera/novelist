(function (angular) {

	"use strict";

	angular
		.main
		.controller("PostsController", PostsController);

	PostsController.$inject = ["postsResource"];

	function PostsController(postsResource) {
		this.model = {
			posts: null
		};

		this.activate(postsResource);
	}

	PostsController.prototype.activate = function (postsResource) {
		this.model.posts = postsResource.query();
	}

})(angular);
