using System.ComponentModel;
using WordStore.Core.Model;

namespace WordStore.Manager {
	public interface IBookPaginationManager : INotifyPropertyChanged {
		Book Book { get; }
		BookPage CurrentPage { get; }
		string Value { get; set; }
		int LineCount { get; set; }
		int PageCount { get; set; }
		Task Initialize(Guid bookId);
		Task ReInitialize();
		Task NextPage();
		Task PreviousPage();
		Task SetCurrentPage(int number);
	}
}