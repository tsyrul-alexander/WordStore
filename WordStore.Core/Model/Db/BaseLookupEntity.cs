namespace WordStore.Core.Model.Db {
	public class BaseLookupEntity : BaseEntity {
		private string displayValue;
		public string DisplayValue { get => displayValue; set => SetPropertyValue(ref displayValue, value); }
		public BaseLookupEntity(Guid? id = null, string displayValue = "") : base(id) {
			DisplayValue = displayValue;
		}
	}
}
