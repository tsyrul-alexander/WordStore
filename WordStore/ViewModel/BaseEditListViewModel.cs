using System.Collections.ObjectModel;
using System.Windows.Input;
using WordStore.Core.Model;
using WordStore.Data;
using WordStore.Manager;

namespace WordStore.ViewModel {
	public abstract class BaseEditListViewModel<T> : BaseViewModel where T : BaseLookupEntity, new() {
		public ObservableCollection<T> Items { get; set; } = new ObservableCollection<T>();
		public ICommand AddCommand { get; set; }
		public ICommand EditCommand { get; set; }
		public ICommand DeleteCommand { get; set; }
		public IDialogManager DialogManager { get; }
		public IRepository<T> Repository { get; }

		public BaseEditListViewModel(IDialogManager dialogManager, IRepository<T> repository) {
			AddCommand = new Command(Add);
			EditCommand = new Command<T>(Edit);
			DeleteCommand = new Command<T>(Delete);
			DialogManager = dialogManager;
			Repository = repository;
		}
		protected virtual async void Add() {
			var text = await DisplayPromptAsync("Enter:");
			if (string.IsNullOrEmpty(text)) {
				return;
			}
			var entity = CreateEntity(text);
			await InsertEntityAsync(entity);
			AddEntityToItems(entity);
		}
		protected virtual async void Edit(T item) {
			var text = await DisplayPromptAsync("Enter:", item.DisplayValue);
			if (string.IsNullOrEmpty(text)) {
				return;
			}
			SetDisplayValueToItemView(item, text);
			await UpdateEntityDisplayValueAsync(item);
		}
		protected virtual async void Delete(T item) {
			await DeleteEntityAsync(item);
			DeleteEntityFromItems(item);
		}
		protected virtual Task<string> DisplayPromptAsync(string actionMessage, string defValue = null) {
			var header = GetHeader();
			return DialogManager.DisplayPromptAsync(header, actionMessage, initialValue: defValue);
		}
		protected virtual void AddEntityToItems(T entity) {
			Items.Add(entity);
		}
		protected virtual void DeleteEntityFromItems(T item) {
			Items.Remove(item);
		}
		protected virtual void SetDisplayValueToItemView(T item, string displayValue) {
			item.DisplayValue = displayValue;
		}
		protected virtual Task InsertEntityAsync(T entity) {
			return Repository.InsertAsync(entity);
		}
		protected virtual Task UpdateEntityDisplayValueAsync(T entity) {
			return Repository.UpdateAsync(entity, nameof(entity.DisplayValue));
		}
		protected virtual Task DeleteEntityAsync(T entity) {
			return Repository.DeleteAsync(entity.Id);
		}
		protected virtual T CreateEntity(string name) {
			return new T() { Id = Guid.NewGuid(), DisplayValue = name };
		}
		protected abstract string GetHeader();
	}
}
