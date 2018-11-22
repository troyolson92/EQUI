CREATE TABLE [WELDING2].[c_severity] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [enable_bit]  INT           NULL,
    [isError_txt] VARCHAR (256) NULL,
    [value]       INT           NULL,
    CONSTRAINT [PK_welding_c_severity] PRIMARY KEY CLUSTERED ([id] ASC)
);

