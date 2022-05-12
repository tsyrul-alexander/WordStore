using System.Windows.Input;
using WordStore.Manager;

namespace WordStore.ViewModel {
	public abstract class BaseViewModel : BaseModel, IDisposable {
		public ICommand BackNavigationCommand { get; set; }
		public IServiceProvider ServiceProvider { get; set; }
		public BaseViewModel() {
			BackNavigationCommand = new Command(BackNavigation);
		}

		public virtual void Initialize(IServiceProvider serviceProvider) {
			ServiceProvider = serviceProvider;
			SubscribeMessages();
		}
		protected virtual void SubscribeMessages() { }
		protected virtual void UnsubscribeMessages() { }
		protected virtual void SendMessage<TSender>(string message, TSender sender) where TSender : class {
			MessagingCenter.Send(sender, message);
		}
		protected virtual void SubscribeMessage<TSender>(string message, Action<TSender> action) 
				where TSender : class {
			MessagingCenter.Subscribe(this, message, action);
		}
		protected virtual void UnsubscribeMessage<TSender>(string message)
				where TSender : class {
			MessagingCenter.Unsubscribe<TSender>(this, message);
		}
		protected virtual void BackNavigation() {
			ServiceProvider.GetService<INavigationManager>().GoBack();
		}
		public virtual void Dispose() {
			UnsubscribeMessages();
		}
	}
}