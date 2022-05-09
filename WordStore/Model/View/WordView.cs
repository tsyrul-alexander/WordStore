using System.Collections.ObjectModel;
using WordStore.Core.Model;
using WordStore.Core.Utility;
using WordStore.ViewModel;

namespace WordStore.Model.View {
	public class WordView : BaseModel {
		private string _value;

		public string Value { get => _value; set => SetPropertyValue(ref _value, value); }
		public ObservableCollection<ListItemView<WordTranslation>> Translations { get; set; } = new ObservableCollection<ListItemView<WordTranslation>>();
		public ObservableCollection<ListItemView<WordExample>> Examples { get; set; } = new ObservableCollection<ListItemView<WordExample>>();
		public WordView(Word word) {
			Value = word.DisplayValue;
			Translations.AddRange(word.Translations.Select(t => new ListItemView<WordTranslation>(t.DisplayValue, t)));
			Examples.AddRange(word.Examples.Select(e => new ListItemView<WordExample>(e.DisplayValue, e)));
		}
	}
}
