(function (angular) {

	"use strict";

	angular
		.main
		.controller("SignupController", SignupController);

	SignupController.$inject = ["securityService", "$location"];

	function SignupController(securityService, $location) {
		this.$injected = {
			securityService: securityService,
			$location: $location
		};

		this.viewModel = {
			name: null,
			email: null,
			password: null,
			repeat: null
		};
	}

	SignupController.prototype.submit = function () {
		var self = this;

		this.$injected
			.securityService
			.register(
				this.viewModel.name,
				this.viewModel.email,
				this.viewModel.password,
				this.viewModel.repeat)
			.then(function (user) {
				self.$injected.$location.url("/");
			})
			.catch(function (error) {
				self.viewModel.error = error;
			});
	};

})(angular);