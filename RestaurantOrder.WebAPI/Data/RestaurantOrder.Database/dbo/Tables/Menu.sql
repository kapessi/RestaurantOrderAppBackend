CREATE TABLE [dbo].[Menu] (
    [DayTimeId]           INT            NOT NULL,
    [DishTypeId]          INT            NOT NULL,
    [Menu]                NVARCHAR (100) NULL,
    [AllowMultipleOrders] BIT            NULL,
    CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED ([DayTimeId] ASC, [DishTypeId] ASC),
    CONSTRAINT [FK_Menu_DayTime] FOREIGN KEY ([DayTimeId]) REFERENCES [dbo].[DayTime] ([DayTimeId]),
    CONSTRAINT [FK_Menu_DishType] FOREIGN KEY ([DishTypeId]) REFERENCES [dbo].[DishType] ([DishTypeId])
);



