CREATE TABLE [WELDING2].[rt_paramvalues] (
    [id]         INT            IDENTITY (1, 1) NOT NULL,
    [timerId]    INT            NULL,
    [_timestamp] DATETIME       NULL,
    [paramName]  VARCHAR (64)   NULL,
    [subindex]   INT            NULL,
    [value]      VARCHAR (1024) NULL,
    [text]       NVARCHAR (150) NULL,
    [param_ID]   INT            NULL,
    CONSTRAINT [PK_RT_paramvalues] PRIMARY KEY CLUSTERED ([id] ASC)
);

