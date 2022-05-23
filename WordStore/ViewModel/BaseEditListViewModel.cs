using System.Collections.ObjectModel;
using System.Windows.Input;
using WordStore.Core.Model.Db;
using WordStore.Data;
using WordStore.Manager;
using WordStore.Model.View;

namespace WordStore.ViewModel {
	public abstract class BaseEditListViewModel<T> : BaseViewModel where T : BaseLookupEntity, new() {
		public ObservableCollection<LookupItemView<T>> Items { get; set; } = new ObservableCollection<LookupItemView<T>>();
		public ICommand AddCommand { get; set; }
		public ICommand EditCommand { get; set; }
		public ICommand DeleteCommand { get; set; }
		public IDialogManager DialogManager { get; }
		public IRepository<T> Repository { get; }

		public BaseEditListViewModel(IDialogManager dialogManager, IRepository<T> repository) {
			AddCommand = new Command(Add);
			EditCommand = new Command<LookupItemView<T>>(Edit);
			DeleteCommand = new Command<LookupItemView<T>>(Delete);
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
		protected virtual async void Edit(LookupItemView<T> itemView) {
			var text = await DisplayPromptAsync("Enter:", itemView.Value);
			if (string.IsNullOrEmpty(text)) {
				return;
			}
			SetDisplayValueToItemView(itemView, text);
			await UpdateEntityDisplayValueAsync(itemView.Item);
		}
		protected virtual async void Delete(LookupItemView<T> itemView) {
			await DeleteEntityAsync(itemView.Item);
			DeleteEntityFromItems(itemView);
		}
		protected virtual Task<string> DisplayPromptAsync(string actionMessage, string defValue = null) {
			var header = GetHeader();
			return DialogManager.DisplayPromptAsync(header, actionMessage, initialValue: defValue);
		}
		protected virtual void AddEntityToItems(T entity) {
			Items.Add(GreateLookupItemView(entity));
		}
		protected virtual void DeleteEntityFromItems(LookupItemView<T> itemView) {
			Items.Remove(itemView);
		}
		protected virtual void SetDisplayValueToItemView(LookupItemView<T> itemView, string displayValue) {
			itemView.Value = displayValue;
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
