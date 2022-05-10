using WordStore.Core.Model;

namespace WordStore.Data {
	public interface IWordStorage {
		IRepository<Word> WordRepository { get; }
		IRepository<WordTranslation> WordTranslationRepository { get; }
		IRepository<WordExample> WordExampleRepository { get; }
	}
}
