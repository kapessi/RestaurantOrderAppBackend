CREATE TABLE [dbo].[DayTime] (
    [DayTimeId]   INT           NOT NULL,
    [DayTimeName] NVARCHAR (50) NULL,
    CONSTRAINT [PK_DayTime] PRIMARY KEY CLUSTERED ([DayTimeId] ASC)
);

