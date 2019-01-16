CREATE TABLE [WELDING2].[c_user] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [enable_bit] INT           NULL,
    [username]   VARCHAR (256) NULL,
    [_timestamp] DATETIME      NULL,
    [role]       NCHAR (10)    NULL,
    CONSTRAINT [PK_welding_c_user] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [CK_username] UNIQUE NONCLUSTERED ([username] ASC)
);



