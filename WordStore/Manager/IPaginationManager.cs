
namespace WordStore.Manager {
	public interface IPaginationManager {
		event Action<PaginationManager, EventArgs> Changed;
		int CurrentPage { get; }
		int PageLineSize { get; set; }
		string[] GetCurrentLines();
		void NextPage();
		void PreviousPage();
		void SetContent(string[] content);
	}
}