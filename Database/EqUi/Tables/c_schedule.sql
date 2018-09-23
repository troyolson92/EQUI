CREATE TABLE [EqUi].[c_schedule] (
    [id]             INT           IDENTITY (1, 1) NOT NULL,
    [enabled]        BIT           NOT NULL,
    [name]           VARCHAR (50)  NOT NULL,
    [description]    VARCHAR (MAX) NOT NULL,
    [jcron]          VARCHAR (50)  NOT NULL,
    [runContinues]   BIT           NOT NULL,
    [minRunInterval] INT           NOT NULL,
    CONSTRAINT [PK_c_schedule] PRIMARY KEY CLUSTERED ([id] ASC)
);

