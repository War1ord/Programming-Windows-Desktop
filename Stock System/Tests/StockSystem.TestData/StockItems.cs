using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockSystem.Models;

namespace StockSystem.TestData
{
	[TestClass]
	public class StockItems
	{
		[TestMethod]
		public void CreateAStockItemAnd1TypeAnd1Group()
		{
			var group = new Models.StockTypeGroup() { Name = "Test Group 1" };
			var type = new Models.StockType() { Name = "Test Type 1", Group = group };
			var item = new Models.StockItem() { Name = "Test Stock Item 1", Type = type };

			using (var groups = new Business.BusinessBase<Models.StockTypeGroup>())
			{
				var resultForGroup = groups.Add(group);
				Assert.IsTrue(resultForGroup.Type == ResultType.Success, resultForGroup.Message);
			}
			using (var types = new Business.BusinessBase<Models.StockType>())
			{
				var resultForType = types.Add(type);
				Assert.IsTrue(resultForType.Type == ResultType.Success, resultForType.Message);
			}
			using (var stockItems = new Business.StockItemsManager())
			{
				var resultForItem = stockItems.Add(item);
				Assert.IsTrue(resultForItem.Type == ResultType.Success, resultForItem.Message);
			}
		}

		[TestMethod]
		public void CreateAListOf100StockItemsWith4TypesAnd2Group()
		{
			var group1 = new Models.StockTypeGroup() { Name = "Test Group 1" };
			var group2 = new Models.StockTypeGroup() { Name = "Test Group 2" };
			var type1 = new Models.StockType() { Name = "Test Type 1", Group = group1 };
			var type2 = new Models.StockType() { Name = "Test Type 2", Group = group1 };
			var type3 = new Models.StockType() { Name = "Test Type 3", Group = group2 };
			var type4 = new Models.StockType() { Name = "Test Type 4", Group = group2 };

			List<StockItem> itemList1ForType1 = Enumerable.Range(1, 20)
				.Select((i, index) => new Models.StockItem()
				{
					Name = "Test Stock Item " + index,
					Type = type1,
					
				}).ToList();
			List<StockItem> itemList2ForType2 = Enumerable.Range(21, 20)
				.Select((i, index) => new Models.StockItem()
				{
					Name = "Test Stock Item " + index,
					Type = type1,
					
				}).ToList();
			List<StockItem> itemList3ForType3 = Enumerable.Range(41, 20)
				.Select((i, index) => new Models.StockItem()
				{
					Name = "Test Stock Item " + index,
					Type = type1,
					
				}).ToList();
			List<StockItem> itemList4ForType4 = Enumerable.Range(61, 20)
				.Select((i, index) => new Models.StockItem()
				{
					Name = "Test Stock Item " + index,
					Type = type1,
					
				}).ToList();

			using (var groups = new Business.BusinessBase<Models.StockTypeGroup>())
			{
				var resultForGroup1 = groups.Add(group1);
				var resultForGroup2 = groups.Add(group2);
				Assert.IsTrue(resultForGroup1.Type == ResultType.Success, resultForGroup1.Message);
				Assert.IsTrue(resultForGroup2.Type == ResultType.Success, resultForGroup2.Message);
			}
			using (var types = new Business.BusinessBase<Models.StockType>())
			{
				var resultForType1 = types.Add(type1);
				var resultForType2 = types.Add(type2);
				var resultForType3 = types.Add(type3);
				var resultForType4 = types.Add(type4);
				Assert.IsTrue(resultForType1.Type == ResultType.Success, resultForType1.Message);
				Assert.IsTrue(resultForType2.Type == ResultType.Success, resultForType2.Message);
				Assert.IsTrue(resultForType3.Type == ResultType.Success, resultForType3.Message);
				Assert.IsTrue(resultForType4.Type == ResultType.Success, resultForType4.Message);
			}
			using (var stockItems = new Business.StockItemsManager())
			{
				var resultForItem1 = itemList1ForType1.Select(i => { return stockItems.Add(i); }).ToList();
				var resultForItem2 = itemList2ForType2.Select(i => { return stockItems.Add(i); }).ToList();
				var resultForItem3 = itemList3ForType3.Select(i => { return stockItems.Add(i); }).ToList();
				var resultForItem4 = itemList4ForType4.Select(i => { return stockItems.Add(i); }).ToList();
				resultForItem1.ForEach(i => Assert.IsTrue(i.Type == ResultType.Success, i.Message));
				resultForItem2.ForEach(i => Assert.IsTrue(i.Type == ResultType.Success, i.Message));
				resultForItem3.ForEach(i => Assert.IsTrue(i.Type == ResultType.Success, i.Message));
				resultForItem4.ForEach(i => Assert.IsTrue(i.Type == ResultType.Success, i.Message));
			}
		}
	}
}
