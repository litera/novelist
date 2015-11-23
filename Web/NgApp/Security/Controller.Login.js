(function (angular) {

	"use strict";

	angular
		.main
		.controller("LoginController", LoginController);

	LoginController.$inject = ["securityService", "$location"];

	function LoginController(securityService, $location) {
		this.$injected = {
			securityService: securityService,
			$location: $location
		};

		this.viewModel = {
			username: null,
			password: null,
			error: null
		}
	}

	LoginController.prototype.login = function () {
		var self = this;

		this.$injected
			.securityService
			.login(
				this.viewModel.username,
				this.viewModel.password)
			.then(function () {
				self.$injected.$location.url("/");
			})
			.catch(function (error) {
				self.viewModel.error = error;
			});
	}

})(angular);