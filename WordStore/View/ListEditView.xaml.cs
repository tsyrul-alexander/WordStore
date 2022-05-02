using System.Collections.ObjectModel;
using WordStore.Model.View;

namespace WordStore.View;

public partial class ListEditView : Microsoft.Maui.Controls.ContentView {
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(ObservableCollection<ListItemView>), typeof(ListEditView), propertyChanged: ItemsSourcePropertyChanged);

    public ObservableCollection<ListItemView> ItemsSource {
        get { return (ObservableCollection<ListItemView>)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }
    public ListEditView() {
		InitializeComponent();
	}

    private static void ItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue) {
        var control = (ListEditView)bindable;
        control.List.ItemsSource = newValue as ObservableCollection<ListItemView>;
    }
}