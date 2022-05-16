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

		public BookListViewModel(IDialogManager dialogManager, IWordStorage wordStorage) {
			AddBookCommand = new Command(AddBook);
			DialogManager = dialogManager;
			WordStorage = wordStorage;
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
			return WordStorage.BookRepository.GetCustomAsync(query => query.Take(30)
					.Select(book => new BookItemView(book, book.Pages.Count)));
		}
		protected virtual async void AddBook() {
			var name = await DialogManager.DisplayPromptAsync("Page", "Name");
			if (string.IsNullOrEmpty(name)) {
				return;
			}
			var book = await CreateBook(name);
			OpenBook(book);
		}
		protected virtual async Task<Book> CreateBook(string name) {
			var book = new Book() {
				Id = Guid.NewGuid(),
				DisplayValue = name
			};
			//var viewItem = new BookItemView(book, 1);
			//Books.Add(viewItem);
			await WordStorage.BookRepository.InsertAsync(book);
			return book;
		}
		protected virtual void OpenBook(Book book) {
			SendMessage("OpenBook", book);
		}
	}
}
