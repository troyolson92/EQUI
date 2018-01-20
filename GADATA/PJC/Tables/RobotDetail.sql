CREATE TABLE [PJC].[RobotDetail] (
    [id]             INT          IDENTITY (1, 1) NOT NULL,
    [name]           VARCHAR (50) NULL,
    [robotSupplier]  VARCHAR (10) NULL,
    [isDead]         BIT          NULL,
    [_timestamp]     DATETIME     NULL,
    [_lasttimestamp] DATETIME     NULL,
    CONSTRAINT [PK_RobotDetail] PRIMARY KEY CLUSTERED ([id] ASC)
);

