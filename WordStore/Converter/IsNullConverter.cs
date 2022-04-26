using System.Globalization;

namespace WordStore.Converter {
	public class IsNullConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			var isInvertValue = parameter is bool boolParam && boolParam == true;
			var isNull = value == null;
			return isInvertValue ? !isNull : isNull;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
