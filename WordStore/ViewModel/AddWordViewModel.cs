using System.Windows.Input;
using WordStore.Model.View;

namespace WordStore.ViewModel {
	public class AddWordViewModel : BaseViewModel {
		private AddWordView addWordView;

		public ICommand CloseCommand { get; set; }
		public AddWordView AddWordView { get => addWordView; set => SetPropertyValue(ref addWordView, value); }

		public AddWordViewModel() {
			CloseCommand = new Command(Close);
		}

		protected override void SubscribeMessages() {
			base.SubscribeMessages();
			SubscribeMessage<AddWordView>("AddWord", AddWord);
		}

		private void Close(object obj) {
			AddWordView = null;
		}
		private void AddWord(AddWordView addWordView) {
			AddWordView = addWordView;
		}
	}
}
