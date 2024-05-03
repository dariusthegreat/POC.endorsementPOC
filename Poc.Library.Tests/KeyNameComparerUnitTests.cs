using FluentAssertions;

namespace TechQ.DocumentManagement.Tests;

[TestClass]
public class KeyNameComparerUnitTests
{
	private readonly KeyNameComparer _sut = new KeyNameComparer();

	[TestMethod]
	public void StringsWithDifferentKeysShouldNotBeEqual() => _sut.Compare("something123", "differentKey123").Should().NotBe(0);

	[DataTestMethod]
	[DataRow("key1", "key2")]
	[DataRow("key2", "key3")]
	[DataRow("key20", "key3456")]
	[DataRow("Another1", "Another2")]
	[DataRow("Another2", "Another3")]
	[DataRow("Another20", "Another3456")]
	public void ComparisonShouldReturnLessThanZeroIfTheLeftSideIndexIsLessThanTheRightSide(string left, string right) => _sut.Compare(left, right).Should().BeLessThan(0);
}