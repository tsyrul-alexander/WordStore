using System.Globalization;

namespace WordStore.Converter {
	internal class IterateNumberConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			var count = (int)value;
			return Enumerable.Range(1, count);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
