using WordStore.Core.Model;

namespace WordStore.ViewModel {
	public abstract class BaseViewModel : BaseModel, IDisposable {
		private bool isActive;
		private bool isInitialized;
		public bool IsInitialized { get => isInitialized; private set => isInitialized = value; }
		public bool IsActive { get => isActive; set => SetPropertyValue(ref isActive, value, OnActiveChange); }
		public virtual void Initialize() {
			SubscribeMessages();
			IsInitialized = true;
		}
		protected virtual void OnActiveChange(bool isActive) { }
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
		public virtual void Dispose() {
			UnsubscribeMessages();
		}
	}
}