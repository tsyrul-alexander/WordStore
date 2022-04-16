namespace WordStore.ViewModel {
	public abstract class BaseViewModel : BaseModel, IDisposable {
		public void Initialize() {
			SubscribeMessages();
		}
		protected virtual void SubscribeMessages() {
			
		}
		protected virtual void UnsubscribeMessages() {

		}
		protected virtual void SendMessage<TSender>(TSender sender, string message) where TSender : class {
			MessagingCenter.Send(sender, message);
		}
		protected virtual void SubscribeMessage<TSender>(TSender sender, string message, Action<TSender> action) 
				where TSender : class {
			MessagingCenter.Subscribe(this, message, action);
		}
		protected virtual void UnsubscribeMessage<TSender>(TSender sender, string message)
				where TSender : class {
			MessagingCenter.Unsubscribe<TSender>(this, message);
		}
		public virtual void Dispose() {
			UnsubscribeMessages();
		}
	}
}