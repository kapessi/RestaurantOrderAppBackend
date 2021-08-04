CREATE TABLE [dbo].[DishType] (
    [DishTypeId]   INT           NOT NULL,
    [DishTypeName] NVARCHAR (50) NULL,
    CONSTRAINT [PK_DishType] PRIMARY KEY CLUSTERED ([DishTypeId] ASC)
);

