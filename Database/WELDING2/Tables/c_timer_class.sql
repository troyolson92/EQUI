CREATE TABLE [WELDING2].[c_timer_class] (
    [id]             INT           IDENTITY (1, 1) NOT NULL,
    [name]           VARCHAR (100) NOT NULL,
    [cBoschViewMask] INT           NULL,
    [cErrorMask]     INT           NULL,
    [cSeverityMask]  INT           NULL,
    CONSTRAINT [PK_c_slave_class] PRIMARY KEY CLUSTERED ([id] ASC)
);

