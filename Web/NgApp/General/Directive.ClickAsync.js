(function (angular) {

	"use strict";

	angular
		.main
		.directive("ngClickAsync", function () {
			return {
				restrict: "A",
				scope: {
					action: "&ngClickAsync",
					replaceContent: "@?ngClickAsyncContent"
				},
				link: function (scope, element, attributes) {
					// set initial disabled value
					scope.externalDisabledValue = element.prop("disabled");

					// save original content
					element.data("originalContent", element.html());

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

						// change label if defined
						if (scope.replaceContent)
						{
							element.html(scope.replaceContent);
						}

						scope
							.action()
							.finally(function () {
								// return to disabled state based on other factors
								element.data("clickAsyncSetter", true);
								element.prop("disabled", scope.externalDisabledValue);

								// revert content
								if (scope.replaceContent)
								{
									element.html(element.data("originalContent"));
								}
							});
					})
				}
			}
		});

})(angular);