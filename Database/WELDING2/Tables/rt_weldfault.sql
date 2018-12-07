CREATE TABLE [WELDING2].[rt_weldfault] (
    [id]                  INT             IDENTITY (1, 1) NOT NULL,
    [timerId]             INT             NULL,
    [_timestamp]          DATETIME        NULL,
    [protRecord_ID]       INT             NULL,
    [dateTime]            DATETIME        NULL,
    [progNo]              INT             NULL,
    [monitorState]        INT             NULL,
    [monitorState_txt]    VARCHAR (64)    NULL,
    [regulationState]     INT             NULL,
    [regulationState_txt] VARCHAR (64)    NULL,
    [measureState]        INT             NULL,
    [measureState_txt]    VARCHAR (64)    NULL,
    [weldProgValue]       REAL            NULL,
    [weldActualValue]     REAL            NULL,
    [wear]                NUMERIC (12, 2) NULL,
    [rt_spot_id]          INT             NULL,
    [isError]             BIT             NULL,
    [WMComment]           VARCHAR (100)   NULL,
    CONSTRAINT [PK_rt_weldfault] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_rt_weldfault_c_timer] FOREIGN KEY ([timerId]) REFERENCES [WELDING2].[c_timer] ([ID])
);



