using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WordStore.Core.Model {
	public abstract class BaseModel : INotifyPropertyChanged {
		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual bool SetPropertyValue<T>(ref T setProperty, T value, Action<T>? changed = null,
				[CallerMemberName] string propertyName = "") {
			if (setProperty == null && value == null || setProperty != null && setProperty.Equals(value)) {
				return false;
			}
			setProperty = value;
			OnPropertyChanged(propertyName);
			changed?.Invoke(value);
			return true;
		}
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
