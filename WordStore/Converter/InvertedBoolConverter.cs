using System.Globalization;

namespace WordStore.Converter {
	internal class InvertBoolConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			var boolValue = (bool)value;
			return !boolValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
