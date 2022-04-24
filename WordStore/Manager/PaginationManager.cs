namespace WordStore.Manager {
	public class PaginationManager : IPaginationManager {
		private int pageLineSize = 30;
		private int currentPage = 1;
		private string[] content;

		public event Action<PaginationManager, EventArgs> Changed;
		public int PageLineSize {
			get => pageLineSize;
			set {
				pageLineSize = value;
				OnChanged();
			}
		}
		public int CurrentPage {
			get => currentPage;
			private set {
				currentPage = value;
				OnChanged();
			}
		}
		protected string[] Content {
			get => content;
			set {
				content = value;
				OnChanged();
			}
		}
		public virtual void SetContent(string[] content) {
			Content = content;
		}
		public virtual void NextPage() {
			CurrentPage++;
		}
		public virtual void PreviousPage() {
			CurrentPage--;
		}
		public virtual string[] GetCurrentLines() {
			var startIndex = CurrentPage - 1;
			var endIndex = CurrentPage * PageLineSize;
			if (endIndex >= Content.Length) {
				return Content[startIndex..];
			}
			return Content[startIndex..endIndex];
		}
		protected virtual void OnChanged() {
			Changed?.Invoke(this, EventArgs.Empty);
		}
	}
}
