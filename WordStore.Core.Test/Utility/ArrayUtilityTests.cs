using Moq;
using NUnit.Framework;
using WordStore.Core.Utility;

namespace WordStore.Core.Test.Utility {
	[TestFixture]
	public class ArrayUtilityTests {
		[Test]
		public void Foreach_AllItems_ShouldCallAction() {
			// Arrange
			var items = new [] { 2, 10, 4 };
			var action = new Mock<Action<int>>();
			// Act
			ArrayUtility.Foreach(items, action.Object);
			// Assert
			action.Verify(a => a.Invoke(2));
			action.Verify(a => a.Invoke(10));
			action.Verify(a => a.Invoke(4));
		}
		[Test]
		public void AddRange_AddItems_ReturnCollectionWithItems() {
			// Arrange
			var collection = new Mock<ICollection<int>>();
			var addItems = new int[] { 2, 10, 4 };
			// Act
			ArrayUtility.AddRange(collection.Object, addItems);
			// Assert
			collection.Verify(c => c.Add(2));
			collection.Verify(c => c.Add(10));
			collection.Verify(c => c.Add(4));
		}
	}
}
