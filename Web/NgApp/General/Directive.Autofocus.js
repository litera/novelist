/*
 *	Autofocus directive
 *
 *	"Autofocus" attribute on "input" HTML element works great if your element is on page when it first loads.
 *	But is your page gets loaded as part of angular "ngView" this attribute won't do anything.
 *
 *	This directive amends this missing behaviour.
 */
(function (angular) {

	"use strict";

	angular
		.main
		.directive("autofocus", function () {
			return {
				link: function (scope, element, attributes) {
					element[0].focus();
				}
			};
		});

})(angular);