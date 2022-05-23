﻿using WordStore.Core.Model;
using WordStore.Core.Utility;
using WordStore.Data;
using WordStore.Manager;

namespace WordStore.ViewModel {
	public abstract class WordItemEditListViewModel<T> : BaseEditListViewModel<T> where T : BaseLookupEntity, new() {
		private Guid wordId;
		private Guid word;

		public Guid WordId { get => wordId; set => SetPropertyValue(ref wordId, value, OnWordIdChanged); }
		public Guid Word { get => word; set => SetPropertyValue(ref word, value, OnWordIdChanged); }
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
		protected abstract Task<List<T>> GetItems(Guid wordId);
	}
}