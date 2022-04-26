using System.Collections.ObjectModel;
using WordStore.Model.View;

namespace WordStore.View;

public partial class LookupEntityListView : Microsoft.Maui.Controls.ContentView {
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(ObservableCollection<ListItemView>), typeof(LookupEntityListView), propertyChanged: ItemsSourcePropertyChanged);

    public ObservableCollection<ListItemView> ItemsSource {
        get { return (ObservableCollection<ListItemView>)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }
    public LookupEntityListView() {
		InitializeComponent();
	}

    private static void ItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue) {
        var control = (LookupEntityListView)bindable;
        control.List.ItemsSource = newValue as ObservableCollection<ListItemView>;
    }
}