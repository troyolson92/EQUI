CREATE TABLE [WELDING2].[rt_spot] (
    [id]                      INT           IDENTITY (1, 1) NOT NULL,
    [_timestamp]              DATETIME      NULL,
    [timerId]                 INT           NULL,
    [spotId]                  INT           NULL,
    [spotname]                VARCHAR (64)  NULL,
    [pjv_joiningpointdata_id] INT           NULL,
    [vwscComment]             VARCHAR (256) NULL,
    [Comment1]                INT           NULL,
    [Comment2]                INT           NULL,
    [Comment3]                INT           NULL,
    [weldProgramNo]           INT           NULL,
    [isDead]                  INT           NULL,
    CONSTRAINT [PK_welding2_spot] PRIMARY KEY CLUSTERED ([id] ASC)
);

