using WordStore.Core.Model;

namespace WordStore.Reader {
	public interface IBookReader {
		Task<Book> ReadBook(Stream stream, BookReaderOptions options);
	}
}
