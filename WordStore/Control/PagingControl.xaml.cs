namespace WordStore.Control;

public partial class PagingControl : ContentView {
	public static readonly BindableProperty NumberProperty = BindableProperty.Create(nameof(Number), typeof(int),
			typeof(PagingControl), default, BindingMode.TwoWay,
			propertyChanged: OnNumberChanged);
	public static readonly BindableProperty CountProperty = BindableProperty.Create(nameof(Count), typeof(int),
			typeof(PagingControl), default);
	public int Count {
		get { return (int)GetValue(CountProperty); }
		set { SetValue(CountProperty, value); }
	}
	public int Number {
		get { return (int)GetValue(NumberProperty); }
		set { SetValue(NumberProperty, value); }
	}

	public PagingControl() {
		InitializeComponent();
	}

	private static void OnNumberChanged(BindableObject bindable, object oldValue, object newValue) {
		var view = (PagingControl)bindable;
		view.NumberLabel.Text = ((int)newValue).ToString();
	}
	private void PreviousButton_Clicked(object sender, EventArgs e) {
		SetNumber(Number - 1);
	}

	private void NextButton_Clicked(object sender, EventArgs e) {
		SetNumber(Number + 1);
	}
	protected virtual bool GetIsValidNumber(int number) {
		if (number > Count || number < 1) {
			return false;
		}
		return true;
	}
	protected virtual void SetNumber(int number) {
		if (!GetIsValidNumber(number)) {
			return;
		}
		Number = number;
	}
}