using System.Runtime.CompilerServices;
using WordStore.Core.Model;
using WordStore.Core.Utility;
using WordStore.Data;
using WordStore.ViewModel;

namespace WordStore.Manager {
	public class BookPaginationManager : BaseModel, IBookPaginationManager {
		private int lineCount;
		private BookPage currentPage;
		private Book book;
		private int pageCount;
		private string _value;

		public Book Book { get => book; private set => SetPropertyValue(ref book, value); }
		public BookPage CurrentPage { 
			get => currentPage; private 
			set => SetPropertyValue(ref currentPage, value, OnCurrentPageChanged); 
		}
		public string Value { get => _value; set => SetPropertyValue(ref _value, value, OnValueChanged); }
		public int LineCount { get => lineCount; set => SetPropertyValue(ref lineCount, value); }
		public int PageCount { get => pageCount; set => SetPropertyValue(ref pageCount, value); }
		public IWordStorage WordStorage { get; }

		public BookPaginationManager(IWordStorage wordStorage) {
			WordStorage = wordStorage;
		}

		public async Task Initialize(Guid bookId) {
			await SetBook(bookId);
			await SetCurrentPage(Book.PageNumber);
		}
		public async Task ReInitialize() {
			await Initialize(Book.Id);
		}
		public virtual async Task NextPage() {
			await SetCurrentPage(Book.PageNumber + 1);
		}
		public virtual async Task PreviousPage() {
			await SetCurrentPage(Book.PageNumber - 1);
		}
		public virtual async Task SetCurrentPage(int number) {
			if (number > PageCount || number < 1) {
				return;
			}
			CurrentPage = await GetBookPage(number);
		}
		protected virtual void OnValueChanged(string value) {
			CurrentPage.Value = value;
			LineCount = value?.GetLineCount() ?? 0;
		}
		protected virtual void OnCurrentPageChanged(BookPage page) {
			Value = CurrentPage?.Value;
		}
		protected virtual async Task SetBook(Guid bookId) {
			var bookInfo = await WordStorage.BookRepository.GetAsync(query => query
					.Where(book => book.Id == bookId)
					.Select(book => new { book, pageCount = book.Pages.Count }));
			Book = bookInfo.book;
			PageCount = bookInfo.pageCount;
		}
		protected virtual async Task<BookPage> GetBookPage(int number) {
			return await WordStorage.BookPageRepository.GetAsync(query =>
					query.Where(page => page.BookId == Book.Id && page.Number == number));
		}
	}
}
