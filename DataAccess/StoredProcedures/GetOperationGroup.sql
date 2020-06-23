CREATE PROCEDURE dbo.GetOperationGroup (
	@GroupName VARCHAR(100)
) AS
BEGIN
    
  DECLARE @sqlquery NVARCHAR(MAX) = REPLACE('SELECT 
                                                CAST(@GroupName AS VARCHAR(100)) as [@GroupName]
                                                ,SUM(Quantity) as [Quantity]
                                                ,SUM((Quantity * Price) / Quantity) as [AveragePrice]
                                            FROM Operation
                                            GROUP BY @GroupName', '@GroupName', @GroupName);

  EXECUTE sp_executesql @sqlquery

END