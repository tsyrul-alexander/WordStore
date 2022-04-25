using WordStore.Core.Model.Db;

namespace WordStore.Core.Model {
	public class BaseDbLookupEntity : BaseDbEntity {
		public string DisplayValue { get; set; }
		public BaseDbLookupEntity(Guid? id = null, string displayValue = "") : base(id) {
			DisplayValue = displayValue;
		}
	}
}
