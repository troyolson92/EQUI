CREATE TABLE [MAXIMO].[WORKORDERS] (
    [WONUM]              VARCHAR (255) NULL,
    [STATUS]             VARCHAR (255) NULL,
    [STATUSDATE]         DATETIME      NULL,
    [WORKTYPE]           VARCHAR (255) NULL,
    [DESCRIPTION]        VARCHAR (MAX) NULL,
    [LOCATION]           VARCHAR (255) NULL,
    [ASSETNUM]           VARCHAR (50)  NULL,
    [REPORTEDBY]         VARCHAR (255) NULL,
    [REPORTDATE]         DATETIME      NULL,
    [CHANGEDATE]         DATETIME      NULL,
    [OWNERGROUP]         VARCHAR (50)  NULL,
    [JPNUM]              VARCHAR (50)  NULL,
    [ANCESTOR]           VARCHAR (255) NULL,
    [SUM_LABTRANS_HOURS] FLOAT (53)    NULL
);









