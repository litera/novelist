(function (angular, moment) {

	angular
		.main
		.constant("moment", moment)
		.filter("moment", ["moment", function (moment) {
			return function (value) {
				if (moment(value).isValid())
				{
					// convert to Moment first
					value = moment(value);

					if (value.isBefore(moment().subtract(1, "d")))
					{
						return value.format("D MMMM");
					}
					return value.fromNow();
				}
				return value;
			};
		}]);

})(angular, moment);