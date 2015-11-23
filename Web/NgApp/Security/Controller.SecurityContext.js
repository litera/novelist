(function (angular) {

	"use strict";

	angular
		.main
		.controller("SecurityContextController", SecurityContextController);

	SecurityContextController.$inject = ["securityService"];

	function SecurityContextController(securityService) {
		this.$injected = {
			securityService: securityService
		};

		this.viewModel = {
			name: null,
			isAuthenticated: false
		};

		this.activate(securityService);
	}

	SecurityContextController.prototype.activate = function () {
		var ss = this.$injected.securityService;

		if (this.viewModel.isAuthenticated = ss.isAuthenticated)
		{
			this.viewModel.name = ss.user.name;
		}

		ss.reportAuthChange(this.activate.bind(this));
	};

	SecurityContextController.prototype.logout = function () {
		this.$injected.securityService.logout();
	};

})(angular);