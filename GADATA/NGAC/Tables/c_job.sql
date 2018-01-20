CREATE TABLE [NGAC].[c_job] (
    [id]         INT           IDENTITY (1, 1) NOT NULL,
    [jobNumber]  INT           NULL,
    [enable_bit] INT           NULL,
    [flags]      INT           NULL,
    [comment]    VARCHAR (MAX) NULL
);

