CREATE TABLE [WELDING2].[c_bosch_param] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [_timestamp] DATETIME      NULL,
    [param_id]   INT           NULL,
    [flag]       INT           NULL,
    [paramName]  VARCHAR (64)  NULL,
    [text]       VARCHAR (150) NULL,
    [enable_bit] INT           NULL,
    CONSTRAINT [PK_L_bosch_param] PRIMARY KEY CLUSTERED ([ID] ASC)
);

