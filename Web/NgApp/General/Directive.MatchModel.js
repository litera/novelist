(function (angular) {

	"use strict";

	// define directive
	angular
		.main
		.directive("matchModel", function () {
			return {
				require: "ngModel",
				link: function (scope, element, attributes, ngModelController) {

					var cachedValue = null;

					var validateMatch = function (value) {
						ngModelController.$setValidity("match", value === cachedValue);
						return value === cachedValue ? value : undefined;
					};

					ngModelController.$parsers.unshift(validateMatch);

					scope.$watch(attributes.matchModel, function (newValue/*, oldValue*/) {
						cachedValue = newValue;
						validateMatch(ngModelController.$viewValue);
					});
				}
			};
		});

})(angular);