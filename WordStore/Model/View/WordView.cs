using System.Collections.ObjectModel;
using WordStore.Core.Model;
using WordStore.Core.Utility;
using WordStore.ViewModel;

namespace WordStore.Model.View {
	public class WordView : BaseModel {
		private string _value;

		public string Value { get => _value; set => SetPropertyValue(ref _value, value); }
		public ObservableCollection<ListItemView> Translations { get; set; } = new ObservableCollection<ListItemView>();
		public ObservableCollection<ListItemView> Examples { get; set; } = new ObservableCollection<ListItemView>();
		public WordView(Word word) {
			Value = word.DisplayValue;
			Translations.AddRange(word.Translations.Select(t => new ListItemView(t.DisplayValue, t)));
			Examples.AddRange(word.Examples.Select(e => new ListItemView(e.DisplayValue, e)));
		}
	}
}
