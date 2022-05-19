namespace WordStore.Converter {
	public class BaseTypeExtension<T> : IMarkupExtension<T> {
		public T Value { get; set; }
		public T ProvideValue(IServiceProvider serviceProvider) {
			return Value;
		}
		object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) {
			return ((IMarkupExtension<T>)this).ProvideValue(serviceProvider);
		}
	}
}
