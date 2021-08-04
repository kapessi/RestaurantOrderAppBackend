-- =============================================
-- Author:		Emmanuel Uraga.
-- Create date: 2021-08-04T01:35:09.5814385-05:00
-- Description:	Selects the menu.
-- =============================================
CREATE PROCEDURE [dbo].[usp_Menu_Select]
	@DayTimeName nvarchar(50),
	@DishTypeId [udtId] READONLY
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		[Menu].[DishTypeId],
		[Menu].[Menu],
		[Menu].[AllowMultipleOrders]
	FROM
		[dbo].[Menu]
		INNER JOIN [dbo].[DayTime] ON [Menu].[DayTimeId] = [DayTime].[DayTimeId]
		INNER JOIN [dbo].[DishType] ON [Menu].[DishTypeId] = [DishType].[DishTypeId]
		INNER JOIN @DishTypeId AS [DishTypeId] ON [DishType].[DishTypeId] = [DishTypeId].[Id]
	WHERE
		[DayTime].[DayTimeName] = @DayTimeName
	ORDER BY
		[DishType].[DishTypeId];

	SET NOCOUNT OFF;

END