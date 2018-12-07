CREATE TABLE [WELDING2].[rt_user] (
    [id]                   INT          IDENTITY (1, 1) NOT NULL,
    [nptId]                INT          NULL,
    [c_UserId]             INT          NULL,
    [_timestamp]           DATETIME     NULL,
    [protRecord_ID_login]  DECIMAL (12) NULL,
    [protRecord_ID_logout] DECIMAL (12) NULL,
    [dateTime_login]       DATETIME     NULL,
    [dateTime_logout]      DATETIME     NULL,
    CONSTRAINT [PK_rt_user] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_rt_user_c_NPT] FOREIGN KEY ([nptId]) REFERENCES [WELDING2].[c_NPT] ([ID]),
    CONSTRAINT [FK_rt_user_c_user] FOREIGN KEY ([c_UserId]) REFERENCES [WELDING2].[c_user] ([id])
);

