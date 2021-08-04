
MERGE
    [dbo].[DayTime] AS [Target]
USING
    (
    VALUES
        (1, 'morning'),
        (2, 'night')
    ) AS [Source]([DayTimeId], [DayTimeName])
    ON [Target].[DayTimeId] = [Source].[DayTimeId]
    WHEN MATCHED THEN
	    UPDATE SET [DayTimeName] = [Source].[DayTimeName]
    WHEN NOT MATCHED BY TARGET THEN
	INSERT ([DayTimeId], [DayTimeName]) VALUES ([Source].[DayTimeId], [Source].[DayTimeName]);

MERGE
    [dbo].[DishType] AS [Target]
USING
    (
    VALUES
        (1, 'entrée'),
        (2, 'side'),
        (3, 'drink'),
        (4, 'dessert')
    ) AS [Source]([DishTypeId], [DishTypeName])
    ON [Target].[DishTypeId] = [Source].[DishTypeId]
    WHEN MATCHED THEN
	    UPDATE SET [DishTypeName] = [Source].[DishTypeName]
    WHEN NOT MATCHED BY TARGET THEN
	INSERT ([DishTypeId], [DishTypeName]) VALUES ([Source].[DishTypeId], [Source].[DishTypeName]);

MERGE
    [dbo].[Menu] AS [Target]
USING
    (
    VALUES
        (1, 1, 'eggs', 0),
        (1, 2, 'Toast', 0),
        (1, 3, 'coffee', 1),
        (2, 1, 'steak', 0),
        (2, 2, 'potato', 1),
        (2, 3, 'wine', 0),
        (2, 4, 'cake', 0)
    ) AS [Source]([DayTimeId], [DishTypeId], [Menu], [AllowMultipleOrders])
    ON [Target].[DayTimeId] = [Source].[DayTimeId] AND [Target].[DishTypeId] = [Source].[DishTypeId]
    WHEN MATCHED THEN
	    UPDATE SET [Menu] = [Source].[Menu], [AllowMultipleOrders] = [Source].[AllowMultipleOrders]
    WHEN NOT MATCHED BY TARGET THEN
	INSERT ([DayTimeId], [DishTypeId], [Menu], [AllowMultipleOrders]) VALUES ([Source].[DayTimeId], [Source].[DishTypeId], [Source].[Menu], [Source].[AllowMultipleOrders]);
GO