using System.Collections.ObjectModel;
using System.Windows.Input;
using WordStore.Core.Model;
using WordStore.Core.Utility;
using WordStore.Data;
using WordStore.Manager;
using WordStore.Model.View;

namespace WordStore.ViewModel {
	public class BookListViewModel : BaseViewModel {
		public ICommand AddBookCommand { get; set; }
		public ObservableCollection<BookItemView> Books { get; set; } = new ObservableCollection<BookItemView>();
		public IDialogManager DialogManager { get; }
		public IWordStorage WordStorage { get; }
		public INavigationManager NavigationManager { get; }

		public BookListViewModel(IDialogManager dialogManager, IWordStorage wordStorage, INavigationManager navigationManager) {
			AddBookCommand = new Command(AddBook);
			DialogManager = dialogManager;
			WordStorage = wordStorage;
			NavigationManager = navigationManager;
		}

		public override void Initialize(IServiceProvider serviceProvider) {
			base.Initialize(serviceProvider);
			InitializeBooks();
		}
		protected virtual async void InitializeBooks() {
			Books.Clear();
			var bookItems = await GetBooks();
			Books.AddRange(bookItems);
		}
		protected virtual Task<List<BookItemView>> GetBooks() {
			return WordStorage.BookRepository.GetListAsync(query => query.Take(30)
					.Select(book => new BookItemView(book, book.Pages.Count)));
		}
		protected virtual async void AddBook() {
			var name = await DialogManager.DisplayPromptAsync("Page", "Name");
			if (string.IsNullOrEmpty(name)) {
				return;
			}
			var book = await CreateBook(name);
			await CreateBookPage(book);
			OpenBookPage(book);
			Books.Add(new BookItemView(book, 1));
		}
		protected virtual async Task<Book> CreateBook(string name) {
			var book = new Book() {
				Id = Guid.NewGuid(),
				DisplayValue = name,
				PageNumber = 1
			};
			//var viewItem = new BookItemView(book, 1);
			//Books.Add(viewItem);
			await WordStorage.BookRepository.InsertAsync(book);
			return book;
		}
		protected virtual async Task<BookPage> CreateBookPage(Book book) {
			var page = new BookPage() {
				Id = Guid.NewGuid(),
				BookId = book.Id,
				Value = string.Empty,
				Number = 1
			};
			await WordStorage.BookPageRepository.InsertAsync(page);
			return page;
		}
		protected virtual void OpenBookPage(Book book) {
			NavigationManager.GoToAsync("//Content/BookEditor", new Dictionary<string, object>() { 
				{ "bookId", book.Id }
			});
		}
	}
}
