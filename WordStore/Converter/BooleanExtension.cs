namespace WordStore.Converter {
	public class BooleanExtension : IMarkupExtension<bool> {
		public bool Value { get; set; }
		public bool ProvideValue(IServiceProvider serviceProvider) {
			return Value;
		}
		object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) {
			return ((IMarkupExtension<bool>)this).ProvideValue(serviceProvider);
		}
	}
}
