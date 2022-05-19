using System.Windows.Input;
using WordStore.Core.Model;
using WordStore.Data;
using WordStore.Manager;

namespace WordStore.ViewModel {
	public class BookEditorViewModel : BaseViewModel, IQueryAttributable {
		public IWordStorage WordStorage { get; }
		public IBookPaginationManager PaginationManager { get; }
		public ICommand SetCurrentPageCommand { get; set; }
		public ICommand AddPageCommand { get; set; }
		public ICommand SavePageCommand { get; set; }

		public BookEditorViewModel(IWordStorage wordStorage, IBookPaginationManager paginationManager) {
			WordStorage = wordStorage;
			PaginationManager = paginationManager;
			SetCurrentPageCommand = new Command<int>(SetCurrentPage);
			AddPageCommand = new Command(AddPage);
			SavePageCommand = new Command(SavePage);
		}

		protected virtual async void SavePage() {
			await WordStorage.BookPageRepository.UpdateAsync(PaginationManager.CurrentPage);
		}
		protected virtual async void AddPage() {
			var page = CreateNewEmptyPage();
			await WordStorage.BookPageRepository.InsertAsync(page);
			await PaginationManager.ReInitialize();
		}
		protected virtual BookPage CreateNewEmptyPage() {
			return new BookPage {
				Id = Guid.NewGuid(),
				Number = PaginationManager.PageCount + 1,
				BookId = PaginationManager.Book.Id
			};
		}
		protected virtual async void SetCurrentPage(int pageNumber) {
			await PaginationManager.SetCurrentPage(pageNumber);
		}
		protected virtual async void OpenBook(Guid bookId) {
			await PaginationManager.Initialize(bookId);
		}
		public void ApplyQueryAttributes(IDictionary<string, object> query) {
			var pageId = (Guid)query["bookId"];
			OpenBook(pageId);
		}
	}
}
