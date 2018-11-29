CREATE TABLE [WELDING2].[c_service_setup] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [bit_id]      BIGINT        NULL,
    [name]        VARCHAR (40)  NULL,
    [value]       VARCHAR (128) NULL,
    [description] VARCHAR (256) NULL,
    CONSTRAINT [PK_c_service_setup] PRIMARY KEY CLUSTERED ([id] ASC)
);

