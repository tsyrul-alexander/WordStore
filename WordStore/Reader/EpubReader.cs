using HtmlAgilityPack;
using VersOne.Epub;
using WordStore.Core.Model;
using WordStore.Core.Utility;

namespace WordStore.Reader {
	internal class EpubReader : IEpubReader {
		public async Task<Book> ReadBook(Stream stream, BookReaderOptions options) {
			var book = CreateBook();
			var epubBook = await VersOne.Epub.EpubReader.ReadBookAsync(stream);
			SetBookMetadata(book, epubBook);
			SetBookPages(book, epubBook, options);
			return book;
		}
		protected virtual Book CreateBook() {
			return new Book {
				Id = Guid.NewGuid(),
				Pages = new List<BookPage>()
			};
		}
		protected virtual BookPage CreateBookPage(Book book, IEnumerable<string> content) {
			return new BookPage {
				Id = Guid.NewGuid(),
				Content = string.Join(Environment.NewLine, content),
				Number = book.Pages.Count + 1
			};
		}
		protected virtual void SetBookMetadata(Book book, EpubBook epubBook) {
			book.DisplayValue = epubBook.Title;
			book.Image = epubBook.CoverImage;
		}
		protected virtual void SetBookPages(Book book, EpubBook epubBook, BookReaderOptions options) {
			foreach (var fileContent in epubBook.ReadingOrder) {
				var pageContent = GetBodyTextContent(fileContent).ToList();
				if (pageContent.Count == 0) {
					continue;
				}
				var pagibleContents = pageContent.Split(options.MaxPageLineSize);
				foreach (var pagibleContent in pagibleContents) {
					var bookPage = CreateBookPage(book, pagibleContent);
					book.Pages.Add(bookPage);
				}
			}
		}
		protected virtual IEnumerable<string> GetBodyTextContent(EpubTextContentFile contentFile) {
			var htmlContent = contentFile.Content;
			var document = new HtmlDocument();
			document.LoadHtml(htmlContent);
			var bodyNode = document.DocumentNode.SelectSingleNode("//body");
			foreach (var node in bodyNode.DescendantsAndSelf()) {
				if (!node.HasChildNodes) {
					string innerText = node.InnerText.Trim();
					if (!string.IsNullOrWhiteSpace(innerText)) {
						yield return System.Net.WebUtility.HtmlDecode(innerText.Replace("\n", "").Replace("\r", ""));
					}
				}
			}
		}
	}
}
