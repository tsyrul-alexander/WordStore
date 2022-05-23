using WordStore.ViewModel;

namespace WordStore.View;

public partial class ExampleEditListView : ContentView {
	public static readonly BindableProperty WordIdProperty = BindableProperty.Create(nameof(WordId), typeof(Guid),
			typeof(TranslationEditListView), Guid.Empty, propertyChanged: OnWordIdChanged);
	public static readonly BindableProperty SentenceProperty = BindableProperty.Create(nameof(Sentence), typeof(string),
			typeof(ExampleEditListView), default, propertyChanged: OnSentenceChanged);

	public Guid WordId {
		get { return (Guid)GetValue(WordIdProperty); }
		set { SetValue(WordIdProperty, value); }
	}
	public string Sentence {
		get { return (string)GetValue(SentenceProperty); }
		set { SetValue(SentenceProperty, value); }
	}

	public ExampleEditListView() {
		InitializeComponent();
	}

	protected static void OnWordIdChanged(BindableObject bindable, object oldValue, object newValue) {
		var view = (ExampleEditListView)bindable;
		var viewModel = (ExampleEditListViewModel)view.BindingContext;
		var wordId = newValue as Guid?;
		viewModel.WordId = wordId ?? Guid.Empty;
	}
	private static void OnSentenceChanged(BindableObject bindable, object oldValue, object newValue) {
		var view = (ExampleEditListView)bindable;
		var viewModel = (ExampleEditListViewModel)view.BindingContext;
		viewModel.CurrentSentence = newValue as string;
	}
}