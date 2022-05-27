using Microsoft.Maui.Controls.Shapes;
using System.Collections.Specialized;
using System.Windows.Input;
using WordStore.Model.View;

namespace WordStore.Control {
	public class LineWordLayout : StackLayout {
		public static readonly BindableProperty ItemsSourceProperty =
				BindableProperty.CreateAttached(nameof(ItemsSource), typeof(IEnumerable<LineWordView>),
					typeof(LineWordLayout), default(IEnumerable<LineWordView>),	propertyChanged: SetItemsSource);
		public static readonly BindableProperty ItemCommandProperty = BindableProperty.Create(nameof(ItemCommand),
				typeof(ICommand), typeof(LineWordLayout), propertyChanged: SetItemCommand);
		public static readonly BindableProperty IsLoadProperty = BindableProperty.Create(nameof(IsLoad), typeof(bool), typeof(LineWordLayout), default);

		public IEnumerable<LineWordView> ItemsSource {
			get { return (IEnumerable<LineWordView>)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}
		public ICommand ItemCommand {
			get { return (ICommand)GetValue(ItemCommandProperty); }
			set { SetValue(ItemCommandProperty, value); }
		}
		public bool IsLoad {
			get { return (bool)GetValue(IsLoadProperty); }
			set { SetValue(IsLoadProperty, value); }
		}

		protected static void SetItemCommand(BindableObject bindable, object oldValue, object newValue) {
			var command = (ICommand)newValue;
			var view = (LineWordLayout)bindable;
			view.IsLoad = true;
			view.SetItemCommand(command);
			view.IsLoad = false;
		}
		protected static void SetItemsSource(BindableObject bindable, object oldValue, object newValue) {
			var view = (LineWordLayout)bindable;
			var oldItems = newValue as IEnumerable<LineWordView>;
			var items = newValue as IEnumerable<LineWordView>;
			view.UnsubscrubeCollection(oldItems);
			view.SubscrubeCollection(items);
			view.IsLoad = true;
			view.Clear();
			view.CreateChildren(items);
			view.IsLoad = false;
		}
		protected virtual void SubscrubeCollection(IEnumerable<LineWordView> items) {
			if (items != null && items is INotifyCollectionChanged collection) {
				collection.CollectionChanged += CollectionChanged;
			}
		}
		protected virtual void UnsubscrubeCollection(IEnumerable<LineWordView> items) {
			if (items != null && items is INotifyCollectionChanged collection) {
				collection.CollectionChanged -= CollectionChanged;
			}
		}
		private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			var items = e.NewItems?.Cast<LineWordView>();
			if (e.Action == NotifyCollectionChangedAction.Add) {
				CreateChildren(items);
			} else if (e.Action == NotifyCollectionChangedAction.Reset) {
				Clear();
			}
		}
		protected virtual void SetItemCommand(ICommand itemCommand) {
			foreach (var child in Children) {
				var border = (Border)child;
				var grid = (Grid)border.Content;
				var wordLayout = (WordLayout)grid.Children.First(child => child is WordLayout);
				wordLayout.Command = itemCommand;
			}
		}
		protected virtual void CreateChildren(IEnumerable<LineWordView> items) {
			if (items == null) {
				return;
			}
			foreach (var item in items) {
				Add(CreateChildView(item));
			}
		}
		protected virtual IView CreateChildView(LineWordView context) {
			return CreateBorderView(context);
		}
		protected virtual IView CreateBorderView(LineWordView context) {
			return new Border {
				StrokeShape = new RoundRectangle {
					CornerRadius = 5
				},
				Content = (Microsoft.Maui.Controls.View)CreateGridView(context)
			};
		}
		protected virtual IView CreateGridView(LineWordView context) {
			var grid = new Grid {
				Margin = 5,
				ColumnDefinitions = new ColumnDefinitionCollection(
					new ColumnDefinition(GridLength.Auto),
					new ColumnDefinition(GridLength.Star),
					new ColumnDefinition(5)
				)
			};
			grid.Add(CreateNumberView(context), 0);
			grid.Add(CreateWordsView(context), 1);
			return grid;
		}
		protected virtual IView CreateNumberView(LineWordView context) {
			return new Label {
				Text = context.Number.ToString(),
				VerticalOptions = LayoutOptions.Center,
				Margin = 2
			};
		}
		protected virtual IView CreateWordsView(LineWordView context) {
			return new WordLayout {
				ItemsSource = context.Words,
				Command = ItemCommand,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Fill
			};
		}
	}
}
