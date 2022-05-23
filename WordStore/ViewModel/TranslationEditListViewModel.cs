using WordStore.Core.Model;
using WordStore.Data;
using WordStore.Manager;

namespace WordStore.ViewModel {
	public class TranslationEditListViewModel : WordItemEditListViewModel<WordTranslation> {
		public TranslationEditListViewModel(IDialogManager dialogManager, IRepository<WordTranslation> repository) : 
				base(dialogManager, repository) {}

		protected override string GetHeader() {
			return "Translations";
		}
		protected override Task<List<WordTranslation>> GetItems(Guid wordId) {
			return Repository.GetListAsync(query => query.Where(ex => ex.WordId == wordId).LookupOrderBy());
		}
	}
}
