using System.Collections;
using System.Windows.Input;
using WordStore.Model.View;

namespace WordStore.Control {
	public class WrapLayout : FlexLayout {
		protected readonly Color DefaultChildTestColor = Color.Parse("#606060");
		public static readonly BindableProperty ItemsSourceProperty =
				BindableProperty.CreateAttached("ItemsSource", typeof(IEnumerable<WordItemView>), typeof(WrapLayout), default(IEnumerable),
					propertyChanged: SetItemsSource);
		public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(WrapLayout),
					propertyChanged: SetCommand);

		public IEnumerable<WordItemView> ItemsSource {
			get { return (IEnumerable<WordItemView>)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}
		public ICommand Command {
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public WrapLayout() {
			Wrap = Microsoft.Maui.Layouts.FlexWrap.Wrap;
			JustifyContent = Microsoft.Maui.Layouts.FlexJustify.Start;
			AlignItems = Microsoft.Maui.Layouts.FlexAlignItems.Start;
			AlignContent = Microsoft.Maui.Layouts.FlexAlignContent.Start;
		}

		protected static void SetItemsSource(BindableObject bindable, object oldValue, object newValue) {
			var view = (WrapLayout)bindable;
			var items = newValue as IEnumerable<WordItemView>;
			view.CreateChildren(items);
		}
		protected static void SetCommand(BindableObject bindable, object oldValue, object newValue) {
			var command = (ICommand)newValue;
			var view = (WrapLayout)bindable;
			foreach (var child in view.Children) {
				if (child is Button button) {
					button.Command = command;
				}
			}
		}
		protected virtual void CreateChildren(IEnumerable<WordItemView> items) {
			Clear();
			if (items == null) {
				return;
			}
			foreach (var item in items) {
				Add(CreateChildView(item));
			}
		}
		protected virtual IView CreateChildView(WordItemView context) {
			if (context.Type == WordItemViewType.Word) {
				return CreateWordItemView(context);
			} else {
				return CreateCharItemView(context);
			}
		}
		protected virtual IView CreateCharItemView(WordItemView context) {
			return new Label {
				Text = context.Value,
				Margin = new Thickness(0, 1, 0, 1),
				Padding = new Thickness(1),
				BackgroundColor = Colors.Transparent,
				TextColor = DefaultChildTestColor
			};
		}
		protected virtual IView CreateWordItemView(WordItemView context) {
			var textColor = context.WordItem == null ? DefaultChildTestColor : Color.Parse("#CCCC00");
			return new Button {
				Text = context.Value,
				Margin = new Thickness(1),
				TextColor = textColor,
				Padding = new Thickness(1),
				BackgroundColor = Colors.Transparent,
				Command = Command,
				CommandParameter = context
			};
		}
	}
}
