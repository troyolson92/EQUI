CREATE TABLE [WELDING2].[rt_comment] (
    [id]         INT          IDENTITY (1, 1) NOT NULL,
    [_timestamp] DATETIME     NULL,
    [comment]    VARCHAR (40) NULL,
    CONSTRAINT [PK_welding2_comment] PRIMARY KEY CLUSTERED ([id] ASC)
);

