using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HTML2JSON.Business.Tests
{
	[TestClass]
	public class HtmlToJsonConverterTests
	{
		[TestMethod]
		public void ToJson_CustomHtml_JsonStringIsNotNullOrBlank()
		{
			//Arrange
			var filePath = System.IO.Path.Combine(System.Environment.CurrentDirectory, 
				"Log New Work Order.htm");
			var html = System.IO.File.ReadAllText(filePath);
			//Act
			var json = new HtmlToJsonConverter(html).ToJson();
			//Assert
			Assert.IsTrue(!string.IsNullOrWhiteSpace(json), 
				"The json returned is not valid.");
		}

	}
}
