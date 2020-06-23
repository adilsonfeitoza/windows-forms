IF EXISTS(SELECT name FROM sys.procedures WHERE name = 'GetOperationGroup')
	DROP PROCEDURE dbo.GetOperationGroup
GO

CREATE PROCEDURE dbo.GetOperationGroup (
	@GroupName VARCHAR(100)
) AS
BEGIN
	
   DECLARE @sqlquery NVARCHAR(MAX) = 'SELECT 
                                            @GroupName as [GroupName]
                                            ,SUM(Quantity) as [Quantity]
                                            ,SUM((Quantity * Price) / Quantity) as [AveragePrice]
                                      FROM tablename
                                      GROUP BY @GroupName';

  DECLARE @params NVARCHAR(MAX) = '@GroupName VARCHAR(100)';

  EXEC sp_sqlexec @sqlquery, @params, @GroupName = @GroupName;

END
GO
