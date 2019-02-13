CREATE TABLE [UL].[rt_active_info] (
    [id]                  INT           IDENTITY (1, 1) NOT NULL,
    [Partname]            CHAR (50)     NULL,
    [InspectionPlanname]  CHAR (50)     NULL,
    [Planlength]          INT           NULL,
    [IndexOfTestsequence] INT           NULL,
    [Inspector]           NCHAR (25)    NULL,
    [teststation]         CHAR (50)     NULL,
    [InspectionLaptop]    VARCHAR (255) NULL,
    [ULDateTime]          DATETIME      NULL,
    [Heartbeat]           DATETIME      NULL,
    CONSTRAINT [PK_rt_active_info_1] PRIMARY KEY CLUSTERED ([id] ASC)
);





