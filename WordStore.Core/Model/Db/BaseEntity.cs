namespace WordStore.Core.Model.Db {
	public class BaseEntity : BaseModel {
		private Guid id;
		public Guid Id { get => id; set => SetPropertyValue(ref id, value); }

		public BaseEntity(Guid? id = null) {
			Id = id ?? Guid.Empty;
		}
	}
}
