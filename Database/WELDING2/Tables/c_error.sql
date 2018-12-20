CREATE TABLE [WELDING2].[c_error] (
    [id]              INT           IDENTITY (1, 1) NOT NULL,
    [enable_bit]      INT           NULL,
    [ordinal]         INT           NULL,
    [isError]         INT           NULL,
    [errorCode1]      INT           NULL,
    [errorCode1_text] VARCHAR (256) NULL,
    [errorCode2]      INT           NULL,
    [errorCode2_text] VARCHAR (256) NULL,
    [_operator]       INT           NULL,
    [flags]           INT           NULL,
    [UserComment]     VARCHAR (256) NULL,
    CONSTRAINT [PK_welding_c_error] PRIMARY KEY CLUSTERED ([id] ASC)
);

