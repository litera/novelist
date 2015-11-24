(function (angular) {

	"use strict";

	angular
		.main
		.service("securityService", SecurityService);

	SecurityService.$inject = ["$http", "$q", "$timeout"];

	function SecurityService($http, $q, $timeout) {
		this.$injected = {
			$http: $http,
			$q: $q,
			$timeout: $timeout
		};

		// initialize state
		this.isAuthenticated = false;
		this.user = null;

		this.activate();
	}

	SecurityService.prototype.activate = function () {
		if (window.name && /^novelist:/i.test(window.name))
		{
			try
			{
				this.user = window.JSON.parse(window.name.substr(9));
				this.isAuthenticated = true;
			}
			catch (ex)
			{
				// supress JSON parsing error
			}
		}
	}

	SecurityService.prototype.login = function (username, password) {
		var self = this;

		return this
			.$injected
			.$http
			.post("/api/login", {
				username: username,
				password: password
			})
			.then(function (response) {
				if (response.data)
				{
					// we got a winner!
					self.isAuthenticated = true;
					self.user = response.data;

					persistUser(response.data);

					notifySubscribers(self.$injected.$timeout);
					
					return self;
				}
				throw "Invalid login credentials.";
			});
	};

	SecurityService.prototype.logout = function () {
		this.isAuthenticated = false;
		this.user = null;

		persistUser(false);

		notifySubscribers(this.$injected.$timeout);

		return this
			.$injected
			.$q
			.when(true);
	};

	SecurityService.prototype.register = function (name, email, password, repeat) {
		var self = this;

		return this
			.$injected
			.$http
			.post("/api/register", {
				name: name,
				email: email,
				password: password,
				repeat: repeat
			})
			.catch(function (response) {
				console.dir(response);
				throw "Server error. Try again later.";
			})
			.then(function (response) {
				if (response.data)
				{
					// we've got a winner
					self.isAuthenticated = true;
					self.user = response.data;

					persistUser(response.data);

					notifySubscribers(self.$injected.$timeout);

					return self;
				}
				throw "Registration failed.";
			})
	};
	
	var subscribers = [];
	SecurityService.prototype.reportAuthChange = function (caller) {
		subscribers.push(caller);
	};

	// Private functionality

	function notifySubscribers(timeout) {
		for (var i = 0, l = subscribers.length; i < l; i++)
		{
			timeout(subscribers[i], 1);
		}
	}

	function persistUser(userInstance) {
		if (userInstance)
		{
			// oldschool cross page-refresh client session hack
			// is ok for the purpose of this app
			window.name = "Novelist:" + window.JSON.stringify(userInstance);
			return;
		}

		window.name = null;
	}

})(angular);