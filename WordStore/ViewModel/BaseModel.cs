using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WordStore.ViewModel {
    public abstract class BaseModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetPropertyValue<T>(ref T setProperty, T value, [CallerMemberName] string propertyName = "") {
            if (setProperty.Equals(value)) {
                return false;
            }
            setProperty = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
