(function (angular) {

	"use strict";

	angular
		.main
		.controller("NewPostController", NewPostController);

	NewPostController.$inject = ["securityService", "postsResource", "$location", "$q"];

	function NewPostController(securityService, postsResource, $location, $q) {
		this.$injected = {
			securityService: securityService,
			postsResource: postsResource,
			$location: $location,
			$q: $q
		};

		this.viewModel = {
			title: null,
			intro: null,
			content: null,
			error: null
		};
	}

	NewPostController.prototype.publish = function () {
		var self = this;

		// reset error
		this.viewModel.error = null;

		if (this.$injected.securityService.isAuthenticated)
		{
			return this.$injected
				.postsResource
				.save({
					author: this.$injected.securityService.user,
					title: this.viewModel.title,
					intro: this.viewModel.intro,
					content: this.viewModel.content
				})
				.$promise
				.then(function (post) {
					self.$injected.$location.path("/");
				})
				.catch(function (response) {
					console.dir(response);
					self.viewModel.error = "Post publishing failed with server error. Try again later.";
				});
		}

		// return a resolved promise
		return this.$injected.$q.when(true);
	};

})(angular);
