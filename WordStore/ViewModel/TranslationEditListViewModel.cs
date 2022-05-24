using System.Linq.Expressions;
using WordStore.Core.Model;
using WordStore.Data;
using WordStore.Manager;

namespace WordStore.ViewModel {
	public class TranslationEditListViewModel : WordItemEditListViewModel<WordTranslation> {
		public TranslationEditListViewModel(IDialogManager dialogManager, IRepository<WordTranslation> repository) :
				base(dialogManager, repository) { }

		protected override WordTranslation CreateEntity(string name) {
			var entity = base.CreateEntity(name);
			entity.WordId = WordId;
			return entity;
		}
		protected override string GetHeader() {
			return "Translations";
		}
		protected override Expression<Func<WordTranslation, bool>> GetItemsFilter(Guid wordId) {
			return ex => ex.WordId == wordId;
		}
	}
}
