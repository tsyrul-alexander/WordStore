namespace WordStore.Model.EventArgs {
	public class ShellTabChangedEventArgs : System.EventArgs {
		public string Title { get; set; }
		public Type ViewModelClassType { get; set; }
	}
}
