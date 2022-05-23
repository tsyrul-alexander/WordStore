using WordStore.ViewModel;

namespace WordStore.View;

public partial class TranslationEditListView : Microsoft.Maui.Controls.ContentView {
	public static readonly BindableProperty WordIdProperty = BindableProperty.Create(nameof(WordId), typeof(Guid?), typeof(TranslationEditListView), propertyChanged: OnWordIdChanged);
	public Guid WordId {
		get { return (Guid)GetValue(WordIdProperty); }
		set { SetValue(WordIdProperty, value); }
	}

	public TranslationEditListView() {
		InitializeComponent();
	}

	protected static void OnWordIdChanged(BindableObject bindable, object oldValue, object newValue) {
		var view = (TranslationEditListView)bindable;
		var viewModel = (TranslationEditListViewModel)view.BindingContext;
		var wordId = newValue as Guid?;
		viewModel.WordId = wordId ?? Guid.Empty;
	}
}