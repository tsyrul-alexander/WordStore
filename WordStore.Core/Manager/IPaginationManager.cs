namespace WordStore.Core.Manager {
	public interface IPaginationManager {
		event Action<IPaginationManager, EventArgs> Changed;
		int CurrentPage { get; }
		int PageLineSize { get; set; }
		string[] GetCurrentLines();
		void NextPage();
		void PreviousPage();
		void SetContent(string[] content);
	}
}