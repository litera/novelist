(function (angular) {

	"use strict";

	angular
		.main
		.directive("elastic", function () {
				return {
					restrict: 'A',
					link: function ($scope, element) {

						var initialHeight = element[0].style.height;
						
						element.on("input change", resize);
						setTimeout(resize, 1);

						function resize() {
							element[0].style.height = initialHeight;
							element[0].style.height = element[0].scrollHeight + "px";
						}
					}
				};
			}
		);
})(angular);