CREATE TABLE [UL].[T_Picture] (
    [PictureID]        INT            NULL,
    [PictureName]      NVARCHAR (50)  NULL,
    [FatherID]         INT            NULL,
    [PicturePath]      NVARCHAR (250) NULL,
    [Picture]          IMAGE          NULL,
    [OriginalFileName] NVARCHAR (50)  NULL
);

