namespace WordStore.Control {
	public class WrapLayout : FlexLayout {
		public WrapLayout() {
			Wrap = Microsoft.Maui.Layouts.FlexWrap.Wrap;
			JustifyContent = Microsoft.Maui.Layouts.FlexJustify.Start;
			AlignItems = Microsoft.Maui.Layouts.FlexAlignItems.Start;
			AlignContent = Microsoft.Maui.Layouts.FlexAlignContent.Start;
		}
	}
}
