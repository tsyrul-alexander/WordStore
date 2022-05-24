using System.Linq.Expressions;
using System.Windows.Input;
using WordStore.Core.Model;
using WordStore.Data;
using WordStore.Manager;

namespace WordStore.ViewModel {
	public class ExampleEditListViewModel : WordItemEditListViewModel<WordExample> {
		private string currentSentence;
		public string CurrentSentence { 
			get => currentSentence; 
			set => SetPropertyValue(ref currentSentence, value);
		}
		public ICommand AddCurrentSentenceCommand { get; set; }

		public ExampleEditListViewModel(IDialogManager dialogManager, IRepository<WordExample> repository) :
				base(dialogManager, repository) {
			AddCurrentSentenceCommand = new Command(AddCurrentSentence);
		}

		protected virtual async void AddCurrentSentence() {
			await Add(CurrentSentence);
			CurrentSentence = null;
		}
		protected override WordExample CreateEntity(string name) {
			var entity = base.CreateEntity(name);
			entity.WordId = WordId;
			return entity;
		}
		protected override string GetHeader() {
			return "Examples";
		}
		protected override Expression<Func<WordExample, bool>> GetItemsFilter(Guid wordId) {
			return ex => ex.WordId == wordId;
		}
	}
}
