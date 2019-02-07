CREATE TABLE [UL].[rt_active_info] (
    [Partname]            CHAR (50)     NULL,
    [InspectionPlanname]  CHAR (50)     NULL,
    [PlanLenght]          INT           NULL,
    [IndexOfTestsequence] INT           NULL,
    [Inspector]           NCHAR (25)    NULL,
    [teststation]         CHAR (50)     NULL,
    [InspectionLaptop]    VARCHAR (255) NULL,
    [ULDateTimeDbl]       FLOAT (53)    NULL,
    [Heartbeat]           DATETIME      NULL
);

