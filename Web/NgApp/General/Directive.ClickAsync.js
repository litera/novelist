(function (angular) {

	"use strict";

	angular
		.main
		.directive("ngClickAsync", function () {
			return {
				restrict: "A",
				scope: {
					action: "&ngClickAsync"
				},
				link: function (scope, element, attributes) {
					// set initial disabled value
					scope.externalDisabledValue = element.prop("disabled");

					// listen to disabled changes when other code disables this element too
					scope.$watch(function () {
						return element.prop("disabled");
					}, function (newValue) {
						if (element.data("clickAsyncSetter"))
						{
							element.removeData("clickAsyncSetter");
							return;
						}
						scope.externalDisabledValue = newValue;
					});

					// bind click event handler to change disabled state
					element.bind("click", function (evt) {
						evt.preventDefault();

						element.data("clickAsyncSetter", true);
						element.prop("disabled", true);
						scope
							.action()
							.finally(function () {
								// return to disabled state based on other factors
								element.data("clickAsyncSetter", true);
								element.prop("disabled", scope.externalDisabledValue);
							});
					})
				}
			}
		});

})(angular);