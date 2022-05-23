using System.Globalization;

namespace WordStore.Converter {
	internal class PositiveNumberConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			var number = (double)value;
			if (number < 0) {
				return 0;
			}
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
