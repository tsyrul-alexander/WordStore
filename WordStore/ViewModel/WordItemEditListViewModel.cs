using System.Linq.Expressions;
using WordStore.Core.Model;
using WordStore.Core.Utility;
using WordStore.Data;
using WordStore.Manager;

namespace WordStore.ViewModel {
	public abstract class WordItemEditListViewModel<T> : BaseEditListViewModel<T> where T : BaseLookupEntity, new() {
		private Guid wordId;

		public Guid WordId { get => wordId; set => SetPropertyValue(ref wordId, value, OnWordIdChanged); }
		protected WordItemEditListViewModel(IDialogManager dialogManager, IRepository<T> repository) : base(dialogManager, repository) { }
		protected virtual void OnWordIdChanged(Guid wordId) {
			LoadItems(wordId);
		}
		protected virtual async void LoadItems(Guid wordId) {
			var items = await GetItems(wordId);
			SetViewItems(items);
		}
		protected virtual void SetViewItems(IEnumerable<T> items) {
			items.Foreach(AddEntityToItems);
		}
		protected virtual Task<List<T>> GetItems(Guid wordId) {
			return Repository.GetListAsync(query => query.Where(GetItemsFilter(wordId)).LookupOrderBy());
		}
		protected abstract Expression<Func<T, bool>> GetItemsFilter(Guid wordId);
		protected virtual async Task<bool> GetIsUniqueDisplayValue(string displayValue) {
			return await Repository.GetIsUniqueLookup(displayValue, GetItemsFilter(wordId));
		}
		protected override async Task Add(string displayValue) {
			if (!await GetIsUniqueDisplayValue(displayValue)) {
				await ShowNotUniqueDisplayValueMessage(displayValue);
				return;
			}
			await base.Add(displayValue);
		}
		protected virtual Task ShowNotUniqueDisplayValueMessage(string displayValue) {
			var title = GetHeader();
			return DialogManager.DisplayAlertAsync(title, displayValue);
		}
	}
}
