USE [StockSystem_01_001_Local]
GO

SELECT     StockItems.Name AS StockItem, Areas.Name AS Area, StockTypes.Name AS StockType
FROM         StockItems LEFT OUTER JOIN
                      Areas ON StockItems.Area_Id = Areas.Id LEFT OUTER JOIN
                      StockTypes ON StockItems.Type_Id = StockTypes.Id
ORDER BY Areas.Name, StockTypes.Name, StockItems.Name

SELECT     AreaTypes.Name AS AreaType, Areas.Name AS Area
FROM         AreaTypes LEFT OUTER JOIN
                      Areas ON AreaTypes.Id = Areas.Type_Id
ORDER BY AreaTypes.Name, Areas.Name

SELECT     StockTypeGroups.Name AS StockTypeGroup, StockTypes.Name AS StockType
FROM         StockTypeGroups LEFT OUTER JOIN
                      StockTypes ON StockTypeGroups.Id = StockTypes.Group_Id
ORDER BY StockTypeGroups.Name, StockTypes.Name

