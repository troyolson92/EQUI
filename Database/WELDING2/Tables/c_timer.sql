CREATE TABLE [WELDING2].[c_timer] (
    [ID]                    INT           IDENTITY (1, 1) NOT NULL,
    [Name]                  NCHAR (15)    NULL,
    [NptId]                 INT           NULL,
    [Robot]                 NCHAR (10)    NULL,
    [location]              VARCHAR (256) NULL,
    [c_timer_class_id]      INT           NULL,
    [enable_bit]            INT           NULL,
    [LocationTree]          VARCHAR (MAX) NULL,
    [Assetnum]              VARCHAR (MAX) NULL,
    [ResponsibleWeldMaster] VARCHAR (50)  NULL,
    [Station]               VARCHAR (50)  NULL,
    [Line]                  VARCHAR (50)  NULL,
    CONSTRAINT [PK_Timer] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_c_timer_c_NPT] FOREIGN KEY ([NptId]) REFERENCES [WELDING2].[c_NPT] ([ID]),
    CONSTRAINT [FK_c_Timer_c_timer_class] FOREIGN KEY ([c_timer_class_id]) REFERENCES [WELDING2].[c_timer_class] ([id])
);



