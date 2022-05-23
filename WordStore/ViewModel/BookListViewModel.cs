using System.Collections.ObjectModel;
using System.Windows.Input;
using WordStore.Core.Model;
using WordStore.Core.Utility;
using WordStore.Data;
using WordStore.Manager;
using WordStore.Model.View;
using WordStore.Reader;

namespace WordStore.ViewModel {
	public class BookListViewModel : BaseViewModel {
		public ICommand AddBookCommand { get; set; }
		public ICommand OpenBookCommand { get; set; }
		public ICommand ImportBookCommand { get; set; }
		public ObservableCollection<BookItemView> Books { get; set; } = new ObservableCollection<BookItemView>();
		public IDialogManager DialogManager { get; }
		public IWordStorage WordStorage { get; }
		public INavigationManager NavigationManager { get; }
		public IEpubReader EpubReader { get; }
		public AppSettings AppSettings { get; }
		public AppConstants AppConstants { get; }

		public BookListViewModel(IDialogManager dialogManager, IWordStorage wordStorage,
				INavigationManager navigationManager, IEpubReader epubReader, AppSettings appSettings,
				AppConstants appConstants) {
			DialogManager = dialogManager;
			WordStorage = wordStorage;
			NavigationManager = navigationManager;
			EpubReader = epubReader;
			AppSettings = appSettings;
			AppConstants = appConstants;
			AddBookCommand = new Command(AddBook);
			OpenBookCommand = new Command<BookItemView>(OpenBook);
			ImportBookCommand = new Command(ImportBook);
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
					.Select(book => new BookItemView(book, book.Pages.Count, book.Image)));
		}
		protected virtual async void AddBook() {
			var name = await DialogManager.DisplayPromptAsync("Page", "Name");
			if (string.IsNullOrEmpty(name)) {
				return;
			}
			var book = CreateEmptyBook(name);
			await SaveBookAsync(book);
			OpenBookPage(book.Id);
			Books.Add(new BookItemView(book, 1));
		}
		protected virtual void OpenBook(BookItemView item) {
			OpenBookPage(item.BookItem.Id);
		}
		protected virtual async void ImportBook() {
			var fileName = await DialogManager.ShowFileDialogAsync(AppConstants.EpubFileType);
			if (string.IsNullOrEmpty(fileName)) {
				return;
			}
			using var stream = File.Open(fileName, FileMode.Open);
			var book = await EpubReader.ReadBook(stream, new BookReaderOptions() {
				MaxPageLineSize = AppSettings.MaxPageLineSize
			});
			await SaveBookAsync(book);
			OpenBookPage(book.Id);
		}
		protected virtual Book CreateEmptyBook(string name) {
			var book = new Book() {
				Id = Guid.NewGuid(),
				DisplayValue = name,
				PageNumber = 1,
				Pages = new List<BookPage>()
			};
			book.Pages.Add(CreateEmptyBookPage(book));
			return book;
		}
		protected virtual BookPage CreateEmptyBookPage(Book book) {
			var page = new BookPage() {
				Id = Guid.NewGuid(),
				BookId = book.Id,
				Content = string.Empty,
				Number = 1
			};
			return page;
		}
		protected virtual async Task SaveBookAsync(Book book) {
			await WordStorage.BookRepository.InsertAsync(book);
			//await WordStorage.BookPageRepository.InsertAsync(book.Pages.ToArray());
		}
		protected virtual void OpenBookPage(Guid bookId) {
			NavigationManager.GoToAsync("//Content/BookEditor", new Dictionary<string, object>() { 
				{ "bookId", bookId }
			});
		}
	}
}
