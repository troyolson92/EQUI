CREATE TABLE [EqUi].[c_areas] (
    [id]                  INT           IDENTITY (1, 1) NOT NULL,
    [Plant]               VARCHAR (50)  NOT NULL,
    [Optgroup]            VARCHAR (50)  NOT NULL,
    [Area]                VARCHAR (50)  NOT NULL,
    [LocationTreeFilter1] VARCHAR (100) NOT NULL,
    [Ordinal]             INT           NOT NULL,
    [UserComment]         VARCHAR (MAX) NULL,
    CONSTRAINT [PK_AreaFilters] PRIMARY KEY CLUSTERED ([id] ASC)
);



