namespace WordStore.Model {
	public class ActivityIndicatorDialogInfo {
		public Action Hide { get; private set; }
		public ActivityIndicatorDialogInfo(Action hide) {
			Hide = hide;
		}
	}
}
