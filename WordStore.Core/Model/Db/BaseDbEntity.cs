namespace WordStore.Core.Model.Db {
	public class BaseDbEntity {
		public Guid Id { get; set; }

		public BaseDbEntity(Guid? id = null) {
			Id = id ?? Guid.Empty;
		}
	}
}
